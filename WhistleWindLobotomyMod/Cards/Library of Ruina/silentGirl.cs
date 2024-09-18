using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SilentGirl_O010()
        {
            const string silentGirl = "silentGirl";

            CardManager.New(pluginPrefix, silentGirl, "Silent Girl",
                attack: 2, health: 2, "A girl hiding a hammer and nail behind her back.")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, silentGirl)
                .AddAbilities(Persecutor.ability)
                .AddTribes(TribeAnthropoid)
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Rare, RiskLevel.Teth, true);
        }
    }
}