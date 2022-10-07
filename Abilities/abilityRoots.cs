using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Roots()
        {
            const string rulebookName = "Roots";
            const string rulebookDescription = "When this card is played, create Thorny Vines on adjacent empty spaces. A Thorny Vine is defined as: 0 Power, 1 Health.";
            const string dialogue = "Resentment bursts forth like a weed.";
            Roots.ability = AbilityHelper.CreateAbility<Roots>(
                Resources.sigilRoots, Resources.sigilRoots_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: true, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Roots : CreateCardsAdjacent
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override string SpawnedCardId => "wstl_snowWhitesVine";
        public override string CannotSpawnDialogue => "Not enough space for the vines to grow.";
    }
}
