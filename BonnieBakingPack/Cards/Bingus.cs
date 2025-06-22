using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using UnityEngine;

namespace BonniesBakingPack {
    public partial class BakingPlugin {
        private void CreateBingus() {
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
                .SetRare().RemoveAppearances(CardAppearanceBehaviour.Appearance.RareCardBackground).AddAct1()
                .SetStatIcon(BingusStatIcon.Icon)
                .AddTraits(Trait.KillsSurvivors, Trait.DeathcardCreationNonOption, Trait.LikesHoney, Trait.Lice, Trait.Uncuttable)
                .SetOnePerDeck();

            TalkingCardManager.Create(bingusFace, BingusAbility.SpecialAbility);

            Sprite face2 = TextureHelper.GetImageAsTexture("bingus_part3.png", Assembly).ConvertTexture(new(0.5f, 0f));
            List<EmotionData> emotionData2 = new()
            {
                new(emotion: 0,
                    face: face2,
                    eyes: GeneratePortrait.EmptyPortraitTuple,
                    mouth: GeneratePortrait.EmptyPortraitTuple,
                    emission: GeneratePortrait.EmptyPortraitTuple)
            };
            FaceInfo faceInfo2 = new(voiceId: "female1_voice");
            FaceData bingusFace2 = new("bbp_act3_bingus", emotionData2, faceInfo2);

            CardManager.New(pluginPrefix3, "bingus", "Bingus", 0, 0, "Seriously? What kind of mods did you install?")
                .SetRare().RemoveAppearances(CardAppearanceBehaviour.Appearance.RareCardBackground).AddAct1()
                .SetStatIcon(BingusStatIcon.Icon)
                .AddTraits(Trait.KillsSurvivors, Trait.DeathcardCreationNonOption, Trait.LikesHoney, Trait.Lice, Trait.Uncuttable)
                .SetOnePerDeck();

            TalkingCardManager.Create(bingusFace2, BingusAbility.SpecialAbility);

            CardManager.New(pluginPrefixG, "bingus", "Looloo", 0, 0, "OH GOODNESS. I-I DO NOT KNOW WHAT TO SAY.")
                .SetRare().RemoveAppearances(CardAppearanceBehaviour.Appearance.RareCardBackground).AddGrimora()
                .SetPortraitAndEmission(GetTexture("looloo.png"), GetTexture("looloo_emission.png"))
                .SetStatIcon(BingusStatIcon.Icon)
                .AddTraits(Trait.KillsSurvivors, Trait.DeathcardCreationNonOption, Trait.LikesHoney, Trait.Uncuttable)
                .AddSpecialAbilities(LoolooAbility.SpecialAbility2)
                .SetOnePerDeck();

            CardManager.New(pluginPrefixM, "bingus", "Bingus (Wizard)", 0, 0, "What is this childish card doing in my tower?")
                .SetRare().RemoveAppearances(CardAppearanceBehaviour.Appearance.RareCardBackground).AddMagnificus()
                .SetPortrait(GetTexture("bingus_wizard.png"))
                .SetStatIcon(BingusStatIcon.Icon)
                .AddTraits(Trait.KillsSurvivors, Trait.DeathcardCreationNonOption, Trait.LikesHoney, Trait.Uncuttable)
                .AddSpecialAbilities(LoolooAbility.SpecialAbility2)
                .SetOnePerDeck();
        }
    }
}