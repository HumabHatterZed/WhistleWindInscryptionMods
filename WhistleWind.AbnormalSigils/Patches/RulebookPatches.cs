/*using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Linq;
using WhistleWind.Core.AbilityClasses;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;

namespace WhistleWind.AbnormalSigils.Patches
{
    [HarmonyPatch]
    internal class RulebookPatches
    {
        [HarmonyPatch(typeof(RuleBookController))]
        public static class OpenToAbilityPage_patch
        {
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.SetShown))]
            public static bool ResetAlteredDescriptions(bool shown)
            {
                if (!shown)
                {
                    foreach (AbilityManager.FullAbility ab in AbilityManager.AllAbilities.Where(a => a.AbilityBehavior != null))
                    {
                        if (ab.AbilityBehavior.IsAssignableFrom(typeof(BetterActivatedAbilityBehaviour)))
                        {
                            Log.LogInfo($"trigger");
                            AbilitiesUtil.GetInfo(ab.Id).rulebookDescription = ab.Info.rulebookDescription;
                        }
                    }
                }
                return true;
            }
            [HarmonyPrefix, HarmonyPatch(nameof(RuleBookController.OpenToAbilityPage))]
            public static bool OpenToAbilityPage(PlayableCard card)
            {
                Log.LogInfo($"has activate : {card.GetComponent<BetterActivatedAbilityBehaviour>() != null}");
                if (card != null && card.GetComponent<BetterActivatedAbilityBehaviour>() != null)
                {
                    foreach (AbilityManager.FullAbility ab in AbilityManager.AllAbilities.Where(a => a.AbilityBehavior != null))
                    if (card.HasAbility(NeuteredLatch.ability))
                        AbilitiesUtil.GetInfo(NeuteredLatch.ability).rulebookDescription = NeuteredLatchStart + card.GetComponent<NeuteredLatch>().BonesCost.ToString() + NeuteredLatchEnd;
                    else if (card.HasAbility(RightfulHeir.ability))
                        AbilitiesUtil.GetInfo(RightfulHeir.ability).rulebookDescription = RightfulHeirStart + card.GetComponent<RightfulHeir>().BonesCost.ToString() + RightfulHeirEnd;
                }
                return true;
            }
        }
    }
}
*/