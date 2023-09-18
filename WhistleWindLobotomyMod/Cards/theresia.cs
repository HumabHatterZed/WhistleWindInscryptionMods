using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Theresia_T0909()
        {
            const string theresia = "theresia";

            NewCard(theresia, "Theresia", "An old music box. It plays a familiar melody.",
                attack: 0, health: 2, energy: 2)
                .SetPortraits(theresia)
                .AddAbilities(Healer.ability)
                .AddTribes(TribeMechanical)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}