using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WallLady_F0118()
        {
            const string wallLady = "wallLady";

            CardManager.New(pluginPrefix, wallLady, "The Lady Facing the Wall",
                attack: 1, health: 2, "A deep sorrow, grown to obsession. Perhaps it's best to leave her be.")
                .SetBonesCost(4)
                .SetPortraits(ModAssembly, wallLady)
                .AddAbilities(Ability.Sharp)
                .AddTribes(TribeAnthropoid)
                .SetDefaultEvolutionName("The Elder Lady Facing the Wall")
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}