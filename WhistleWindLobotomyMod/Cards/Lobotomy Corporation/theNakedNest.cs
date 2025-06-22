using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string theNakedNest = "wstl_theNakedNest";
        private static void TheNakedNest_O0274() {
            string textureName = "theNakedNest";
            CardManager.New(LobotomyPlugin.pluginPrefix, theNakedNest, "The Naked Nest",
                attack: 0, health: 3, "They can enter your body through any aperture.")
                .SetBonesCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(SerpentsNest.ability)
                .AddTraits(Trait.KillsSurvivors)
                .SetDefaultEvolutionName("The Elder Naked Nest")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}