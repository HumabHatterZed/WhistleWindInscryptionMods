using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void WhiteNight_T0346()
        {
            List<Ability> abilities = new()
            {
                TrueSaviour.ability,
                Idol.ability
            };
            List<Trait> traits = new()
            {
                Trait.Uncuttable,
                Trait.Terrain
            };
            CardHelper.CreateCard(
                "wstl_whiteNight", "WhiteNight",
                "The time has come.",
                0, 666, 0, 0,
                Resources.whiteNight, Resources.whiteNight_emission, titleTexture: Resources.whiteNight_title,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                onePerDeck: true);
        }
    }
}