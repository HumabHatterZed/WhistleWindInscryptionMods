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
        private void Card_MagicalGirlSpade_O0173()
        {
            const string knightName = "The Knight of Despair";
            const string knightOfDespair = "knightOfDespair";
            const string magicalGirlSpade = "magicalGirlSpade";
            Tribe[] tribes = new[] { TribeFae };
            Trait[] traits = new[] { MagicalGirl };

            CardManager.New(pluginPrefix, knightOfDespair, knightName,
                attack: 1, health: 4)
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, knightOfDespair)
                .AddAbilities(Ability.SplitStrike, Piercing.ability)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build();

            CardManager.New(pluginPrefix, magicalGirlSpade, knightName,
                attack: 1, health: 4, "A loyal knight fighting to protect those close to her.")
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, magicalGirlSpade)
                .AddAbilities(Protector.ability)
                .AddSpecialAbilities(SwordWithTears.specialAbility)
                .AddTribes(tribes)
                .AddTraits(traits)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}