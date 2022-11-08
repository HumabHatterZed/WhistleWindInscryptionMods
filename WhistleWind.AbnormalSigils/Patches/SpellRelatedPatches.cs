using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;

namespace WhistleWind.AbnormalSigils.Patches
{
    // Patches for spells to work properly
    // These are only patched in if Spell API is installed
    internal class SpellRelatedPatches
    {
        public static void ShowStatsForTargetedSpells(ref int[] __result, ref VariableStatBehaviour __instance)
        {
            // if has spell that shows stats, update stat values
            if (__instance.PlayableCard.HasAnyOfAbilities(TargetGainStats.ability, TargetGainStatsSigils.ability, Scrambler.ability))
                __instance.PlayableCard.Info.hideAttackAndHealth = false;
        }
    }

    [HarmonyPatch]
    internal class VariableStatBehaviourPatch
    {
        [HarmonyPatch(typeof(VariableStatBehaviour), nameof(VariableStatBehaviour.UpdateStats))]
        [HarmonyPrefix]
        public static bool ShowStatsWhenInHand(ref VariableStatBehaviour __instance)
        {
            if (!AbnormalPlugin.SpellAPI.Enabled)
                return true;

            // if a spell that displays stats, show when in hand or on board
            if (__instance.PlayableCard.HasAnyOfAbilities(TargetGainStats.ability, TargetGainStatsSigils.ability, Scrambler.ability))
            {
                int[] array = new int[2];
                if (__instance.PlayableCard.InHand || __instance.PlayableCard.OnBoard)
                {
                    array = __instance.GetStatValues();
                    __instance.statsMod.attackAdjustment = array[0];
                    __instance.statsMod.healthAdjustment = array[1];
                }
                __instance.PlayableCard.RenderInfo.showSpecialStats = __instance.PlayableCard.InHand || __instance.PlayableCard.OnBoard;
                if (!__instance.StatValuesEqual(__instance.prevStatValues, array) || __instance.prevOnBoard != __instance.PlayableCard.OnBoard)
                    __instance.PlayableCard.OnStatsChanged();

                __instance.prevStatValues = array;
                __instance.prevOnBoard = __instance.PlayableCard.OnBoard;
                return false;
            }
            return true;
        }
    }
}
