using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public abstract class WstlAbilityBehaviour : AbilityBehaviour
    {
        // literally just copied from ExtendedAbilityBehaviour
        private static ConditionalWeakTable<PlayableCard, List<WstlAbilityBehaviour>> AttackBuffAbilitiesAlly = new ConditionalWeakTable<PlayableCard, List<WstlAbilityBehaviour>>();
        private static ConditionalWeakTable<PlayableCard, List<WstlAbilityBehaviour>> AttackBuffAbilitiesOpponent = new ConditionalWeakTable<PlayableCard, List<WstlAbilityBehaviour>>();
        public virtual bool ProvidesPassiveAttackBuffAlly => false;
        public virtual bool ProvidesPassiveAttackBuffOpponent => false;
        private static List<WstlAbilityBehaviour> GetAttackBuffsAlly(PlayableCard card)
        {
            if (AttackBuffAbilitiesAlly.TryGetValue(card, out var value))
            {
                return value;
            }
            value = (from x in ((Component)card).GetComponents<WstlAbilityBehaviour>()
                     where x.ProvidesPassiveAttackBuffAlly
                     select x).ToList();
            AttackBuffAbilitiesAlly.Add(card, value);
            return value;
        }
        private static List<WstlAbilityBehaviour> GetAttackBuffsOpponent(PlayableCard card)
        {
            if (AttackBuffAbilitiesOpponent.TryGetValue(card, out var value))
            {
                return value;
            }
            value = (from x in ((Component)card).GetComponents<WstlAbilityBehaviour>()
                     where x.ProvidesPassiveAttackBuffOpponent
                     select x).ToList();
            AttackBuffAbilitiesOpponent.Add(card, value);
            return value;
        }
        public virtual int[] GetPassiveAttackBuffsAlly()
        {
            return null;
        }
        public virtual int[] GetPassiveAttackBuffsOpponent()
        {
            return null;
        }

        [HarmonyPatch(typeof(PlayableCard), "GetPassiveAttackBuffs")]
        [HarmonyPostfix]
        private static void AddPassiveAttackBuffsAlly(ref PlayableCard __instance, ref int __result)
        {
            if ((Object)(object)__instance.slot == (Object)null)
            {
                return;
            }
            // grab card's side's slots
            foreach (CardSlot item in (__instance.OpponentCard ? Singleton<BoardManager>.Instance.opponentSlots : Singleton<BoardManager>.Instance.playerSlots).Where((CardSlot s) => (Object)(object)s.Card != (Object)null))
            {
                foreach (WstlAbilityBehaviour item2 in from ab in GetAttackBuffsAlly(item.Card)
                                                       where (Object)(object)ab != (Object)null
                                                       select ab)
                {
                    int[] passiveAttackBuffs = item2.GetPassiveAttackBuffsAlly();
                    if (passiveAttackBuffs == null)
                    {
                        continue;
                    }
                    for (int i = 0; i < passiveAttackBuffs.Length; i++)
                    {
                        if (__instance.slot.Index == i)
                        {
                            __result += passiveAttackBuffs[i];
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(PlayableCard), "GetPassiveAttackBuffs")]
        [HarmonyPostfix]
        private static void AddPassiveAttackBuffsOpponent(ref PlayableCard __instance, ref int __result)
        {
            if ((Object)(object)__instance.slot == (Object)null)
            {
                return;
            }
            // grab opposing side's slots
            foreach (CardSlot item in (__instance.OpponentCard ? Singleton<BoardManager>.Instance.playerSlots : Singleton<BoardManager>.Instance.opponentSlots).Where((CardSlot s) => (Object)(object)s.Card != (Object)null))
            {
                foreach (WstlAbilityBehaviour item2 in from ab in GetAttackBuffsOpponent(item.Card)
                                                       where (Object)(object)ab != (Object)null
                                                       select ab)
                {
                    int[] passiveAttackBuffsOpponent = item2.GetPassiveAttackBuffsOpponent();
                    if (passiveAttackBuffsOpponent == null)
                    {
                        continue;
                    }
                    for (int i = 0; i < passiveAttackBuffsOpponent.Length; i++)
                    {
                        if (__instance.slot.Index == i)
                        {
                            __result += passiveAttackBuffsOpponent[i];
                        }
                    }
                }
            }
        }
    }
}