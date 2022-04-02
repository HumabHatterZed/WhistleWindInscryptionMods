using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_MountainOfBodies()
        {
            const string rulebookName = "Mountain of Smiling Bodies";
            const string rulebookDescription = "Transforms whenever it kills a card, up to two times. When killed after transforming, returns to a previous forme.";
            const string dialogue = "femboy";
            EntryMountainOfBodies.ability = WstlUtils.CreateAbility<EntryMountainOfBodies>(
                Resources.sigilAbnormality,
                rulebookName, rulebookDescription, dialogue, 0,
                overrideModular: true).Id;
        }
    }
    public class EntryMountainOfBodies : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
