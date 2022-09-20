using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void ArmyInPink_D01106()
        {
            List<Ability> abilities = new()
            {
                Protector.ability,
                Ability.MoveBeside
            };

            CardHelper.CreateCard(
                "wstl_armyInPink", "Army in Pink",
                "A friendly soldier the colour of the human heart. It will protect you wherever you go.",
                3, 3, 2, 0,
                Resources.armyInPink, Resources.armyInPink_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isRare: true, isDonator: true, riskLevel: 5);
        }
    }
}