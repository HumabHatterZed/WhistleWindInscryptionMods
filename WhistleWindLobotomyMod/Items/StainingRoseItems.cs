using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Items;
using InscryptionAPI.Items.Extensions;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod {
    public partial class Items {
        private static void StainingRose() {
            GameObject obj = AssetManager.GetGameObject("FizzyLifter");
            //GameObject obj2 = AssetManager.GetGameObject("OceanSoda");
            //GameObject obj3 = AssetManager.GetGameObject("PotshotPop");
            //GameObject obj4 = AssetManager.GetGameObject("SurefireDrink");
            ConsumableItemManager.New(LobotomyPlugin.pluginGuid,
                "Staining Rose (Dry)",
                "Select a card on the board to gain Airborne for this and next turn.",
                TextureLoader.LoadTextureFromFile("itemRose1.png", LobotomyPlugin.ModAssembly),
                typeof(FizzyLifterItem), obj)
                .SetPowerLevel(99)
                //.SetPlacedSoundId("can_hit")
                //.SetPickupSoundId("can_hit")
                .SetAct1();

            ConsumableItemManager.New(LobotomyPlugin.pluginGuid,
                "Staining Rose (Wet)",
                "Select a card on the board to gain Airborne for this and next turn.",
                TextureLoader.LoadTextureFromFile("itemRose2.png", LobotomyPlugin.ModAssembly),
                typeof(FizzyLifterItem), obj)
                .SetPowerLevel(99)
                //.SetPlacedSoundId("can_hit")
                //.SetPickupSoundId("can_hit")
                .SetAct1();

            ConsumableItemManager.New(LobotomyPlugin.pluginGuid,
                "Staining Rose (Drenched)",
                "Select a card on the board to gain Airborne for this and next turn.",
                TextureLoader.LoadTextureFromFile("itemRose3.png", LobotomyPlugin.ModAssembly),
                typeof(FizzyLifterItem),
                obj)
                .SetPowerLevel(99)
                //.SetPlacedSoundId("can_hit")
                //.SetPickupSoundId("can_hit")
                .SetAct1();

            ConsumableItemManager.New(LobotomyPlugin.pluginGuid,
                "Staining Rose (Drained)",
                "Select a card on the board to gain Airborne for this and next turn.",
                TextureLoader.LoadTextureFromFile("itemRose4.png", LobotomyPlugin.ModAssembly),
                typeof(FizzyLifterItem), obj)
                .SetPowerLevel(99)
                //.SetPlacedSoundId("can_hit")
                //.SetPickupSoundId("can_hit")
                .SetAct1();
        }
    }
}
