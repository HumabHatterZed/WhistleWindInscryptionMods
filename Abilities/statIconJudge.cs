using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void StatIcon_Judge()
        {
            const string name = "Judge";
            const string description = "Cards killed by this card die regardless of their health.";
            Judge.specialStatIcon = AbilityHelper.CreateStatIcon<Judge>(name, description,
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel).Id;
        }
    }
    public class Judge : VariableStatBehaviour
    {
        public static SpecialStatIcon specialStatIcon;
        public override SpecialStatIcon IconType => specialStatIcon;
        public override int[] GetStatValues()
        {
            int[] array = new int[2];
            array[1] = 1;
            return array;
        }
    }
}
