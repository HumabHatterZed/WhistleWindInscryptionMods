using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

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
                Resources.sigilAbnormality,
                rulebookName, rulebookDescription, dialogue, 0,
                overrideModular: true).Id;
        }
    }
    public class EntryArmyInPink : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
