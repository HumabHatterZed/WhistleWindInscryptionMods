using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string nothingThere = "wstl_nothingThere";
        public const string nothingThereTrue = "wstl_nothingThereTrue";
        public const string nothingThereEgg = "wstl_nothingThereEgg";
        public const string nothingThereFinal = "wstl_nothingThereFinal";
        private static void NothingThere_O0620()
        {
            string nothingName = "Nothing There";
            string textureName = "nothingThereFinal";
            string textureName2 = "nothingThereEgg";
            string textureName3 = "nothingThereTrue";
            string textureName4 = "nothingThere";
            Ability[] abilities = new[] { Ability.Evolve };

            CardInfo nothingThereFinalCard = CardManager.New(LobotomyPlugin.pluginPrefix,
                nothingThereFinal, nothingName,
                attack: 8, health: 8)
                .SetBloodCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Piercing.ability, Persistent.ability)
                .AddSpecialAbilities(MimicryCombat.specialAbility)
                .SetDefaultEvolutionName(nothingName)
                .AddAppearances(ForcedEmission.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardInfo nothingThereEggCard = CardManager.New(LobotomyPlugin.pluginPrefix,
                nothingThereEgg, "An Egg",
                attack: 0, health: 3)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(abilities)
                .SetEvolve(nothingThereFinalCard, 1)
                .AddAppearances(ForcedEmission.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardManager.New(LobotomyPlugin.pluginPrefix,
                nothingThereTrue, nothingName,
                attack: 3, health: 3)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(abilities)
                .AddSpecialAbilities(MimicryCombat.specialAbility)
                .AddTribes(Tribe.Canine, Tribe.Hooved, Tribe.Reptile)
                .SetEvolve(nothingThereEggCard, 1)
                .AddAppearances(ForcedEmission.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardManager.New(LobotomyPlugin.pluginPrefix, nothingThere, "Yumi",
                attack: 1, health: 1, "I don't remember this challenger...")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName4)
                .AddAbilities(abilities)
                .AddSpecialAbilities(Mimicry.specialAbility)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph, true);
        }
    }
}