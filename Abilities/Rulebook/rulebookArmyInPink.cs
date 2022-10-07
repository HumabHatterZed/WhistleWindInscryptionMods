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
        private void Rulebook_ArmyInPink()
        {
            const string rulebookName = "Army in Pink";
            const string rulebookDescription = "Transforms when an adjacent card dies.";
            const string dialogue = "femboy";
            EntryArmyInPink.ability = AbilityHelper.CreateAbility<EntryArmyInPink>(
                Artwork.sigilAbnormality, Artwork.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryArmyInPink : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
