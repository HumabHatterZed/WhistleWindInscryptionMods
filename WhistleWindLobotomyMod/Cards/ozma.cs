using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Ozma_F04116()
        {
            List<Ability> abilities = new()
            {
                RightfulHeir.ability
            };
            CreateCard(
                "wstl_ozma", "Ozma",
                "The former ruler of a far away land, now reduced to this.",
                atk: 1, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.ozma, Artwork.ozma_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw,
                modTypes: ModCardType.Ruina, customTribe: TribeFae);
        }
    }
}