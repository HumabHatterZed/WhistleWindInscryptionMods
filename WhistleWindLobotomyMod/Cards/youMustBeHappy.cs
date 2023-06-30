using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_YouMustBeHappy_T0994()
        {
            const string youMustBeHappy = "youMustBeHappy";

            CardInfo youMustBeHappyCard = NewCard(
                youMustBeHappy,
                "You Must Be Happy",
                "Those that undergo the procedure find themselves rested and healthy again.",
                attack: 0, health: 2, energy: 2, temple: CardTemple.Tech)
                .SetPortraits(youMustBeHappy)
                .AddAbilities(Scrambler.ability)
                .SetSpellType(SpellType.TargetedStats)
                .SetEvolveInfo("[name]You Must Be Happier");

            CreateCard(youMustBeHappyCard, CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
    }
}