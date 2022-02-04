using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BloodBath_T0551()
        {
            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
            {
                BloodBath.GetSpecialAbilityId
            };

            List<Trait> traits = new List<Trait>
            {
                Trait.Goat
            };

            WstlUtils.Add(
                "wstl_bloodBath", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                3, 0, 1, 0,
                Resources.bloodBath,
                new List<Ability>(), specialAbilities: specialAbilities,
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                traits: traits, emissionTexture: Resources.bloodBath_emission);
        }
    }
}