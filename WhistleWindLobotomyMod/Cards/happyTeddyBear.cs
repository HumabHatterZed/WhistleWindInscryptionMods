using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_HappyTeddyBear_T0406()
        {
            const string happyTeddyBear = "happyTeddyBear";

            NewCard(happyTeddyBear, "Happy Teddy Bear", "A discarded stuffed bear. Its memories began with a warm hug.",
                attack: 1, health: 5, bones: 6, temple: CardTemple.Undead)
                .SetPortraits(happyTeddyBear)
                .AddAbilities(Ability.GuardDog)
                .SetDefaultEvolutionName("Big, Happy Teddy Bear")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}