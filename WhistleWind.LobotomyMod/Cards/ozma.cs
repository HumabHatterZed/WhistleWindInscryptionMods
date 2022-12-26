using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Ozma_F04116()
        {
            List<Ability> abilities = new()
            {
                RightfulHeir.ability
            };
            LobotomyCardHelper.CreateCard(
                "wstl_ozma", "Ozma",
                "The former ruler of a far away land, now reduced to this.",
                atk: 1, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.ozma, Artwork.ozma_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Waw,
                modTypes: LobotomyCardHelper.ModCardType.Ruina, customTribe: TribeFae);
        }
    }
}