using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string censored = "wstl_censored";
        public const string censoredMinion = "wstl_censoredMinion";
        private static void CENSORED_O0389() {
            string name = "CENSORED";
            string textureName = "censored";
            string textureName2 = "censoredMinion";
            CardManager.New(LobotomyPlugin.pluginPrefix, censored, name,
                attack: 4, health: 4, "It's best you never learn what it looks like.")
                .SetBloodCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Bloodfiend.ability)
                .AddSpecialAbilities(CensoredSpecial.specialAbility)
                .SetDefaultEvolutionName("CENSORED CENSORED")
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph, true);

            CardManager.New(LobotomyPlugin.pluginPrefix, censoredMinion, name,
                attack: 0, health: 0)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .SetDefaultEvolutionName("CENSORED CENSORED")
                .Build();
        }
    }
}