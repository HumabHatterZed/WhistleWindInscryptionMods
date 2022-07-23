using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void PlagueDoctor_O0145()
        {
            List<Ability> abilities = new()
            {
                Ability.Flying,
                Healer.ability
            };
            CardHelper.CreateCard(
                "wstl_plagueDoctor", "Plague Doctor",
                "A worker of miracles.",
                0, 3, 0, 3,
                Resources.plagueDoctor, Resources.plagueDoctor_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                onePerDeck: true, riskLevel: 1);
        }
    }
}