using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_OldLady_O0112()
        {
            const string oldLady = "oldLady";

            CardManager.New(pluginPrefix, oldLady, "Old Lady",
                attack: 1, health: 2, "An aged storyteller. She can tell you any tale, even those that can't exist.")
                .SetBonesCost(4)
                .SetPortraits(ModAssembly, oldLady)
                .AddAbilities(Ability.DebuffEnemy)
                .AddTribes(TribeAnthropoid)
                .SetDefaultEvolutionName("Elderly Lady")
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}