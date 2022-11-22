using DiskCardGame;
using InscryptionAPI.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    static class SephirahHod
    {
        public static GameObject Face;
        public static void Init()
        {
            Face = LobotomyPlugin.sephirahBundle.LoadAsset<GameObject>("TalkingCardHod");

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
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes1_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_mouth1_open, new(0.5f, 0f)),
                    mouthClosed = face.mouth.GetComponent<SpriteRenderer>().sprite
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Surprise,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes2_open, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes2_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_mouth2_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_mouth2_closed, new(0.5f, 0f))
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Laughter,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes3_open, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes1_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes_emission2, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_mouth3_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_mouth3_closed, new(0.5f, 0f)),
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Quiet,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes4_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_mouth4_closed, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes4_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_mouth4_open, new(0.5f, 0f))
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Curious,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes5_open, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes5_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_mouth4_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_mouth4_closed, new(0.5f, 0f))
                },
                new CharacterFace.EmotionSprites()
                {
                    emotion = Emotion.Anger,
                    face = face.face.sprite,
                    eyesOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes6_open, new(0.5f, 0f)),
                    eyesClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes6_closed, new(0.5f, 0f)),
                    eyesOpenEmission = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_eyes_emission1, new(0.5f, 0f)),
                    mouthOpen = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_mouth6_open, new(0.5f, 0f)),
                    mouthClosed = TextureLoader.LoadSpriteFromBytes(Artwork.TalkingCardHod_mouth6_closed, new(0.5f, 0f))
                }
            };

            face.voiceSoundId = "female1_voice";
            face.voiceSoundPitch = 1.5f;
            face.eyes.blinkRate = 1.1f;

            int offscreen = LayerMask.NameToLayer("CardOffscreen");
            foreach (Transform t in Face.GetComponentsInChildren<Transform>()) { t.gameObject.layer = offscreen; }
            Face.layer = offscreen;
            face.eyes.emissionRenderer = face.eyes.transform.Find("Emission")?.GetComponent<SpriteRenderer>();
            if (face.eyes.emissionRenderer != null) { face.eyes.emissionRenderer.gameObject.layer = LayerMask.NameToLayer("CardOffscreenEmission"); }
        }
    }
    public class TalkingCardHod : PaperTalkingCard
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;
        public override string OnDiscoveredInExplorationDialogueId => "SephirahHodChoice";
        public override string OnDrawnDialogueId => "SephirahHodDrawn";
        public override string OnDrawnFallbackDialogueId => "SephirahHodDrawn";
        public override string OnAttackedDialogueId => "SephirahHodHurt";
        public override string OnSacrificedDialogueId => "SephirahHodSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "SephirahHodSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "SephirahHodSelectableGood";
        public override string OnPlayFromHandDialogueId => "SephirahHodPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "SephirahHodSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "SephirahHodGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "SephirahHodTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new Dictionary<Opponent.Type, string>()
        {
            { Opponent.Type.ProspectorBoss, "SephirahHodChoice" }
        };
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;
        public override IEnumerator OnShownForCardSelect(bool forPositiveEffect)
        {
            yield return new WaitForEndOfFrame();
            yield return base.OnShownForCardSelect(forPositiveEffect);
            yield break;
        }
        public override void OnShownForCardChoiceNode()
        {
            this.TriggerSoloDialogue(this.OnDiscoveredInExplorationDialogueId);
            base.OnShownForCardChoiceNode();
        }
    }
    public partial class LobotomyPlugin
    {
        private void Card_Hod()
        {
            List<Ability> abilities = new() { };
            List<SpecialTriggeredAbility> specialAbilities = new() { TalkingCardHod.specialAbility };
            List<CardMetaCategory> metaCategories = new() { CardHelper.SEPHIRAH_CARD };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.AnimatedPortrait
            };
            CardHelper.CreateCard(
                "wstl_sephirahHod", "Hod",
                "The head of the Training Department. She will assist you the best she can.",
                atk: 1, hp: 1,
                blood: 1, bones: 0, energy: 0,
                null, null,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: metaCategories, tribes: new(), traits: new(),
                appearances: appearances, onePerDeck: true, face: SephirahHod.Face);
        }
        private void SpecialAbility_Hod() => TalkingCardHod.specialAbility = AbilityHelper.CreatePaperTalkingCard<TalkingCardHod>("Hod").Id;
    }
}