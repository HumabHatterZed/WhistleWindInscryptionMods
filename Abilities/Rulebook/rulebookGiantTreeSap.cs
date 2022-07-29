using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_GiantTreeSap()
        {
            const string rulebookName = "Giant Tree Sap";
            const string rulebookDescription = "Has a chance to cause the sacrificed card to explode when sacrificed.";
            const string dialogue = "femboy";
            EntryGiantTreeSap.ability = AbilityHelper.CreateAbility<EntryGiantTreeSap>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                overrideModular: true).Id;
        }
    }
    public class EntryGiantTreeSap : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
