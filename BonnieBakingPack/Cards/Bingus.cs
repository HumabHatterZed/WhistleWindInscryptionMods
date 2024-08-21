using BepInEx.Bootstrap;
using DiskCardGame;
using Infiniscryption.PackManagement;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using InscryptionAPI.Helpers;
using InscryptionAPI.TalkingCards.Create;
using InscryptionAPI.TalkingCards;
using System;
using System.Collections.Generic;
using UnityEngine;
using InscryptionAPI.TalkingCards.Animation;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateBingus()
        {
            Sprite face = TextureHelper.GetImageAsTexture("bingus.png", Assembly).ConvertTexture(new(0.5f, 0f));
            List <EmotionData> emotionData = new()
            {
                new(emotion: 0,
                    face: face,
                    eyes: GeneratePortrait.EmptyPortraitTuple,
                    mouth: GeneratePortrait.EmptyPortraitTuple,
                    emission: GeneratePortrait.EmptyPortraitTuple)
            };
            FaceInfo faceInfo = new(voiceId: "female1_voice");
            FaceData bingusFace = new("bbp_bingus", emotionData, faceInfo);

            CardManager.New(pluginPrefix, "bingus", "Bingus", 62123, 62123, "I must admit I do not know what this thing is.")
                .SetPixelPortrait(GetTexture("bingus_pixel.png"))
                .SetRare().RemoveAppearances(CardAppearanceBehaviour.Appearance.RareCardBackground)
                .SetStatIcon(BingusStatIcon.Icon)
                .AddTraits(Trait.KillsSurvivors, Trait.DeathcardCreationNonOption, Trait.LikesHoney, Trait.Lice, Trait.Uncuttable)
                .SetOnePerDeck();
            
            TalkingCardManager.Create(bingusFace, BingusAbility.SpecialAbility);
        }
    }
}