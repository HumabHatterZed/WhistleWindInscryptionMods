using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 1/4 Split Strike, Piercing
        /// </summary>
        public const string knightOfDespair = "wstl_knightOfDespair";
        /// <summary>
        /// 1/4 Protector
        /// </summary>
        public const string magicalGirlSpade = "wstl_magicalGirlSpade";
        public const string knightOfDespairPixel = "wstlGBC_knightOfDespairPixel";
        public const string magicalGirlSpadePixel = "wstlGBC_magicalGirlSpadePixel";
        private static void MagicalGirlSpade_O0173() {
            string knightName = "The Knight of Despair";
            string desc = "A loyal knight fighting to protect those close to her.";
            string textureName = "knightOfDespair";
            string textureName2 = "magicalGirlSpade";
            Tribe[] tribes = new[] { TribeFae };
            Trait[] traits = new[] { MagicalGirl };

            CardManager.New(LobotomyPlugin.pluginPrefix, knightOfDespair, knightName,
                attack: 1, health: 4)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.SplitStrike, Piercing.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, magicalGirlSpade, knightName,
                attack: 1, health: 4, desc)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Protector.ability)
                .AddSpecialAbilities(SwordWithTears.specialAbility)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.Waw);

            CardManager.New(LobotomyPlugin.pixelPrefix, knightOfDespairPixel, knightName,
                attack: 1, health: 3)
                .SetGemsCost(GemType.Blue, GemType.Blue)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.SplitStrike, Piercing.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build();

            CardManager.New(LobotomyPlugin.pixelPrefix, magicalGirlSpadePixel, knightName,
                attack: 1, health: 3, desc)
                .SetGemsCost(GemType.Blue)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Protector.ability)
                .AddSpecialAbilities(SwordWithTears.specialAbility)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}