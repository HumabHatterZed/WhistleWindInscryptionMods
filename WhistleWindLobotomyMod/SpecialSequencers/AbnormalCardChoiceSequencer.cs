using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Nodes;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod
{
    public class AbnormalCardChoiceSequencer : CardSingleChoicesSequencer, ICustomNodeSequencer, IInherit
    {
        // Pulled wholesale from Cardbase and CardChoiceSequencer
        private readonly Texture2D RewardBackRare = TextureLoader.LoadTextureFromFile("abnormalRewardBackRare");
        private readonly Texture2D RewardBack = TextureLoader.LoadTextureFromFile("abnormalRewardBack");

        public IEnumerator DoCustomSequence(CustomSpecialNodeData choicesData)
        {
            if (gamepadGrid != null)
                gamepadGrid.enabled = true;

            Singleton<TableRuleBook>.Instance.SetOnBoard(onBoard: true);
            base.StartCoroutine(deckPile.SpawnCards(SaveManager.SaveFile.CurrentDeck.Cards.Count));

            // First-time dialogue for the node
            if (!DialogueEventsData.EventIsPlayed("AbnormalChoiceNodeIntro"))
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("AbnormalChoiceNodeIntro");
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

            base.chosenReward = null;
            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();

            // card choice loop
            while (base.chosenReward == null)
            {
                // generate card choices
                List<CardChoice> choices = GenerateRiskChoices(randomSeed);
                randomSeed *= 2;
                float x = (choices.Count - 1) * -0.75f;
                base.selectableCards = base.SpawnCards(choices.Count, base.transform, new Vector3(x, 5.01f, 0f));
                
                // spawn selectable cards
                for (int i = 0; i < choices.Count; i++)
                {
                    CardChoice cardChoice = choices[i];
                    SelectableCard card = base.selectableCards[i];
                    card.gameObject.SetActive(value: true);
                    card.SetParticlesEnabled(enabled: true);
                    card.SetEnabled(enabled: false);
                    card.ChoiceInfo = cardChoice;
                    if (cardChoice.CardInfo != null)
                        card.Initialize(cardChoice.CardInfo, OnRewardChosen, this.OnCardFlipped, startFlipped: true, base.OnCardInspected);

                    SpecialCardBehaviour[] components = card.GetComponents<SpecialCardBehaviour>();
                    for (int j = 0; j < components.Length; j++)
                    {
                        components[j].OnShownForCardChoiceNode();
                    }

                    card.SetCardback(card.ChoiceInfo.CardInfo.HasCardMetaCategory(CardMetaCategory.Rare) ? RewardBackRare : RewardBack);
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
                base.choicesRerolled = false;
                base.EnableViewDeck(base.viewControlMode, base.basePosition);
                yield return new WaitUntil(() => base.chosenReward != null || base.choicesRerolled);
                base.DisableViewDeck();
                base.CleanUpCards();
            }
            yield return this.AddCardToDeckAndCleanUp(base.chosenReward);
        }
        private List<CardChoice> GenerateRiskChoices(int randomSeed)
        {
            List<CardChoice> listOfChoices = new();
            int regionTier = RunState.CurrentRegionTier;
            while (listOfChoices.Count < 3)
            {
                CardChoice cardChoice = new();
                int riskLevel = GetRiskLevel(randomSeed++, regionTier);

                bool overrideWithRare = SeededRandom.Value(randomSeed++) <= RareChoiceChance(regionTier);
                CardInfo card = overrideWithRare ? LobotomyCardLoader.GetRandomRareModCard(randomSeed++) : LobotomyCardLoader.GetRandomChoosableModCard(randomSeed++, riskLevel);

                // if this is a duplicate card, generate a new card
                while (listOfChoices.Exists((CardChoice x) => x.CardInfo.name == card.name))
                    {
                        int riskLevel2 = GetRiskLevel(randomSeed++, regionTier);
                        card = overrideWithRare ? LobotomyCardLoader.GetRandomRareModCard(randomSeed++) : LobotomyCardLoader.GetRandomChoosableModCard(randomSeed++, riskLevel2);
                    }
                    cardChoice.CardInfo = card;
                    listOfChoices.Add(cardChoice);
            }
            return new List<CardChoice>(listOfChoices.Randomize());
        }
        private int GetRiskLevel(int randomSeed, int regionTier) // determines which risk level to draw cards from
        {
            if (AllCardsDisabled)
                return 0;

            List<int> tiers = new();
            if (!DisabledRiskLevels.HasFlag(RiskLevel.Zayin))
                tiers.Add(0);
            if (!DisabledRiskLevels.HasFlag(RiskLevel.Teth))
                tiers.Add(1);
            if (!DisabledRiskLevels.HasFlag(RiskLevel.He))
                tiers.Add(2);
            if (!DisabledRiskLevels.HasFlag(RiskLevel.Waw))
                tiers.Add(3);

            if (tiers.Count == 1)
                return tiers[0];

            // when there are disabled Risk Levels, the other Risk Levels become more common
            for (int i = 0; i < tiers.Count; i++)
            {
                tiers.Add(tiers[0]);
                if (tiers.Count >= 4)
                    break;
            }

            int[] probabilities = regionTier switch
            {
                0 => new int[] { // 35 35 20 10
                        tiers[0], tiers[0], tiers[0], tiers[0], tiers[0], tiers[0], tiers[0],
                        tiers[1], tiers[1], tiers[1], tiers[1], tiers[1], tiers[1], tiers[1],
                        tiers[2], tiers[2], tiers[2], tiers[2],
                        tiers[3], tiers[3]
                    },
                1 => new int[] { // 20 30 30 20
                        tiers[0], tiers[0],
                        tiers[1], tiers[1], tiers[1],
                        tiers[2], tiers[2], tiers[2],
                        tiers[3], tiers[3]
                    },
                _ => new int[] { tiers[0], tiers[1], tiers[2], tiers[3] }, // 25 25 25 25
            };

            return probabilities[SeededRandom.Range(0, probabilities.Length, randomSeed)];
        }
        private float RareChoiceChance(int regionTier)
        {
            int regionMultiplier = regionTier;

            if (SaveFile.IsAscension ? AscensionSaveData.Data.ChallengeIsActive(BetterRareChances.Id) : LobotomyConfigManager.Instance.BetterRareChances)
                regionMultiplier++;

            return regionMultiplier switch
            {
                0 => 0f,
                1 => 0.02f,
                2 => 0.05f,
                3 => 0.10f,
                _ => (regionMultiplier - 1) * 0.05f
            };
        }

        private new IEnumerator AddCardToDeckAndCleanUp(SelectableCard card)
        {
            CleanUpRerollItem();
            Singleton<RuleBookController>.Instance.SetShown(shown: false);
            yield return this.RewardChosenSequence(card);
            LobotomySaveManager.LearnedAbnormalChoice = true;
            AddChosenCardToDeck();
            Singleton<TextDisplayer>.Instance.Clear();
            yield return new WaitForSeconds(0.1f);
        }
        private new IEnumerator RewardChosenSequence(SelectableCard card)
        {
            card.OnCardAddedToDeck();
            float num = !LobotomySaveManager.LearnedAbnormalChoice ? 0.5f : 0f;
            base.deckPile.MoveCardToPile(card, flipFaceDown: true, num);
            yield return new WaitForSeconds(num);
            if (!LobotomySaveManager.LearnedAbnormalChoice)
            {
                Singleton<TextDisplayer>.Instance.Clear();
                yield return HelperMethods.ChangeCurrentView(View.Default);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("The creature emerges from the water and joins your caravan.", 0f, 0.4f, Emotion.Neutral);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Ignoring the cries from the well, you hastily continue on.", 0f, 0.4f, Emotion.Neutral);
            }
        }

        private new void OnRewardChosen(SelectableCard card)
        {
            if (!LobotomySaveManager.LearnedAbnormalChoice && !this.AllCardsFlippedUp())
                HintsHandler.OnClickCardChoiceWhileOtherFlipped();

            else if (base.chosenReward == null)
            {
                base.SetCollidersEnabled(collidersEnabled: false);
                base.chosenReward = card;
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
            yield return TutorialTextSequence(card);
            if (base.DuplicateInDeck(card))
                base.SpawnMushroom(originalCardPos);
        }
        private new IEnumerator TutorialTextSequence(SelectableCard card)
        {
            Debug.Log("TutorialText");
            if (!string.IsNullOrEmpty(card.Info.description) && !ProgressionData.IntroducedCard(card.Info))
            {
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
                Singleton<RuleBookController>.Instance.SetShown(shown: false);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(card.Info.description);
                ProgressionData.SetCardIntroduced(card.Info);
                if (!LobotomySaveManager.LearnedAbnormalChoice && this.AllCardsFlippedUp())
                {
                    yield return new WaitForSeconds(0.25f);
                    Singleton<TextDisplayer>.Instance.ShowMessage("You may choose [c:bR]1[c:] to draw from the well.");
                }
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
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

    public partial class LobotomyPlugin
    {
        private void Node_ModCardChoice()
        {
            List<string> animationFrames = new()
            {
                "nodeAbnormalityCardChoice1",
                "nodeAbnormalityCardChoice2",
                "nodeAbnormalityCardChoice3",
                "nodeAbnormalityCardChoice4"
            };

            GenerationType main = GenerationType.SpecialCardChoice;
            GenerationType extra = LobotomyConfigManager.Instance.BoxStart ? GenerationType.RegionStart : GenerationType.None;

            // don't generate node if it's disabled or no cards to spawn
            if (LobotomyConfigManager.Instance.NoBox || AllCardsDisabled)
            {
                main = GenerationType.None;
                extra = GenerationType.None;
            }
            NodeHelper.CreateNode("AbnormalCardChoice", typeof(AbnormalCardChoiceSequencer), animationFrames, main, extraGenType: extra);
        }
    }
}