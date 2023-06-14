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
        private void Card_Schadenfreude_O0576()
        {
            const string schadenfreude = "schadenfreude";

            CardInfo schadenfreudeCard = NewCard(
                schadenfreude,
                "Schadenfreude",
                "A strange machine. You can feel someone's persistent gaze through the keyhole.",
                attack: 1, health: 1, energy: 3)
                .SetPortraits(schadenfreude)
                .AddAbilities(Ability.Sentry)
                .AddTribes(TribeMechanical)
                .SetEvolveInfo("Große {0}");

            CreateCard(schadenfreudeCard, CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}