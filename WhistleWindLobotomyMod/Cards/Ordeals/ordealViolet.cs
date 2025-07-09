using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string fruitUnderstanding = "wstl_fruitUnderstanding";
        public const string grantUsLove = "wstl_fruitGrantLove";
        private static void Cards_VioletOrdeal() {
            string textureName = "fruitUnderstanding";
            CardManager.New(LobotomyPlugin.pluginPrefix, fruitUnderstanding, "Fruit of Understanding",
                attack: 0, health: 3)
                .SetBonesCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetTitle(LobotomyPlugin.ModAssembly, "fruitUnderstanding_title.png")
                .AddAbilities(StartingDecay.ability, StartingDecay.ability, StartingDecay.ability, Understanding.ability, Bleachproof.ability)
                .AddAppearances(ForcedPurpleEmission.appearance)
                .AddTribes(TribeDivine)
                .AddTraits(Ordeal, Trait.Uncuttable)
                .Build();

            CardInfo love = CardManager.New(LobotomyPlugin.pluginPrefix, grantUsLove, "Grant Us Love",
                attack: 0, health: 8)
                .SetBonesCost(16)
                .AddAbilities(IntenseVolley.ability, ExplosiveOpening.ability, Ability.Evolve, Challenging.ability)
                .AddAppearances(ForcedPurpleEmission.appearance)
                .AddTribes(TribeDivine)
                .AddTraits(Ordeal, Trait.Uncuttable, Trait.Structure, ImmuneToInstaDeath)
                .SetAnimatedPortrait(LobOpponentUtils.GrantUsLovePrefab)
                .SetMiniGiant()
                .SetMiniGiantEmission(TextureLoader.LoadTextureFromFile("grantUsLove_emission.png", LobotomyPlugin.ModAssembly))
                .Build();

            love.SetEvolve(love, 2, new List<CardModificationInfo>() { new(1, 0) });
        }
    }
}