using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void ParasiteTree_D04108()
        {
            List<Ability> abilities = new()
            {
                Gardener.ability
            };
            CardHelper.CreateCard(
                "wstl_parasiteTree", "Parasite Tree",
                "A beautiful tree. It wants only to help you and your beasts.",
                0, 3, 1, 0,
                Resources.parasiteTree, Resources.parasiteTree_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, isDonator: true, riskLevel: 4);
        }
    }
}