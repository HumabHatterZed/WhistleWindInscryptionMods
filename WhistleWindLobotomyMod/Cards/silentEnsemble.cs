using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SilentEnsemble_T0131()
        {
            List<Ability> abilities = new() { Ability.BuffNeighbours };
            List<Tribe> tribes = new() { TribeAnthropoid };

            CreateCard(
                "wstl_silentEnsemble", "Chairs",
                "",
                atk: 0, hp: 3,
                blood: 0, bones: 0, energy: 0,
                Artwork.silentEnsemble, Artwork.silentEnsemble_emission,
                abilities: abilities, specialAbilities: new(),
                tribes: tribes);
        }
    }
}