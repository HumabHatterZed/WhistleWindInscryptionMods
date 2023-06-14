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
        private void Card_OldLady_O0112()
        {
            const string oldLady = "oldLady";

            CardInfo oldLadyCard = NewCard(
                oldLady,
                "Old Lady",
                "An aged storyteller. She can tell you any tale, even those that can't exist.",
                attack: 1, health: 2, bones: 2)
                .SetPortraits(oldLady)
                .AddAbilities(Ability.DebuffEnemy)
                .AddTribes(TribeAnthropoid)
                .SetEvolveInfo("[name]Elderly Lady");

            CreateCard(oldLadyCard, CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}