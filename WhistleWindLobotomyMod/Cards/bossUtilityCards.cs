using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using InscryptionAPI.Regions;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string RETURNONE = "wstl_RETURN_CARD";
        public const string RETURNALL = "wstl_RETURN_CARD_ALL";
        public const string REFRESHDECKS = "wstl_REFRESH_DECKS";
        private static void UtilityCards() {
            string textureName = "RETURN_CARD";
            string textureName2 = "RETURN_CARD_ALL";
            string textureName3 = "REFRESH_DECKS";
            CardManager.New(LobotomyPlugin.pluginPrefix, RETURNONE, "Single Recall",
                attack: 0, health: 0)
                .SetEnergyCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName, "", "")
                .AddAbilities(ReturnCard.ability, Ability.DrawCopyOnDeath)
                .SetTargetedSpell()
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, RETURNALL, "Total Recall",
                attack: 0, health: 0)
                .SetEnergyCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2, "", "")
                .AddAbilities(ReturnCard.ability, Ability.DrawCopyOnDeath)
                .SetGlobalSpell()
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, REFRESHDECKS, "Reshuffle Decks",
                attack: 0, health: 0)
                .SetEnergyCost(6)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3, "", "")
                .AddAbilities(RefreshDecks.ability)
                .SetGlobalSpell()
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, "RETURN_CARD_WEAK", "Single Recall",
                attack: 0, health: 0)
                .SetEnergyCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName, "", "")
                .AddAbilities(ReturnCard.ability)
                .SetTargetedSpell()
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, "RETURN_CARD_ALL_WEAK", "Total Recall",
                attack: 0, health: 0)
                .SetEnergyCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2, "", "")
                .AddAbilities(ReturnCard.ability)
                .SetGlobalSpell()
                .Build();
        }
    }
}