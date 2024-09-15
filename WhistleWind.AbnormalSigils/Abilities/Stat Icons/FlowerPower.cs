using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Slots;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class FlowerPower : VariableStatBehaviour
    {
        public static SpecialStatIcon icon;
        public static SpecialStatIcon Icon => icon;
        public override SpecialStatIcon IconType => icon;
        public override int[] GetStatValues()
        {
            int num = 0;
            foreach (CardSlot slot in BoardManager.Instance.AllSlotsCopy)
            {
                if (slot.Card != null && slot.Card.HasTrait(AbnormalPlugin.BloomingFlower))
                    num++;

                if (slot.GetSlotModification() == BloomingSlot.Id)
                    num++;
            }
            return new int[2] { num, 0 };
        }
    }

    public partial class AbnormalPlugin
    {
        private void StatIcon_FlowerPower()
        {
            const string rulebookName = "Flower Power";
            const string rulebookDescription = "The value represented with this sigil will be equal to number of Flower cards and Blooming spaces on the board.";
            FlowerPower.icon = AbilityHelper.CreateStatIcon<FlowerPower>(
                pluginGuid, "sigilFlowerPower", rulebookName, rulebookDescription, true, false).Id;
        }
    }
}
