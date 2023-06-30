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
        private void Card_OneSin_O0303()
        {
            CardInfo oneSinCard = NewCard(
                oneSin,
                oneSinName,
                "A floating skull. Its hollow sockets see through you.",
                attack: 0, health: 1, bones: 1, temple: CardTemple.Undead)
                .SetPortraits(oneSin)
                .AddAbilities(Martyr.ability)
                .AddTribes(TribeDivine)
                .SetEvolveInfo("{0}");

            CreateCard(oneSinCard, CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
    }
}