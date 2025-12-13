using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        /// <summary>
        /// 3/9 Omni-, Split Strike
        /// </summary>
        public const string apocalypseBird = "wstl_apocalypseBird";
        private static void ApocalypseBird_O0263() {
            string textureName = "apocalypseBird";
            CardManager.New(LobotomyPlugin.pluginPrefix, apocalypseBird, "Apocalypse Bird",
                attack: 3, health: 9)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetBloodCost(4)
                .AddAbilities(Ability.AllStrike, Ability.TriStrike)
                .AddTribes(Tribe.Bird)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .SetNodeRestrictions(true, false, false, true)
                .SetDefaultEvolutionName("Final Apocalypse Bird")
                .SetEventCard(true)
                .Build(riskLevel: RiskLevel.Aleph, overrideCardChoice: true);
        }
    }
}