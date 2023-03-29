using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Theresia_T0909()
        {
            List<Ability> abilities = new() { Healer.ability };
            List<Tribe> tribes = new() { TribeMechanical };

            CreateCard(
                "wstl_theresia", "Theresia",
                "An old music box. It plays a familiar melody.",
                atk: 0, hp: 2,
                blood: 0, bones: 0, energy: 2,
                Artwork.theresia, Artwork.theresia_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Teth);
        }
    }
}