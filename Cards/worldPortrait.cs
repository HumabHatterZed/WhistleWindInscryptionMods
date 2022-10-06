using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_WorldPortrait_O0991()
        {
            List<Ability> abilities = new()
            {
               Reflector.ability
            };
            CardHelper.CreateCard(
                "wstl_worldPortrait", "Portrait of Another World",
                "This portrait captures a moment, one we're destined to lose.",
                0, 4, 1, 0,
                Resources.worldPortrait, Resources.worldPortrait_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 3);
        }
    }
}