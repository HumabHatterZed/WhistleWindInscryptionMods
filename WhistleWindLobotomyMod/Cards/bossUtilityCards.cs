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

            NewCard(RETURNONE, "Single Recall",
                attack: 0, health: 0, energy: 1)
                .SetPortraits(RETURNONE, "", "")
                .AddAbilities(Ability.DrawCopyOnDeath, ReturnCard.ability)
                .SetTargetedSpell()
                .Build();

            NewCard(RETURNALL, "Total Recall",
                attack: 0, health: 0, energy: 3)
                .SetPortraits(RETURNALL, "", "")
                .AddAbilities(Ability.DrawCopyOnDeath, ReturnCard.ability)
                .SetGlobalSpell()
                .Build();

            NewCard(REFRESHDECKS, "Reshuffle Decks",
                attack: 0, health: 0, energy: 0)
                .SetPortraits(REFRESHDECKS, "", "")
                .AddAbilities(RefreshDecks.ability, BoneMeal.ability)
                .SetGlobalSpell()
                .Build();

            NewCard("RETURN_CARD_WEAK", "Single Recall",
                attack: 0, health: 0, energy: 1)
                .SetPortraits(RETURNONE, "", "")
                .AddAbilities(ReturnCard.ability)
                .SetTargetedSpell()
                .Build();

            NewCard("RETURN_CARD_ALL_WEAK", "Total Recall",
                attack: 0, health: 0, energy: 3)
                .SetPortraits(RETURNALL, "", "")
                .AddAbilities(ReturnCard.ability)
                .SetGlobalSpell()
                .Build();
        }
    }
}