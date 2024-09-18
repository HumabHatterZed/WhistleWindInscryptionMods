using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Items;
using InscryptionAPI.Items.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Item_RecallBottle()
        {
            ConsumableItemManager.NewCardInABottle(pluginGuid, "wstl_RETURN_CARD_WEAK")
                .SetPowerLevel(1)
                .SetRulebookDescription("A Single Recall is created in your hand. A Single Recall can return a card on your side of the board to your hand.")
                .SetLearnItemDescription("This will let you return a played card to your hand. Returned cards will cost [c:bR]Bones[c:] to replay.");
            
            ConsumableItemManager.NewCardInABottle(pluginGuid, "wstl_RETURN_CARD_ALL_WEAK")
                .SetPowerLevel(3)
                .SetRulebookDescription("A Total Recall is created in your hand. A Total Recall will return all cards on your side of the board to your hand.")
                .SetLearnItemDescription("This will return all your cards to your hand. Returned cards will cost [c:bR]Bones[c:] to replay.");
        }
    }
}
