using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_OldFaithAndPromise_T0997()
        {
            List<Ability> abilities = new()
            {
                Alchemist.ability
            };

            CardHelper.CreateCard(
                "wstl_oldFaithAndPromise", "Old Faith and Promise",
                "A mysterious marble. Use it without desire or expectation, and you may be rewarded.",
                0, 1, 0, 2,
                Artwork.oldFaithAndPromise, Artwork.oldFaithAndPromise_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Zayin);
        }
    }
}