using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Yang_O07103()
        {
            const string yang = "yang";

            CardManager.New(pluginPrefix, yang, "Yang",
                attack: 0, health: 3, "A white pendant that heals those nearby.")
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, yang)
                .SetAltPortraits(ModAssembly, "yangAlt")
                .AddAbilities(Regenerator.ability)
                .AddSpecialAbilities(Concord.specialAbility)
                .AddAppearances(AlternateBattlePortrait.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}