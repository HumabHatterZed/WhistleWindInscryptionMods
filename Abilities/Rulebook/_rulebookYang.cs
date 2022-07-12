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
            EntryYang.ability = WstlUtils.CreateAbility<EntryYang>(
                Resources.sigilAbnormality,
                rulebookName, rulebookDescription, dialogue, 0,
                overrideModular: true).Id;
        }
    }
    public class EntryYang : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
