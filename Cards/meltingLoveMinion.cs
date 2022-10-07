using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_MeltingLoveMinion_D03109()
        {
            List<Ability> abilities = new()
            {
                Slime.ability
            };

            CardHelper.CreateCard(
                "wstl_meltingLoveMinion", "Slime",
                "Don't let your beasts get too close now.",
                0, 0, 0, 0,
                Artwork.meltingLoveMinion, Artwork.meltingLoveMinion_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}
