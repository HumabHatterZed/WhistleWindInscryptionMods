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
        private void Rulebook_BigBird()
        {
            const string rulebookName = "Big Bird";
            const string rulebookDescription = "Transforms when Punishing Bird and Judgement Bird are played on the same side of the board.";
            const string dialogue = "femboy";
            EntryBigBird.ability = AbilityHelper.CreateAbility<EntryBigBird>(
                Artwork.sigilAbnormality, Artwork.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryBigBird : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
