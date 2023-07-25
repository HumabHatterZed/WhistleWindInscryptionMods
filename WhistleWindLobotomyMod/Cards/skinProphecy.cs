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
        private void Card_SkinProphecy_T0990()
        {
            const string skinProphecy = "skinProphecy";

            NewCard(skinProphecy, "Skin Prophecy", "A holy book. Its believers wrapped it in skin to preserve its sanctity.",
                attack: 0, health: 2, blood: 1)
                .SetPortraits(skinProphecy)
                .AddAbilities(Witness.ability)
                .AddTribes(TribeDivine)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}