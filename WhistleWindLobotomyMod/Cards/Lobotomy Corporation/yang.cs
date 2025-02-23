using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string yang = "wstl_yang";
        private static void Yang_O07103()
        {
            string textureName = "yang";
            CardManager.New(LobotomyPlugin.pluginPrefix, yang, "Yang",
                attack: 0, health: 3, "A white pendant that heals those nearby.")
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetAltPortraits(LobotomyPlugin.ModAssembly, "yangAlt")
                .AddAbilities(Regenerator.ability)
                .AddSpecialAbilities(Concord.specialAbility)
                .AddAppearances(AlternateBattlePortrait.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}