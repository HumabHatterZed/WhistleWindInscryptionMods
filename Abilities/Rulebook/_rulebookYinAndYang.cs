using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_YinAndYang()
        {
            const string rulebookName = "Yin and Yang";
            const string rulebookDescription = "Transforms when adjacent to their other half.";
            const string dialogue = "femboy";
            EntryYinAndYang.ability = WstlUtils.CreateAbility<EntryYinAndYang>(
                Resources.sigilAbnormality,
                rulebookName, rulebookDescription, dialogue, 0,
                overrideModular: true).Id;
        }
    }
    public class EntryYinAndYang : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
