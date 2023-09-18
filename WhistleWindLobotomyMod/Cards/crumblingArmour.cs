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

            NewCard(crumblingArmour, "Crumbling Armour", "A suit of armour that rewards the brave and punishes the cowardly.",
                attack: 0, health: 3, bones: 4)
                .SetPortraits(crumblingArmour)
                .AddAbilities(Courageous.ability)
                .SetTerrain(false)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}