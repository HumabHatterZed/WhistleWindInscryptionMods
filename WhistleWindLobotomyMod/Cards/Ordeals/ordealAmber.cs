using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
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
            CardInfo food = CardManager.New(LobotomyPlugin.pluginPrefix, perfectFood, OrdealUtils.GetOrdealTitle(OrdealType.Amber, 0),
                attack: 1, health: 1)
                .SetBonesCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities()
                .AddAppearances(ForcedOrangeEmission.appearance)
                .AddTribes(Tribe.Insect)
                .SetOrdealCard(Opponents.OrdealType.Amber)
                .Build();

            CardInfo chain = CardManager.New(LobotomyPlugin.pluginPrefix, foodChain, OrdealUtils.GetOrdealTitle(OrdealType.Amber, 2),
                attack: 2, health: 5)
                .SetBonesCost(18)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Challenging.ability, Food.ability)
                .AddAppearances(ForcedOrangeEmission.appearance)
                .AddTribes(Tribe.Insect)
                .AddTraits(PriorityMovement)
                .SetOrdealCard(Opponents.OrdealType.Amber)
                .Build();

            CardInfo meal = CardManager.New(LobotomyPlugin.pluginPrefix, eternalMeal, OrdealUtils.GetOrdealTitle(OrdealType.Amber, 3),
                attack: 2, health: 20)
                .SetBonesCost(40)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(Challenging.ability, Survival.ability)
                .AddAppearances(ForcedOrangeEmission.appearance)
                .AddTribes(Tribe.Insect)
                .AddTraits(Trait.Uncuttable, ImmuneToInstaDeath, PriorityMovement)
                .SetOrdealCard(Opponents.OrdealType.Amber)
                .Build();
        }
    }
}