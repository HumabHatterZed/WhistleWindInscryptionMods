using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_DerFreischutz()
        {
            const string rulebookName = "Der Freischütz";
            const string rulebookDescription = "After attacking six times, fires a seventh time at a random ally slot.";
            const string dialogue = "femboy";
            EntryDerFreischutz.ability = AbilityHelper.CreateAbility<EntryDerFreischutz>(
                Resources.sigilAbnormality,
                rulebookName, rulebookDescription, dialogue, 0,
                overrideModular: true).Id;
        }
    }
    public class EntryDerFreischutz : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
