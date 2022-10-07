using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_SilentEnsemble_T0131()
        {
            List<Ability> abilities = new()
            {
                Ability.BuffNeighbours
            };

            CardHelper.CreateCard(
                "wstl_silentEnsemble", "Chairs",
                "The conductor begins to direct the apocalypse.",
                1, 3, 0, 0,
                Artwork.silentEnsemble, Artwork.silentEnsemble_emission,
                abilities: abilities, specialAbilities: new(),
                tribes: new());
        }
    }
}