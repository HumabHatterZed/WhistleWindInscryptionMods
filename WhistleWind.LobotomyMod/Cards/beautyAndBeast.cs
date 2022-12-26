using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
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
            LobotomyCardHelper.CreateCard(
                "wstl_beautyAndBeast", "Beauty and the Beast",
                "A pitiable creature. Death would be a mercy for it.",
                atk: 1, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.beautyAndBeast, Artwork.beautyAndBeast_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Teth);
        }
    }
}