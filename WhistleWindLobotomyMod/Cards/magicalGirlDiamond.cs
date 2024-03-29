﻿using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MagicalGirlDiamond_O0164()
        {
            const string kingName = "The King of Greed";
            const string magicalGirlDiamond = "magicalGirlDiamond";
            const string kingOfGreed = "kingOfGreed";
            SpecialTriggeredAbility[] specialAbilities = new[] { MagicalGirls.specialAbility };
            Tribe[] tribes = new[] { TribeFae };
            Trait[] traits = new[] { MagicalGirl };

            CardInfo kingOfGreedCard = NewCard(
                kingOfGreed, kingName,
                attack: 2, health: 5, blood: 1, temple: CardTemple.Wizard)
                .SetPortraits(kingOfGreed)
                .AddAbilities(Cycler.ability)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build();

            NewCard(magicalGirlDiamond, kingName, "A girl encased in hardened amber. Happiness trapped by greed.",
                attack: 0, health: 2, blood: 1, temple: CardTemple.Wizard)
                .SetPortraits(magicalGirlDiamond)
                .AddAbilities(Ability.Evolve)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .SetEvolve(kingOfGreedCard, 1)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}