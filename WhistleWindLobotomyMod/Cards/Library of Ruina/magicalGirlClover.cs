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
        private void Card_MagicalGirlClover_O01111()
        {
            const string servantName = "The Servant of Wrath";
            const string magicalGirlClover = "magicalGirlClover";
            const string servantOfWrath = "servantOfWrath";
            Trait[] traits = new[] { MagicalGirl };

            CardManager.New(pluginPrefix, servantOfWrath, servantName,
                attack: 3, health: 2)
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, servantOfWrath)
                .AddAbilities(Ability.DoubleStrike, Persistent.ability)
                .AddSpecialAbilities(BlindRage.specialAbility)
                .AddTribes(TribeFae, Tribe.Reptile)
                .AddTraits(traits)
                .SetOnePerDeck()
                .AddMetaCategories(RuinaCard)
                .Build();

            CardManager.New(pluginPrefix, magicalGirlClover, servantName,
                attack: 2, health: 2, "Blind protector of another world, betrayed by their closest friend.")
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, magicalGirlClover)
                .AddAbilities(Scorching.ability)
                .AddSpecialAbilities(CloverCompanion.specialAbility)
                .AddTribes(TribeFae)
                .AddTraits(traits)
                .SetOnePerDeck()
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}