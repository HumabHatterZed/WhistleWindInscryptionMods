using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SnowQueen_F0137()
        {
            const string snowQueen = "snowQueen";

            CardManager.New(pluginPrefix, snowQueen, "The Snow Queen",
                attack: 2, health: 2, "A queen from far away. Those who enter her palace never leave.")
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, snowQueen)
                .AddAbilities(FrostRuler.ability)
                .AddTribes(TribeFae)
                .SetDefaultEvolutionName("The Snow Empress")
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}