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
        private void Card_DontTouchMe_O0547()
        {
            const string dontTouchMe = "dontTouchMe";

            CardInfo card = NewCard(
                dontTouchMe,
                "Don't Touch Me",
                "Don't touch it.",
                attack: 0, health: 1, energy: 2)
                .SetPortraits(dontTouchMe)
                .AddAbilities(Punisher.ability)
                .AddTribes(TribeMechanical)
                .SetTerrain()
                .SetEvolveInfo("Please {0}");

            CreateCard(card, CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
    }
}