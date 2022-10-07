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
        private void Rulebook_BlueStar()
        {
            const string rulebookName = "Blue Star";
            const string rulebookDescription = "Strikes all opposing slots directly when no opposing cards can block a direct attack.";
            const string dialogue = "femboy";
            EntryBlueStar.ability = AbilityHelper.CreateAbility<EntryBlueStar>(
                Artwork.sigilAbnormality, Artwork.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryBlueStar : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
