using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_SeeMore()
        {
            const string rulebookName = "Status Effect Overflow";
            const string rulebookDescription = "If a card has more than 5 status effects, display the overflow here.";
            SeeMore.ability = AbnormalAbilityHelper.CreateAbility<SeeMore>(
                "sigilSeeMore",
                rulebookName, rulebookDescription, unobtainable: true).Id;
        }
    }
    public class SeeMore : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}