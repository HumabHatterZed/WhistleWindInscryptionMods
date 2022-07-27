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
                BloodBath.specialAbility,
                SpecialTriggeredAbility.SacrificesThisTurn
            };

            List<Trait> traits = new()
            {
                Trait.Goat
            };

            CardHelper.CreateCard(
                "wstl_bloodBath", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                0, 1, 1, 0,
                Resources.bloodBath, Resources.bloodBath_emission,
                abilities: new(), specialAbilities: specialAbilities, statIcon: SpecialStatIcon.SacrificesThisTurn,
                metaCategories: new(), tribes: new(), traits: traits,
                isChoice: true, riskLevel: 2);
        }
    }
}