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
        private void Card_GraveOfBlossoms_O04100()
        {
            const string graveOfBlossoms = "graveOfBlossoms";

            CardInfo graveOfBlossomsCard = NewCard(
                graveOfBlossoms,
                "Grave of Cherry Blossoms",
                "A blooming cherry tree. The more blood it has, the more beautiful it becomes.",
                attack: 0, health: 3, blood: 1)
                .SetPortraits(graveOfBlossoms)
                .AddAbilities(Ability.Sharp, Bloodfiend.ability)
                .AddTribes(TribeBotanic)
                .SetEvolveInfo("Mass {0}");

            CreateCard(graveOfBlossomsCard, CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}