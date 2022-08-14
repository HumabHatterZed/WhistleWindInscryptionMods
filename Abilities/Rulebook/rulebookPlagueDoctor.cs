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
            const string rulebookDescription = "Changes appearance upon healing cards. Will heal opposing cards if no allies can be healed.";
            const string dialogue = "femboy";
            EntryPlagueDoctor.ability = AbilityHelper.CreateAbility<EntryPlagueDoctor>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryPlagueDoctor : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
