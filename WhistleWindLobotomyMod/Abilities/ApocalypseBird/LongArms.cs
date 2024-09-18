using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.RuleBook;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    [HarmonyPatch]
    public class LongArms : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        [HarmonyPostfix, HarmonyPatch(typeof(ConsumableItem), nameof(ConsumableItem.CanActivate))]
        private static void PreventHourglassItem(ConsumableItem __instance, ref bool __result)
        {
            if (!__result)
                return;

            if (__instance is HourglassItem && BoardManager.Instance.AllSlotsCopy.Exists(x => x.Card != null && x.Card.HasAbility(LongArms.ability)))
            {
                __result = false;
            }
        }
    }

    public partial class LobotomyPlugin
    {
        private void Ability_LongArms()
        {
            const string rulebookName = "Long Arms";
            LongArms.ability = AbilityHelper.New<LongArms>(pluginGuid, "sigilLongArms", rulebookName,
                "[creature] is immune to status ailments. While this card is on the board, time cannot be altered.",
                0, true)
                .SetItemRedirect("time cannot be altered", "Hourglass", GameColors.Instance.red).Id;
        }
    }
}
