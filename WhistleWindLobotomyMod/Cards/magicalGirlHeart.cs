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
        private void Card_MagicalGirlHeart_O0104()
        {
            const string queenName = "The Queen of Hatred";
            const string magicalGirlHeart = "magicalGirlHeart";
            const string queenOfHatred = "queenOfHatred";
            const string queenOfHatredTired = "queenOfHatredTired";
            SpecialTriggeredAbility[] specialAbilities = new[] { QueenOfHateExhaustion.specialAbility }; 
            Tribe[] tribes = new[] { TribeFae, Tribe.Reptile };
            Trait[] traits = new[] { TraitMagicalGirl };

            CardInfo queenOfHatredTiredCard = NewCard(
                queenOfHatredTired,
                queenName,
                attack: 0, health: 2, blood: 1)
                .SetPortraits(queenOfHatredTired)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck();

            CardInfo queenOfHatredCard = NewCard(
                queenOfHatred,
                queenName,
                attack: 8, health: 2, blood: 1)
                .SetPortraits(queenOfHatred)
                .AddAbilities(Piercing.ability, OneSided.ability)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck();

            CardInfo magicalGirlHeartCard = NewCard(
                magicalGirlHeart,
                "Magical Girl",
                "A hero of love and justice. She will aid you on your journey.",
                attack: 1, health: 2, blood: 1)
                .SetPortraits(magicalGirlHeart)
                .AddAbilities(OneSided.ability)
                .AddSpecialAbilities(LoveAndHate.specialAbility)
                .AddTribes(TribeFae)
                .AddTraits(traits)
                .SetOnePerDeck();

            CreateCards(queenOfHatredTiredCard, queenOfHatredCard);
            CreateCard(magicalGirlHeartCard, CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}