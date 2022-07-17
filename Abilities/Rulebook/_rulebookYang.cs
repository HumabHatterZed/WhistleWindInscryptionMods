using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_Yang()
        {
            const string rulebookName = "Yang";
            const string rulebookDescription = "Transforms when adjacent to their other half.";
            const string dialogue = "femboy";
            EntryYang.ability = AbilityHelper.CreateAbility<EntryYang>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                overrideModular: true).Id;
        }
    }
    public class EntryYang : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
