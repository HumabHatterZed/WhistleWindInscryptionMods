using DiskCardGame;
using HarmonyLib;
using Infiniscryption.Core.Helpers;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infiniscryption.Spells.Patchers
{
    public static class SpellBehavior
    {
        public class SpellBackgroundAppearance : CardAppearanceBehaviour
        {
            public static Appearance ID = CardAppearanceBehaviourManager.Add(InfiniscryptionSpellsPlugin.OriginalPluginGuid, "SpellBackground", typeof(SpellBackgroundAppearance)).Id;
            private static readonly Texture _emptySpell = AssetHelper.LoadTexture("empty_spell_background");
            public override void ApplyAppearance()
            {
                base.Card.RenderInfo.baseTextureOverride = _emptySpell;
            }
        }
        public class RareSpellBackgroundAppearance : CardAppearanceBehaviour
        {
            public static Appearance ID = CardAppearanceBehaviourManager.Add(InfiniscryptionSpellsPlugin.OriginalPluginGuid, "RareSpellBackground", typeof(RareSpellBackgroundAppearance)).Id;
            private static readonly Texture _emptySpell = AssetHelper.LoadTexture("empty_spell_background_rare");
            public override void ApplyAppearance()
            {
                base.Card.RenderInfo.baseTextureOverride = _emptySpell;
            }
        }

        public static bool IsGlobalSpell(this CardInfo card) => card.HasSpecialAbility(GlobalSpellAbility.ID);
        public static bool IsTargetedSpell(this CardInfo card) => card.HasSpecialAbility(TargetedSpellAbility.ID);
        public static bool IsSpell(this CardInfo card) => card.IsTargetedSpell() || card.IsGlobalSpell();

        public static List<CardSlot> GetAffectedSlots(this CardSlot slot, PlayableCard card)
        {
            if (card.HasAbility(Ability.AllStrike))
                return slot.IsPlayerSlot ? Singleton<BoardManager>.Instance.PlayerSlotsCopy : Singleton<BoardManager>.Instance.OpponentSlotsCopy;

            List<CardSlot> retval = new();

            if (card.HasAbility(Ability.SplitStrike) || card.HasAbility(Ability.TriStrike))
            {
                CardSlot leftSlot = Singleton<BoardManager>.Instance.GetAdjacent(slot, true);
                CardSlot rightSlot = Singleton<BoardManager>.Instance.GetAdjacent(slot, true);

                if (leftSlot != null)
                    retval.Add(leftSlot);

                if (rightSlot != null)
                    retval.Add(rightSlot);

                if (card.HasAbility(Ability.TriStrike))
                    retval.Add(slot);
            }
            else
            {
                retval.Add(slot);
            }

            return retval;
        }
        public static bool IsValidTarget(this CardSlot slot, PlayableCard card, bool singleSlotOverride = false)
        {
            if (singleSlotOverride)
            {
                if (slot.IsPlayerSlot && card.TriggerHandler.RespondsToTrigger(Trigger.ResolveOnBoard, Array.Empty<object>()))
                    return true;

                if (card.TriggerHandler.RespondsToTrigger(Trigger.SlotTargetedForAttack, new object[] { slot, card }))
                    return true;

                return false;
            }
            else
            {
                // We need to test all possible slots
                foreach (CardSlot subSlot in slot.GetAffectedSlots(card))
                    if (subSlot.IsValidTarget(card, true))
                        return true;

                return false;
            }
        }
        public static bool HasValidTarget(this PlayableCard card)
        {
            List<CardSlot> allSlots = Singleton<BoardManager>.Instance.AllSlotsCopy;
            foreach (CardSlot slot in allSlots)
            {
                if (slot.IsValidTarget(card))
                    return true; // There is at least one slot that responds to this trigger, so leave the result as-is
            }

            // If we got this far without finding a slot that the card responds to, then...no good
            return false;
        }

        // First: we don't need room on board
        [HarmonyPatch(typeof(BoardManager), "SacrificesCreateRoomForCard")]
        [HarmonyPrefix]
        public static bool SpellsDoNotNeedSpace(PlayableCard card, ref bool __result)
        {
            if (card != null && card.Info.IsSpell())
            {
                __result = true;
                return false;
            }
            return true;
        }

        // Next, there has to be at least one slot (for targeted spells)
        // that the spell targets
        [HarmonyPatch(typeof(PlayableCard), "CanPlay")]
        [HarmonyPostfix]
        public static void TargetSpellsMustHaveValidTarget(ref bool __result, ref PlayableCard __instance)
        {
            if (!__result) // Don't do anything if the result's already false
                return;

            if (__instance.Info.IsTargetedSpell() && !__instance.HasValidTarget())
                __result = false;
        }

        [HarmonyPatch(typeof(DialogueDataUtil), "ReadDialogueData")]
        [HarmonyPostfix]
        public static void SpellHints()
        {
            // Here we add a line of dialogue for when the player tries to play a spell card with no valid targets
            DialogueHelper.AddOrModifySimpleDialogEvent("NoValidTargets", new string[]
            {
                "You cannot use that without a target."
            });
        }

        public static HintsHandler.Hint TargetSpellsNeedTargetHint = new("NoValidTargets", 2);

        [HarmonyPatch(typeof(HintsHandler), "OnNonplayableCardClicked")]
        [HarmonyPrefix]
        public static bool TargetSpellsNeedATarget(PlayableCard card)
        {
            if (card.Info.IsTargetedSpell() && !card.HasValidTarget())
            {
                TargetSpellsNeedTargetHint.TryPlayDialogue(null);
                return false;
            }
            return true;
        }

        // this allows the card's stats to be displayed as-is in-battle
        [HarmonyPatch(typeof(VariableStatBehaviour), nameof(VariableStatBehaviour.UpdateStats))]
        [HarmonyPrefix]
        public static bool ShowStatsWhenInHand(ref VariableStatBehaviour __instance)
        {
            // if a spell that displays stats, show when in hand or on board
            if (__instance.PlayableCard.Info.IsSpell() && !__instance.PlayableCard.Info.hideAttackAndHealth)
            {
                bool handOrBoard = __instance.PlayableCard.InHand || __instance.PlayableCard.OnBoard;
                int[] array = new int[2];
                if (handOrBoard)
                {
                    array = __instance.GetStatValues();
                    __instance.statsMod.attackAdjustment = array[0];
                    __instance.statsMod.healthAdjustment = array[1];
                    __instance.PlayableCard.RenderInfo.showSpecialStats = handOrBoard;
                }
                // call this every 1 second to minimise lag
                if (__instance.prevOnBoard != __instance.PlayableCard.OnBoard || (handOrBoard && Environment.TickCount % 50 == 0))
                {
                    __instance.PlayableCard.OnStatsChanged();
                }
                __instance.prevStatValues = array;
                __instance.prevOnBoard = __instance.PlayableCard.OnBoard;
                return false;
            }
            return true;
        }

        // method for adding show-stats spells to the campfire list of valid cards
        // not patched automatically
        public static void AllowStatBoostForSpells(ref List<CardInfo> __result)
        {
            List<CardInfo> deckList = new(RunState.DeckList);
            if (deckList.Exists(dl => dl.HasAnyOfSpecialAbilities(TargetedSpellAbility.ID, GlobalSpellAbility.ID)))
            {
                deckList.RemoveAll(ci => ci.LacksAllSpecialAbilities(TargetedSpellAbility.ID, GlobalSpellAbility.ID));
                deckList.RemoveAll(ci => ci.hideAttackAndHealth || (ci.baseAttack == 0 && ci.baseHealth == 0));
                __result.AddRange(deckList);
            }
        }

        // Next: we don't resolve normally
        // It's way easier to copy-paste this and only keep the stuff we need
        [HarmonyPatch(typeof(PlayerHand), "SelectSlotForCard")]
        [HarmonyPostfix]
        public static IEnumerator SpellsResolveDifferently(IEnumerator sequenceResult, PlayableCard card)
        {
            // If this isn't a spell ability, behave like normal
            if (card != null && !card.Info.IsSpell())
            {
                while (sequenceResult.MoveNext())
                    yield return sequenceResult.Current;

                yield break;
            }

            // The rest of this comes from the original code in PlayerHand.SelectSlotForCard

            Singleton<PlayerHand>.Instance.CardsInHand.ForEach(delegate (PlayableCard x)
            {
                x.SetEnabled(enabled: false);
            });
            yield return new WaitWhile(() => Singleton<PlayerHand>.Instance.ChoosingSlot);

            Singleton<PlayerHand>.Instance.OnSelectSlotStartedForCard(card);

            if (Singleton<RuleBookController>.Instance != null)
                Singleton<RuleBookController>.Instance.SetShown(shown: false);

            Singleton<BoardManager>.Instance.CancelledSacrifice = false;

            Singleton<PlayerHand>.Instance.choosingSlotCard = card;

            if (card != null && card.Anim != null)
                card.Anim.SetSelectedToPlay(selected: true);

            Singleton<BoardManager>.Instance.ShowCardNearBoard(card, showNearBoard: true);

            if (Singleton<TurnManager>.Instance.SpecialSequencer != null)
                yield return Singleton<TurnManager>.Instance.SpecialSequencer.CardSelectedFromHand(card);

            bool cardWasPlayed = false;
            bool requiresSacrifices = card.Info.BloodCost > 0;
            if (requiresSacrifices)
            {
                List<CardSlot> validSlots = Singleton<BoardManager>.Instance.PlayerSlotsCopy.FindAll((CardSlot x) => x.Card != null);
                yield return Singleton<BoardManager>.Instance.ChooseSacrificesForCard(validSlots, card);
            }

            // All card slots
            List<CardSlot> allSlots = Singleton<BoardManager>.Instance.AllSlotsCopy;

            if (!Singleton<BoardManager>.Instance.CancelledSacrifice)
            {
                IEnumerator chooseSlotEnumerator = Singleton<BoardManager>.Instance.ChooseSlot(allSlots, !requiresSacrifices);
                chooseSlotEnumerator.MoveNext();

                // Mark which slots can be targeted before letting the code continue
                foreach (CardSlot slot in allSlots)
                {
                    bool isValidTarget;
                    if (card.Info.IsTargetedSpell())
                        isValidTarget = slot.IsValidTarget(card);
                    else
                        isValidTarget = true;

                    slot.SetEnabled(isValidTarget);
                    slot.ShowState(isValidTarget ? HighlightedInteractable.State.Interactable : HighlightedInteractable.State.NonInteractable);
                    slot.Chooseable = isValidTarget;
                }
                yield return chooseSlotEnumerator.Current;

                // Run through the rest of the code to determine what slot has been targeted
                while (chooseSlotEnumerator.MoveNext())
                    yield return chooseSlotEnumerator.Current;

                if (!Singleton<BoardManager>.Instance.cancelledPlacementWithInput)
                {
                    cardWasPlayed = true;
                    card.Anim.SetSelectedToPlay(false);
                    // Now we take care of actually playing the card
                    if (Singleton<PlayerHand>.Instance.CardsInHand.Contains(card))
                    {
                        if (card.Info.BonesCost > 0)
                            yield return Singleton<ResourcesManager>.Instance.SpendBones(card.Info.BonesCost);

                        if (card.EnergyCost > 0)
                            yield return Singleton<ResourcesManager>.Instance.SpendEnergy(card.EnergyCost);

                        Singleton<PlayerHand>.Instance.RemoveCardFromHand(card);

                        // Activate triggers
                        // PlayFromHand
                        if (card.TriggerHandler.RespondsToTrigger(Trigger.PlayFromHand, Array.Empty<object>()))
                            yield return card.TriggerHandler.OnTrigger(Trigger.PlayFromHand, Array.Empty<object>());

                        // Recreate the OnResolve behaviour
                        if (card.TriggerHandler.RespondsToTrigger(Trigger.ResolveOnBoard, Array.Empty<object>()))
                        {
                            List<CardSlot> resolveSlots;
                            if (card.Info.IsTargetedSpell())
                                resolveSlots = Singleton<BoardManager>.Instance.LastSelectedSlot.GetAffectedSlots(card);
                            else
                                resolveSlots = new List<CardSlot>() { null }; // For global spells, just resolve once, globally

                            foreach (CardSlot slot in resolveSlots)
                            {
                                card.Slot = slot;

                                IEnumerator resolveTrigger = card.TriggerHandler.OnTrigger(Trigger.ResolveOnBoard, Array.Empty<object>());
                                for (bool active = true; active;)
                                {
                                    // Catch exceptions only on executing/resuming the iterator function
                                    try
                                    {
                                        active = resolveTrigger.MoveNext();
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.Log("IteratorFunction() threw exception: " + ex);
                                    }

                                    // Yielding and other loop logic is moved outside of the try-catch
                                    if (active)
                                        yield return resolveTrigger.Current;
                                }

                                card.Slot = null;
                            }
                        }

                        // If this is targeted, fire the targets
                        if (card.Info.IsTargetedSpell())
                        {
                            foreach (CardSlot targetSlot in Singleton<BoardManager>.Instance.LastSelectedSlot.GetAffectedSlots(card))
                            {
                                object[] targetArgs = new object[] { targetSlot, card };
                                yield return card.TriggerHandler.OnTrigger(Trigger.SlotTargetedForAttack, targetArgs);
                            }
                        }

                        card.Dead = true;
                        card.Anim.PlayDeathAnimation(false);

                        object[] diedArgs = new object[] { true, null };
                        if (card.TriggerHandler.RespondsToTrigger(Trigger.Die, diedArgs))
                            yield return card.TriggerHandler.OnTrigger(Trigger.Die, diedArgs);

                        yield return new WaitUntil(() => Singleton<GlobalTriggerHandler>.Instance.StackSize == 0);

                        if (Singleton<TurnManager>.Instance.IsPlayerTurn)
                            Singleton<BoardManager>.Instance.playerCardsPlayedThisRound.Add(card.Info);

                        Singleton<InteractionCursor>.Instance.ClearForcedCursorType();
                        yield return new WaitForSeconds(0.6f);
                        GameObject.Destroy(card.gameObject, 0.5f);
                        Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                    }
                }
            }
            if (!cardWasPlayed)
                Singleton<BoardManager>.Instance.ShowCardNearBoard(card, false);

            Singleton<PlayerHand>.Instance.choosingSlotCard = null;

            if (card != null && card.Anim != null)
                card.Anim.SetSelectedToPlay(false);

            Singleton<PlayerHand>.Instance.CardsInHand.ForEach(delegate (PlayableCard x)
            {
                x.SetEnabled(true);
            });

            // Enable every slot
            foreach (CardSlot slot in allSlots)
            {
                slot.SetEnabled(true);
                slot.ShowState(HighlightedInteractable.State.Interactable);
                slot.Chooseable = false;
            }

            yield break;
        }
    }
}