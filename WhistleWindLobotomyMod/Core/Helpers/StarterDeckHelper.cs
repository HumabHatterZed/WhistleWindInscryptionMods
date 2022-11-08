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
        public static StarterDeckManager.FullStarterDeck AddStartDeck(string title, byte[] icon, List<CardInfo> cards, int unlockLevel = 0)
        {
            StarterDeckInfo starterDeckInfo = ScriptableObject.CreateInstance<StarterDeckInfo>();
            starterDeckInfo.title = title;
            starterDeckInfo.iconSprite = TextureLoader.LoadSpriteFromBytes(icon, true);
            starterDeckInfo.cards = cards;
            return StarterDeckManager.Add(pluginPrefix, starterDeckInfo, unlockLevel);
        }
    }
}
