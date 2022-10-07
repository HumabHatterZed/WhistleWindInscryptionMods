using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_GiantTreeSap_T0980()
        {
            List<Ability> abilities = new()
            {
                Ability.Morsel,
                Ability.Sacrificial
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                Sap.specialAbility
            };

            CardHelper.CreateCard(
                "wstl_giantTreeSap", "Giant Tree Sap",
                "Sap from a tree at the end of the world. It is a potent healing agent.",
                0, 3, 0, 4,
                Artwork.giantTreeSap, Artwork.giantTreeSap_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}