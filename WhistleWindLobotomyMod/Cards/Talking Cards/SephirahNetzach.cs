/*using DiskCardGame;
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
        private void Card_Netzach()
        {
            List<Ability> abilities = new()
            {
                Ability.LatchDeathShield
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TalkingCardNetzach.specialAbility
            };
            List<CardMetaCategory> metaCategories = new()
            {
                CardHelper.SephirahCard
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.AnimatedPortrait
            };
            CardHelper.CreateCard(
                "wstl_sephirahNetzach", "Netzach",
                "The head of the Security Department. Perhaps you can motivate him.",
                atk: 1, hp: 1,
                blood: 1, bones: 0, energy: 0,
                null, null,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: metaCategories, tribes: new(), traits: new(),
                appearances: appearances, onePerDeck: true, face: SephirahNetzach.Face);
        }
        private void SpecialAbility_Netzach() => TalkingCardNetzach.specialAbility = AbilityHelper.CreatePaperTalkingCard<TalkingCardNetzach>("Netzach").Id;
    }
    public class TalkingCardNetzach : PaperTalkingCard
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;
        public override string OnDiscoveredInExplorationDialogueId => "SephirahNetzachChoice";
        public override string OnDrawnDialogueId => "SephirahNetzachDrawn";
        public override string OnDrawnFallbackDialogueId => "SephirahNetzachDrawn";
        public override string OnAttackedDialogueId => "SephirahNetzachHurt";
        public override string OnSacrificedDialogueId => "SephirahNetzachSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "SephirahNetzachSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "SephirahNetzachSelectableGood";
        public override string OnPlayFromHandDialogueId => "SephirahNetzachPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "SephirahNetzachSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "SephirahNetzachGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "SephirahNetzachTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new Dictionary<Opponent.Type, string>()
        {
            { Opponent.Type.ProspectorBoss, "SephirahNetzachChoice" }
        };
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;
        public override IEnumerator OnShownForCardSelect(bool forPositiveEffect)
        {
            yield return new WaitForEndOfFrame();
            yield return base.OnShownForCardSelect(forPositiveEffect);
            yield break;
        }
    }
    static class SephirahNetzach
    {
        public static GameObject Face;
        public static void Init()
        {
            Face = LobotomyPlugin.sephirahBundle.LoadAsset<GameObject>("TalkingCardNetzach");

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
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes1_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_mouth1_open, new(0.5f, 0f))
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Surprise,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes2_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_mouth2_closed, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes2_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_mouth2_open, new(0.5f, 0f))
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Laughter,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes3_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_mouth3_closed, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes3_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes_emission2, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_mouth3_open, new(0.5f, 0f))
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Quiet,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes4_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_mouth4_closed, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes4_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_mouth4_open, new(0.5f, 0f))
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Curious,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes5_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_mouth4_closed, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes5_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_mouth4_open, new(0.5f, 0f))
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Anger,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes6_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_mouth6_closed, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes6_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardNetzach_mouth6_open, new(0.5f, 0f))
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