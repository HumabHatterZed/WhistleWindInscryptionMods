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
                .AddTraits(Trait.Ant)
                .SetOrdealCard(Opponents.OrdealType.Amber)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, foodChain, OrdealUtils.GetOrdealTitle(OrdealType.Amber, 2),
                attack: 0, health: 4)
                .SetBonesCost(18)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Challenging.ability, Food.ability)
                .AddAppearances(ForcedOrangeEmission.appearance)
                .AddTribes(Tribe.Insect)
                .SetStatIcon(SpecialStatIcon.Ants)
                .AddTraits(PriorityMovement, Trait.Ant)
                .SetOrdealCard(Opponents.OrdealType.Amber)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, eternalMeal, OrdealUtils.GetOrdealTitle(OrdealType.Amber, 3),
                attack: 0, health: 8)
                .SetBonesCost(40)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(Ability.AllStrike, Ability.Reach, Challenging.ability, Survival.ability)
                .AddAppearances(ForcedOrangeEmission.appearance)
                .AddTribes(Tribe.Insect)
                .SetStatIcon(SpecialStatIcon.Ants)
                .AddTraits(Trait.Uncuttable, ImmuneToInstaDeath, PriorityMovement, Trait.Ant)
                .SetMiniGiant()
                .SetOrdealCard(Opponents.OrdealType.Amber)
                .Build();
        }
    }
}