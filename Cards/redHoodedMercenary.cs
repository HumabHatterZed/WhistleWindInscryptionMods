using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_RedHoodedMercenary_F0157()
        {
            List<Ability> abilities = new()
            {
                Marksman.ability,
                BitterEnemies.ability
            };

            CardHelper.CreateCard(
                "wstl_redHoodedMercenary", "Little Red Riding Hooded Mercenary",
                "A skilled mercenary with a bloody vendetta. Perhaps you can help her sate it.",
                2, 3, 2, 0,
                Artwork.redHoodedMercenary, Artwork.redHoodedMercenary_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}