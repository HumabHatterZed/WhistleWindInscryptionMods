using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void WhiteNight_T0346()
        {
            List<Ability> abilities = new List<Ability>
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
                666, 0, 0, 0,
                Resources.whiteNight,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), traits: traits,
                emissionTexture: Resources.whiteNight_emission, titleTexture: Resources.whiteNight_title);
        }
    }
}