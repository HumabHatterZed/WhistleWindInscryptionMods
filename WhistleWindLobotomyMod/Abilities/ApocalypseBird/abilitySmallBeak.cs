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
    public class SmallBeak : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_SmallBeak()
        {
            const string rulebookName = "Small Beak";
            SmallBeak.ability = LobotomyAbilityHelper.CreateAbility<SmallBeak>(
                "sigilSmallBeak", rulebookName,
                "While this card is on the board, all creatures take twice as much damage.",
                null, powerLevel: 0,
                canStack: false).Id;
        }
    }
}
