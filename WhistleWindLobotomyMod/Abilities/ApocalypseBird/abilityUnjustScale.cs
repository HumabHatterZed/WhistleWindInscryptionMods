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
    public class UnjustScale : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_UnjustScale()
        {
            const string rulebookName = "Unjust Scale";
            UnjustScale.ability = LobotomyAbilityHelper.CreateAbility<UnjustScale>(
                "sigilUnjustScale", rulebookName,
                "At the end of the owner's turn, inflict 2 Sin on all other cards. At the start of the owner's turn, kill all other cards with Sin.",
                null, powerLevel: 0,
                canStack: false).Id;
        }
    }
}
