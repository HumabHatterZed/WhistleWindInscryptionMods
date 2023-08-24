using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using InscryptionAPI.Helpers.Extensions;
using InscryptionCommunityPatch.Card;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch]
    public static class BindPatches
    {
/*        [HarmonyPrefix, HarmonyPatch(typeof(CombatPhaseManager), nameof(CombatPhaseManager.DoCombatPhase))]
        private static bool AddSpeedSupport(ref IEnumerator __result, CombatPhaseManager __instance, bool playerIsAttacker, SpecialBattleSequencer specialSequencer)
        {
            __result = DoCombatPhaseWithSpeed(__instance, playerIsAttacker, specialSequencer);
            return false;
        }

        public static IEnumerator DoCombatPhaseWithSpeed(CombatPhaseManager __instance, bool playerIsAttacker, SpecialBattleSequencer specialSequencer)
        {
            __instance.DamageDealtThisPhase = 0;
            List<CardSlot> attackingSlots = playerIsAttacker ? Singleton<BoardManager>.Instance.PlayerSlotsCopy : Singleton<BoardManager>.Instance.OpponentSlotsCopy;
            attackingSlots.RemoveAll((CardSlot x) => x.Card == null || x.Card.Attack == 0);
            attackingSlots = ModifyAttackSlots(attackingSlots, playerIsAttacker);
            bool atLeastOneAttacker = attackingSlots.Count > 0;
            yield return __instance.InitializePhase(attackingSlots, playerIsAttacker);
            if (specialSequencer != null)
            {
                if (playerIsAttacker)
                    yield return specialSequencer.PlayerCombatStart();
                else
                    yield return specialSequencer.OpponentCombatStart();
            }
            if (atLeastOneAttacker)
            {
                bool attackedWithSquirrel = false;
                foreach (CardSlot item in attackingSlots)
                {
                    item.Card.AttackedThisTurn = false;
                    if (item.Card.Info.IsOfTribe(Tribe.Squirrel))
                        attackedWithSquirrel = true;
                }
                foreach (CardSlot item2 in attackingSlots)
                {
                    AbnormalPlugin.Log.LogInfo($"Combat {item2}");
                    if (item2.Card != null && !item2.Card.AttackedThisTurn)
                    {
                        item2.Card.AttackedThisTurn = true;
                        yield return __instance.SlotAttackSequence(item2);
                    }
                }
                if (specialSequencer != null && playerIsAttacker)
                    yield return specialSequencer.PlayerCombatPostAttacks();

                yield return SelfAttackDamagePatch.NewAddDamageToScales(__instance, playerIsAttacker, specialSequencer, attackedWithSquirrel);
                yield return new WaitForSeconds(0.15f);
                foreach (CardSlot item3 in attackingSlots)
                {
                    if (item3.Card != null && item3.Card.TriggerHandler.RespondsToTrigger(Trigger.AttackEnded))
                        yield return item3.Card.TriggerHandler.OnTrigger(Trigger.AttackEnded);
                }
            }
            if (specialSequencer != null)
            {
                if (playerIsAttacker)
                    yield return specialSequencer.PlayerCombatEnd();
                else
                    yield return specialSequencer.OpponentCombatEnd();
            }
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            if (atLeastOneAttacker)
                yield return new WaitForSeconds(0.15f);
        }*/

        [HarmonyTranspiler, HarmonyPatch(typeof(CombatPhaseManager), nameof(CombatPhaseManager.DoCombatPhase), MethodType.Enumerator)]
        private static IEnumerable<CodeInstruction> AddBindSupport(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new(instructions);

            object attackingSlots = null;
            object playerIsAttacker = null;
            for (int i = 0; i < codes.Count; i++)
            {
                if (playerIsAttacker == null && codes[i].operand?.ToString() == "System.Boolean playerIsAttacker")
                {
                    playerIsAttacker = codes[i].operand;
                    continue;
                }
                if (attackingSlots == null && codes[i].operand?.ToString() == "System.Collections.Generic.List`1[DiskCardGame.CardSlot] <attackingSlots>5__2")
                {
                    attackingSlots = codes[i].operand;
                    continue;
                }

                if (codes[i].opcode == OpCodes.Callvirt && codes[i].operand.ToString() == "Int32 RemoveAll(System.Predicate`1[DiskCardGame.CardSlot])")
                {
                    MethodBase customMethod = AccessTools.Method(typeof(BindPatches), nameof(BindPatches.ModifyAttackSlots),
                        new Type[] { typeof(List<CardSlot>), typeof(bool) });

                    // we want to add this:
                    // this.attackingSlots = ModifyAttackSlots(this.attackingSlots, this.playerIsAttacker);
                    codes.Insert(i += 3, new(OpCodes.Ldarg_0));
                    codes.Insert(i++, new(OpCodes.Ldarg_0));
                    codes.Insert(i++, new(OpCodes.Ldfld, attackingSlots));
                    codes.Insert(i++, new(OpCodes.Ldarg_0));
                    codes.Insert(i++, new(OpCodes.Ldfld, playerIsAttacker));
                    codes.Insert(i++, new(OpCodes.Callvirt, customMethod));
                    codes.Insert(i++, new(OpCodes.Stfld, attackingSlots));

                    break;
                }
            }
            return codes;
        }

        private static List<CardSlot> ModifyAttackSlots(List<CardSlot> cardSlots, bool playerIsAttacker)
        {
            AbnormalPlugin.Log.LogInfo($"Start Modify {cardSlots.Count}");

            if (playerIsAttacker)
                cardSlots.RemoveAll(x => CardSpeed(x.Card) < 0);
            else
                cardSlots.RemoveAll(x => CardSpeed(x.Card, true) > 3);

            // get opposing cards with 5+ bind, as they attack this turn
            List<CardSlot> opposingSlots = BoardManager.Instance.GetSlotsCopy(!playerIsAttacker);
            opposingSlots.RemoveAll(x => x.Card == null || x.Card.Attack == 0 || x.Card.AttackedThisTurn);

            if (playerIsAttacker)
                opposingSlots.RemoveAll(x => CardSpeed(x.Card, true) <= 3);
            else
                opposingSlots.RemoveAll(x => CardSpeed(x.Card) > 0);

            if (opposingSlots.Count > 0)
                cardSlots.AddRange(opposingSlots);

            cardSlots.Sort((CardSlot a, CardSlot b) => CardSpeed(b.Card, b.Card.OpponentCard) - CardSpeed(a.Card, a.Card.OpponentCard));

            foreach (var c in cardSlots)
            {
                AbnormalPlugin.Log.LogInfo($"{c} {c.IsPlayerSlot} {CardSpeed(c.Card, c.Card.OpponentCard)}");
            }
            return cardSlots;
        }

        // cards owned by the owner have 3 Speed, while opposing have 0 Speed
        // Ex: Player's turn
        // Opponent card D has 4 Haste, giving 4 Speed
        // Opponent card D moves before all player cards
        private static int CardSpeed(PlayableCard card, bool checkingOpposing = false)
        {
            int cardSpeed = card.OpponentCard ? 0 : 3;
            cardSpeed -= card.GetAbilityStacks(Bind.iconId);
            cardSpeed += card.GetAbilityStacks(Haste.iconId);

            //AbnormalPlugin.Log.LogInfo($"{checkingOpposing} {cardSpeed}");
            return cardSpeed;
        }
    }
}
