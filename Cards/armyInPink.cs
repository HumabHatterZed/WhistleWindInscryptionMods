using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ArmyInPink_D01106()
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
                Artwork.armyInPink, Artwork.armyInPink_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isDonator: true,
                choiceType: CardHelper.ChoiceType.Rare, riskLevel: CardHelper.RiskLevel.Aleph);
        }
    }
}