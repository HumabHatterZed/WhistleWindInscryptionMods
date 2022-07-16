using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_TodaysShyLook()
        {
            const string rulebookName = "Today's Shy Look";
            const string rulebookDescription = "Changes forme when drawn.";
            const string dialogue = "femboy";
            EntryTodaysShyLook.ability = AbilityHelper.CreateAbility<EntryTodaysShyLook>(
                Resources.sigilAbnormality,
                rulebookName, rulebookDescription, dialogue, 0,
                overrideModular: true).Id;
        }
    }
    public class EntryTodaysShyLook : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
