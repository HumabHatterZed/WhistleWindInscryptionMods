using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void MagicalGirlDiamond_O0164()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                SpecialAbilityFledgling.specialAbility
            };

            CardHelper.CreateCard(
                "wstl_magicalGirlDiamond", "Magical Girl",
                "Greed hardened into golden amber, trapping the happiness inside.",
                0, 3, 2, 0,
                Resources.magicalGirlDiamond, Resources.magicalGirlDiamond_emission, gbcTexture: Resources.magicalGirlDiamond_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, evolveName: "wstl_kingOfGreed", onePerDeck: true, riskLevel: 4);
        }
    }
}