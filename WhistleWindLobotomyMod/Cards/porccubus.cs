using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Porccubus_O0298()
        {
            List<Ability> abilities = new()
            {
                Ability.Deathtouch
            };
            CreateCard(
                "wstl_porccubus", "Porccubus",
                "A prick from one of its quills creates a deadly euphoria.",
                atk: 1, hp: 1,
                blood: 0, bones: 5, energy: 0,
                Artwork.porccubus, Artwork.porccubus_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He,
                customTribe: TribePlant);
        }
    }
}