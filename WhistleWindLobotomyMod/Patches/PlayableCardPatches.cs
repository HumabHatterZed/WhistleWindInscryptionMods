using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

// Patches to make abilities function properly
namespace WhistleWindLobotomyMod.Patches
{
    [HarmonyPatch(typeof(PlayableCard))]
    internal class PlayableCardPatches
    {
        [HarmonyPostfix, HarmonyPatch(nameof(PlayableCard.CanBeSacrificed), MethodType.Getter)]
        private static void CannotSacrificeApostles(PlayableCard __instance, ref bool __result)
        {
            if (__instance.HasTrait(TraitApostle))
                __result = false;
        }

        [HarmonyPrefix, HarmonyPatch(nameof(PlayableCard.Die))]
        private static bool ApostleTransformOnDie(ref IEnumerator __result, PlayableCard __instance, bool wasSacrifice, PlayableCard killer)
        {
            if (__instance.HasAbility(Apostle.ability))
            {
                // if killed by WhiteNight or One Sin, die normally
                if (killer != null && killer.HasAnyOfAbilities(Confession.ability, TrueSaviour.ability))
                    return true;

                bool saviour = BoardManager.Instance.GetSlotsCopy(!__instance.OpponentCard).Exists(x => x.Card?.HasAbility(TrueSaviour.ability) ?? false);

                // Downed Apostles can't die if WhiteNight is an ally
                // Active Apostles will always be downed
                if (!__instance.Info.name.EndsWith("Down") || saviour)
                    __result = ApostleDie(__instance, wasSacrifice, killer);
                else
                    return true;

                return false;
            }
            return true;
        }
        private static IEnumerator ApostleDie(PlayableCard instance, bool wasSacrifice, PlayableCard killer)
        {
            // play hit anim then trigger Die
            instance.Anim.PlayHitAnimation();
            instance.Anim.SetShielded(shielded: false);
            yield return instance.Anim.ClearLatchAbility();
            if (instance.TriggerHandler.RespondsToTrigger(Trigger.Die, wasSacrifice, killer))
                yield return instance.TriggerHandler.OnTrigger(Trigger.Die, wasSacrifice, killer);

            yield break;
        }
    }
}
