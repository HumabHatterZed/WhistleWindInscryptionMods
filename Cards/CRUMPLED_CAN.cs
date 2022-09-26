using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Crumpled_Can()
        {
            CardHelper.CreateCard(
                "wstl_CRUMPLED_CAN", "Crumpled Can of WellCheers",
                "Soda can can soda dota 2 electric boo.",
                0, 1, 0, 0,
                Resources.skeleton_can, Resources.skeleton_can_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isTerrain: true, isChoice: false);
        }
    }
}