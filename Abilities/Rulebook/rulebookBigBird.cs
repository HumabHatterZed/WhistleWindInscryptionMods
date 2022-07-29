using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_BigBird()
        {
            const string rulebookName = "Big bird";
            const string rulebookDescription = "Transforms when Punishing Bird and Judgement Bird are played on the same side of the board.";
            const string dialogue = "femboy";
            EntryBigBird.ability = AbilityHelper.CreateAbility<EntryBigBird>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                overrideModular: true).Id;
        }
    }
    public class EntryBigBird : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
