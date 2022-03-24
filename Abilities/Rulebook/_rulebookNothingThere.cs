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
            EntryNothingThere.ability = WstlUtils.CreateAbility<EntryNothingThere>(
                Resources.sigilAbnormality,
                rulebookName, rulebookDescription, dialogue, 0,
                overrideModular: true).Id;
        }
    }
    public class EntryNothingThere : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
