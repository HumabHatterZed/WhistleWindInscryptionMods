using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SilentEnsemble_T0131()
        {
            List<Ability> abilities = new()
            {
                Ability.BuffNeighbours
            };

            LobotomyCardHelper.CreateCard(
                "wstl_silentEnsemble", "Chairs",
                "The conductor begins to direct the apocalypse.",
                atk: 1, hp: 3,
                blood: 0, bones: 0, energy: 0,
                Artwork.silentEnsemble, Artwork.silentEnsemble_emission,
                abilities: abilities, specialAbilities: new(),
                tribes: new());
        }
    }
}