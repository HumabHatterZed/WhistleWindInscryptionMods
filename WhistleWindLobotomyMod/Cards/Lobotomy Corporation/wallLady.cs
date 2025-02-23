using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string wallLady = "wstl_wallLady";
        private static void WallLady_F0118()
        {
            string textureName = "wallLady";
            CardManager.New(LobotomyPlugin.pluginPrefix, wallLady, "The Lady Facing the Wall",
                attack: 1, health: 2, "A deep sorrow, grown to obsession. Perhaps it's best to leave her be.")
                .SetBonesCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Sharp)
                .AddTribes(TribeAnthropoid)
                .SetDefaultEvolutionName("The Elder Lady Facing the Wall")
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}