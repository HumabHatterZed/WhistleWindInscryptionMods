using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Schadenfreude_O0576()
        {
            const string schadenfreude = "schadenfreude";

            CardManager.New(pluginPrefix, schadenfreude, "Schadenfreude",
                attack: 1, health: 1, "A strange machine. You can feel someone's persistent gaze through the keyhole.")
                .SetEnergyCost(3)
                .SetPortraits(ModAssembly, schadenfreude)
                .AddAbilities(Ability.Sentry)
                .AddTribes(TribeMechanical)
                .SetDefaultEvolutionName("Große Schadenfreude")
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}