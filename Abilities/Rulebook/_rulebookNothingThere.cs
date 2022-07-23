using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_NothingThere()
        {
            const string rulebookName = "Nothing There";
            const string rulebookDescription = "Disguises as past challengers. Reveals itself on death.";
            const string dialogue = "femboy";
            EntryNothingThere.ability = AbilityHelper.CreateAbility<EntryNothingThere>(
                Resources.sigilAbnormality,// Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                overrideModular: true).Id;
        }
    }
    public class EntryNothingThere : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
