using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string youMustBeHappy = "wstl_youMustBeHappy";
        private static void YouMustBeHappy_T0994() {
            string name = "You Must Be Happy";
            string name2 = "You Must Be Happier";
            string desc = "Those that undergo the procedure find themselves rested and healthy again.";
            string textureName = "youMustBeHappy";
            CardManager.New(LobotomyPlugin.pluginPrefix, youMustBeHappy, name,
                attack: 0, health: 2, desc)
                .SetEnergyCost(2)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Scrambler.ability)
                .SetSpellType(SpellType.TargetedStats)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 2, desc)
                .SetEnergyCost(2)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Scrambler.ability)
                .SetSpellType(SpellType.TargetedStats)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}