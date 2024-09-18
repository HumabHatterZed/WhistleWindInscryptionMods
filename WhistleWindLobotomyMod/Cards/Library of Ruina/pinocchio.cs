using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Pinocchio_F01112()
        {
            const string pinocchio = "pinocchio";

            CardManager.New(pluginPrefix, pinocchio, "Pinocchio",
                attack: 0, health: 1, "A wooden doll that mimics the beasts it encounters. Can you see through its lie?")
                .SetBonesCost(1)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, pinocchio)
                .AddAbilities(Copycat.ability)
                .AddTribes(TribeBotanic)
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}