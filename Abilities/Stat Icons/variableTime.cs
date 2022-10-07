using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void StatIcon_Time()
        {
            const string rulebookName = "Passing Time";
            const string rulebookDescription = "The value represented by this sigil will be equal to the number of turns that have passed since this card resolved on the board.";
            Time.specialAbility = AbilityHelper.CreateSpecialAbility<Time>(rulebookName, rulebookDescription).Id;
            Time.icon = AbilityHelper.CreateStatIcon<Time>(
                rulebookName, rulebookDescription, Artwork.sigilTime, Artwork.sigilTime_pixel,
                true, true).Id;
        }
    }
    public class Time : VariableStatBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public static SpecialStatIcon icon;
        public static SpecialStatIcon Icon => icon;
        public override SpecialStatIcon IconType => icon;
        private int turns;
        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return base.PlayableCard.OpponentCard != playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            turns += 1;
            return base.OnUpkeep(playerUpkeep);
        }

        public override int[] GetStatValues()
        {
            return new int[2] {turns, turns};
        }
    }
}
