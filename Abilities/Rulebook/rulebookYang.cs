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
        private void Rulebook_Yang()
        {
            const string rulebookName = "Yang";
            const string rulebookDescription = "When adjacent to Yin, wipes the board then removes either Yin or itself from the deck at random.";
            const string dialogue = "femboy";
            EntryYang.ability = AbilityHelper.CreateAbility<EntryYang>(
                Artwork.sigilAbnormality, Artwork.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryYang : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
