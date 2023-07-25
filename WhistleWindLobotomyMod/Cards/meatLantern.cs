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
        private void Card_MeatLantern_O0484()
        {
            const string meatLantern = "meatLantern";

            NewCard(meatLantern, "Meat Lantern", "A beautiful flower attached to a mysterious creature.",
                attack: 1, health: 2, blood: 2)
                .SetPortraits(meatLantern)
                .AddAbilities(Ability.Reach, Punisher.ability)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}