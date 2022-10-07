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
        private void Rulebook_GiantTreeSap()
        {
            const string rulebookName = "Giant Tree Sap";
            const string rulebookDescription = "When sacrificed, has a chance to cause the sacrificing card to explode.";
            const string dialogue = "femboy";
            EntryGiantTreeSap.ability = AbilityHelper.CreateAbility<EntryGiantTreeSap>(
                Artwork.sigilAbnormality, Artwork.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryGiantTreeSap : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
