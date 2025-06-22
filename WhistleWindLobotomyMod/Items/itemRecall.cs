using InscryptionAPI.Card;
using InscryptionAPI.Items;
using InscryptionAPI.Items.Extensions;


namespace WhistleWindLobotomyMod {
    public partial class Items {
        private static void RecallBottle() {
            ConsumableItemManager.NewCardInABottle(LobotomyPlugin.pluginGuid, "wstl_RETURN_CARD_WEAK")
                .SetPowerLevel(1)
                .SetRulebookName("Single Recall Bottle")
                .SetRulebookDescription("A Single Recall is created in your hand. A Single Recall can return a card on your side of the board to your hand.")
                .SetLearnItemDescription("This will let you return a played card to your hand. Returned cards will cost [c:bR]Bones[c:] to replay.");

            ConsumableItemManager.NewCardInABottle(LobotomyPlugin.pluginGuid, "wstl_RETURN_CARD_ALL_WEAK")
                .SetPowerLevel(3)
                .SetRulebookName("Total Recall Bottle")
                .SetRulebookDescription("A Total Recall is created in your hand. A Total Recall will return all cards on your side of the board to your hand.")
                .SetLearnItemDescription("This will return all your cards to your hand. Returned cards will cost [c:bR]Bones[c:] to replay.");
        }
    }
}
