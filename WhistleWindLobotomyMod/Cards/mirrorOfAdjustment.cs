using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MirrorOfAdjustment_O0981()
        {
            const string mirrorOfAdjustment = "mirrorOfAdjustment";

            NewCard(mirrorOfAdjustment, "The Mirror of Adjustment", "A mirror that reflects nothing on its surface.",
                attack: 0, health: 1, blood: 1)
                .SetPortraits(mirrorOfAdjustment)
                .AddAbilities(Woodcutter.ability)
                .SetStatIcon(SpecialStatIcon.Mirror)
                .SetTerrain(false)
                .SetDefaultEvolutionName("The Grand Mirror of Adjustment")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
    }
}