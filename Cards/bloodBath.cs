using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void BloodBath_T0551()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                BloodBath.specialAbility
            };

            List<Trait> traits = new()
            {
                Trait.Goat
            };

            WstlUtils.Add(
                "wstl_bloodBath", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                0, 3, 1, 0,
                Resources.bloodBath, Resources.bloodBath_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                isChoice: true);
        }
    }
}