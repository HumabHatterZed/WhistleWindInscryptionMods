using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_SilentOrchestra_T0131()
        {
            List<Ability> abilities = new()
            {
                Conductor.ability
            };

            CardHelper.CreateCard(
                "wstl_silentOrchestra", "The Silent Orchestra",
                "A conductor of the apocalypse.",
                1, 5, 3, 0,
                Artwork.silentOrchestra, Artwork.silentOrchestra_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Rare, riskLevel: CardHelper.RiskLevel.Aleph);
        }
    }
}