using DiskCardGame;
using HarmonyLib;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Bleachproof() {
            const string rulebookName = "Bleachproof";
            const string rulebookDescription = "[creature] cannot have its sigils removed by effects like the Bleach Pot.";
            Bleachproof.ID = AbnormalAbilityHelper.CreateAbility<Bleachproof>(
                "sigilBleachproof",
                rulebookName, rulebookDescription, powerLevel: 2,
                modular: false, opponent: false, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// [creature] cannot have its sigils removed by effects like the Bleach Pot.
    /// </summary>
    [HarmonyPatch]
    public class Bleachproof : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        [HarmonyPostfix, HarmonyPatch(typeof(BleachPotItem), nameof(BleachPotItem.GetValidOpponentSlots))]
        private static void RemoveImmuneCards(ref List<CardSlot> __result) {
            __result.RemoveAll(x => x.Card.HasAbility(Bleachproof.ID));
        }
    }
}
