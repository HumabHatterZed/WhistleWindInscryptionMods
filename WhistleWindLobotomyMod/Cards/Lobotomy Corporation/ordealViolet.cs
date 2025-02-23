using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string fruitUnderstanding = "wstl_fruitUnderstanding";
        public const string grantUsLove = "wstl_fruitGrantLove";
        private static void Cards_VioletOrdeal()
        {
            string textureName = "fruitUnderstanding";
            string textureName2 = "grantUsLove";
            //string textureName3 = ""
            CardInfo fruit = CardManager.New(LobotomyPlugin.pluginPrefix, fruitUnderstanding, "Fruit of Understanding",
                attack: 0, health: 4)
                .SetBonesCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetTitle(LobotomyPlugin.ModAssembly, "fruitUnderstanding_title.png")
                .AddAbilities(StartingDecay.ability, StartingDecay.ability, Understanding.ability, Bleachproof.ability)
                .AddAppearances(ForcedPurpleEmission.appearance)
                .AddTribes(TribeDivine)
                .AddTraits(Ordeal)
                .Build();

            CardInfo love = CardManager.New(LobotomyPlugin.pluginPrefix, grantUsLove, "Grant Us Love",
                attack: 1, health: 10)
                .SetBonesCost(12)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Ability.AllStrike, ExplosiveOpening.ability, Ability.Evolve, Challenging.ability)
                .AddAppearances(ForcedPurpleEmission.appearance)
                .AddSpecialAbilities(MiniGiantCard.Id)
                .AddTribes(TribeDivine)
                .AddTraits(Ordeal, Trait.Uncuttable, Trait.Structure, ImmuneToInstaDeath)
                .Build();

            love.SetEvolve(love, 2, new List<CardModificationInfo>() { new(1, 0) });
        }
    }
}