using DiskCardGame;
using InscryptionAPI.Ascension;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core.Helpers
{
    public static class StarterDeckHelper // Methods specific to getting modded cards
    {
        public static bool AddStarterDeck(string title, byte[] icon, int unlockLevel, params string[] cardNames)
        {
            if (cardNames.Length == 0)
            {
                Log.LogError($"Failed to create new start deck: {title}. No cards specified.");
                return false;
            }

            List<CardInfo> cards = new();
            foreach (string name in cardNames)
            {
                CardInfo info = CardLoader.GetCardByName(name);
                cards.Add(info);
            }
            StarterDeckInfo starterDeckInfo = ScriptableObject.CreateInstance<StarterDeckInfo>();
            starterDeckInfo.title = title;
            starterDeckInfo.iconSprite = TextureLoader.LoadSpriteFromBytes(icon);
            starterDeckInfo.cards = cards;
            StarterDeckManager.Add(pluginPrefix, starterDeckInfo, unlockLevel);
            return true;
        }
    }
}
