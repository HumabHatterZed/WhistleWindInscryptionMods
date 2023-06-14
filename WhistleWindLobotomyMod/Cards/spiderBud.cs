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
        private void Card_SpiderBud_O0243()
        {
            const string spiderBud = "spiderBud";

            CardInfo spiderBudCard = NewCard(
                spiderBud,
                "Spider Bud",
                "A grotesque mother of spiders. Its children are small but grow quickly.",
                attack: 0, health: 2, bones: 3)
                .SetPortraits(spiderBud)
                .AddAbilities(BroodMother.ability)
                .AddTribes(Tribe.Insect);

            CreateCard(spiderBudCard, CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}