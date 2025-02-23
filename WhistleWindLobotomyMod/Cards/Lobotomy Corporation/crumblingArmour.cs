using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string crumblingArmour = "wstl_crumblingArmour";
        private static void CrumblingArmour_O0561()
        {
            string name = "Crumbling Armour";
            string desc = "A suit of armour that rewards the brave and punishes the cowardly.";
            string textureName = "crumblingArmour";
            CardManager.New(LobotomyPlugin.pluginPrefix, crumblingArmour, name,
                attack: 0, health: 3, desc)
                .SetBonesCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Courageous.ability)
                .SetTerrain(true)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 3, desc)
                .SetBonesCost(4)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Courageous.ability)
                .SetTerrain(true)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth);
        }
    }
}