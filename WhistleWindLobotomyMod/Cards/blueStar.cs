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
        private void Card_BlueStar_O0393()
        {
            const string starName = "Blue Star";
            const string blueStar = "blueStar";
            Tribe[] tribes = new[] { TribeDivine };

            CardInfo star3 = NewCard(
                "blueStar3",
                starName,
                attack: 4, health: 4, blood: 4)
                .SetPortraits(blueStar, pixelPortraitName: "blueStar2_pixel")
                .AddAbilities(Ability.Evolve, Ability.AllStrike)
                .AddSpecialAbilities(StarSound.specialAbility)
                .AddTribes(tribes)
                .AddAppearances(ForcedEmission.appearance)
                .SetEvolveInfo("wstl_blueStar");

            CardInfo star2 = NewCard(
                "blueStar2",
                starName,
                attack: 0, health: 4, blood: 3)
                .SetPortraits(blueStar)
                .AddAbilities(Ability.Evolve, Idol.ability)
                .AddTribes(tribes)
                .SetEvolveInfo("wstl_blueStar3");

            CardInfo star = NewCard(
                blueStar,
                starName,
                "When this is over, let's meet again as stars.",
                attack: 0, health: 4, blood: 2)
                .SetPortraits(blueStar)
                .AddAbilities(Ability.Evolve)
                .AddTribes(tribes)
                .SetEvolveInfo("wstl_blueStar2");

            CreateCard(star3, CardHelper.ChoiceType.Rare, nonChoice: true);
            CreateCard(star2, CardHelper.ChoiceType.Rare, nonChoice: true);
            CreateCard(star, CardHelper.ChoiceType.Rare, RiskLevel.Aleph);
        }
    }
}