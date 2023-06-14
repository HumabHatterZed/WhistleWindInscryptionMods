using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Ppodae_D02107()
        {
            const string name = "Ppodae";
            const string ppodae = "ppodae";
            const string ppodaeBuff = "ppodaeBuff";
            Tribe[] tribes = new[] { Tribe.Canine };

            CardInfo ppodaeBuffCard = NewCard(
                ppodaeBuff,
                name,
                attack: 3, health: 2, bones: 8)
                .SetPortraits(ppodaeBuff)
                .AddAbilities(Ability.DebuffEnemy)
                .AddTribes(tribes);

            CardInfo ppodaeCard = NewCard(
                ppodae,
                name,
                "An innocent little puppy.",
                attack: 1, health: 1, bones: 4)
                .SetPortraits(ppodae)
                .AddAbilities(Ability.DebuffEnemy, Ability.Evolve)
                .AddTribes(tribes)
                .SetEvolveInfo("wstl_ppodaeBuff");

            CreateCard(ppodaeBuffCard);
            CreateCard(ppodaeCard, CardHelper.ChoiceType.Common, RiskLevel.Teth, ModCardType.Donator);
        }
    }
}