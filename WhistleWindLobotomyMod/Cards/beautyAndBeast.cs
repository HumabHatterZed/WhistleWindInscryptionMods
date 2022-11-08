using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BeautyAndBeast_O0244()
        {
            List<Ability> abilities = new()
            {
                Cursed.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Hooved,
                Tribe.Insect
            };
            CardHelper.CreateCard(
                "wstl_beautyAndBeast", "Beauty and the Beast",
                "A pitiable creature. Death would be a mercy for it.",
                atk: 1, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.beautyAndBeast, Artwork.beautyAndBeast_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}