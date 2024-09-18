﻿using DiskCardGame;
using InscryptionAPI.Nodes;
using InscryptionAPI.TalkingCards;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Node_SefirotCardChoice()
        {
            List<string> animationFrames = new()
            {
                "nodeSefirotCardChoice1",
                "nodeSefirotCardChoice2",
                "nodeSefirotCardChoice3",
                "nodeSefirotCardChoice4"
            };

            GenerationType main = GenerationType.SpecialCardChoice;
            GenerationType extra = LobotomyConfigManager.Instance.SefirotChoiceAtStart ? GenerationType.RegionStart : GenerationType.None;

            // don't generate node if it's disabled
            if (LobotomyConfigManager.Instance.NoSefirot)
            {
                main = GenerationType.None;
                extra = GenerationType.None;
            }
            NodeHelper.CreateNode("SefirotCardChoice", typeof(SefirotCardChoiceSequencer), animationFrames,
                main, extraGenType: extra);
        }
    }
    // Pulled wholesale from CardSingleChoicesSequencer and CardChoiceSequencer
    public class SefirotCardChoiceSequencer : CardSingleChoicesSequencer, ICustomNodeSequencer, IInherit
    {
        public IEnumerator DoCustomSequence(CustomSpecialNodeData choicesData)
        {
            // Spawns the rulebook and deck before the dialogue starts
            if (gamepadGrid != null)
                gamepadGrid.enabled = true;

            Singleton<TableRuleBook>.Instance.SetOnBoard(onBoard: true);
            base.StartCoroutine(deckPile.SpawnCards(SaveManager.SaveFile.CurrentDeck.Cards.Count));

            // First-time dialogue for the node
            if (!DialogueEventsData.EventIsPlayed("SefirotChoiceNodeIntro"))
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("SefirotChoiceNodeIntro");
            }

            Singleton<ViewManager>.Instance.SwitchToView(this.choicesView, immediate: false, lockAfter: true);
            yield return this.CardSelectionSequence(choicesData);
            if (gamepadGrid != null)
                gamepadGrid.enabled = false;

            yield return new WaitForSeconds(0.75f);
            yield return deckPile.DestroyCards();
        }
        public override IEnumerator CardSelectionSequence(SpecialNodeData choicesData)
        {
            if (StoryEventsData.EventCompleted(StoryEvent.CloverFound) && rerollInteractable != null)
            {
                if (!AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.NoClover))
                {
                    rerollInteractable.gameObject.SetActive(value: true);
                    rerollInteractable.SetEnabled(enabled: false);
                    CustomCoroutine.WaitThenExecute(1f, delegate
                    {
                        rerollInteractable.SetEnabled(enabled: true);
                    });
                }
                ChallengeActivationUI.TryShowActivation(AscensionChallenge.NoClover);
                if (AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.NoClover) && !DialogueEventsData.EventIsPlayed("ChallengeNoClover"))
                {
                    yield return new WaitForSeconds(1f);
                    yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("ChallengeNoClover", TextDisplayer.MessageAdvanceMode.Input);
                }

                ChallengeActivationUI.TryShowActivation(BetterRareChances.Id);
            }

            chosenReward = null;
            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
            while (chosenReward == null)
            {
                List<CardChoice> choices = GenerateSephirahChoices(randomSeed);
                randomSeed *= 2;
                float x = (float)(choices.Count - 1) * 0.5f * -1.5f;
                base.selectableCards = base.SpawnCards(choices.Count, base.transform, new Vector3(x, 5.01f, 0f));
                for (int i = 0; i < choices.Count; i++)
                {
                    CardChoice cardChoice = choices[i];
                    SelectableCard card = base.selectableCards[i];
                    card.gameObject.SetActive(value: true);
                    card.SetParticlesEnabled(enabled: true);
                    card.SetEnabled(enabled: false);
                    card.ChoiceInfo = cardChoice;
                    if (cardChoice.CardInfo != null)
                        card.Initialize(cardChoice.CardInfo, this.OnRewardChosen, this.OnCardFlipped, startFlipped: true, base.OnCardInspected);

                    SpecialCardBehaviour[] components = card.GetComponents<SpecialCardBehaviour>();
                    for (int j = 0; j < components.Length; j++)
                    {
                        components[j].OnShownForCardChoiceNode();
                    }

                    card.SetCardback(TextureLoader.LoadTextureFromFile("sefirotRewardBack"));
                    card.SetFaceDown(faceDown: true, immediate: true);

                    Vector3 position = card.transform.position;
                    card.transform.position = card.transform.position + Vector3.forward * 5f + new Vector3(-0.5f + UnityEngine.Random.value * 1f, 0f, 0f);
                    Tween.Position(card.transform, position, 0.3f, 0f, Tween.EaseInOut);
                    Tween.Rotate(card.transform, new Vector3(0f, 0f, UnityEngine.Random.value * 1.5f), Space.Self, 0.4f, 0f, Tween.EaseOut);
                    yield return new WaitForSeconds(0.2f);
                    ParticleSystem componentInChildren = card.GetComponentInChildren<ParticleSystem>();
                    if (componentInChildren != null)
                    {
                        ParticleSystem.EmissionModule emission = componentInChildren.emission;
                        emission.rateOverTime = 0f;
                    }
                }
                yield return new WaitForSeconds(0.2f);
                base.SetCollidersEnabled(collidersEnabled: true);
                this.choicesRerolled = false;
                base.EnableViewDeck(this.viewControlMode, this.basePosition);
                yield return new WaitUntil(() => this.chosenReward != null || this.choicesRerolled);
                base.DisableViewDeck();
                CleanUpCards();
            }
            yield return this.AddCardToDeckAndCleanUp(chosenReward);
        }

        private List<CardChoice> GenerateSephirahChoices(int randomSeed)
        {
            List<CardChoice> listOfChoices = new();
            List<CardInfo> sephirahCards = LobotomyCardLoader.GetSephirahCards();

            // if the player has 2 sephirah already, unlock Angela and make her a guaranteed choice
            if (sephirahCards.Count <= 7 && !LobotomySaveManager.UnlockedAngela)
                listOfChoices.Add(new() { CardInfo = CardLoader.GetCardByName("wstl_angela") });

            while (listOfChoices.Count < 3)
            {
                CardInfo card;
                if (sephirahCards.Count > 0)
                {
                    card = CardLoader.Clone(sephirahCards[SeededRandom.Range(0, sephirahCards.Count, randomSeed++)]);
                    while (listOfChoices.Exists(x => x.CardInfo.name == card.name))
                        card = CardLoader.Clone(sephirahCards[SeededRandom.Range(0, sephirahCards.Count, randomSeed++)]);

                    sephirahCards.RemoveAll(x => x.name == card.name);
                }
                else
                    card = LobotomyCardLoader.GetRandomModDeathCard(randomSeed++);

                CardChoice cardChoice = new() { CardInfo = card };
                listOfChoices.Add(cardChoice);
            }
            return new(listOfChoices.Randomize());
        }

        private new IEnumerator AddCardToDeckAndCleanUp(SelectableCard card)
        {
            CleanUpRerollItem();
            Singleton<RuleBookController>.Instance.SetShown(shown: false);
            yield return this.RewardChosenSequence(card);
            LobotomySaveManager.LearnedSefirotChoice = true;
            AddChosenCardToDeck();
            Singleton<TextDisplayer>.Instance.Clear();
            yield return new WaitForSeconds(0.1f);
        }
        private new IEnumerator RewardChosenSequence(SelectableCard card)
        {
            card.OnCardAddedToDeck();
            float num = !LobotomySaveManager.LearnedSefirotChoice ? 0.5f : 0f;
            deckPile.MoveCardToPile(card, flipFaceDown: true, num);
            yield return new WaitForSeconds(num);
            if (!LobotomySaveManager.LearnedSefirotChoice)
            {
                Singleton<TextDisplayer>.Instance.Clear();
                yield return new WaitForSeconds(0.15f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("As soon as you make your choice, a thick fog envelops the others.", 0f, 0.4f, Emotion.Neutral);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("When it clears, they have vanished without a trace.", 0f, 0.4f, Emotion.Neutral);
            }
        }
        private new void OnRewardChosen(SelectableCard card)
        {
            if (!LobotomySaveManager.LearnedAbnormalChoice && !AllCardsFlippedUp())
                HintsHandler.OnClickCardChoiceWhileOtherFlipped();

            else if (chosenReward == null)
            {
                base.SetCollidersEnabled(collidersEnabled: false);
                chosenReward = card;
            }
        }
        private new void OnCardFlipped(SelectableCard card)
        {
            card.SetLocalPosition(Vector3.zero, 0f, immediate: true);
            if (Singleton<InteractionCursor>.Instance.CurrentInteractable == card)
                base.OnCardInspected(card);

            if (card.Info != null)
                base.StartCoroutine(this.RegularChoiceFlipped(card));

        }
        private new IEnumerator RegularChoiceFlipped(SelectableCard card)
        {
            Vector3 originalCardPos = card.transform.position;
            yield return this.TutorialTextSequence(card);
            if (DuplicateInDeck(card))
                SpawnMushroom(originalCardPos);

            // unlock achievement upon flipping the card
            if (card.Info.name == "wstl_angela" && !LobotomySaveManager.UnlockedAngela)
            {
                yield return new WaitForSeconds(0.25f);
                LobotomySaveManager.UnlockedAngela = true;
                AchievementAPI.Unlock(true, AchievementAPI.Impuritas);
            }
        }
        private new IEnumerator TutorialTextSequence(SelectableCard card)
        {
            CustomPaperTalkingCard component = card.GetComponent<CustomPaperTalkingCard>();
            if (component != null)
                component.CurrentDialogueSequence = base.StartCoroutine(Singleton<TalkingCardDialogueHandler>.Instance.DialogueSequence($"{card.Info.name.Split('_')[1]}Choice", component));

            if (!string.IsNullOrEmpty(card.Info.description) && !ProgressionData.IntroducedCard(card.Info))
            {
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
                Singleton<RuleBookController>.Instance.SetShown(shown: false);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(card.Info.description);
                ProgressionData.SetCardIntroduced(card.Info);
                if (!LobotomySaveManager.LearnedAbnormalChoice && this.AllCardsFlippedUp())
                {
                    yield return new WaitForSeconds(0.25f);
                    Singleton<TextDisplayer>.Instance.ShowMessage("Only [c:bR]1[c:] may join you at this time.");
                }
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            }
            if (component != null)
            {
                yield return new WaitUntil(() => !component.PlayingDialogue);
                component.CurrentDialogueSequence = null;
            }
        }

        public void Inherit(CustomSpecialNodeData nodeData)
        {
            CardSingleChoicesSequencer inheritTarget = SpecialNodeHandler.Instance.cardChoiceSequencer;
            base.transform.position = inheritTarget.transform.position;
            base.transform.rotation = Quaternion.Euler(inheritTarget.transform.rotation.eulerAngles);

            if (inheritTarget.deckPile != null)
            {
                deckPile = Instantiate(inheritTarget.deckPile, inheritTarget.deckPile.transform.position, inheritTarget.deckPile.transform.rotation);
                deckPile.transform.parent = base.transform;
            }

            selectableCardPrefab = inheritTarget.selectableCardPrefab;
            if (inheritTarget.gamepadGrid != null)
            {
                gamepadGrid = Instantiate(inheritTarget.gamepadGrid, inheritTarget.gamepadGrid.transform.position, inheritTarget.gamepadGrid.transform.rotation);
                gamepadGrid.transform.parent = base.transform;
            }

            if (inheritTarget.rerollInteractable != null)
            {
                rerollInteractable = Instantiate(inheritTarget.rerollInteractable, inheritTarget.rerollInteractable.transform.position, inheritTarget.rerollInteractable.transform.rotation);
                rerollInteractable.transform.parent = base.transform;

                rerollAnim = rerollInteractable.transform.GetComponentInChildren<Animator>();
            }
        }
    }
}