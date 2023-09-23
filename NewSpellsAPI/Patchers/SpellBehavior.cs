using DiskCardGame;
using HarmonyLib;
using Infiniscryption.Core.Helpers;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infiniscryption.Spells.Patchers
{
    public static class SpellBehavior
    {
        #region CardAppearanceBehaviours
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
        #endregion

        #region Spell Helpers
        public static bool IsGlobalSpell(this CardInfo card) => card.HasSpecialAbility(GlobalSpellAbility.ID);
        public static bool IsTargetedSpell(this CardInfo card) => card.HasSpecialAbility(TargetedSpellAbility.ID);
        public static bool IsSpell(this CardInfo card) => card.IsTargetedSpell() || card.IsGlobalSpell();
        #endregion

        #region Target Validators
        public static List<CardSlot> GetAffectedSlots(this CardSlot slot, PlayableCard card)
        {
            if (card.HasAbility(Ability.AllStrike))
                return Singleton<BoardManager>.Instance.AllSlotsCopy.FindAll(s => s.IsValidTarget(card));

            List<CardSlot> retval = new();

            if (card.HasAnyOfAbilities(Ability.SplitStrike, Ability.TriStrike))
            {
                CardSlot leftSlot = Singleton<BoardManager>.Instance.GetAdjacent(slot, true);
                CardSlot rightSlot = Singleton<BoardManager>.Instance.GetAdjacent(slot, false);

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

            retval.Sort((CardSlot a, CardSlot b) => a.Index - b.Index);

            return retval;
        }
        public static bool IsValidTarget(this CardSlot slot, PlayableCard card, bool checkSingleSlot = false)
        {
            if (!slot)
                return false;

            if (checkSingleSlot)
            {
                if (card.TriggerHandler.RespondsToTrigger(Trigger.ResolveOnBoard, Array.Empty<object>()))
                    return true;

                if (card.TriggerHandler.CustomRespondsToTrigger(Trigger.SlotTargetedForAttack, new object[] { slot, card }))
                    return true;

                return false;
            }
            else
            {
                // We need to test all possible slots
                return slot.GetAffectedSlots(card).Exists(subSlot => subSlot.IsValidTarget(card, true));
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
        #endregion

        #region Hint Patches
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
        #endregion

        #region Stat Spell Patches
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
        #endregion

        #region Main Spell Patches
        // First: we don't need room on board
        [HarmonyPatch(typeof(BoardManager), "SacrificesCreateRoomForCard")]
        [HarmonyPrefix]
        public static bool SpellsDoNotNeedSpace(PlayableCard card, BoardManager __instance, List<CardSlot> sacrifices, ref bool __result)
        {
            if (!card || !card.Info.IsSpell())
                return true;

            if (card.Info.BloodCost <= 0)
            {
                __result = true;
                return false;
            }

            // iterate through each slot that hasn't been selected for sacrifice
            // to determine if there will still be valid targets afterwards
            foreach (CardSlot slot in __instance.AllSlotsCopy
                .Where(asc => (asc.Card != null && asc.Card.HasAbility(Ability.Sacrificial)) || !sacrifices.Contains(asc)))
            {
                if (slot.IsValidTarget(card))
                {
                    __result = true;
                    break;
                }
            }

            return false;
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

        // Spells don't resolve normally
        // It's way easier to copy-paste this and only keep the stuff we need
        [HarmonyPatch(typeof(PlayerHand), "SelectSlotForCard")]
        [HarmonyPostfix]
        public static IEnumerator SpellsResolveDifferently(IEnumerator sequenceResult, PlayableCard card)
        {
            // If this isn't a spell card and we're lacking Give Sigils/S&S, behave normally
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

                bool canPlayCard = false;

                // Act 2 has some ManagedUpdate bull making things not do the do
                // so we go like this and it works, yeah
                if (SaveManager.SaveFile.IsPart2 && InputButtons.GetButtonDown(Button.Select))
                    canPlayCard = (Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot).IsValidTarget(card, true);
                else
                    canPlayCard = !Singleton<BoardManager>.Instance.cancelledPlacementWithInput;

                if (canPlayCard)
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

                        // PlayFromHand
                        if (card.TriggerHandler.RespondsToTrigger(Trigger.PlayFromHand, Array.Empty<object>()))
                            yield return card.TriggerHandler.OnTrigger(Trigger.PlayFromHand, Array.Empty<object>());

                        // ResolveOnBoard - recreates full behaviour
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
                                    try // Catch exceptions only on executing/resuming the iterator function
                                    {
                                        active = resolveTrigger.MoveNext();
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.Log("IteratorFunction() threw exception: " + ex);
                                    }

                                    if (active) // Yielding and other loop logic is moved outside of the try-catch
                                        yield return resolveTrigger.Current;
                                }

                                card.Slot = null;
                            }
                        }

                        // SlotTargetedForAttack (targeted spells only)
                        if (card.Info.IsTargetedSpell())
                        {
                            // LastSelectedSlot doesn't seem to work in Act 2, or at least not when there's a card
                            // so we do this
                            CardSlot selectedSlot = SaveManager.SaveFile.IsPart2 ?
                                Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot :
                                Singleton<BoardManager>.Instance.LastSelectedSlot;

                            foreach (CardSlot targetSlot in selectedSlot.GetAffectedSlots(card))
                            {
                                object[] targetArgs = new object[] { targetSlot, card };
                                yield return card.TriggerHandler.OnTrigger(Trigger.SlotTargetedForAttack, targetArgs);
                            }
                        }

                        card.Dead = true;
                        card.Anim.PlayDeathAnimation(false);

                        // Die
                        object[] diedArgs = new object[] { true, null };
                        if (card.TriggerHandler.RespondsToTrigger(Trigger.Die, diedArgs))
                            yield return card.TriggerHandler.OnTrigger(Trigger.Die, diedArgs);

                        yield return new WaitUntil(() => Singleton<GlobalTriggerHandler>.Instance.StackSize == 0);

                        if (Singleton<TurnManager>.Instance.IsPlayerTurn)
                            Singleton<BoardManager>.Instance.playerCardsPlayedThisRound.Add(card.Info);

                        Singleton<InteractionCursor>.Instance.ClearForcedCursorType();
                        yield return new WaitForSeconds(0.6f);
                        UnityEngine.Object.Destroy(card.gameObject, 0.5f);
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

        [HarmonyPostfix, HarmonyPatch(typeof(CardMergeSequencer), nameof(CardMergeSequencer.GetValidCardsForSacrifice))]
        private static void RemoveFromValidCardsForSacrifice(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.Abilities.Exists(x => !x.CanMerge()));
            if (InfiniscryptionSpellsPlugin.SpellMerge)
                __result.RemoveAll(x => x.IsSpell());
        }

        // Prevents card from being merged / gaining sigils
        [HarmonyPostfix, HarmonyPatch(typeof(CardMergeSequencer), nameof(CardMergeSequencer.GetValidCardsForHost))]
        private static void RemoveFromValidCardsForHost(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.Abilities.Exists(x => !x.CanMerge()));
            if (InfiniscryptionSpellsPlugin.SpellMerge)
                __result.RemoveAll(x => x.IsSpell());
        }

        #endregion

        #region Opponent Spell Patches
        // Change how opponent spells behave
        [HarmonyPatch(typeof(BoardManager), nameof(BoardManager.ResolveCardOnBoard))]
        [HarmonyPostfix]
        public static IEnumerator OpponentSpellsResolveDifferently(IEnumerator enumerator, PlayableCard card, CardSlot slot)
        {
            if (card != null && card.OpponentCard && card.Info.IsSpell())
            {
                CombatPhaseManager instance = Singleton<CombatPhaseManager>.Instance;
                SpellSniperVisualiser visualiser = null;
                if ((SaveManager.SaveFile?.IsPart1).GetValueOrDefault())
                    visualiser = instance.GetComponent<SpellSniperVisualiser>() ?? instance.gameObject.AddComponent<SpellSniperVisualiser>();

                // determine which slot will be initially targeted (if targeted spell)
                // also used to determine where the card moves to
                CardSlot targetSlot = null;
                if (card.Info.IsTargetedSpell())
                    targetSlot = OpponentGetTargetSlot(Singleton<BoardManager>.Instance.AllSlotsCopy.FindAll(s => s.IsValidTarget(card)));

                bool targetPlayer = card.Info.IsGlobalSpell() || (targetSlot != null && card.OpponentCard == targetSlot.IsPlayerSlot);

                Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue);
                yield return new WaitForSeconds(0.3f);

                // move card to position above board corresponding to the side it's targeting
                Tween.LocalPosition(card.transform, new(0.65f, 6.2f, targetPlayer ? 0f : 1f),
                    0.1f, 0f, Tween.EaseOut, Tween.LoopType.None, delegate
                    {
                        if (targetPlayer)
                            Singleton<ViewManager>.Instance.SwitchToView(View.Board);

                    }, delegate
                    {
                        card.Anim.PlayRiffleSound();
                        Tween.Rotation(card.transform, slot.transform.GetChild(0).rotation, 0.1f, 0f, Tween.EaseOut);
                    });

                yield return new WaitForSeconds(0.4f);

                if (card.TriggerHandler.RespondsToTrigger(Trigger.PlayFromHand, Array.Empty<object>()))
                    yield return card.TriggerHandler.OnTrigger(Trigger.PlayFromHand, Array.Empty<object>());

                // ResolveOnBoard
                if (card.TriggerHandler.RespondsToTrigger(Trigger.ResolveOnBoard, Array.Empty<object>()))
                {
                    List<CardSlot> resolveSlots;
                    if (card.Info.IsTargetedSpell())
                        resolveSlots = slot.GetAffectedSlots(card);
                    else
                        resolveSlots = new List<CardSlot>() { null }; // For global spells, just resolve once, globally

                    if (!card.Info.IsGlobalSpell())
                    {
                        foreach (CardSlot resolveTarget in resolveSlots)
                        {
                            instance.VisualizeAimSniperAbility(card.Slot, resolveTarget);
                            visualiser?.VisualizeAimSniperAbility(card.Slot, resolveTarget);
                            instance.VisualizeConfirmSniperAbility(resolveTarget);
                            visualiser?.VisualizeConfirmSniperAbility(resolveTarget);
                            yield return new WaitForSeconds(0.1f);
                        }
                        yield return new WaitForSeconds(0.2f);
                    }

                    for (int i = 0; i < resolveSlots.Count; i++)
                    {
                        card.Slot = resolveSlots[i];
                        IEnumerator resolveTrigger = card.TriggerHandler.OnTrigger(Trigger.ResolveOnBoard, Array.Empty<object>());

                        if (visualiser?.sniperIcons.Count > i && visualiser?.sniperIcons[i] != null)
                            visualiser?.CleanUpTargetIcon(visualiser?.sniperIcons[i]);

                        for (bool active = true; active;)
                        {
                            try // Catch exceptions only on executing/resuming the iterator function
                            {
                                active = resolveTrigger.MoveNext();
                            }
                            catch (Exception ex)
                            {
                                Debug.Log("IteratorFunction() threw exception: " + ex);
                            }

                            if (active) // Yielding and other loop logic is moved outside of the try-catch
                                yield return resolveTrigger.Current;
                        }

                        card.Slot = null;
                    }

                    yield return new WaitForSeconds(0.2f);
                    instance.VisualizeClearSniperAbility();
                    visualiser?.VisualizeClearSniperAbility();
                }

                // SlotTargetedForAttack (targeted spells only)
                if (card.Info.IsTargetedSpell())
                {
                    if (targetSlot != null)
                    {
                        // get a list of all slots that will be affected by sigils
                        List<CardSlot> targetSlots = targetSlot.GetAffectedSlots(card);
                        targetSlots.RemoveAll(s => !card.TriggerHandler.CustomRespondsToTrigger(Trigger.SlotTargetedForAttack, new object[] { s, card }));

                        foreach (CardSlot target in targetSlots)
                        {
                            instance.VisualizeAimSniperAbility(card.Slot, target);
                            visualiser?.VisualizeAimSniperAbility(card.Slot, target);
                            instance.VisualizeConfirmSniperAbility(target);
                            visualiser?.VisualizeConfirmSniperAbility(target);
                            yield return new WaitForSeconds(0.1f);
                        }
                        yield return new WaitForSeconds(0.2f);

                        for (int i = 0; i < targetSlots.Count; i++)
                        {
                            if (visualiser?.sniperIcons.Count > i && visualiser?.sniperIcons[i] != null)
                                visualiser?.CleanUpTargetIcon(visualiser?.sniperIcons[i]);
                            yield return card.TriggerHandler.OnTrigger(Trigger.SlotTargetedForAttack, new object[] { targetSlots[i], card });
                        }

                        yield return new WaitForSeconds(0.2f);
                        instance.VisualizeClearSniperAbility();
                        visualiser?.VisualizeClearSniperAbility();
                    }
                }

                card.Dead = true;
                card.Anim.PlayDeathAnimation(false);

                // Die
                object[] diedArgs = new object[] { true, null };
                if (card.TriggerHandler.RespondsToTrigger(Trigger.Die, diedArgs))
                    yield return card.TriggerHandler.OnTrigger(Trigger.Die, diedArgs);

                yield return new WaitUntil(() => Singleton<GlobalTriggerHandler>.Instance.StackSize == 0);
                yield break;
            }
            yield return enumerator;
        }

        [HarmonyPatch(typeof(Opponent), nameof(Opponent.PlayCardsInQueue))]
        [HarmonyPostfix]
        public static IEnumerator QueuedSpellsGoLast(IEnumerator enumerator, Opponent __instance, float tweenLength)
        {
            if (__instance.Queue.Count <= 0)
                yield break;

            if (__instance.Queue.Any(x => x.Info.IsSpell()))
            {
                List<PlayableCard> queuedCards = new(__instance.Queue);

                yield return __instance.VisualizePrePlayQueuedCards();
                List<PlayableCard> playedCards = new();

                queuedCards.Sort((PlayableCard a, PlayableCard b) => a.QueuedSlot.Index - b.QueuedSlot.Index);

                // play non-spell cards first - this is just the vanilla code
                foreach (PlayableCard queuedCard in queuedCards.Where(qc => !qc.Info.IsSpell()))
                {
                    if (!__instance.QueuedCardIsBlocked(queuedCard))
                    {
                        CardSlot queuedSlot = queuedCard.QueuedSlot;
                        queuedCard.QueuedSlot = null;
                        if (queuedCard != null)
                            queuedCard.OnPlayedFromOpponentQueue();

                        yield return Singleton<BoardManager>.Instance.ResolveCardOnBoard(queuedCard, queuedSlot, tweenLength);
                        playedCards.Add(queuedCard);
                    }
                }
                foreach (PlayableCard queuedCard in queuedCards.Where(qc => qc.Info.IsSpell()))
                {
                    // spell cards don't need space to be played
                    CardSlot queuedSlot = queuedCard.QueuedSlot;
                    queuedCard.QueuedSlot = null;
                    if (queuedCard != null)
                        queuedCard.OnPlayedFromOpponentQueue();

                    yield return Singleton<BoardManager>.Instance.ResolveCardOnBoard(queuedCard, queuedSlot, tweenLength);
                    playedCards.Add(queuedCard);
                }
                __instance.Queue.RemoveAll((PlayableCard x) => playedCards.Contains(x));
                yield return new WaitForSeconds(0.5f);
                yield break;
            }
            else
                yield return enumerator;
        }
        public static CardSlot OpponentGetTargetSlot(List<CardSlot> validTargets)
        {
            CardSlot selectedSlot = null;

            if (validTargets.Count > 0)
            {
                validTargets.Sort((CardSlot a, CardSlot b) => AIEvaluateTarget(b.Card) - AIEvaluateTarget(a.Card));
                selectedSlot = validTargets[0];
            }

            return selectedSlot;
        }
        private static int AIEvaluateTarget(PlayableCard card)
        {
            if (card == null)
                return UnityEngine.Random.Range(0, 5);

            int num = card.PowerLevel;
            if (card.Info.HasTrait(Trait.Terrain))
                num = 10 * (!card.OpponentCard ? -1 : 1);

            return num;
        }
        #endregion

        #region Trigger Helpers
        private static bool CustomRespondsToTrigger(this CardTriggerHandler handler, Trigger trigger, params object[] otherArgs)
        {
            foreach (TriggerReceiver allReceiver in GetAllReceivers(handler))
            {
                if (GlobalTriggerHandler.ReceiverRespondsToTrigger(trigger, allReceiver, otherArgs))
                    return true;
            }
            return false;
        }
        private static List<TriggerReceiver> GetAllReceivers(CardTriggerHandler handler)
        {
            List<TriggerReceiver> list = new();

            foreach (Tuple<SpecialTriggeredAbility, SpecialCardBehaviour> specialAbility in handler.specialAbilities)
                list.Add(specialAbility.Item2);

            foreach (Tuple<Ability, AbilityBehaviour> triggeredAbility in handler.triggeredAbilities)
            {
                if (triggeredAbility.Item1 != Ability.Brittle)
                    list.Add(triggeredAbility.Item2);
            }

            list.AddRange(handler.permanentlyAttachedBehaviours);
            list.Sort((TriggerReceiver a, TriggerReceiver b) => b.Priority.CompareTo(a.Priority));
            return list;
        }
        #endregion
    }
}