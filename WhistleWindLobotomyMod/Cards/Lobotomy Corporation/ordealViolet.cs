using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Cards_VioletOrdeal()
        {
            const string fruitUnderstanding = "fruitUnderstanding";
            const string grantUsLove = "grantUsLove";

            CardInfo fruit = CardManager.New(pluginPrefix, fruitUnderstanding, "Fruit of Understanding",
                attack: 0, health: 4)
                .SetBonesCost(4)
                .SetPortraits(ModAssembly, fruitUnderstanding)
                .SetTitle(ModAssembly, "fruitUnderstanding_title.png")
                .AddAbilities(StartingDecay.ability, StartingDecay.ability, Understanding.ability, Bleachproof.ability)
                .AddAppearances(ForcedPurpleEmission.appearance)
                .AddTribes(TribeDivine)
                .AddTraits(Ordeal)
                .Build();

            CardInfo love = CardManager.New(pluginPrefix, grantUsLove, "Grant Us Love",
                attack: 1, health: 10)
                .SetBonesCost(12)
                .SetPortraits(ModAssembly, grantUsLove)
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