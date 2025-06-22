using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string schadenfreude = "wstl_schadenfreude";
        private static void Schadenfreude_O0576() {
            string textureName = "schadenfreude";
            CardManager.New(LobotomyPlugin.pluginPrefix, schadenfreude, "Schadenfreude",
                attack: 1, health: 1, "A strange machine. You can feel someone's persistent gaze through the keyhole.")
                .SetEnergyCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Sentry)
                .AddTribes(TribeMechanical)
                .SetDefaultEvolutionName("Große Schadenfreude")
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}