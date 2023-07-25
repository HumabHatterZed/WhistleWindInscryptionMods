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
        private void Card_Alriune_T0453()
        {
            const string alriune = "alriune";

            NewCard(alriune, "Alriune", "A doll yearning to be a human. A human yearning to be a doll.",
                attack: 4, health: 5, blood: 3)
                .SetPortraits(alriune)
                .AddAbilities(Ability.Strafe)
                .AddTribes(TribeBotanic, Tribe.Hooved)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}