using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
            CardHelper.CreateCard(
                "wstl_magicalGirlClover", "Magical Girl",
                "Blind protector of another world.",
                2, 4, 1, 0,
                Resources.servantOfWrath, Resources.servantOfWrath_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                isChoice: false, onePerDeck: true);
        }
    }
}