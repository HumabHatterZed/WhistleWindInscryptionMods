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
        private void Card_MagicalGirlClover_O01111()
        {
            const string servantName = "The Servant of Wrath";
            const string magicalGirlClover = "magicalGirlClover";
            const string servantOfWrath = "servantOfWrath";
            Trait[] traits = new[] { TraitMagicalGirl };

            CardInfo servantOfWrathCard = NewCard(
                servantOfWrath,
                servantName,
                attack: 3, health: 2, blood: 2)
                .SetPortraits(servantOfWrath)
                .AddAbilities(Ability.DoubleStrike, Persistent.ability)
                .AddSpecialAbilities(BlindRage.specialAbility)
                .AddTribes(TribeFae, Tribe.Reptile)
                .AddTraits(traits)
                .SetOnePerDeck();

            CardInfo magicalGirlCloverCard = NewCard(
                magicalGirlClover,servantName,
                "Blind protector of another world.",
                attack: 2, health: 2, blood: 2)
                .SetPortraits(magicalGirlClover)
                .AddAbilities(Scorching.ability)
                .AddSpecialAbilities(SwordWithTears.specialAbility)
                .AddTribes(TribeFae)
                .AddTraits(traits)
                .SetOnePerDeck();

            CreateCard(servantOfWrathCard);
            CreateCard(magicalGirlCloverCard, CardHelper.ChoiceType.Common, RiskLevel.Waw, ModCardType.Ruina);
        }
    }
}