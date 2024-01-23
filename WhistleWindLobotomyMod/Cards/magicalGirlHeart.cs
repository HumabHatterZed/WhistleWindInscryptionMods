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
        private void Card_MagicalGirlHeart_O0104()
        {
            const string queenName = "The Queen of Hatred";
            const string magicalGirlHeart = "magicalGirlHeart";
            const string queenOfHatred = "queenOfHatred";
            const string queenOfHatredTired = "queenOfHatredTired";
            SpecialTriggeredAbility[] specialAbilities = new[] { QueenOfHateExhaustion.specialAbility };
            Tribe[] tribes = new[] { TribeFae, Tribe.Reptile };
            Trait[] traits = new[] { MagicalGirl };

            NewCard(queenOfHatredTired, queenName,
                attack: 0, health: 2, blood: 1, temple: CardTemple.Wizard)
                .SetPortraits(queenOfHatredTired)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build();

            NewCard(queenOfHatred, queenName,
                attack: 8, health: 2, blood: 1, temple: CardTemple.Wizard)
                .SetPortraits(queenOfHatred)
                .AddAbilities(Piercing.ability, OneSided.ability)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build();

            NewCard(magicalGirlHeart, "Magical Girl", "A hero of love and justice. She will aid you on your journey.",
                attack: 1, health: 2, blood: 1, temple: CardTemple.Wizard)
                .SetPortraits(magicalGirlHeart)
                .AddAbilities(OneSided.ability)
                .AddSpecialAbilities(LoveAndHate.specialAbility)
                .AddTribes(TribeFae)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}