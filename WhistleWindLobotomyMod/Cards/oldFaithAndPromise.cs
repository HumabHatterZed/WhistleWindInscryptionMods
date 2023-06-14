using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_OldFaithAndPromise_T0997()
        {
            const string oldFaithAndPromise = "oldFaithAndPromise";

            CardInfo oldFaithAndPromiseCard = NewCard(
                oldFaithAndPromise,
                "Old Faith and Promise",
                "A mysterious marble. Use it without desire or expectation and you may be rewarded.",
                attack: 0, health: 1, energy: 3)
                .SetPortraits(oldFaithAndPromise)
                .AddAbilities(Alchemist.ability)
                .AddTribes(TribeMechanical)
                .SetEvolveInfo("[name]Older Faith and Promise");

            CreateCard(oldFaithAndPromiseCard, CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
    }
}