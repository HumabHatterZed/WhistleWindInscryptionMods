using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string silentGirl = "wstl_silentGirl";
        private static void SilentGirl_O010()
        {
            string textureName = "silentGirl";
            CardManager.New(LobotomyPlugin.pluginPrefix, silentGirl, "Silent Girl",
                attack: 2, health: 2, "A girl hiding a hammer and nail behind her back.")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Persecutor.ability)
                .AddTribes(TribeAnthropoid)
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Rare, RiskLevel.Teth, true);
        }
    }
}