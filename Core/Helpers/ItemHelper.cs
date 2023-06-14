using DiskCardGame;
using InscryptionAPI.Helpers;
using InscryptionAPI.Items;
using InscryptionAPI.Items.Extensions;
using UnityEngine;
using static InscryptionAPI.Items.ConsumableItemManager;

namespace WhistleWind.Core.Helpers
{
    public static class ItemHelper
    {
        public static void CreateItem<T>(
            string pluginGuid, string internalName, string rulebookName, string rulebookDescription,
            string rulebookIcon, string nodeDialogue,
            ModelType modelType = ModelType.BasicRune,
            int powerLevel = 1, bool regionSpecific = false, bool notRandom = false, bool outsideBattle = false)
        {
            Texture2D texture2D = TextureLoader.LoadTextureFromFile(rulebookIcon);
            ConsumableItemData itemData = ScriptableObject.CreateInstance<ConsumableItemData>();

            itemData.SetRulebookName(internalName);
            itemData.SetRulebookDescription(rulebookDescription);
            itemData.SetLearnItemDescription(nodeDialogue);
            itemData.SetRulebookSprite(texture2D.ConvertTexture());
            itemData.SetRegionSpecific(regionSpecific: regionSpecific);
            itemData.SetCanActivateOutsideBattles(outsideBattle);
            itemData.SetNotRandomlyGiven(notRandomlyGiven: notRandom);
            itemData.SetExtendedProperty("PrefabModelType", (int)modelType);
            itemData.SetPickupSoundId("stone_object_up");
            itemData.SetPlacedSoundId("stone_object_hit");
            itemData.SetExamineSoundId("stone_object_hit");
            itemData.SetComponentType(typeof(T));
            itemData.SetPowerLevel(powerLevel);

            ConsumableItemManager.Add(pluginGuid, itemData);

            // ConsumableItemManager uses the rulebook name for the prefab id,
            // so I set the rulebook name to the internal name first
            // and then set it to the actual rulebook name after
            itemData.SetRulebookName(rulebookName);
        }
        public static void CreateItemWithPrefab<T>(
            string pluginGuid, string internalName, string rulebookName, string rulebookDescription,
            string rulebookIcon, string nodeDialogue,
            GameObject prefab,
            int powerLevel = 1, bool regionSpecific = false, bool notRandom = false, bool outsideBattle = false)
        {
            ConsumableItemResource consumableItemResource = new ConsumableItemResource();
            consumableItemResource.FromPrefab(prefab);
            ModelType modelType = RegisterPrefab(pluginGuid, rulebookName, consumableItemResource);
            Transform parent = prefab.transform.parent;
            prefab.transform.SetParent(null);
            Object.DontDestroyOnLoad(prefab);
            prefab.transform.SetParent(parent);
            prefab.SetActive(value: false);

            CreateItem<T>(
                pluginGuid, internalName, rulebookName, rulebookDescription, rulebookIcon,
                nodeDialogue, modelType, powerLevel, regionSpecific, notRandom, outsideBattle);
        }
        public static ConsumableItemData CreateBottleItem(
            string pluginGuid, string internalName, string cardByName, string rulebookIcon,
            string nodeDialogue = "", int powerLevel = 1, string rulebookName = null, bool outsideBattle = false)
        {
            CardInfo cardInfo = CardLoader.GetCardByName(cardByName);
            Texture2D texture2D = TextureLoader.LoadTextureFromFile(rulebookIcon);

            ConsumableItemData itemData = ScriptableObject.CreateInstance<ConsumableItemData>();

            // if a custom rulebook name has been defined, use that
            // otherwise set the name to [displayedName] in a Bottle
            string rulebookName_ = rulebookName ?? cardInfo.displayedName + " in a Bottle";

            // by default assume we're starting with a consonant, and use the game's helpers to do this automatically
            string startingPreposition = "A";
            string definition = $"[define:{cardByName}]";

            // if the displayed name starts with a vowel we'll need to construct the card definition ourself
            if (NameStartsWithVowel(cardInfo.displayedName))
            {
                // proper grammar :)
                startingPreposition = "An";

                // create a string list of abilities
                string abilities = "";
                foreach (Ability ability in cardInfo.Abilities)
                {
                    abilities += ", " + AbilitiesUtil.GetInfo(ability).rulebookName;
                }
                // end the abilities string with a full stop; if there are no abilities, abilities will just be a full stop
                abilities += ".";

                // construct the definition string with the proper grammar
                definition = $"An {cardInfo.displayedName} is defined as: {cardInfo.Attack} Power, {cardInfo.Health} Health" + abilities;
            }
            string rulebookDescription = $"{startingPreposition} " + cardInfo.displayedName + " is created in your hand. " + definition;

            itemData
                .SetRulebookName(internalName)
                .SetLearnItemDescription(nodeDialogue)
                .SetRulebookDescription(rulebookDescription)
                .SetRulebookSprite(texture2D.ConvertTexture())
                .SetCardWithinBottle(cardByName)
                .SetRegionSpecific(regionSpecific: false)
                .SetCanActivateOutsideBattles(outsideBattle)
                .SetNotRandomlyGiven(notRandomlyGiven: false)
                .SetExtendedProperty("PrefabModelType", (int)ModelType.CardInABottle)
                .SetComponentType(typeof(CardBottleItem))
                .SetPowerLevel(powerLevel);

            Add(pluginGuid, itemData);

            itemData.SetRulebookName(rulebookName_);

            return itemData;
        }

        private static bool NameStartsWithVowel(string name)
        {
            if (name.Length == 0)
                return false;

            char startingLetter = name.ToLowerInvariant()[0];
            if (startingLetter == 'a' || startingLetter == 'e' || startingLetter == 'i' ||
                startingLetter == 'o' || startingLetter == 'u')
                return true;
            return false;
        }
    }
}
