using DiskCardGame;
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
            NodeHelper.CreateNode("wstlSefirotCardChoiceNode", typeof(SefirotCardChoiceSequencer), animationFrames,
                main, extraGenType: extra);
        }
    }
    // Pulled wholesale from CardSingleChoicesSequencer and CardChoiceSequencer
    public class SefirotCardChoiceSequencer : CustomCardChoiceNodeSequencer
    {
        protected CardPile modDeckPile = SpecialNodeHandler.Instance.cardChoiceSequencer.deckPile;
        protected GamepadGridControl modGamepadGrid = SpecialNodeHandler.Instance.cardChoiceSequencer.gamepadGrid;

        public ViewController.ControlMode viewControlMode = ViewController.ControlMode.CardChoice;
        public MainInputInteractable modRerollInteractable = SpecialNodeHandler.Instance.cardChoiceSequencer.rerollInteractable;
        private Animator modRerollAnim = SpecialNodeHandler.Instance.cardChoiceSequencer.rerollAnim;
        private readonly bool showMushrooms = true;
        private readonly List<GameObject> mushrooms = new();
        private SelectableCard chosenReward;
        private bool choicesRerolled;
        private Vector3 basePosition;
        private void Start()
        {
            // Sets up the clover and the basePosition,
            // which just makes sure that when you're moving b/t your deck and the choices it doesn't get funky
            if (modRerollInteractable != null)
            {
                MainInputInteractable mainInputInteractable = modRerollInteractable;
                mainInputInteractable.CursorSelectEnded = (Action<MainInputInteractable>)Delegate.Combine(mainInputInteractable.CursorSelectEnded, (Action<MainInputInteractable>)delegate
                {
                    this.OnRerollChoices();
                });
                MainInputInteractable mainInputInteractable2 = modRerollInteractable;
                mainInputInteractable2.CursorEntered = (Action<MainInputInteractable>)Delegate.Combine(mainInputInteractable2.CursorEntered, (Action<MainInputInteractable>)delegate
                {
                    this.OnCursorEnterRerollInteractable();
                });
            }
            this.basePosition = base.transform.position;
        }
        // The framework code, basically
        // This is essentially ChooseCards from CardChoiceSequencer plus the code of deckpile.DestroyCards()
        public override IEnumerator DoCustomSequence(CustomSpecialNodeData choicesData)
        {
            // Spawns the rulebook and deck before the dialogue starts
            if (modGamepadGrid != null)
                modGamepadGrid.enabled = true;

            Singleton<TableRuleBook>.Instance.SetOnBoard(onBoard: true);
            base.StartCoroutine(modDeckPile.SpawnCards(SaveManager.SaveFile.CurrentDeck.Cards.Count));

            // First-time dialogue for the node
            if (!DialogueEventsData.EventIsPlayed("SefirotChoiceNodeIntro"))
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("SefirotChoiceNodeIntro");
            }

            Singleton<ViewManager>.Instance.SwitchToView(this.choicesView, immediate: false, lockAfter: true);
            yield return this.CardSelectionSequence(choicesData);
            if (modGamepadGrid != null)
                modGamepadGrid.enabled = false;

            yield return new WaitForSeconds(0.75f);
            yield return modDeckPile.DestroyCards();
        }
        public override IEnumerator CardSelectionSequence(SpecialNodeData choicesData)
        {
            if (StoryEventsData.EventCompleted(StoryEvent.CloverFound) && modRerollInteractable != null)
            {
                if (!AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.NoClover))
                {
                    modRerollInteractable.gameObject.SetActive(value: true);
                    modRerollInteractable.SetEnabled(enabled: false);
                    CustomCoroutine.WaitThenExecute(1f, delegate
                    {
                        modRerollInteractable.SetEnabled(enabled: true);
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
                        card.Initialize(cardChoice.CardInfo, OnRewardChosen, OnCardFlipped, startFlipped: true, base.OnCardInspected);

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
            
            // if the player has 3 sephirah already, unlock Angela and make her a guaranteed choice
            if (sephirahCards.Count <= 6 && !LobotomySaveManager.UnlockedAngela)
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
        private IEnumerator AddCardToDeckAndCleanUp(SelectableCard card)
        {
            CleanUpRerollItem();
            Singleton<RuleBookController>.Instance.SetShown(shown: false);
            yield return RewardChosenSequence(card);
            LobotomySaveManager.LearnedSefirotChoice = true;
            AddChosenCardToDeck();
            Singleton<TextDisplayer>.Instance.Clear();
            yield return new WaitForSeconds(0.1f);
        }
        private IEnumerator RewardChosenSequence(SelectableCard card)
        {
            card.OnCardAddedToDeck();
            float num = !LobotomySaveManager.LearnedSefirotChoice ? 0.5f : 0f;

            modDeckPile.MoveCardToPile(card, flipFaceDown: true, num);
            yield return new WaitForSeconds(num);
            if (!LobotomySaveManager.LearnedSefirotChoice)
            {
                Singleton<TextDisplayer>.Instance.Clear();
                yield return new WaitForSeconds(0.15f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("The Sephirah agrees, and the others continue their separate way.", 0f, 0.4f, Emotion.Neutral);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Before they go, they promise to see you again.", 0f, 0.4f, Emotion.Neutral);
            }
        }
        private void AddChosenCardToDeck()
        {
            modDeckPile.AddToPile(this.chosenReward.transform);
            SaveManager.SaveFile.CurrentDeck.AddCard(this.chosenReward.Info);
            AnalyticsManager.SendCardPickedEvent(this.chosenReward.Info.name);
        }
        private void OnRewardChosen(SelectableCard card)
        {
            if (!LobotomySaveManager.LearnedAbnormalChoice && !this.AllCardsFlippedUp())
                HintsHandler.OnClickCardChoiceWhileOtherFlipped();

            else if (chosenReward == null)
            {
                base.SetCollidersEnabled(collidersEnabled: false);
                chosenReward = card;
            }
        }
        private void OnCardFlipped(SelectableCard card)
        {
            card.SetLocalPosition(Vector3.zero, 0f, immediate: true);
            if (Singleton<InteractionCursor>.Instance.CurrentInteractable == card)
                base.OnCardInspected(card);

            if (card.Info != null)
                base.StartCoroutine(RegularChoiceFlipped(card));

        }
        private IEnumerator RegularChoiceFlipped(SelectableCard card)
        {
            Vector3 originalCardPos = card.transform.position;
            yield return TutorialTextSequence(card);
            if (DuplicateInDeck(card))
                SpawnMushroom(originalCardPos);

            // unlock achievement upon flipping the card
            if (card.Info.name == "wstl_angela" && !LobotomySaveManager.UnlockedAngela)
            {
                yield return new WaitForSeconds(0.25f);
                LobotomyPlugin.AchievementAPI.Unlock(LobotomyPlugin.AchievementAPI.Impuritas);
                LobotomySaveManager.UnlockedAngela = true;
            }
        }
        private IEnumerator TutorialTextSequence(SelectableCard card)
        {
            var component = card.GetComponent<CustomPaperTalkingCard>();

            if (component)
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
                    Singleton<TextDisplayer>.Instance.ShowMessage("You may ask [c:bR]1[c:] to join you. The others will continue on their own.");
                }
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            }
            if (component)
            {
                yield return new WaitUntil(() => !component.PlayingDialogue);
                component.CurrentDialogueSequence = null;
            }
        }
        private bool AllCardsFlippedUp() => !selectableCards.Exists((SelectableCard x) => x.Flipped);
        private bool DuplicateInDeck(SelectableCard card)
        {
            if (card != null && card.Info != null && !card.Info.name.ToLowerInvariant().Contains("deathcard"))
                return SaveManager.SaveFile.CurrentDeck.Cards.Exists((CardInfo x) => x.name == card.Info.name);

            return false;
        }
        private void CleanUpCards(bool doTween = true)
        {
            base.ResetLocalRotations();
            this.CleanupMushrooms();
            foreach (SelectableCard selectableCard in base.selectableCards)
            {
                selectableCard.SetInteractionEnabled(interactionEnabled: false);
                if (selectableCard != this.chosenReward)
                {
                    if (doTween)
                        Tween.Position(selectableCard.transform, selectableCard.transform.position + Vector3.forward * 20f, 0.5f, 0f, Tween.EaseInOut);

                    Destroy(selectableCard.gameObject, doTween ? 0.5f : 0f);
                }
            }
            base.selectableCards.Clear();
        }
        private void OnRerollChoices()
        {
            choicesRerolled = true;
            CleanUpRerollItem();
        }
        private void OnCursorEnterRerollInteractable() => modRerollAnim.Play("shake", 0, 0f);
        private void CleanUpRerollItem()
        {
            if (modRerollInteractable != null && modRerollInteractable.gameObject.activeSelf)
            {
                modRerollInteractable.SetEnabled(enabled: false);
                modRerollAnim.Play("exit", 0, 0f);
                CustomCoroutine.WaitThenExecute(0.25f, delegate
                {
                    modRerollInteractable.gameObject.SetActive(value: false);
                });
            }
        }
        private void SpawnMushroom(Vector3 cardPosition)
        {
            if (showMushrooms && selectableCards.Count > 0)
            {
                GameObject gameObject = UnityEngine.Object.Instantiate(ResourceBank.Get<GameObject>("Prefabs/SpecialNodeSequences/Mushroom_CardChoice"));
                gameObject.transform.parent = base.transform;
                gameObject.transform.position = cardPosition + Vector3.back * 1.1f;
                gameObject.transform.eulerAngles = new Vector3(CustomRandom.RandomBetween(10f, 30f), CustomRandom.RandomBetween(-50f, 50f), 0f);
                gameObject.transform.localScale = Vector3.one * 0.35f;
                AudioController.Instance.PlaySound3D("small_mushroom_grow", MixerGroup.TableObjectsSFX, gameObject.transform.position, 0.5f, 0f, new AudioParams.Pitch(AudioParams.Pitch.Variation.Small));
                mushrooms.Add(gameObject);
            }
        }
        private void CleanupMushrooms()
        {
            if (!showMushrooms)
                return;
            foreach (GameObject mushroom in mushrooms)
            {
                if (mushroom != null)
                {
                    AudioController.Instance.PlaySound3D("small_mushroom_shrink", MixerGroup.TableObjectsSFX, mushroom.transform.position, 0.5f, 0f, new AudioParams.Pitch(AudioParams.Pitch.Variation.Small), new AudioParams.Repetition(0.1f));
                    mushroom.GetComponent<MushroomInteractable>().SetEnabled(enabled: false);
                    mushroom.GetComponentInChildren<Animator>().Play("mushroom_shrink", 0, 0f);
                    UnityEngine.Object.Destroy(mushroom, 1f);
                }
            }
            mushrooms.Clear();
        }
    }
}