using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_DerFreischutz()
        {
            const string rulebookName = "Der Freischütz";
            const string rulebookDescription = "After attacking six times, attacks a random ally slot.";
            const string dialogue = "femboy";
            EntryDerFreischutz.ability = AbilityHelper.CreateAbility<EntryDerFreischutz>(
                Artwork.sigilAbnormality, Artwork.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryDerFreischutz : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
