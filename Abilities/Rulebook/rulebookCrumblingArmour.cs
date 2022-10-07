using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_CrumblingArmour()
        {
            const string rulebookName = "Crumbling Armour";
            const string rulebookDescription = "Kills adjacent cards if they possess the Waterborne or Loose Tail sigils.";
            const string dialogue = "femboy";
            EntryCrumblingArmour.ability = AbilityHelper.CreateAbility<EntryCrumblingArmour>(
                Artwork.sigilAbnormality, Artwork.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryCrumblingArmour : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
