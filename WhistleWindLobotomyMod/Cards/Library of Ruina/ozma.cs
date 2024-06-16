using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Ozma_F04116()
        {
            const string ozma = "ozma";

            NewCard(ozma, "Ozma", "The former ruler of a far away land, now reduced to this.",
                attack: 1, health: 2, blood: 1, temple: CardTemple.Wizard)
                .SetPortraits(ozma)
                .AddAbilities(RightfulHeir.ability)
                .AddTribes(TribeFae)
                .AddTraits(EmeraldCity)
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw, ModCardType.Ruina);
        }
    }
}