using DiskCardGame;
using HarmonyLib;

namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch]
    internal static class CustomBossPatches
    {
        [HarmonyPrefix, HarmonyPatch(typeof(LifeManager), nameof(LifeManager.ShowDamageSequence))]
        internal static bool PreventOpponentDamage(bool toPlayer)
        {
            if (!toPlayer)
                return !LobotomyPlugin.PreventOpponentDamage;

            return true;
        }
    }
}
