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
        private static void Cards_VioletOrdeal() {
            string textureName = "fruitUnderstanding";
            CardManager.New(LobotomyPlugin.pluginPrefix, fruitUnderstanding, OrdealUtils.GetOrdealTitle(OrdealType.Violet, 0),
                attack: 0, health: 4)
                .SetBonesCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
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
        }
    }
}