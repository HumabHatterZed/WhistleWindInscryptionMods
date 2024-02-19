using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_RANDOM_PLACEHOLDER()
        {
            const string RANDOM_PLACEHOLDER = "RANDOM_PLACEHOLDER";
            NewCard(RANDOM_PLACEHOLDER)
                .SetPortraits(RANDOM_PLACEHOLDER)
                .SetStatIcon(SigilPower.Icon)
                .Build(nonChoice: true);
        }
    }
}