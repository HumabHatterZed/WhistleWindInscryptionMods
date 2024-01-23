using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
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
            if (__instance.HasTrait(Apostle))
                __result = false;
        }

        [HarmonyPrefix, HarmonyPatch(nameof(PlayableCard.Die))]
        private static bool ApostleTransformOnDie(ref IEnumerator __result, PlayableCard __instance, bool wasSacrifice, PlayableCard killer)
        {
            if (__instance.HasSpecialAbility(Smile.specialAbility) && __instance.Info.name != "wstl_mountainOfBodies")
            {
                __result = ApostleDie(__instance, wasSacrifice, killer);
                return false;
            }

            if (__instance.HasAnyOfAbilities(ApostleSigil.ability, Confession.ability))
            {
                // if killed by WhiteNight or One Sin, die normally
                if (killer != null && killer.HasAnyOfAbilities(Confession.ability, TrueSaviour.ability))
                    return true;

                bool saviour = BoardManager.Instance.AllSlotsCopy.Exists(x => x.Card?.HasAbility(TrueSaviour.ability) ?? false);

                // Active Apostles will always be downed
                // Downed Apostles can't die if WhiteNight is on the board
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
            // play hit anim then trigger Die (don't actually die though)
            instance.Anim.PlayHitAnimation();
            instance.Anim.SetShielded(shielded: false);
            yield return instance.Anim.ClearLatchAbility();
            if (instance.TriggerHandler.RespondsToTrigger(Trigger.Die, wasSacrifice, killer))
                yield return instance.TriggerHandler.OnTrigger(Trigger.Die, wasSacrifice, killer);
        }
    }
}
