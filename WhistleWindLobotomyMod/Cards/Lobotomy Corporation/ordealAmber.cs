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
        private void Cards_AmberOrdeal()
        {
            const string perfectFood = "perfectFood";
            const string foodChain = "foodChain";
            const string eternalMeal = "eternalMeal";

            CardInfo food = CardManager.New(pluginPrefix, perfectFood, "The Perfect Food",
                attack: 1, health: 1)
                .SetBonesCost(2)
                .SetPortraits(ModAssembly, perfectFood)
                .AddAbilities()
                //.AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(Tribe.Insect)
                .AddTraits(Ordeal)
                .Build();

            CardInfo chain = CardManager.New(pluginPrefix, foodChain, "The Food Chain",
                attack: 3, health: 4)
                .SetBonesCost(12)
                .SetPortraits(ModAssembly, foodChain)
                .AddAbilities(Food.ability, Challenging.ability)
                //.AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(Tribe.Insect)
                .AddTraits(Ordeal)
                .Build();

            CardInfo meal = CardManager.New(pluginPrefix, eternalMeal, "The Eternal Meal",
                attack: 2, health: 20)
                .SetBonesCost(40)
                .SetPortraits(ModAssembly, eternalMeal)
                .AddAbilities(Survival.ability, Challenging.ability)
                //.AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(Tribe.Insect)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .Build();
        }
    }
}