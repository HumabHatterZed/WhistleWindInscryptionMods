using BepInEx;
using InscryptionAPI;
using InscryptionAPI.Nodes;
using DiskCardGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Node_ModCardChoice()
        {
            List<byte[]> animationFrames = new()
            {
                Resources.nodeAbnormalityCardChoice1,
                Resources.nodeAbnormalityCardChoice2,
                Resources.nodeAbnormalityCardChoice3,
                Resources.nodeAbnormalityCardChoice4
            };
            NodeHelper.CreateNode("wstlModCardChoiceNode", typeof(WstlModCardChoicesSequencer), animationFrames, GenerationType.SpecialCardChoice, extraGenType: ConfigUtils.Instance.BoxStart ? GenerationType.RegionStart : GenerationType.None);
        }
    }
    // Pulled wholesale from CardSingleChoicesSequencer and CardChoiceSequencer
    public class WstlModCardChoicesSequencer : CustomCardChoiceNodeSequencer
    {
        protected CardPile modDeckPile = SpecialNodeHandler.Instance.cardChoiceSequencer.deckPile;
        protected GamepadGridControl modGamepadGrid = SpecialNodeHandler.Instance.cardChoiceSequencer.gamepadGrid;

        public ViewController.ControlMode viewControlMode = ViewController.ControlMode.CardChoice;
        public MainInputInteractable modRerollInteractable = SpecialNodeHandler.Instance.cardChoiceSequencer.rerollInteractable;
        private Animator modRerollAnim = SpecialNodeHandler.Instance.cardChoiceSequencer.rerollAnim;
        private readonly bool showMushrooms = true;
        private List<GameObject> mushrooms = new();
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
            {
                modGamepadGrid.enabled = true;
            }
            Singleton<TableRuleBook>.Instance.SetOnBoard(onBoard: true);
            base.StartCoroutine(modDeckPile.SpawnCards(SaveManager.SaveFile.CurrentDeck.Cards.Count));

            // First-time dialogue for the node
            if (!WstlSaveManager.AbnormalityCardChoice)
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                yield return new WaitForSeconds(0.4f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("The trees pull away from you as you enter a strange clearing.");
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Three black metal poles jut from the ashen ground and into the sky. Suspended in the air on each is a black container.");
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("The boxes lower to the ground. The clink of intricate machinery fills the air as each box shudders and opens, revealing their contents.");
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("[c:bR]3[c:] strange creatures stand before you. Powerful, mysterious...");
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("[c:bR]abnormal[c:].");
                yield return new WaitForSeconds(0.25f);
            }

            Singleton<ViewManager>.Instance.SwitchToView(this.choicesView, immediate: false, lockAfter: true);
            yield return this.CardSelectionSequence(choicesData);
            if (modGamepadGrid != null)
            {
                modGamepadGrid.enabled = false;
            }
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
            }

            chosenReward = null;
            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
            while (chosenReward == null)
            {
                List<CardChoice> choices = GenerateRiskChoices(randomSeed);
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
                    {
                        card.Initialize(cardChoice.CardInfo, OnRewardChosen, OnCardFlipped, startFlipped: true, base.OnCardInspected);
                    }
                    SpecialCardBehaviour[] components = card.GetComponents<SpecialCardBehaviour>();
                    for (int j = 0; j < components.Length; j++)
                    {
                        components[j].OnShownForCardChoiceNode();
                    }
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
        private List<CardChoice> GenerateRiskChoices(int randomSeed)
        {
            bool gotRare = false;
            List<CardChoice> listOfChoices = new();
            int regionTier = RunState.CurrentRegionTier;
            while (listOfChoices.Count < 3)
            {
                CardChoice cardChoice = new();
                string risk = GetRiskLevel(randomSeed++, regionTier);
                float mult = SaveFile.IsAscension ? 0.02f : 0.04f;
                bool rareChoice = !gotRare && !(SeededRandom.Value(randomSeed++) >= regionTier * mult);
                CardInfo card = ModCardLoader.GetRandomChoosableModCard(randomSeed++, risk);
                if (rareChoice)
                {
                    gotRare = true;
                    card = ModCardLoader.GetRandomRareModCard(randomSeed++);
                }
                while (listOfChoices.Exists((CardChoice x) => x.CardInfo.name == card.name))
                {
                    string risk2 = GetRiskLevel(randomSeed++, regionTier);
                    card = rareChoice ? ModCardLoader.GetRandomRareModCard(randomSeed++) : ModCardLoader.GetRandomChoosableModCard(randomSeed++, risk2);
                }
                cardChoice.CardInfo = card;
                listOfChoices.Add(cardChoice);
            }
            return new List<CardChoice>(listOfChoices.Randomize());
        }
        private string GetRiskLevel(int randomSeed, int regionTier)
        {
            float riskW = regionTier switch { 2 => 0.25f, 1 => 0.20f, _ => 0.10f};
            float riskH = regionTier switch { 2 => 0.25f, _ => 0.20f};
            float riskT = regionTier switch { 2 => 0.25f, _ => 0.30f};
            float value = SeededRandom.Value(randomSeed);
            if (value <= riskW)
            {
                return "Waw";
            }
            if (value <= (riskW + riskH))
            {
                return "He";
            }
            if (value <= (riskW + riskH + riskT))
            {
                return "Teth";
            }
            return "Zayin";
        }
        private IEnumerator AddCardToDeckAndCleanUp(SelectableCard card)
        {
            CleanUpRerollItem();
            Singleton<RuleBookController>.Instance.SetShown(shown: false);
            yield return RewardChosenSequence(card);
            ProgressionData.SetMechanicLearned(MechanicsConcept.CardChoice);
            AddChosenCardToDeck();
            Singleton<TextDisplayer>.Instance.Clear();
            yield return new WaitForSeconds(0.1f);
            UnityEngine.Object.Destroy(card.gameObject, 0.5f);
        }
        private IEnumerator RewardChosenSequence(SelectableCard card)
        {
            card.OnCardAddedToDeck();
            float num = 0f;
            if (!ProgressionData.LearnedMechanic(MechanicsConcept.CardChoice))
            {
                num = 0.5f;
            }
            base.deckPile.MoveCardToPile(card, flipFaceDown: true, num);
            yield return new WaitForSeconds(num);
            if (!ProgressionData.LearnedMechanic(MechanicsConcept.CardChoice))
            {
                Singleton<TextDisplayer>.Instance.Clear();
                yield return new WaitForSeconds(0.15f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("The creature joins your caravan as the other boxes close themselves.", 0f, 0.4f, Emotion.Neutral, TextDisplayer.LetterAnimation.WavyJitter);
            }
        }
        private void AddChosenCardToDeck()
        {
            base.deckPile.AddToPile(this.chosenReward.transform);
            SaveManager.SaveFile.CurrentDeck.AddCard(this.chosenReward.Info);
            AnalyticsManager.SendCardPickedEvent(this.chosenReward.Info.name);
            if (this.chosenReward.Info.name == "MantisGod")
            {
                AscensionStatsData.TryIncrementStat(AscensionStat.Type.MantisGodsPicked);
                if (StoryEventsData.EventCompleted(StoryEvent.LeshyDefeated))
                {
                    VoiceOverPlayer.Instance.PlayVoiceOver("Yeah. Always pick Mantis God.", "VO_mantisgod", VoiceOverPlayer.VOCameraAnim.MediumRefocus, StoryEvent.LukeVOMantisGod);
                }
            }
        }
        private void OnRewardChosen(SelectableCard card)
        {
            if (!ProgressionData.LearnedMechanic(MechanicsConcept.CardChoice) && !this.AllCardsFlippedUp())
            {
                HintsHandler.OnClickCardChoiceWhileOtherFlipped();
            }
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
            {
                base.OnCardInspected(card);
            }
            if (card.Info != null)
            {
                base.StartCoroutine(RegularChoiceFlipped(card));
            }
        }
        private IEnumerator RegularChoiceFlipped(SelectableCard card)
        {
            Vector3 originalCardPos = card.transform.position;
            yield return TutorialTextSequence(card);
            if (DuplicateInDeck(card))
            {
                SpawnMushroom(originalCardPos);
            }
            yield break;
        }
        private IEnumerator TutorialTextSequence(SelectableCard card)
        {
            if (!string.IsNullOrEmpty(card.Info.description) && !ProgressionData.IntroducedCard(card.Info))
            {
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
                Singleton<RuleBookController>.Instance.SetShown(shown: false);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(card.Info.description);
                ProgressionData.SetCardIntroduced(card.Info);
                if (!WstlSaveManager.AbnormalityCardChoice && this.AllCardsFlippedUp())
                {
                    WstlSaveManager.AbnormalityCardChoice = true;
                    yield return new WaitForSeconds(0.25f);
                    Singleton<TextDisplayer>.Instance.ShowMessage("You may choose [c:bR]1[c:] to join you. The others will remain here, sealed in their tombs.");
                }
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            }
        }
        private bool AllCardsFlippedUp()
        {
            return !selectableCards.Exists((SelectableCard x) => x.Flipped);
        }
        private bool DuplicateInDeck(SelectableCard card)
        {
            if (card != null && card.Info != null && !card.Info.name.ToLowerInvariant().Contains("deathcard"))
            {
                return SaveManager.SaveFile.CurrentDeck.Cards.Exists((CardInfo x) => x.name == card.Info.name);
            }
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
                    {
                        Tween.Position(selectableCard.transform, selectableCard.transform.position + Vector3.forward * 20f, 0.5f, 0f, Tween.EaseInOut);
                    }
                    UnityEngine.Object.Destroy(selectableCard.gameObject, doTween ? 0.5f : 0f);
                }
            }
            base.selectableCards.Clear();
        }
        private Texture GetCardbackTexture(CardChoice choice)
        {
            if (choice.resourceType == ResourceType.Blood)
            {
                return ResourceBank.Get<Texture>("Art/Cards/RewardBacks/card_rewardback_" + choice.resourceAmount + "blood");
            }
            if (choice.resourceType == ResourceType.Bone)
            {
                return ResourceBank.Get<Texture>("Art/Cards/RewardBacks/card_rewardback_bones");
            }
            return ResourceBank.Get<Texture>("Art/Cards/RewardBacks/card_rewardback_" + choice.tribe.ToString().ToLowerInvariant());
        }
        private void OnRerollChoices()
        {
            choicesRerolled = true;
            CleanUpRerollItem();
        }
        private void OnCursorEnterRerollInteractable()
        {
            modRerollAnim.Play("shake", 0, 0f);
        }
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
            {
                return;
            }
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