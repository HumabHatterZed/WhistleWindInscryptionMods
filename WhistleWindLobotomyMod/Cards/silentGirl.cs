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

            NewCard(silentGirl, "Silent Girl", "A girl hiding a hammer and nail behind her back.",
                attack: 2, health: 2, blood: 2)
                .SetPortraits(silentGirl)
                .AddAbilities(Persecutor.ability)
                .AddTribes(TribeAnthropoid)
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.Teth, ModCardType.Ruina);
        }
    }
}