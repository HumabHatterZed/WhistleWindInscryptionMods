using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using InscryptionAPI.Regions;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_UtilityCards()
        {
            const string RETURNONE = "RETURN_CARD";
            const string RETURNALL = "RETURN_CARD_ALL";
            const string REFRESHDECKS = "REFRESH_DECKS";

            CardManager.New(pluginPrefix, RETURNONE, "Single Recall",
                attack: 0, health: 0)
                .SetEnergyCost(1)
                .SetPortraits(ModAssembly, RETURNONE, "", "")
                .AddAbilities(ReturnCard.ability, Ability.DrawCopyOnDeath)
                .SetTargetedSpell()
                .Build();

            CardManager.New(pluginPrefix, RETURNALL, "Total Recall",
                attack: 0, health: 0)
                .SetEnergyCost(3)
                .SetPortraits(ModAssembly, RETURNALL, "", "")
                .AddAbilities(ReturnCard.ability, Ability.DrawCopyOnDeath)
                .SetGlobalSpell()
                .Build();

            CardManager.New(pluginPrefix, REFRESHDECKS, "Reshuffle Decks",
                attack: 0, health: 0)
                .SetEnergyCost(6)
                .SetPortraits(ModAssembly, REFRESHDECKS, "", "")
                .AddAbilities(RefreshDecks.ability)
                .SetGlobalSpell()
                .Build();

            CardManager.New(pluginPrefix, "RETURN_CARD_WEAK", "Single Recall",
                attack: 0, health: 0)
                .SetEnergyCost(1)
                .SetPortraits(ModAssembly, RETURNONE, "", "")
                .AddAbilities(ReturnCard.ability)
                .SetTargetedSpell()
                .Build();

            CardManager.New(pluginPrefix, "RETURN_CARD_ALL_WEAK", "Total Recall",
                attack: 0, health: 0)
                .SetEnergyCost(3)
                .SetPortraits(ModAssembly, RETURNALL, "", "")
                .AddAbilities(ReturnCard.ability)
                .SetGlobalSpell()
                .Build();
        }
    }
}