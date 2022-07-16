using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SilentEnsemble_T0131()
        {
            List<Ability> abilities = new()
            {
                Ability.BuffNeighbours
            };
            CardHelper.CreateCard(
                "wstl_silentEnsemble", "Chairs",
                "The conductor begins to direct the apocalypse.",
                0, 2, 0, 0,
                Resources.silentEnsemble, Resources.silentEnsemble_emission,
                abilities: abilities, specialAbilities: new(),
                tribes: new(), riskLevel: 3);
        }
    }
}