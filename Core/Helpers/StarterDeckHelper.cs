using DiskCardGame;
using InscryptionAPI.Ascension;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public static class StarterDeckHelper // Methods specific to getting modded cards
    {
        public static StarterDeckManager.FullStarterDeck AddStartDeck(string title, byte[] icon, List<CardInfo> cards, int unlockLevel = 0)
        {
            StarterDeckInfo starterDeckInfo = ScriptableObject.CreateInstance<StarterDeckInfo>();
            starterDeckInfo.title = title;
            starterDeckInfo.iconSprite = WstlTextureHelper.LoadSpriteFromResource(icon, true);
            starterDeckInfo.cards = cards;
            return StarterDeckManager.Add("wstl", starterDeckInfo, unlockLevel);
        }
    }
}
