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
        private void Rulebook_BloodBath()
        {
            const string rulebookName = "Bloodbath";
            const string rulebookDescription = "Transforms whenever another card is sacrificed, up to three times.";
            const string dialogue = "femboy";
            EntryBloodBath.ability = AbilityHelper.CreateAbility<EntryBloodBath>(
                Artwork.sigilAbnormality, Artwork.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryBloodBath : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
