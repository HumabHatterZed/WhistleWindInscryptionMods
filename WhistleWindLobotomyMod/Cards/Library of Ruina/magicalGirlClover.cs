using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string magicalGirlClover = "wstl_magicalGirlClover";
        public const string servantOfWrath = "wstl_servantOfWrath";
        public const string magicalGirlCloverPixel = "wstlGBC_magicalGirlClover";
        public const string servantOfWrathPixel = "wstlGBC_servantOfWrath";
        private static void MagicalGirlClover_O01111()
        {
            string name = "The Servant of Wrath";
            string desc = "Blind protector of another world, betrayed by their closest friend.";
            string textureName = "servantOfWrath";
            string textureName2 = "magicalGirlClover";
            Trait[] traits = new[] { MagicalGirl };

            CardManager.New(LobotomyPlugin.pluginPrefix, servantOfWrath, name,
                attack: 3, health: 2)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.DoubleStrike, Persistent.ability)
                .AddSpecialAbilities(BlindRage.specialAbility)
                .AddTribes(TribeFae, Tribe.Reptile)
                .AddTraits(traits)
                .SetOnePerDeck()
                .AddMetaCategories(RuinaCard)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, magicalGirlClover, name,
                attack: 2, health: 2, desc)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Scorching.ability)
                .AddSpecialAbilities(CloverCompanion.specialAbility)
                .AddTribes(TribeFae)
                .AddTraits(traits)
                .SetOnePerDeck()
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw);

            CardManager.New(LobotomyPlugin.pixelPrefix, servantOfWrathPixel, name,
                attack: 3, health: 2)
                .SetGemsCost(GemType.Green, GemType.Green)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.DoubleStrike, Persistent.ability)
                .AddSpecialAbilities(BlindRage.specialAbility)
                .AddTribes(TribeFae, Tribe.Reptile)
                .AddTraits(traits)
                .SetOnePerDeck()
                .AddMetaCategories(RuinaCard)
                .Build();

            CardManager.New(LobotomyPlugin.pixelPrefix, magicalGirlCloverPixel, name,
                attack: 2, health: 2, desc)
                .SetGemsCost(GemType.Green)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
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