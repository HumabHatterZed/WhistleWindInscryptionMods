using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// <c>(1/2) (<see cref="OneSided"/>) (1 Blood)</c>
        /// </summary>
        public const string magicalGirlHeart = "wstl_magicalGirlHeart";
        /// <summary>
        /// <c>(8/2) (<see cref="OneSided"/>, <see cref="Piercing"/>) (1 Blood)</c>
        /// </summary>
        public const string queenOfHatred = "wstl_queenOfHatred";
        /// <summary>
        /// <c>(0/2) (1 Blood)</c>
        /// </summary>
        public const string queenOfHatredTired = "wstl_queenOfHatredTired";
        /// <summary>
        /// <c>(1/1) (<see cref="OneSided"/>) (1 Blood)</c>
        /// </summary>
        public const string magicalGirlHeartPixel = "wstlGBC_magicalGirlHeart";
        /// <summary>
        /// <c>(6/2) (<see cref="OneSided"/>, <see cref="Piercing"/>) (1 Blood)</c>
        /// </summary>
        public const string queenOfHatredPixel = "wstlGBC_queenOfHatred";
        /// <summary>
        /// <c>(0/2) (1 Blood)</c>
        /// </summary>
        public const string queenOfHatredTiredPixel = "wstlGBC_queenOfHatredTired";
        private static void MagicalGirlHeart_O0104() {
            string name = "Magical Girl";
            string queenName = "The Queen of Hatred";
            string desc = "A hero of love and justice. She will aid you on your journey.";
            string textureName = "queenOfHatredTired";
            string textureName2 = "queenOfHatred";
            string textureName3 = "magicalGirlHeart";
            SpecialTriggeredAbility[] specialAbilities = new[] { QueenOfHateExhaustion.specialAbility };
            Tribe[] tribes = new[] { TribeFae, Tribe.Reptile };
            Trait[] traits = new[] { MagicalGirl };

            CardManager.New(LobotomyPlugin.pluginPrefix, queenOfHatredTired, queenName,
                attack: 0, health: 2)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, queenOfHatred, queenName,
                attack: 8, health: 2)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Piercing.ID, OneSided.ID)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, magicalGirlHeart, name,
                attack: 1, health: 2, desc)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(OneSided.ID)
                .AddSpecialAbilities(LoveAndHate.specialAbility)
                .AddTribes(TribeFae)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.Waw);

            CardManager.New(LobotomyPlugin.pixelPrefix, queenOfHatredTiredPixel, queenName,
                attack: 0, health: 2)
                .SetGemsCost(GemType.Orange, GemType.Orange)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build();

            CardManager.New(LobotomyPlugin.pixelPrefix, queenOfHatredPixel, queenName,
                attack: 6, health: 2)
                .SetGemsCost(GemType.Orange, GemType.Orange)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Piercing.ID, OneSided.ID)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build();

            CardManager.New(LobotomyPlugin.pixelPrefix, magicalGirlHeartPixel, name,
                attack: 1, health: 1, desc)
                .SetGemsCost(GemType.Orange)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(OneSided.ID)
                .AddSpecialAbilities(LoveAndHate.specialAbility)
                .AddTribes(TribeFae)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}