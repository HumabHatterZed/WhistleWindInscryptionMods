using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Slots;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public class FlowerPower : VariableStatBehaviour {
        public static SpecialStatIcon icon;
        public static SpecialStatIcon Icon => icon;
        public override SpecialStatIcon IconType => icon;
        private readonly int[] statValue = new int[2] { 0, 0 };
        public override int[] GetStatValues() {
            statValue[0] = BoardManager.Instance.AllSlotsCopy.Count(x => x.GetSlotModification() == BloomingSlot.Id);
            return statValue;
        }
    }

    public partial class AbnormalPlugin {
        private void StatIcon_FlowerPower() {
            const string rulebookName = "Flower Power";
            const string rulebookDescription = "The value represented with this sigil will be equal to the number of Blooming spaces on the board.";
            FlowerPower.icon = AbilityHelper.CreateStatIcon<FlowerPower>(
                pluginGuid, "sigilFlowerPower", rulebookName, rulebookDescription, true, false)
                .SetSlotRedirect("Blooming", BloomingSlot.Id, Color.green).Id;
        }
    }
}
