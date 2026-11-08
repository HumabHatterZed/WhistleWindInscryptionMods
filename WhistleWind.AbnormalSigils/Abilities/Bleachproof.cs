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

        [HarmonyPrefix, HarmonyPatch(typeof(MagnificusBattleSequencer), nameof(MagnificusBattleSequencer.RemoveCardAbilities))]
        private static bool DontRemoveAbilities(PlayableCard card) {
            if (card.HasAbility(Bleachproof.ID)) {
                AudioController.Instance.PlaySound2D("magnificus_brush_splatter_bleach", MixerGroup.None, 0.5f);
                return false;
            }
            return true;
        }
    }
}
