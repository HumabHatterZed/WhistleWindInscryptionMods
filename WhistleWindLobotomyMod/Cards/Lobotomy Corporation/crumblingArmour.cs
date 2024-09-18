using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_CrumblingArmour_O0561()
        {
            const string crumblingArmour = "crumblingArmour";

            CardManager.New(pluginPrefix, crumblingArmour, "Crumbling Armour",
                attack: 0, health: 3, "A suit of armour that rewards the brave and punishes the cowardly.")
                .SetBonesCost(4)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, crumblingArmour)
                .AddAbilities(Courageous.ability)
                .SetTerrain(true)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}