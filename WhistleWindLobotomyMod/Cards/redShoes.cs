using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_RedShoes_O0408()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp,
                Ability.GuardDog
            };
            CreateCard(
                "wstl_redShoes", "Red Shoes",
                "How pretty. Maybe they'll fit.",
                atk: 0, hp: 3,
                blood: 0, bones: 3, energy: 0,
                Artwork.redShoes, Artwork.redShoes_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He,
                metaTypes: CardHelper.CardMetaType.Terrain);
        }
    }
}