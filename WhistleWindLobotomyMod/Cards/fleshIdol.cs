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
        private void Card_FleshIdol_T0979()
        {
            const string idolName = "Flesh Idol";
            const string fleshIdol = "fleshIdol";
            Tribe[] tribes = new[] { TribeDivine };

            CardInfo fleshIdolGoodCard = NewCard(
                "fleshIdolGood",
                idolName,
                attack: 0, health: 3, bones: 2)
                .SetPortraits(fleshIdol)
                .AddAbilities(TeamLeader.ability, GroupHealer.ability, Ability.Evolve)
                .AddTribes(tribes)
                .SetEvolveInfo("wstl_fleshIdol");

            CardInfo fleshIdolCard = NewCard(
                fleshIdol,
                idolName,
                "Prayer inevitably ends with the worshipper's despair.",
                attack: 0, health: 3, bones: 2)
                .SetPortraits(fleshIdol)
                .AddAbilities(Aggravating.ability, Ability.Evolve)
                .AddTribes(tribes)
                .SetEvolveInfo("wstl_fleshIdolGood", 2);

            CreateCard(fleshIdolGoodCard);
            CreateCard(fleshIdolCard, CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}