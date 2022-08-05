using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_PlagueDoctor()
        {
            const string rulebookName = "Plague Doctor";
            const string rulebookDescription = "Changes appearance on healing cards.";
            const string dialogue = "femboy";
            EntryPlagueDoctor.ability = AbilityHelper.CreateAbility<EntryPlagueDoctor>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                overrideModular: true).Id;
        }
    }
    public class EntryPlagueDoctor : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
