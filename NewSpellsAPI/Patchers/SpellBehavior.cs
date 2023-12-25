using DiskCardGame;
using GBC;
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
    public class Act1QueuedCardInteractable : MainInputInteractable
    {
        public HighlightedInteractable queueSlot;
        public PlayableCard playableCard;

        public void QueueCursorEnter()
        {
            if (playableCard?.QueuedSlot == null)
            {
                playableCard = null;
                return;
            }
            SpellBehavior.UpdatePlayableStatsSpellDisplay(playableCard, true);
        }
        public void QueueCursorExit()
        {
            if (playableCard?.QueuedSlot == null)
            {
                playableCard = null;
                return;
            }
            SpellBehavior.UpdatePlayableStatsSpellDisplay(playableCard, false);
        }
    }
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
        public static bool IsGlobalSpell(this CardInfo card) => card.HasAnyOfSpecialAbilities(GlobalSpellAbility.ID, InstaGlobalSpellAbility.ID);
        public static bool IsInstaGlobalSpell(this CardInfo card) => card.HasSpecialAbility(InstaGlobalSpellAbility.ID);
        public static bool IsTargetedSpell(this CardInfo card) => card.HasSpecialAbility(TargetedSpellAbility.ID);
        public static bool IsSpell(this CardInfo card) => card.IsTargetedSpell() || card.IsGlobalSpell();
        public static bool IsSpellShowStats(this CardInfo card) => card.IsSpell() && !card.hideAttackAndHealth;
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
        public static bool NotStatsSpellInfo(CardInfo info) => info == null || !info.IsSpellShowStats();

        public static void UpdateStatsSpellDisplay<T>(T card, bool showStats) where T : Card
        {
            if (NotStatsSpellInfo(card.Info))
                return;

            card.RenderInfo.showSpecialStats = showStats;
            if (showStats)
            {
                card.RenderInfo.attack = card.Info.Attack;
                card.RenderInfo.health = card.Info.Health;
            }

            card.RenderInfo.attackTextColor = card.RenderInfo.attack > 0 ? GameColors.Instance.darkBlue : Color.black;
            card.RenderInfo.healthTextColor = card.RenderInfo.health > 0 ? GameColors.Instance.darkBlue : Color.black;
            card.RenderCard();
        }
        public static void UpdatePlayableStatsSpellDisplay(PlayableCard card, bool showStats)
        {
            if (NotStatsSpellInfo(card.Info))
                return;

            card.RenderInfo.showSpecialStats = showStats;
            if (showStats)
            {
                card.RenderInfo.attack = card.Info.Attack;
                card.RenderInfo.health = card.Info.Health;
            }

            card.RenderInfo.attackTextColor = (card.GetPassiveAttackBuffs() + card.GetStatIconAttackBuffs() != 0) ? GameColors.Instance.darkBlue : Color.black;
            card.RenderInfo.healthTextColor = (card.GetPassiveHealthBuffs() + card.GetStatIconHealthBuffs() != 0) ? GameColors.Instance.darkBlue : Color.black;
            card.RenderCard();
        }

        // these allow the player to view a stat spell's stats when hovering over a card - how fancy~
        [HarmonyPostfix, HarmonyPatch(typeof(SelectableCard), "OnCursorEnter")]
        private static void ShowStatsSelectableCards(SelectableCard __instance) => UpdateStatsSpellDisplay(__instance, true);

        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), "OnCursorEnter")]
        private static void ShowStatsPlayableCards(PlayableCard __instance) => UpdatePlayableStatsSpellDisplay(__instance, true);

        [HarmonyPostfix, HarmonyPatch(typeof(PixelSelectableCard), "OnCursorEnter")]
        private static void ShowStatsPixelSelectableCards(PixelSelectableCard __instance) => UpdateStatsSpellDisplay(__instance, true);

        [HarmonyPostfix, HarmonyPatch(typeof(PixelPlayableCard), "OnCursorEnter")]
        private static void ShowStatsPixelPlayableCards(PixelPlayableCard __instance) => UpdatePlayableStatsSpellDisplay(__instance, true);

        [HarmonyPostfix, HarmonyPatch(typeof(PixelSelectableCard), "OnCursorExit")]
        private static void HideStatsPixelSelectableCards(PixelSelectableCard __instance) => UpdateStatsSpellDisplay(__instance, false);

        [HarmonyPostfix, HarmonyPatch(typeof(PixelPlayableCard), "OnCursorExit")]
        private static void HideStatsPixelPlayableCards(PixelPlayableCard __instance) => UpdatePlayableStatsSpellDisplay(__instance, false);

        [HarmonyPostfix, HarmonyPatch(typeof(MainInputInteractable), "OnCursorExit")]
        private static void ShowStatsSelectableCards(MainInputInteractable __instance)
        {
            if (__instance is SelectableCard)
            {
                SelectableCard card = __instance as SelectableCard;
                UpdateStatsSpellDisplay(card, false);
            }
            else if (__instance is PlayableCard)
            {
                PlayableCard playableCard = __instance as PlayableCard;
                UpdatePlayableStatsSpellDisplay(playableCard, false);
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(BoardManager), nameof(BoardManager.QueueCardForSlot))]
        private static void ShowStatsSelectableCards(PlayableCard card)
        {
            if (SaveManager.SaveFile.IsPart2 || NotStatsSpellInfo(card.Info))
                return;

            HighlightedInteractable queueSlot = BoardManager3D.Instance.opponentQueueSlots[card.QueuedSlot.Index];
            Act1QueuedCardInteractable interactable = queueSlot.GetComponent<Act1QueuedCardInteractable>();
            if (interactable == null)
            {
                interactable = queueSlot.gameObject.AddComponent<Act1QueuedCardInteractable>();
                queueSlot.CursorEntered += x => interactable.QueueCursorEnter();
                queueSlot.CursorExited += x => interactable.QueueCursorExit();
            }
            interactable.playableCard = card;
            Debug.Log($"Butt {card.Info.name}");
        }
        // method for adding show-stats spells to the campfire list of valid cards
        // not patched automatically; stat spells need to be able to stat boost in the first place
        public static void AllowStatBoostForSpells(List<CardInfo> __result)
        {
            List<CardInfo> deckList = new(RunState.DeckList);
            deckList.RemoveAll(ci => __result.Contains(ci) || !ci.IsSpellShowStats() || (ci.baseAttack == 0 && ci.baseHealth == 0));
            __result.AddRange(deckList);
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
            Singleton<RuleBookController>.Instance?.SetShown(shown: false);
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
                bool canPlayCard = true;
                if (!card.Info.IsInstaGlobalSpell())
                {
                    IEnumerator chooseSlotEnumerator = Singleton<BoardManager>.Instance.ChooseSlot(allSlots, !requiresSacrifices);
                    chooseSlotEnumerator.MoveNext();

                    // Mark which slots can be targeted before letting the code continue
                    foreach (CardSlot slot in allSlots)
                    {
                        bool isValidTarget = !card.Info.IsTargetedSpell() || slot.IsValidTarget(card);

                        slot.SetEnabled(isValidTarget);
                        slot.ShowState(isValidTarget ? HighlightedInteractable.State.Interactable : HighlightedInteractable.State.NonInteractable);
                        slot.Chooseable = isValidTarget;
                    }

                    yield return chooseSlotEnumerator.Current; // not sure why this is separate, but it was like this in the original soo
                    while (chooseSlotEnumerator.MoveNext()) // Run through the rest of the code to determine what slot has been targeted
                        yield return chooseSlotEnumerator.Current;

                    // Act 2 has some ManagedUpdate bull making things not do the do
                    // so we check if the current interactable is A) a CardSlot and B) a valid target
                    if (SaveManager.SaveFile.IsPart2 && InputButtons.GetButtonDown(Button.Select) && card.Info.IsTargetedSpell())
                    {
                        canPlayCard = (Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot).IsValidTarget(card, true);
                    }
                    else
                    {
                        // if we didn't cancel placement
                        canPlayCard = !Singleton<BoardManager>.Instance.cancelledPlacementWithInput;
                    }
                }

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

                        if (card.Info.IsSpellShowStats())
                            UpdatePlayableStatsSpellDisplay(card, true);

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

                    if (card.Info.IsTargetedSpell())
                    {
                        foreach (CardSlot resolveTarget in resolveSlots)
                        {
                            instance.VisualizeAimSniperAbility(card.Slot, resolveTarget);
                            visualiser?.VisualizeAimSniperAbility(card.Slot, resolveTarget);
                            instance.VisualizeConfirmSniperAbility(resolveTarget);
                            visualiser?.VisualizeConfirmSniperAbility(resolveTarget);
                            yield return new WaitForSeconds(0.1f);
                        }
                        yield return new WaitForSeconds(0.4f);
                    }

                    for (int i = 0; i < resolveSlots.Count; i++)
                    {
                        card.Slot = resolveSlots[i];
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

                        if (visualiser?.sniperIcons.Count > i && visualiser?.sniperIcons[i] != null)
                            visualiser?.CleanUpTargetIcon(visualiser?.sniperIcons[i]);

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

            if (!__instance.Queue.Exists(x => x.Info.IsSpell()))
            {
                yield return enumerator;
                yield break;
            }

            yield return __instance.VisualizePrePlayQueuedCards();
            List<PlayableCard> playedCards = new(), queuedCards = new(__instance.Queue);
            queuedCards.Sort((PlayableCard a, PlayableCard b) => a.QueuedSlot.Index - b.QueuedSlot.Index);

            // play non-spell cards before spell cards
            foreach (PlayableCard queuedCard in queuedCards.Where(qc => !qc.Info.IsSpell()))
            {
                if (__instance.QueuedCardIsBlocked(queuedCard))
                    continue;

                CardSlot queuedSlot = queuedCard.QueuedSlot;
                queuedCard.QueuedSlot = null;
                queuedCard?.OnPlayedFromOpponentQueue();
                yield return Singleton<BoardManager>.Instance.ResolveCardOnBoard(queuedCard, queuedSlot, tweenLength);
                playedCards.Add(queuedCard);
            }
            foreach (PlayableCard queuedCard in queuedCards.Where(qc => qc.Info.IsSpell()))
            {
                if (__instance.QueuedCardIsBlocked(queuedCard))
                    continue;

                CardSlot queuedSlot = queuedCard.QueuedSlot;
                queuedCard.QueuedSlot = null;
                queuedCard?.OnPlayedFromOpponentQueue();
                yield return Singleton<BoardManager>.Instance.ResolveCardOnBoard(queuedCard, queuedSlot, tweenLength);
                playedCards.Add(queuedCard);
            }
            __instance.Queue.RemoveAll(playedCards.Contains);
            yield return new WaitForSeconds(0.5f);
        }
        [HarmonyPostfix, HarmonyPatch(typeof(Opponent), nameof(Opponent.QueuedCardIsBlocked))]
        private static void QueuedSpellsCantBeBlocked(ref bool __result, PlayableCard queuedCard)
        {
            if (queuedCard != null && queuedCard.Info.IsSpell())
                __result = false;
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