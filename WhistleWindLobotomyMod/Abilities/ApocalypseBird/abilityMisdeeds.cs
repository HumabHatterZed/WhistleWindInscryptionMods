using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class Misdeeds : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_Misdeeds()
        {
            const string rulebookName = "Misdeeds Not Allowed";
            Misdeeds.ability = LobotomyAbilityHelper.CreateAbility<Misdeeds>(
                "sigilMisdeeds", rulebookName,
                "When this card takes damage, gain 2 Power until the end of the owner's turn.",
                null, powerLevel: 0,
                canStack: false).Id;
        }
    }
}
