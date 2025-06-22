using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string magicalGirlDiamond = "wstl_magicalGirlDiamond";
        public const string kingOfGreed = "wstl_kingOfGreed";
        public const string magicalGirlDiamondPixel = "wstlGBC_magicalGirlDiamond";
        public const string kingOfGreedPixel = "wstlGBC_kingOfGreed";
        private static void MagicalGirlDiamond_O0164() {
            string kingName = "The King of Greed";
            string desc = "A girl encased in hardened amber. Happiness trapped by greed.";
            string textureName = "kingOfGreed";
            string textureName2 = "magicalGirlDiamond";
            SpecialTriggeredAbility[] specialAbilities = new[] { MagicalGirls.specialAbility };
            Tribe[] tribes = new[] { TribeFae };
            Trait[] traits = new[] { MagicalGirl };

            CardInfo kingOfGreedCard = CardManager.New(LobotomyPlugin.pluginPrefix, kingOfGreed, kingName,
                attack: 2, health: 5)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Cycler.ability)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, magicalGirlDiamond, kingName,
                attack: 0, health: 2, desc)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Ability.Evolve)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .SetEvolve(kingOfGreedCard, 1)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw);

            CardInfo kingOfGreedCard2 = CardManager.New(LobotomyPlugin.pixelPrefix, kingOfGreedPixel, kingName,
                attack: 2, health: 3)
                .SetGemsCost(GemType.Orange, GemType.Orange)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Cycler.ability)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build();

            CardManager.New(LobotomyPlugin.pixelPrefix, magicalGirlDiamondPixel, kingName,
                attack: 0, health: 2, desc)
                .SetGemsCost(GemType.Orange)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Ability.Evolve)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .SetEvolve(kingOfGreedCard2, 1)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}