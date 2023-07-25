using DiskCardGame;
using System;
using System.Collections.Generic;
using UnityEngine;
using static InscryptionAPI.Ascension.StarterDeckManager;
namespace WhistleWind.Core.Helpers
{
    public static class StarterDeckHelper // Methods specific to getting modded cards
    {
        public static FullStarterDeck AddStarterDeck(string pluginPrefix, string title, string icon, int unlockLevel, List<string> cardNames, Func<int, bool> customUnlock = null)
        {
            if (cardNames.Count == 0)
                return null;

            List<CardInfo> cardInfos = new();
            foreach (string name in cardNames)
            {
                CardInfo info = CardLoader.GetCardByName(name);
                cardInfos.Add(info);
            }
            return AddStarterDeck(pluginPrefix, title, icon, unlockLevel, cardInfos, customUnlock);
        }
        public static FullStarterDeck AddStarterDeck(string pluginPrefix, string title, string icon, int unlockLevel, List<CardInfo> cardInfos, Func<int, bool> customUnlock = null)
        {
            if (cardInfos.Count == 0)
                return null;

            StarterDeckInfo starterDeckInfo = ScriptableObject.CreateInstance<StarterDeckInfo>();
            starterDeckInfo.title = title;
            starterDeckInfo.iconSprite = TextureLoader.LoadSpriteFromFile(icon);
            starterDeckInfo.cards = cardInfos;

            FullStarterDeck fullDeck = Add(pluginPrefix, starterDeckInfo, unlockLevel);

            if (customUnlock != null)
                fullDeck.CustomUnlockCheck = customUnlock;

            return fullDeck;
        }
    }
}
