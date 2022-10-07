using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_GraveOfBlossoms_O04100()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp,
                Bloodfiend.ability
            };

            CardHelper.CreateCard(
                "wstl_graveOfBlossoms", "Grave of Cherry Blossoms",
                "A blooming cherry tree. The more blood it has, the more beautiful it becomes.",
                0, 3, 1, 0,
                Artwork.graveOfBlossoms, Artwork.graveOfBlossoms_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}