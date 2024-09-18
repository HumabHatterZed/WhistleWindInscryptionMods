using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private const string oneSinName = "One Sin and Hundreds of Good Deeds";
        private const string oneSin = "oneSin";
        private void Card_OneSin_O0303()
        {
            CardManager.New(pluginPrefix, oneSin, oneSinName,
                attack: 0, health: 1, "A floating skull. Its hollow sockets see through you.")
                .SetBonesCost(1)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, oneSin)
                .AddAbilities(Martyr.ability)
                .AddTribes(TribeDivine)
                .SetDefaultEvolutionName(oneSinName)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}