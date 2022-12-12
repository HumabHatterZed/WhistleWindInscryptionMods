using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_OldFaithAndPromise_T0997()
        {
            List<Ability> abilities = new()
            {
                Alchemist.ability
            };
            LobotomyCardHelper.CreateCard(
                "wstl_oldFaithAndPromise", "Old Faith and Promise",
                "A mysterious marble. Use it without desire or expectation, and you may be rewarded.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 3,
                Artwork.oldFaithAndPromise, Artwork.oldFaithAndPromise_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Zayin,
                customTribe: TribeMachine);
        }
    }
}