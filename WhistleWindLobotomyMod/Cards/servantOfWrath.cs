using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ServantOfWrath_O01111()
        {
            List<Ability> abilities = new()
            {
                Ability.DoubleStrike,
                Burning.ability,
                Burning.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Reptile
            };
            LobotomyCardHelper.CreateCard(
                "wstl_magicalGirlClover", "Magical Girl",
                "Blind protector of another world.",
                atk: 2, hp: 4,
                blood: 1, bones: 0, energy: 0,
                Artwork.servantOfWrath, Artwork.servantOfWrath_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                onePerDeck: true);
        }
    }
}