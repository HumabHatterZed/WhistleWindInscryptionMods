using DiskCardGame;
using InscryptionAPI.Items;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Core.Helpers
{
    public static class LobotomyItemHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static void CreateItem<T>(
            byte[] rulebookIcon, string internalName, string rulebookName, string rulebookDescription, string nodeDialogue,
            ConsumableItemManager.ModelType modelType = ConsumableItemManager.ModelType.BasicRune,
            int powerLevel = 1, bool regionSpecific = false, bool notRandom = false)
        {
            ItemHelper.CreateItem<T>(
                LobotomyPlugin.pluginGuid, internalName, rulebookName, rulebookDescription, rulebookIcon,
                nodeDialogue, modelType, powerLevel, regionSpecific, notRandom);
        }
        public static void CreateItemWithPrefab<T>(
            byte[] rulebookIcon, string internalName, string rulebookName, string rulebookDescription, string nodeDialogue,
            GameObject prefab, int powerLevel = 1, bool regionSpecific = false, bool notRandom = false)
        {
            ItemHelper.CreateItemWithPrefab<T>(
                LobotomyPlugin.pluginGuid, internalName, rulebookName, rulebookDescription, rulebookIcon,
                nodeDialogue, prefab, powerLevel, regionSpecific, notRandom);
        }
        public static ConsumableItemData CreateBottleItem(
            byte[] rulebookIcon, string internalName, string cardByName,
            string nodeDialogue, int powerLevel = 1, string rulebookName = null)
        {
            return ItemHelper.CreateBottleItem(LobotomyPlugin.pluginGuid, internalName, cardByName, rulebookIcon, nodeDialogue, powerLevel, rulebookName);
        }
    }
}
