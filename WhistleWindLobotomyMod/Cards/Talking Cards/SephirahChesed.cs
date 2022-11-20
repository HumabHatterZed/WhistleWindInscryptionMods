﻿/*using DiskCardGame;
using InscryptionAPI.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Chesed()
        {
            List<Ability> abilities = new()
            {
                Ability.LatchDeathShield
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TalkingCardChesed.specialAbility
            };
            List<CardMetaCategory> metaCategories = new()
            {
                CardHelper.SEPHIRAH_CARD
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.AnimatedPortrait
            };
            CardHelper.CreateCard(
                "wstl_sephirahChesed", "Chesed",
                "The head of the Information Department. Incompetence is not tolerated.",
                atk: 1, hp: 1,
                blood: 1, bones: 0, energy: 0,
                null, null,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: metaCategories, tribes: new(), traits: new(),
                appearances: appearances, onePerDeck: true, face: SephirahChesed.Face);
        }
        private void SpecialAbility_Chesed() => TalkingCardChesed.specialAbility = AbilityHelper.CreatePaperTalkingCard<TalkingCardChesed>("Chesed").Id;
    }
    public class TalkingCardChesed : PaperTalkingCard
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;
        public override string OnDiscoveredInExplorationDialogueId => "SephirahChesedChoice";
        public override string OnDrawnDialogueId => "SephirahChesedDrawn";
        public override string OnDrawnFallbackDialogueId => "SephirahChesedDrawn";
        public override string OnAttackedDialogueId => "SephirahChesedHurt";
        public override string OnSacrificedDialogueId => "SephirahChesedSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "SephirahChesedSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "SephirahChesedSelectableGood";
        public override string OnPlayFromHandDialogueId => "SephirahChesedPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "SephirahChesedSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "SephirahChesedGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "SephirahChesedTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new Dictionary<Opponent.Type, string>()
        {
            { Opponent.Type.ProspectorBoss, "SephirahChesedChoice" }
        };
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;
        public override IEnumerator OnShownForCardSelect(bool forPositiveEffect)
        {
            yield return new WaitForEndOfFrame();
            yield return base.OnShownForCardSelect(forPositiveEffect);
            yield break;
        }
    }
    static class SephirahChesed
    {
        public static GameObject Face;
        public static void Init()
        {
            Face = LobotomyPlugin.sephirahBundle.LoadAsset<GameObject>("TalkingCardChesed");

            CharacterFace face = Face.AddComponent<CharacterFace>();
            face.anim = Face.transform.Find("Anim").GetComponent<Animator>();
            face.eyes = Face.transform.Find("Anim").Find("Body").Find("Eyes").gameObject.AddComponent<CharacterEyes>();
            face.mouth = Face.transform.Find("Anim").Find("Body").Find("Mouth").gameObject.AddComponent<CharacterMouth>();
            face.face = Face.transform.Find("Anim").Find("Body").GetComponent<SpriteRenderer>();

            face.emotionSprites = new List<CharacterFace.EmotionSprites>()
            {
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Neutral,
                    face = face.face.sprite,
                    eyesOpen = face.eyes.GetComponent<SpriteRenderer>().sprite,
                    mouthClosed = face.mouth.GetComponent<SpriteRenderer>().sprite,
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes1_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_mouth1_open, new(0.5f, 0f))
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Surprise,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes2_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_mouth2_closed, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes2_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_mouth2_open, new(0.5f, 0f))
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Laughter,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes3_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_mouth3_closed, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes3_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes_emission2, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_mouth3_open, new(0.5f, 0f))
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Quiet,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes4_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_mouth4_closed, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes4_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_mouth4_open, new(0.5f, 0f))
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Curious,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes5_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_mouth4_closed, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes5_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_mouth4_open, new(0.5f, 0f))
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Anger,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes6_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_mouth6_closed, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes6_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardChesed_mouth6_open, new(0.5f, 0f))
                }
            };

            face.voiceSoundId = "female1_voice";
            face.voiceSoundPitch = 0.5f;
            face.eyes.blinkRate = 1.2f;

            int offscreen = LayerMask.NameToLayer("CardOffscreen");
            foreach (Transform t in Face.GetComponentsInChildren<Transform>()) { t.gameObject.layer = offscreen; }
            Face.layer = offscreen;
            face.eyes.emissionRenderer = face.eyes.transform.Find("Emission")?.GetComponent<SpriteRenderer>();
            if (face.eyes.emissionRenderer != null) { face.eyes.emissionRenderer.gameObject.layer = LayerMask.NameToLayer("CardOffscreenEmission"); }
        }
    }
}*/