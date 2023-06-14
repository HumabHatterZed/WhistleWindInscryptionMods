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
        private void Card_CENSORED_O0389()
        {
            const string name = "CENSORED";
            const string censored = "censored";
            const string censoredMinion = "censoredMinion";

            CardInfo main = NewCard(
                censored,
                name,
                "It's best you never learn what it looks like.",
                attack: 4, health: 4, blood: 3)
                .SetPortraits(censored)
                .AddAbilities(Bloodfiend.ability)
                .AddSpecialAbilities(CensoredSpecial.specialAbility)
                .SetEvolveInfo("CENSORED {0}");

            CardInfo minion = NewCard(
                censoredMinion,
                name)
                .SetPortraits(censoredMinion)
                .SetEvolveInfo("CENSORED {0}");

            CreateCard(main, CardHelper.ChoiceType.Rare, RiskLevel.Aleph);
            CreateCard(minion);
        }
    }
}