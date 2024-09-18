using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SpiderBud_O0243()
        {
            const string spiderBud = "spiderBud";

            CardManager.New(pluginPrefix, spiderBud, "Spider Bud",
                attack: 0, health: 3, "A grotesque mother of spiders. Its children are small but grow quickly.")
                .SetBonesCost(4)
                .SetPortraits(ModAssembly, spiderBud)
                .AddAbilities(BroodMother.ability)
                .AddTribes(Tribe.Insect)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}