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
        private void Rulebook_NamelessFetus()
        {
            const string rulebookName = "Nameless Fetus";
            const string rulebookDescription = "Transforms when sacrificed six times.";
            const string dialogue = "femboy";
            EntryNamelessFetus.ability = AbilityHelper.CreateAbility<EntryNamelessFetus>(
                Artwork.sigilAbnormality, Artwork.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryNamelessFetus : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
