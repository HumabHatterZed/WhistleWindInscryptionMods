using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Yin_O05102()
        {
            const string yin = "yin";

            CardManager.New(pluginPrefix, yin, "Yin",
                attack: 2, health: 3, "A black pendant in search of its missing half.")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, yin)
                .SetAltPortraits(ModAssembly, "yinAlt")
                .AddAbilities(Ability.Strafe, Ability.Submerge)
                .AddAppearances(AlternateBattlePortrait.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}