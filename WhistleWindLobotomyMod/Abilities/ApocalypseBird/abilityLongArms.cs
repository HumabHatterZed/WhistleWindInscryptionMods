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
    public class LongArms : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_LongArms()
        {
            const string rulebookName = "Long Arms";
            LongArms.ability = LobotomyAbilityHelper.CreateAbility<LongArms>(
                "sigilLongArms", rulebookName,
                "This card is immune to status ailments.",
                null, powerLevel: 0,
                canStack: false).Id;
        }
    }
}
