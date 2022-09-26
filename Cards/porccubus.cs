using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Porccubus_O0298()
        {
            List<Ability> abilities = new()
            {
                Ability.Deathtouch
            };
            List<Trait> traits = new()
            {
                Trait.KillsSurvivors
            };
            CardHelper.CreateCard(
                "wstl_porccubus", "Porccubus",
                "A prick from one of its quills creates a deadly euphoria.",
                1, 2, 0, 5,
                Resources.porccubus, Resources.porccubus_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                isChoice: true, riskLevel: 3);
        }
    }
}