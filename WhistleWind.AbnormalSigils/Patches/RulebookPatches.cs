using DiskCardGame;
using HarmonyLib;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;

namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch]
    internal class RulebookPatches
    {
        [HarmonyPatch(typeof(RuleBookController))]
        public static class OpenToAbilityPage_patch
        {
            // Reset the descriptions of WhiteNight-related abilities
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.SetShown))]
            public static bool ResetAlteredDescriptions(bool shown)
            {
                if (!shown)
                {
                    AbilitiesUtil.GetInfo(NeuteredLatch.ability).rulebookDescription = NeuteredLatchStart + "2" + NeuteredLatchEnd;
                }
                return true;
            }
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.OpenToAbilityPage))]
            public static bool OpenToAbilityPage(PlayableCard card)
            {
                if (card != null)
                {
                    if (card.HasAbility(NeuteredLatch.ability))
                    {
                        AbilitiesUtil.GetInfo(NeuteredLatch.ability).rulebookDescription = NeuteredLatchStart + card.GetComponent<NeuteredLatch>().BonesCost.ToString() + NeuteredLatchEnd;
                    }
                }
                return true;
            }
        }
    }
}
