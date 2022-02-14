using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BloodBath_T0551()
        {
            List<SpecialAbilityIdentifier> specialAbilities = new()
            {
                BloodBath.GetSpecialAbilityId
            };

            List<Trait> traits = new()
            {
                Trait.Goat
            };

            WstlUtils.Add(
                "wstl_bloodBath", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                0, 3, 1, 0,
                Resources.bloodBath,
                new List<Ability>(), specialAbilities: specialAbilities,
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                traits: traits, emissionTexture: Resources.bloodBath_emission);
        }
    }
}