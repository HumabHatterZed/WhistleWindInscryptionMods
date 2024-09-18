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
        private void Card_MagicalGirlDiamond_O0164()
        {
            const string kingName = "The King of Greed";
            const string magicalGirlDiamond = "magicalGirlDiamond";
            const string kingOfGreed = "kingOfGreed";
            SpecialTriggeredAbility[] specialAbilities = new[] { MagicalGirls.specialAbility };
            Tribe[] tribes = new[] { TribeFae };
            Trait[] traits = new[] { MagicalGirl };

            CardInfo kingOfGreedCard = CardManager.New(pluginPrefix, 
                kingOfGreed, kingName,
                attack: 2, health: 5)
                .SetBloodCost(1)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, kingOfGreed)
                .AddAbilities(Cycler.ability)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build();

            CardManager.New(pluginPrefix, magicalGirlDiamond, kingName,
                attack: 0, health: 2, "A girl encased in hardened amber. Happiness trapped by greed.")
                .SetBloodCost(1)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, magicalGirlDiamond)
                .AddAbilities(Ability.Evolve)
                .AddSpecialAbilities(specialAbilities)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .SetEvolve(kingOfGreedCard, 1)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}