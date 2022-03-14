using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
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
                Trait.Pelt
            };

            WstlUtils.Add(
                "wstl_whiteNight", "WhiteNight",
                "The time has come.",
                0, 666, 0, 0,
                Resources.whiteNight,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), traits: traits,
                emissionTexture: Resources.whiteNight_emission, onePerDeck: true,
                titleTexture: Resources.whiteNight_title);
        }
    }
}