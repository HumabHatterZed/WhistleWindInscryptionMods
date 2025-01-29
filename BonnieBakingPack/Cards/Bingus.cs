using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using UnityEngine;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateBingus()
        {
            Sprite face = TextureHelper.GetImageAsTexture("bingus.png", Assembly).ConvertTexture(new(0.5f, 0f));
            List<EmotionData> emotionData = new()
            {
                new(emotion: 0,
                    face: face,
                    eyes: GeneratePortrait.EmptyPortraitTuple,
                    mouth: GeneratePortrait.EmptyPortraitTuple,
                    emission: GeneratePortrait.EmptyPortraitTuple)
            };
            FaceInfo faceInfo = new(voiceId: "female1_voice");
            FaceData bingusFace = new("bbp_act1_bingus", emotionData, faceInfo);

            CardManager.New(pluginPrefix, "bingus", "Bingus", 0, 0, "I must admit I do not know what this thing is.")
                .SetPixelPortrait(GetTexture("bingus_pixel.png"))
                .SetRare().RemoveAppearances(CardAppearanceBehaviour.Appearance.RareCardBackground)
                .SetStatIcon(BingusStatIcon.Icon)
                .AddTraits(Trait.KillsSurvivors, Trait.DeathcardCreationNonOption, Trait.LikesHoney, Trait.Lice, Trait.Uncuttable)
                .SetOnePerDeck();

            TalkingCardManager.Create(bingusFace, BingusAbility.SpecialAbility);
        }
    }
}