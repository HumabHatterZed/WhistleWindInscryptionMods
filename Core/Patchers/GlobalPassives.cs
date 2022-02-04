using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using HarmonyLib;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

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
                CardSlot thisSlot = __instance.Slot;
                
                #region TEAM LEADER
                if (thisSlot.IsPlayerSlot)
                {
                    // if the card is on our side
                    foreach (CardSlot cardSlot in Singleton<BoardManager>.Instance.GetSlots(true).Where(cardSlot => cardSlot != thisSlot))
                    {
                        if (cardSlot.Card != null)
                        {
                            __result += cardSlot.Card.AbilityCount(TeamLeader.ability);
                        }
                    }
                }    
                else
                {
                    // if the card is on Leshy's side
                    foreach (CardSlot cardSlot in Singleton<BoardManager>.Instance.GetSlots(false).Where(cardSlot => cardSlot != thisSlot))
                    {
                        if (cardSlot.Card != null)
                        {
                            __result += cardSlot.Card.AbilityCount(TeamLeader.ability);
                        }
                    }
                }
                #endregion
                
                #region AGGRAVATING AND IDOL
                if (thisSlot.IsPlayerSlot)
                {
                    // if the card's on our side
                    foreach (CardSlot cardSlot in Singleton<BoardManager>.Instance.GetSlots(false).Where(cardSlot => cardSlot))
                    {
                        if (cardSlot.Card != null)
                        {
                            // subtract Aggravator buff by Idol debuff
                            __result += cardSlot.Card.AbilityCount(Aggravating.ability) - cardSlot.Card.AbilityCount(Idol.ability);
                        }
                    }
                }
                else
                {
                    // if the card's on Leshy's side
                    foreach (CardSlot cardSlot in Singleton<BoardManager>.Instance.GetSlots(true).Where(cardSlot => cardSlot))
                    {
                        if (cardSlot.Card != null)
                        {
                            // subtract Aggravator buff by Idol debuff
                            __result += cardSlot.Card.AbilityCount(Aggravating.ability) - cardSlot.Card.AbilityCount(Idol.ability);
                        }
                    }
                }
                #endregion

                #region BITTER ENEMIES
                if (__instance.HasAbility(BitterEnemies.ability))
                {
                    foreach (CardSlot cardSlot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(cardSlot => cardSlot != thisSlot))
                    {
                        if (cardSlot.Card != null)
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
            }
        }
    }
}
