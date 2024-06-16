using DiskCardGame;
using InscryptionAPI.Card;
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

            CardInfo star3 = NewCard("blueStar3", starName,
                attack: 4, health: 4, blood: 4)
                .SetPortraits(blueStar, pixelPortraitName: "blueStar2_pixel")
                .AddAbilities(Ability.Transformer, Ability.AllStrike)
                .AddSpecialAbilities(StarSound.specialAbility)
                .AddTribes(tribes)
                .AddAppearances(ForcedEmission.appearance)
                .Build(CardHelper.ChoiceType.Rare, nonChoice: true);

            CardInfo star2 = NewCard("blueStar2", starName,
                attack: 0, health: 4, blood: 4)
                .SetPortraits(blueStar)
                .AddAbilities(Ability.Transformer, Idol.ability)
                .AddTribes(tribes)
                .SetEvolve(star3, 1)
                .Build(CardHelper.ChoiceType.Rare, nonChoice: true);

            CardInfo star = NewCard(blueStar, starName, "When this is over, let's meet again as stars.",
                attack: 0, health: 4, blood: 3)
                .SetPortraits(blueStar)
                .AddAbilities(Ability.Transformer)
                .AddTribes(tribes)
                .SetEvolve(star2, 1)
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.Aleph);

            // set the evolve info here to prevent any errors with evolving cards and encounters
            star3.SetEvolve(star, 1);
        }
    }
}