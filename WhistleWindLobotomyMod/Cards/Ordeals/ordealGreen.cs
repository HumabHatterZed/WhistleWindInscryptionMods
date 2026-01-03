using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string doubtA = "wstl_doubtA";
        public const string doubtB = "wstl_doubtB";
        public const string doubtY = "wstl_doubtY";
        public const string doubtO = "wstl_doubtO";
        public const string doubtProcess = "wstl_doubtProcess";
        public const string doubtProcessDown = "wstl_doubtProcess_down";
        public const string whereWeReach = "wstl_doubtReach";
        public const string lastHelix = "wstl_doubtHelix";
        private static void Cards_GreenOrdeal() {
            string textureName = "doubtA";
            string textureName2 = "doubtB";
            string textureName3 = "doubtY";
            string textureName4 = "doubtO";
            string textureName5 = "doubtProcess";
            string textureName51 = "doubtProcess_down";
            string textureName6 = "doubtReach";
            CardInfo infoO = CardManager.New(LobotomyPlugin.pluginPrefix, doubtO, "Doubt O",
                attack: 2, health: 3)
                .SetEnergyCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName4)
                .SetTitle(LobotomyPlugin.ModAssembly, "doubtO_title.png")
                .AddAbilities(Piercing.ability, Challenging.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .SetOrdealCard(OrdealType.Green)
                .Build();

            CardInfo infoY = CardManager.New(LobotomyPlugin.pluginPrefix, doubtY, "Doubt Y",
                attack: 2, health: 2)
                .SetEnergyCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .SetTitle(LobotomyPlugin.ModAssembly, "doubtY_title.png")
                .AddAbilities(Piercing.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .SetEvolve(infoO, 1)
                .SetOrdealCard(OrdealType.Green)
                .Build();

            CardInfo infoB = CardManager.New(LobotomyPlugin.pluginPrefix, doubtB, "Doubt B",
                attack: 1, health: 2)
                .SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .SetTitle(LobotomyPlugin.ModAssembly, "doubtB_title.png")
                .AddAbilities(Piercing.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .SetEvolve(infoY, 1)
                .SetOrdealCard(OrdealType.Green)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, doubtA, "Doubt A",
                attack: 1, health: 1)
                .SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetTitle(LobotomyPlugin.ModAssembly, "doubtA_title.png")
                .AddAbilities(Piercing.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .SetEvolve(infoB, 1)
                .SetOrdealCard(OrdealType.Green)
                .Build();

            CardInfo down = CardManager.New(LobotomyPlugin.pluginPrefix, doubtProcessDown, "Process of Understanding",
                attack: 0, health: 4)
                .SetEnergyCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName51)
                .AddAbilities(Piercing.ability, Ability.Transformer)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .SetOrdealCard(OrdealType.Green)
                .Build();

            CardInfo pro = CardManager.New(LobotomyPlugin.pluginPrefix, doubtProcess, "Process of Understanding",
                attack: 3, health: 4)
                .SetEnergyCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName5)
                .AddAbilities(Piercing.ability, Ability.Transformer)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .SetOrdealCard(OrdealType.Green)
                .SetEvolve(down, 2)
                .Build();

            down.SetEvolve(pro, 1);

            CardManager.New(LobotomyPlugin.pluginPrefix, whereWeReach, "Where We Must Reach",
                attack: 0, health: 7)
                .SetEnergyCost(5)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName6)
                .AddAbilities(Life.ability, Challenging.ability)
                .AddAppearances(ForcedGreenEmission.appearance)
                .AddTribes(TribeMechanical)
                .AddTraits(Trait.Uncuttable, Trait.Structure, ImmuneToInstaDeath)
                .SetTerrain(false)
                .SetOrdealCard(OrdealType.Green)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, lastHelix, "Last Helix",
                attack: 0, health: 30)
                .AddAbilities(Ability.Reach, Challenging.ability, Tower.ability)
                .AddAppearances(ForcedGreenEmission.appearance)
                .AddTribes(TribeMechanical)
                .AddTraits(Ordeal, Trait.Uncuttable, Trait.Structure, ImmuneToInstaDeath)
                .SetAnimatedPortrait(LobOpponentUtils.HelixBossPrefab)
                .SetTerrain()
                .SetOrdealCard(OrdealType.Green)
                .SetMiniGiant()
                .Build();
        }
    }
}