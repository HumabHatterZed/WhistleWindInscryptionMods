using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class CardHelper
    {
        private const string _EMISSION = "_emission.png";
        public const string _PIXEL = "_pixel.png";
        public const string _PNG = ".png";
        
        /// <remarks>
        /// portraitName must not end with a file extension. emissionName and pixelPortrait must end with a file extension if not null.
        /// </remarks>
        public static CardInfo SetPortraits(this CardInfo info, Assembly targetAssembly, string portraitName, string emissionName = null, string pixelPortraitName = null)
        {
            emissionName ??= portraitName + _EMISSION;
            pixelPortraitName ??= portraitName + _PIXEL;
            portraitName += _PNG;

            info.SetPortrait(TextureLoader.LoadTextureFromFile(portraitName, targetAssembly));

            Texture2D tex = TextureLoader.LoadTextureFromFile(emissionName, targetAssembly);
            if (tex != null)
                info.SetEmissivePortrait(tex);

            Texture2D tex2 = TextureLoader.LoadTextureFromFile(pixelPortraitName, targetAssembly);
            if (tex2 != null)
                info.SetPixelPortrait(tex2);

            return info;
        }
        public static CardInfo SetAltPortraits(this CardInfo info, Assembly targetAssembly, string portraitName, string emissionName = null, string pixelPortraitName = null)
        {
            emissionName ??= portraitName + _EMISSION;
            pixelPortraitName ??= portraitName + _PIXEL;
            portraitName += _PNG;

            info.SetAltPortrait(TextureLoader.LoadTextureFromFile(portraitName, targetAssembly));

            Texture2D tex = TextureLoader.LoadTextureFromFile(emissionName, targetAssembly);
            if (tex != null)
                info.SetEmissiveAltPortrait(tex);

            Texture2D tex2 = TextureLoader.LoadTextureFromFile(pixelPortraitName, targetAssembly);
            if (tex2 != null)
                info.SetPixelAlternatePortrait(tex2);

            return info;
        }
        public static CardInfo SetTitle(this CardInfo info, Assembly asm, string fileName)
        {
            info.titleGraphic = TextureLoader.LoadTextureFromFile(fileName, asm);
            return info;
        }
        public static CardInfo SetCardType(this CardInfo cardInfo, CardType cardChoice, bool availableAsCardChoice = true)
        {
            if (cardChoice == CardType.Common && availableAsCardChoice)
            {
                cardInfo.SetDefaultPart1Card();
            }
            else if (cardChoice == CardType.Rare)
            {
                cardInfo.SetRare().RemoveAppearances(CardAppearanceBehaviour.Appearance.TerrainBackground);
                
                if (!availableAsCardChoice)
                    cardInfo.RemoveCardMetaCategories(CardMetaCategory.Rare);
            }

            return cardInfo;
        }

        public static CardModificationInfo FullClone(this CardModificationInfo modToClone)
        {
            CardModificationInfo clone = modToClone.Clone() as CardModificationInfo;
            clone.fromCardMerge = modToClone.fromCardMerge;
            clone.fromDuplicateMerge = modToClone.fromDuplicateMerge;
            clone.fromLatch = modToClone.fromLatch;
            clone.fromTotem = modToClone.fromTotem;
            clone.fromOverclock = modToClone.fromOverclock;
            clone.bountyHunterInfo = modToClone.bountyHunterInfo;
            clone.buildACardPortraitInfo = modToClone.buildACardPortraitInfo;
            clone.deathCardInfo = modToClone.deathCardInfo;

            return clone.SetAttackAndHealth(modToClone.attackAdjustment, modToClone.healthAdjustment)
                .SetCosts(modToClone.bloodCostAdjustment, modToClone.bonesCostAdjustment, modToClone.energyCostAdjustment)
                .SetSingletonId(modToClone.singletonId)
                .SetNameReplacement(modToClone.nameReplacement);
        }

        public static CardAppearanceBehaviourManager.FullCardAppearanceBehaviour CreateAppearance<T>(string pluginGuid, string name)
            where T : CardAppearanceBehaviour
        {
            return CardAppearanceBehaviourManager.Add(pluginGuid, name, typeof(T));
        }

        public enum CardType
        {
            None,
            Common,
            Rare
        }
    }
}
