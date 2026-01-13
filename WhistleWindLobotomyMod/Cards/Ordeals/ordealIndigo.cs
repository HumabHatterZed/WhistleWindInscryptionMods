using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string sweeperA = "wstl_sweeperA";
        public const string sweeperB = "wstl_sweeperB";
        public const string sweeperC = "wstl_sweeperC";
        public const string sweeperD = "wstl_sweeperD";
        public const string sweeperE = "wstl_sweeperE";
        public const string sweeperF = "wstl_sweeperF";
        public const string sweeperG = "wstl_sweeperG";
        public const string sweeperCorpse = "wstl_sweeperCorpse";
        private static void Cards_IndigoOrdeal() {
            string cardName = "Sweeper";

            CardInfo info = CardManager.New(LobotomyPlugin.pluginPrefix, sweeperCorpse, "Corpse",
                attack: 0, health: 1)
                .SetPortraits(LobotomyPlugin.ModAssembly, "sweeperCorpse")
                .AddAbilities(StartingDecay.ability, StartingDecay.ability)
                .SetOrdealCard(OrdealType.Indigo)
                .RemoveTraits(Ordeal)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, sweeperA, cardName,
                attack: 2, health: 2)
                .SetBloodCost(2).SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, "sweeperA")
                .AddAbilities(Bloodfiend.ability, SweeperPersistence.ability, Piercing.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .SetOrdealCard(OrdealType.Indigo)
                .SetIceCube(info)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, sweeperB, cardName,
                attack: 1, health: 2)
                .SetBloodCost(2).SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, "sweeperB")
                .AddAbilities(Bloodfiend.ability, SweeperPersistence.ability, Ability.BuffNeighbours)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .SetOrdealCard(OrdealType.Indigo)
                .SetIceCube(info)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, sweeperC, cardName,
                attack: 1, health: 2)
                .SetBloodCost(2).SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, "sweeperC")
                .AddAbilities(Bloodfiend.ability, SweeperPersistence.ability, Protector.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .SetOrdealCard(OrdealType.Indigo)
                .SetIceCube(info)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, sweeperD, cardName,
                attack: 1, health: 1)
                .SetBloodCost(2).SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, "sweeperD")
                .AddAbilities(Bloodfiend.ability, SweeperPersistence.ability, Ability.DoubleStrike)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .SetOrdealCard(OrdealType.Indigo)
                .SetIceCube(info)
                .Build();

            //CardManager.New(LobotomyPlugin.pluginPrefix, sweeperE, cardName,
            //    attack: 1, health: 2)
            //    .SetBloodCost(2).SetEnergyCost(2)
            //    .SetPortraits(LobotomyPlugin.ModAssembly, "sweeperE")
            //    .AddAbilities(Bloodfiend.ability, SweeperPersistence.ability, OneSided.ability)
            //    .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
            //    .AddTribes(TribeAnthropoid)
            //    .SetOrdealCard(OrdealType.Indigo)
            //    .SetIceCube(info)
            //    .Build();

            //CardManager.New(LobotomyPlugin.pluginPrefix, sweeperF, cardName,
            //    attack: 1, health: 2)
            //    .SetBloodCost(2).SetEnergyCost(2)
            //    .SetPortraits(LobotomyPlugin.ModAssembly, "sweeperF")
            //    .AddAbilities(Bloodfiend.ability, SweeperPersistence.ability, Ability.Sentry)
            //    .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
            //    .AddTribes(TribeAnthropoid)
            //    .SetOrdealCard(OrdealType.Indigo)
            //    .SetIceCube(info)
            //    .Build();

            //CardManager.New(LobotomyPlugin.pluginPrefix, sweeperG, cardName,
            //    attack: 2, health: 1)
            //    .SetBloodCost(2).SetEnergyCost(2)
            //    .SetPortraits(LobotomyPlugin.ModAssembly, "sweeperG")
            //    .AddAbilities(Bloodfiend.ability, SweeperPersistence.ability, GreedyHealing.ability)
            //    .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
            //    .AddTribes(TribeAnthropoid)
            //    .SetOrdealCard(OrdealType.Indigo)
            //    .SetIceCube(info)
            //    .Build();
        }
    }
}