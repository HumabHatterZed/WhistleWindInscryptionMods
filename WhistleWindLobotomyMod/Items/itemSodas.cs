using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Items;
using InscryptionAPI.Items.Extensions;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod
{
    public partial class Items
    {
        private static void SodaCans()
        {
            GameObject obj = AssetManager.AssetBundle.LoadAsset<GameObject>("FizzyLifter");
            GameObject obj2 = AssetManager.AssetBundle.LoadAsset<GameObject>("OceanSoda");
            GameObject obj3 = AssetManager.AssetBundle.LoadAsset<GameObject>("PotshotPop");
            GameObject obj4 = AssetManager.AssetBundle.LoadAsset<GameObject>("SurefireDrink");
            ConsumableItemManager.New(LobotomyPlugin.pluginGuid,
                "Fizzy Lifting Drink",
                "To the user: Select a card on the board to gain Airborne for 3 turns.", TextureLoader.LoadTextureFromFile("itemFizzyLifter.png", LobotomyPlugin.ModAssembly), typeof(FizzyLifterItem), obj)
                .SetPowerLevel(1)
                .SetPlacedSoundId("can_hit")
                .SetPickupSoundId("can_hit")
                .SetAct1();

            ConsumableItemManager.New(LobotomyPlugin.pluginGuid,
                "Ocean Flavoured Soda",
                "To the user: Select a card on the board to gain Waterborne for 3 turns.", TextureLoader.LoadTextureFromFile("itemOceanSoda.png", LobotomyPlugin.ModAssembly), typeof(OceanSodaItem), obj2)
                .SetPowerLevel(1)
                .SetPlacedSoundId("can_hit")
                .SetPickupSoundId("can_hit")
                .SetAct1();

            ConsumableItemData item = ConsumableItemManager.New(LobotomyPlugin.pluginGuid,
                "Potshot Pop",
                "To the user: Select a card on the board to gain Sentry for this and next turn.", TextureLoader.LoadTextureFromFile("itemPotshotPop.png", LobotomyPlugin.ModAssembly), typeof(PotshotPopItem), obj3)
                .SetPowerLevel(2)
                .SetPlacedSoundId("can_hit")
                .SetPickupSoundId("can_hit")
                .SetAct1();

            ConsumableItemData item2 = ConsumableItemManager.New(LobotomyPlugin.pluginGuid,
                "Surefire Soda",
                "To the user: Select a card on the board to gain Sniper for this turn.", TextureLoader.LoadTextureFromFile("itemSurefireDrink.png", LobotomyPlugin.ModAssembly), typeof(SurefireDrinkItem), obj4)
                .SetPlacedSoundId("can_hit")
                .SetPickupSoundId("can_hit")
                .SetPowerLevel(4)
                .SetAct1();

            if (LobotomyConfigManager.ReskinSigils)
            {
                item.SetRulebookDescription("To the user: Select a card on the board to gain Quick Draw for this and next turn.");
                item2.SetRulebookDescription("To the user: Select a card on the board to gain Marksman for this turn.");
            }
        }
    }
}
