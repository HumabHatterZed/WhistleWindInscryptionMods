using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_LoveAndHate()
        {
            const string rulebookName = "Love and Hate";
            const string rulebookDescription = "Transforms when the 2 more allied cards than opponent cards have died, or vice versa. Transforms on upkeep.";
            LoveAndHate.specialAbility = AbilityHelper.CreateSpecialAbility<LoveAndHate>(rulebookName, rulebookDescription).Id;
        }
    }
    public class LoveAndHate : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        private int allyDeaths;
        private int opponentDeaths;
        private bool IsMagical => base.PlayableCard.Info.name == "wstl_magicalGirlHeart";
        private readonly string dialogue = "The balance must be maintained. Good cannot exist without evil.";
        private readonly string altDialogue = "Good cannot exist without evil.";

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return CheckForMagicGirls();
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return IsMagical && base.PlayableCard.OnBoard && fromCombat && killer != null;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            // does not increment if Magical Girl H is the killer
            if (killer != base.PlayableCard)
            {
                if (card.OpponentCard)
                    opponentDeaths++;
                else
                    allyDeaths++;
            }
            yield break;
        }
        
        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return IsMagical && base.PlayableCard.OnBoard && base.PlayableCard.OpponentCard != playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            // 2 more player cards have died than opponent cards
            if (allyDeaths > opponentDeaths + 1)
            {
                CardInfo evolution = GetEvolve(base.PlayableCard);

                yield return PerformTransformation(evolution);

                // If on opponent's side, move to player's if there's room, otherwise create in hand
                if (base.PlayableCard.OpponentCard)
                {
                    if (base.PlayableCard.Slot.opposingSlot.Card != null)
                    {
                        base.PlayableCard.RemoveFromBoard();
                        yield return new WaitForSeconds(0.5f);

                        if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
                        {
                            yield return new WaitForSeconds(0.2f);
                            Singleton<ViewManager>.Instance.SwitchToView(View.Hand);
                            yield return new WaitForSeconds(0.2f);
                        }
                        yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(evolution);
                        yield return new WaitForSeconds(0.45f);
                    }
                    else
                    {
                        yield return MoveToSlot(false, base.PlayableCard.Slot.opposingSlot);
                        yield return new WaitForSeconds(0.25f);
                    }
                }
                yield return PlayDialogue();
                Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            }

            // 2 more Leshy card deaths than player card deaths
            if (allyDeaths + 1 < opponentDeaths)
            {
                CardInfo evolution = GetEvolve(base.PlayableCard);

                yield return PerformTransformation(evolution);

                List<CardSlot> giantCheck = Singleton<BoardManager>.Instance.OpponentSlotsCopy.Where(s => s.Card != null && s.Card.Info.HasTrait(Trait.Giant)).ToList();
                // if owned by the player and there are no giant cards, go to the opponent's side
                if (!base.PlayableCard.OpponentCard && giantCheck.Count() == 0)
                {
                    CardSlot opposingSlot = base.PlayableCard.Slot.opposingSlot;
                    List<CardSlot> queuedSlots = Singleton<TurnManager>.Instance.Opponent.QueuedSlots;

                    // if the opposing slot is empty, move over to it
                    if (opposingSlot.Card == null)
                    {
                        WstlPlugin.Log.LogDebug("Moving Queen of Hatred to opposing slot.");
                        yield return MoveToSlot(true, opposingSlot);
                    }
                    // if the opposing slot is occupied add to queue
                    else
                    {
                        WstlPlugin.Log.LogDebug("Adding Queen of Hatred to queue.");
                        base.PlayableCard.RemoveFromBoard();
                        yield return new WaitForSeconds(0.5f);
                        CustomMethods.QueueCreatedCard(evolution);
                    }
                    yield return new WaitForSeconds(0.25f);
                }
                yield return PlayDialogue();
            }
        }

        private CardInfo GetEvolve(PlayableCard card)
        {
            CardInfo evolution = CardLoader.GetCardByName("wstl_queenOfHatred");
            foreach (CardModificationInfo item in card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            return evolution;
        }
        private IEnumerator PlayDialogue()
        {
            if (!WstlSaveManager.HasSeenHatredTransformation)
            {
                WstlSaveManager.HasSeenHatredTransformation = true;
                yield return CustomMethods.PlayAlternateDialogue(dialogue: dialogue);
            }
            else
            {
                yield return CustomMethods.PlayAlternateDialogue(dialogue: altDialogue);
            }
            yield return new WaitForSeconds(0.2f);
        }
        private IEnumerator PerformTransformation(CardInfo evolution)
        {
            yield return new WaitForSeconds(0.15f);
            base.PlayableCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return base.PlayableCard.TransformIntoCard(evolution);
            yield return new WaitForSeconds(0.5f);
        }
        private IEnumerator MoveToSlot(bool opponent, CardSlot slot)
        {
            base.PlayableCard.SetIsOpponentCard(opponent);
            base.PlayableCard.transform.eulerAngles += new Vector3(0f, 0f, -180f);
            yield return Singleton<BoardManager>.Instance.AssignCardToSlot(base.PlayableCard, slot, 0.25f);
        }

        private IEnumerator CheckForMagicGirls()
        {
            // Break if already have Jester
            if (WstlSaveManager.HasJester)
            {
                WstlPlugin.Log.LogDebug("Player already has Jester of Nihil.");
                yield break;
            }

            CardSlot greedSlot = null;
            CardSlot despairSlot = null;
            CardSlot wrathSlot = null;
            foreach (CardSlot slot in CustomMethods.GetBoardSlotsCopy(base.PlayableCard.OpponentCard).Where((CardSlot s) => s.Card != null))
            {
                if (slot != base.PlayableCard.Slot)
                {
                    string slotName = slot.Card.Info.name;
                    if (slotName == "wstl_magicalGirlDiamond" || slotName == "wstl_kingOfGreed")
                    {
                        WstlPlugin.Log.LogDebug("Player has Diamond.");
                        greedSlot = slot;
                    }
                    if (slotName == "wstl_magicalGirlSpade" || slotName == "wstl_knightOfDespair")
                    {
                        WstlPlugin.Log.LogDebug("Player has Spade.");
                        despairSlot = slot;
                    }
                    if (slotName == "wstl_magicalGirlClover" || slotName == "wstl_servantOfWrath")
                    {
                        WstlPlugin.Log.LogDebug("Player has Clover.");
                        wrathSlot = slot;
                    }
                }
            }
            if (greedSlot != null && despairSlot != null && wrathSlot != null)
                yield return Entropy(greedSlot, despairSlot, wrathSlot);

            yield break;
        }

        private IEnumerator Entropy(CardSlot greed, CardSlot despair, CardSlot wrath)
        {
            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            yield return new WaitForSeconds(0.2f);

            greed.Card.Anim.StrongNegationEffect();
            despair.Card.Anim.StrongNegationEffect();
            wrath.Card.Anim.StrongNegationEffect();
            base.PlayableCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.5f);

            if (!WstlSaveManager.HasSeenJester)
            {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Watch them carefully.");
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("They're calling - no, praying for something.");
            }

            // turn out the lights, activate table effects, remove magic girls
            Singleton<ExplorableAreaManager>.Instance.HangingLight.gameObject.SetActive(value: false);
            Singleton<ExplorableAreaManager>.Instance.HandLight.gameObject.SetActive(value: false);
            yield return TableEffects();
            RemoveMagic(greed, despair, wrath);

            yield return new WaitForSeconds(0.4f);

            // switch to default view while the lights are off
            Singleton<ViewManager>.Instance.SwitchToView(View.Default, true);

            yield return new WaitForSeconds(0.4f);

            if (!WstlSaveManager.HasSeenJester)
            {
                yield return CustomMethods.PlayAlternateDialogue(Emotion.Neutral, DialogueEvent.Speaker.Leshy, 0.2f,
                    "[c:gray]The jester[c:] retraced the steps of a path everyone would've taken.",
                    "No matter what it did, the jester always found itself at the end of that road.");
                yield return new WaitForSeconds(0.2f);
            }
            else
                yield return new WaitForSeconds(0.4f);

            if (Singleton<VideoCameraRig>.Instance != null)
                Singleton<VideoCameraRig>.Instance.PlayCameraAnim("refocus_quick");

            Singleton<ExplorableAreaManager>.Instance.HangingLight.gameObject.SetActive(value: true);
            Singleton<ExplorableAreaManager>.Instance.HandLight.gameObject.SetActive(value: true);

            // Show hand so player can see the jester
            yield return new WaitForSeconds(0.4f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Hand);
            yield return new WaitForSeconds(0.2f);

            // add jester to deck and hand
            CardInfo info = CardLoader.GetCardByName("wstl_jesterOfNihil");
            RunState.Run.playerDeck.AddCard(info);

            // set cost to 0 for this fight (can play immediately that way)
            info.cost = 0;
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(info, null, 0f, null);
            WstlSaveManager.HasJester = true;
            yield return new WaitForSeconds(0.2f);

            if (!WstlSaveManager.HasSeenJester)
            {
                WstlSaveManager.HasSeenJester = true;
                yield return CustomMethods.PlayAlternateDialogue(Emotion.Neutral, DialogueEvent.Speaker.Leshy, 0.2f,
                    "There was no way to know if they had gathered to become the jester,",
                    "or if the jester had come to resemble them.");
            }

            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.15f);

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }

        private void RemoveMagic(CardSlot greed, CardSlot despair, CardSlot wrath)
        {
            // Remove cards
            greed.Card.RemoveFromBoard(true, 0f);
            despair.Card.RemoveFromBoard(true, 0f);
            wrath.Card.RemoveFromBoard(true, 0f);
            base.PlayableCard.RemoveFromBoard(true, 0f);
        }

        private IEnumerator TableEffects()
        {
            WstlSaveManager.HasSeenJesterEffects = true;

            Color glowRed = GameColors.Instance.lightGray;
            Color darkRed = GameColors.Instance.lightGray;
            darkRed.a = 0.5f;
            Color gray = GameColors.Instance.gray;
            gray.a = 0.5f;

            Singleton<TableVisualEffectsManager>.Instance.ChangeTableColors(GameColors.Instance.nearBlack, GameColors.Instance.lightGray, GameColors.Instance.gray, darkRed, darkRed, glowRed, glowRed, glowRed, glowRed);

            AudioController.Instance.PlaySound2D($"grimoravoice_laughing#1");

            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(0.166f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(1.418f);
        }
    }
}
