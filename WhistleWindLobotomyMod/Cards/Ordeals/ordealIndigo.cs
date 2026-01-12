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
        public const string sweeperCorpse = "wstl_sweeperCorpse";
        private static void Cards_IndigoOrdeal() {
            string cardName = "Sweeper";

            CardInfo info = CardManager.New(LobotomyPlugin.pluginPrefix, sweeperCorpse, "Corpse",
                attack: 0, health: 1)
                .SetPortraits(LobotomyPlugin.ModAssembly, "sweeperCorpse")
                .AddAbilities(Bloodfiend.ability, Ability.IceCube, Ability.SplitStrike)
                .SetOrdealCard(OrdealType.Indigo)
                .RemoveTraits(Ordeal)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, sweeperA, cardName,
                attack: 2, health: 2)
                .SetBloodCost(2).SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, "sweeperA")
                .AddAbilities(Bloodfiend.ability, SweeperPersistence.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .SetOrdealCard(OrdealType.Indigo)
                .SetIceCube(info)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, sweeperB, cardName,
                attack: 1, health: 2)
                .SetBloodCost(2).SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, "sweeperB")
                .AddAbilities(Bloodfiend.ability, SweeperPersistence.ability, OneSided.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .SetOrdealCard(OrdealType.Indigo)
                .SetIceCube(info)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, sweeperC, cardName,
                attack: 1, health: 3)
                .SetBloodCost(2).SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, "sweeperC")
                .AddAbilities(Bloodfiend.ability, SweeperPersistence.ability, Piercing.ability)
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
        }
    }
}