using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_NamelessFetus()
        {
            const string rulebookName = "Nameless Fetus";
            const string rulebookDescription = "Transforms when sacrificed six times.";
            const string dialogue = "femboy";
            EntryNamelessFetus.ability = AbilityHelper.CreateAbility<EntryNamelessFetus>(
                Resources.sigilAbnormality,
                rulebookName, rulebookDescription, dialogue, 0,
                overrideModular: true).Id;
        }
    }
    public class EntryNamelessFetus : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
