using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string fruitUnderstanding = "wstl_fruitUnderstanding";
        public const string grantUsLove = "wstl_fruitGrantLove";
        public const string grantMeSize = "wstl_grantMeSize";

        public const string godDelusionR = "wstl_godDelusionR";
        public const string godDelusionW = "wstl_godDelusionW";
        public const string godDelusionB = "wstl_godDelusionB";
        public const string godDelusionP = "wstl_godDelusionP";

        private static void Cards_VioletOrdeal() {
            CardManager.New(LobotomyPlugin.pluginPrefix, fruitUnderstanding, OrdealUtils.GetOrdealTitle(OrdealType.Violet, 0),
                attack: 0, health: 4)
                .SetBonesCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, "fruitUnderstanding")
                .SetTitle(LobotomyPlugin.ModAssembly, "fruitUnderstanding_title.png")
                .AddAbilities(StartingDecay.ability, StartingDecay.ability, Understanding.ability, Bleachproof.ability)
                .AddAppearances(ForcedPurpleEmission.appearance)
                .AddTribes(TribeDivine)
                .AddTraits(Trait.Uncuttable)
                .SetOrdealCard(OrdealType.Violet)
                .Build();

            CardInfo mini = CardManager.New(LobotomyPlugin.pluginPrefix, grantMeSize, "Grant Me Size",
                attack: 0, health: 4)
                .SetBonesCost(8)
                .SetPortraits(LobotomyPlugin.ModAssembly, "grantUsLove")
                .AddAbilities(Ability.Evolve, Ability.MadeOfStone)
                .AddAppearances(ForcedPurpleEmission.appearance)
                .AddTribes(TribeDivine)
                .Build(overrideCardChoice: true);
            mini.SetEvolve(mini, 2, new List<CardModificationInfo>() { new(1, 0) });

            CardInfo love = CardManager.New(LobotomyPlugin.pluginPrefix, grantUsLove, OrdealUtils.GetOrdealTitle(OrdealType.Violet, 1),
                attack: 0, health: 8)
                .SetBonesCost(16)
                .AddAbilities(IntenseVolley.ability, ExplosiveOpening.ability, Ability.Evolve, Challenging.ability)
                .AddAppearances(ForcedPurpleEmission.appearance)
                .AddTribes(TribeDivine)
                .AddTraits(Trait.Uncuttable, Trait.Structure, ImmuneToInstaDeath)
                .SetOrdealCard(OrdealType.Violet)
                .SetAnimatedPortrait(AssetManager.GetAnimatedPortraitPrefab("GrantUsLovePortrait"))
                .SetMiniGiant()
                .SetMiniGiantEmission(TextureLoader.LoadTextureFromFile("grantUsLove_emission.png", LobotomyPlugin.ModAssembly))
                .SetUniqueCopycat(grantMeSize)
                .Build();
            love.SetEvolve(love, 2, new List<CardModificationInfo>() { new(1, 0) });

            CardManager.New(LobotomyPlugin.pluginPrefix, godDelusionR, OrdealUtils.GetOrdealTitle(OrdealType.Violet, 3),
                 attack: 0, health: 10)
                 .SetBonesCost(4)
                 //.SetPortraits(LobotomyPlugin.ModAssembly, "godDelusionR")
                 .AddAbilities(Ability.Reach, Delusion.ability, GodRed.ability, Challenging.ability)
                 .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                 .AddTribes(TribeDivine)
                 .AddTraits(Trait.Uncuttable, Trait.Structure, ImmuneToInstaDeath)
                 .SetOrdealCard(OrdealType.Violet)
                 .SetTerrain()
                 .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, godDelusionW, OrdealUtils.GetOrdealTitle(OrdealType.Violet, 3),
                 attack: 0, health: 10)
                 .SetBonesCost(4)
                 //.SetPortraits(LobotomyPlugin.ModAssembly, "godDelusionW")
                 .AddAbilities(Ability.Reach, Delusion.ability, GodWhite.ability, Challenging.ability)
                 .AddAppearances(ForcedWhiteEmission.appearance)
                 .AddTribes(TribeDivine)
                 .AddTraits(Trait.Uncuttable, Trait.Structure, ImmuneToInstaDeath)
                 .SetOrdealCard(OrdealType.Violet)
                 .SetTerrain()
                 .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, godDelusionB, OrdealUtils.GetOrdealTitle(OrdealType.Violet, 3),
                 attack: 0, health: 10)
                 .SetBonesCost(4)
                 //.SetPortraits(LobotomyPlugin.ModAssembly, "godDelusionB")
                 .AddAbilities(Ability.Reach, Delusion.ability, GodBlack.ability, Challenging.ability)
                 .AddAppearances(ForcedPurpleEmission.appearance)
                 .AddTribes(TribeDivine)
                 .AddTraits(Trait.Uncuttable, Trait.Structure, ImmuneToInstaDeath)
                 .SetOrdealCard(OrdealType.Violet)
                 .SetTerrain()
                 .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, godDelusionP, OrdealUtils.GetOrdealTitle(OrdealType.Violet, 3),
                 attack: 0, health: 10)
                 .SetBonesCost(4)
                 //.SetPortraits(LobotomyPlugin.ModAssembly, "godDelusionP")
                 .AddAbilities(Ability.Reach, Delusion.ability, GodPale.ability, Challenging.ability)
                 .AddAppearances(ForcedEmission.appearance)
                 .AddTribes(TribeDivine)
                 .AddTraits(Trait.Uncuttable, Trait.Structure, ImmuneToInstaDeath)
                 .SetOrdealCard(OrdealType.Violet)
                 .SetTerrain()
                 .Build();
        }
    }
}