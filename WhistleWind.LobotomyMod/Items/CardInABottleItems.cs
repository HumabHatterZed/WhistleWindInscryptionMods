using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void AddBottleCards()
        {
            LobotomyItemHelper.CreateBottleItem(
                Artwork.itemBottledTrain, "BottledTrain", "wstl_BottledExpressHellTrain",
                "A train with no destination, leaving behind not even bone. Use it to clear the board in a pinch.",
                powerLevel: 5, rulebookName: "Train in a Bottle");
        }
    }
}
