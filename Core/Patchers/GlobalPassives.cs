using APIPlugin;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public static class GlobalPassives // ripped in its near entirety from  divisionbyz0rro / Infiniscryption / VanillaStackable
    {
        private static int AbilityCount(this PlayableCard card, Ability ability)
        {
            // count total number of abilities, since these are mod abilities and thus aren't automatically run
            int count = card.Info.Abilities
                        .Concat(AbilitiesUtil.GetAbilitiesFromMods(card.TemporaryMods))
                        .Where(ab => ab == ability)
                        .Count();

            if (count > 0)
                return count;
            else
                return 0;
        }

        [HarmonyPatch(typeof(PlayableCard), "GetPassiveAttackBuffs")]
        [HarmonyPostfix]
        public static void GetGlobalBuffs(ref int __result, ref PlayableCard __instance)
        {
            if (__instance.OnBoard)
            {
                #region TEAM LEADER
                foreach (CardSlot cardSlot in Singleton<BoardManager>.Instance.GetSlots(__instance.Slot.IsPlayerSlot))
                {
                    if (cardSlot.Card != null && cardSlot.Card != __instance)
                    {
                        __result += cardSlot.Card.AbilityCount(TeamLeader.ability);
                    }
                }
                #endregion

                #region AGGRAVATING AND IDOL
                foreach (CardSlot cardSlot in Singleton<BoardManager>.Instance.GetSlots(!__instance.Slot.IsPlayerSlot))
                {
                    if (cardSlot.Card != null && cardSlot.Card != __instance)
                    {
                        // subtract Aggravator buff by Idol debuff
                        __result += cardSlot.Card.AbilityCount(Aggravating.ability) - cardSlot.Card.AbilityCount(Idol.ability);
                    }
                }
                #endregion

                #region BITTER ENEMIES
                if (__instance.HasAbility(BitterEnemies.ability))
                {
                    foreach (CardSlot cardSlot in Singleton<BoardManager>.Instance.AllSlotsCopy)
                    {
                        if (cardSlot.Card != null && cardSlot.Card != __instance)
                        {
                            __result += cardSlot.Card.AbilityCount(BitterEnemies.ability);
                        }
                    }
                }
                #endregion

                #region COURAGEOUS
                if (!__instance.HasAbility(Ability.Submerge) && !__instance.HasAbility(Ability.TailOnHit))
                {
                    foreach (CardSlot cardSlot in Singleton<BoardManager>.Instance.GetAdjacentSlots(__instance.Slot))
                    {
                        if (cardSlot.Card != null)
                        {
                            if (cardSlot.Card.HasAbility(Courageous.ability))
                            {
                                if (__instance.TemporaryMods.Contains(Courageous.courageMod))
                                {
                                    __result += 1;
                                    if (__instance.TemporaryMods.Contains(Courageous.courageMod2))
                                    {
                                        __result += 1;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region IRRITATING
                if (__instance.Slot.opposingSlot.Card != null)
                {
                    __result += __instance.Slot.opposingSlot.Card.AbilityCount(Irritating.ability);
                }
                #endregion
            }
        }
    }
}
