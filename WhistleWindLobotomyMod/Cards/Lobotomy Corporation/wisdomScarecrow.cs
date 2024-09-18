using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WisdomScarecrow_F0187()
        {
            const string wisdomScarecrow = "wisdomScarecrow";

            CardManager.New(pluginPrefix, wisdomScarecrow, "Scarecrow Searching for Wisdom",
                attack: 1, health: 1, "A hollow-headed scarecrow. Blood soaks its straw limbs.")
                .SetBonesCost(4)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, wisdomScarecrow)
                .AddAbilities(Bloodfiend.ability)
                .AddTribes(TribeBotanic)
                .AddTraits(EmeraldCity)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}