using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
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
        public const string doubtProcessDown2 = "wstl_doubtProcess_down2";
        public const string whereWeReach = "wstl_doubtReach";
        public const string lastHelix = "wstl_doubtHelix";
        public const string lilHelix = "wstl_lilHelix";
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
                .AddAbilities(Piercing.ID, Challenging.ID)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .SetOrdealCard(OrdealType.Green)
                .Build();

            CardInfo infoY = CardManager.New(LobotomyPlugin.pluginPrefix, doubtY, "Doubt Y",
                attack: 2, health: 2)
                .SetEnergyCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .SetTitle(LobotomyPlugin.ModAssembly, "doubtY_title.png")
                .AddAbilities(Piercing.ID)
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
                .AddAbilities(Piercing.ID)
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
                .AddAbilities(Piercing.ID)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .SetEvolve(infoB, 1)
                .SetOrdealCard(OrdealType.Green)
                .Build();

            CardInfo down = CardManager.New(LobotomyPlugin.pluginPrefix, doubtProcessDown, "Process of Understanding",
                attack: 0, health: 4)
                .SetEnergyCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName51)
                .AddAbilities(Piercing.ID, Ability.Transformer)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .SetOrdealCard(OrdealType.Green)
                .Build();

            CardInfo down2 = CardManager.New(LobotomyPlugin.pluginPrefix, doubtProcessDown2, "Process of Understanding",
                attack: 0, health: 4)
                .SetEnergyCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName51)
                .AddAbilities(Piercing.ID, Ability.Transformer)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .SetOrdealCard(OrdealType.Green)
                .Build();

            CardInfo pro = CardManager.New(LobotomyPlugin.pluginPrefix, doubtProcess, OrdealUtils.GetOrdealTitle(OrdealType.Green, 1),
                attack: 3, health: 4)
                .SetEnergyCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName5)
                .AddAbilities(Piercing.ID, Ability.Transformer)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeMechanical)
                .SetOrdealCard(OrdealType.Green)
                .SetEvolve(down, 2)
                .Build();

            down.SetEvolve(pro, 1);
            down2.SetEvolve(pro, 2);
            CardManager.New(LobotomyPlugin.pluginPrefix, whereWeReach, OrdealUtils.GetOrdealTitle(OrdealType.Green, 2),
                attack: 0, health: 7)
                .SetEnergyCost(5)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName6)
                .AddAbilities(Life.ID, Challenging.ID)
                .AddAppearances(ForcedGreenEmission.appearance)
                .AddTribes(TribeMechanical)
                .AddTraits(Trait.Uncuttable, Trait.Structure, ImmuneToInstaDeath)
                .SetTerrain()
                .SetOrdealCard(OrdealType.Green)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, lilHelix, "Li'l Helix",
                attack: 1, health: 1)
                .SetEnergyCost(6)
                .SetPortraits(LobotomyPlugin.ModAssembly, "lilHelix")
                .AddAbilities(Ability.AllStrike)
                .AddAppearances(ForcedGreenEmission.appearance)
                .AddTribes(TribeMechanical)
                .AddTraits(Trait.Structure)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, lastHelix, string.Empty,
                attack: 0, health: 20)
                .AddAbilities(Ability.Reach, TowerAbility.ID, Ability.AllStrike, Challenging.ID)
                .AddTribes(TribeMechanical)
                .AddTraits(Ordeal, Trait.Giant, Trait.Uncuttable, Trait.Structure, ImmuneToInstaDeath, ImmuneToAilments, NotStoredByScenario)
                .SetAnimatedPortrait(AssetManager.GetAnimatedPortraitPrefab("LastHelixPortrait"))
                .SetTerrain(false)
                .SetOrdealCard(OrdealType.Green)
                .AddSpecialAbilities(SpecialTriggeredAbility.GiantCard)
                .AddAppearances(GiantTowerAppearance.appearance)
                .SetUniqueCopycat(lilHelix)
                .Build();
        }
    }
}