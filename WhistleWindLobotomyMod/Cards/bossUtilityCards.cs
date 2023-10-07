using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
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

            // spawn card
            NewCard(RETURNONE, "Single Recall",
                attack: 0, health: 0)
                .SetPortraits(RETURNONE, "", "")
                .SetEnergyCost(1)
                .AddAbilities(Ability.DrawCopyOnDeath, ReturnCard.ability)
                .SetTargetedSpellStats()
                .Build();

            // only 3
            NewCard(RETURNALL, "Total Recall",
                attack: 0, health: 0)
                .SetPortraits(RETURNALL, "", "")
                .SetEnergyCost(3)
                .AddAbilities(Ability.DrawCopyOnDeath, ReturnCard.ability)
                .SetGlobalSpellStats()
                .Build();

            NewCard(REFRESHDECKS, "Reshuffle Decks",
                attack: 0, health: 0)
                .SetPortraits(REFRESHDECKS, "", "")
                .AddAbilities(RefreshDecks.ability)
                .SetEnergyCost(6)
                .SetGlobalSpell()
                .Build();
        }
    }
}