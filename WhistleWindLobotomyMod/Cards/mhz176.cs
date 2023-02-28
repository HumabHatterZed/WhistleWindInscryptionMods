using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MHz176_T0727()
        {
            List<Ability> abilities = new()
            {
                Ability.BuffNeighbours,
                Ability.BuffEnemy
            };

            CreateCard(
                "wstl_mhz176", "1.76 MHz",
                "This is a record, a record of a day we must never forget.",
                atk: 2, hp: 1,
                blood: 0, bones: 0, energy: 3,
                Artwork.mhz176, Artwork.mhz176_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Teth,
                evolveName: "{0}", customTribe: TribeMachine);
        }
    }
}