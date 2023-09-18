using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Laetitia_O0167()
        {
            const string laetitia = "laetitia";

            NewCard(laetitia, "Laetitia", "A little witch carrying a heart-shaped gift.",
                attack: 1, health: 2, blood: 1)
                .SetPortraits(laetitia)
                .AddAbilities(GiftGiver.ability)
                .AddTribes(TribeFae)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}