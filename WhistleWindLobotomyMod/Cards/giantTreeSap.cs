using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_GiantTreeSap_T0980()
        {
            const string giantTreeSap = "giantTreeSap";

            NewCard(giantTreeSap, "Giant Tree Sap", "Sap from a tree at the end of the world. It is a potent healing agent.",
                attack: 0, health: 3, bones: 3)
                .SetPortraits(giantTreeSap)
                .AddAbilities(Ability.Sacrificial, Ability.Morsel)
                .AddSpecialAbilities(Sap.specialAbility)
                .AddTribes(TribeBotanic)
                .SetDefaultEvolutionName("Giant Elder Tree Sap")
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.He);
        }
    }
}