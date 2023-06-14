using DiskCardGame;
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
            CardInfo RANDOM_PLACEHOLDERCard = NewCard(RANDOM_PLACEHOLDER)
                .SetPortraits(RANDOM_PLACEHOLDER)
                .SetStatIcon(SigilPower.Icon);

            CreateCard(RANDOM_PLACEHOLDERCard, nonChoice: true);
        }
    }
}