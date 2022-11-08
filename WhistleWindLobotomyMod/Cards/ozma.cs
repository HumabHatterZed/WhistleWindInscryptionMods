using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

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
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.Ruina
            };
            CardHelper.CreateCard(
                "wstl_ozma", "Ozma",
                "The former ruler of a far away land, now reduced to this.",
                atk: 2, hp: 2,
                blood: 2, bones: 0, energy: 0,
                Artwork.ozma, Artwork.ozma_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Waw,
                metaTypes: CardHelper.MetaType.Ruina);
        }
    }
}