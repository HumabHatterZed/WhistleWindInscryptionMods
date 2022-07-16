using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_CrumblingArmour()
        {
            const string rulebookName = "Crumbling Armour";
            const string rulebookDescription = "Kills adjacent cards if they possess the Waterborne or Loose Tail ability.";
            const string dialogue = "femboy";
            EntryCrumblingArmour.ability = AbilityHelper.CreateAbility<EntryCrumblingArmour>(
                Resources.sigilAbnormality,
                rulebookName, rulebookDescription, dialogue, 0,
                overrideModular: true).Id;
        }
    }
    public class EntryCrumblingArmour : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
