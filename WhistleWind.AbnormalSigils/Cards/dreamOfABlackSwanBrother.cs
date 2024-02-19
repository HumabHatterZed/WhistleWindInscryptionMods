using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_SwanBrothers_F0270()
        {
            const string dreamOfABlackSwanBrother1 = "dreamOfABlackSwanBrother1";
            const string dreamOfABlackSwanBrother2 = "dreamOfABlackSwanBrother2";
            const string dreamOfABlackSwanBrother3 = "dreamOfABlackSwanBrother3";
            const string dreamOfABlackSwanBrother4 = "dreamOfABlackSwanBrother4";
            const string dreamOfABlackSwanBrother5 = "dreamOfABlackSwanBrother5";
            const string dreamOfABlackSwanBrother6 = "dreamOfABlackSwanBrother6";
            Tribe[] tribes = new[] { TribeAnthropoid };
            Trait[] traits = new[] { SwanBrother };
            CardAppearanceBehaviour.Appearance[] appearances = new[] { CardAppearanceBehaviour.Appearance.TerrainLayout };

            CreateCard(MakeCard(
                cardName: dreamOfABlackSwanBrother1,
                "First Brother",
                attack: 0, health: 1, blood: 1)
                .SetPortraits(dreamOfABlackSwanBrother1)
                .AddAbilities(Ability.DoubleStrike)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances));

            CreateCard(MakeCard(
                cardName: dreamOfABlackSwanBrother2,
                "Second Brother",
                attack: 0, health: 1, blood: 1)
                .SetPortraits(dreamOfABlackSwanBrother2)
                .AddAbilities(Piercing.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances));

            CreateCard(MakeCard(
                cardName: dreamOfABlackSwanBrother3,
                "Third Brother",
                attack: 0, health: 2, blood: 1)
                .SetPortraits(dreamOfABlackSwanBrother3)
                .AddAbilities(Ability.Sharp)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances));

            CreateCard(MakeCard(
                cardName: dreamOfABlackSwanBrother4,
                "Fourth Brother",
                attack: 0, health: 1, blood: 1)
                .SetPortraits(dreamOfABlackSwanBrother4)
                .AddAbilities(Ability.Deathtouch)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances));

            CreateCard(MakeCard(
                cardName: dreamOfABlackSwanBrother5,
                "Fifth Brother",
                attack: 0, health: 1, blood: 1)
                .SetPortraits(dreamOfABlackSwanBrother5)
                .AddAbilities(Scorching.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances));

            CreateCard(MakeCard(
                cardName: dreamOfABlackSwanBrother6,
                "Sixth Brother",
                attack: 0, health: 1, blood: 1)
                .SetPortraits(dreamOfABlackSwanBrother6)
                .AddAbilities(ThickSkin.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .AddAppearances(appearances));
        }
    }
}