using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_BloodBath()
        {
            const string rulebookName = "Bloodbath";
            const string rulebookDescription = "Transforms whenever another card is sacrificed, up to three times.";
            const string dialogue = "femboy";
            EntryBloodBath.ability = AbilityHelper.CreateAbility<EntryBloodBath>(
                Resources.sigilAbnormality,// Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                overrideModular: true).Id;
        }
    }
    public class EntryBloodBath : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
