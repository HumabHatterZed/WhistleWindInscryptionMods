using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string perfectFood = "wstl_foodPerfect";
        public const string foodChain = "wstl_foodChain";
        public const string eternalMeal = "wstl_foodEternal";
        private static void Cards_AmberOrdeal() {
            string textureName = "perfectFood";
            string textureName2 = "foodChain";
            string textureName3 = "eternalMeal";
            CardInfo food = CardManager.New(LobotomyPlugin.pluginPrefix, perfectFood, "The Perfect Food",
                attack: 1, health: 1)
                .SetBonesCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities()
                //.AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(Tribe.Insect)
                .AddTraits(Ordeal)
                .Build();

            CardInfo chain = CardManager.New(LobotomyPlugin.pluginPrefix, foodChain, "The Food Chain",
                attack: 2, health: 5)
                .SetBonesCost(18)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Food.ability, Challenging.ability)
                //.AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(Tribe.Insect)
                .AddTraits(Ordeal, PriorityMovement)
                .Build();

            CardInfo meal = CardManager.New(LobotomyPlugin.pluginPrefix, eternalMeal, "The Eternal Meal",
                attack: 2, health: 20)
                .SetBonesCost(40)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(Survival.ability, Challenging.ability)
                //.AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(Tribe.Insect)
                .AddTraits(Ordeal, Trait.Uncuttable, ImmuneToInstaDeath)
                .Build();
        }
    }
}