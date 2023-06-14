using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_YoureBald_BaldIsAwesome()
        {
            const string youreBald = "youreBald";

            CardInfo youreBaldCard = NewCard(
                youreBald,
                "You're Bald...",
                "I've always wondered what it's like to be bald.",
                attack: 0, health: 1, energy: 2)
                .SetPortraits(youreBald)
                .AddAbilities(Ability.DrawCopy)
                .SetEvolveInfo("[name]You're Extra Bald...");

            CreateCard(youreBaldCard, CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
    }
}