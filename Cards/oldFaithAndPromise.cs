using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void OldFaithAndPromise_T0997()
        {
            List<Ability> abilities = new()
            {
                Alchemist.ability
            };
            CardHelper.CreateCard(
                "wstl_oldFaithAndPromise", "Old Faith and Promise",
                "A mysterious marble. Use it without desire or expectation, and you may be rewarded.",
                0, 1, 0, 2,
                Resources.oldFaithAndPromise, Resources.oldFaithAndPromise_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 1);
        }
    }
}