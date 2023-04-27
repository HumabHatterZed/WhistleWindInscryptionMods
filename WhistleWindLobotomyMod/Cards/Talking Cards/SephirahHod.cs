using DiskCardGame;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.Core.Helpers.TextureLoader;

namespace WhistleWindLobotomyMod
{
    public class TalkingCardHod : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_sephirahHod";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 1.0f, voiceSoundPitch: 1.4f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromBytes(Artwork.talkingHodBody, new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim(Artwork.talkingHodEmission1);
                FaceAnim emissionLaugh = MakeFaceAnim(Artwork.talkingHodEmission2);

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingHodEyesOpen1, Artwork.talkingHodEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingHodMouthOpen1, Artwork.talkingHodMouthClosed1),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingHodEyesOpen2, Artwork.talkingHodEyesClosed2),
                        mouth: MakeFaceAnim(Artwork.talkingHodMouthOpen2, Artwork.talkingHodMouthClosed2),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingHodEyesOpen3, Artwork.talkingHodEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingHodMouthOpen3, Artwork.talkingHodMouthClosed3),
                        emission: emissionLaugh),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingHodEyesOpen4, Artwork.talkingHodEyesClosed4),
                        mouth: MakeFaceAnim(Artwork.talkingHodMouthOpen4, Artwork.talkingHodMouthClosed4),
                        emission: emissionMain),
                    new(emotion: Emotion.Curious,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingHodEyesOpen5, Artwork.talkingHodEyesClosed5),
                        mouth: MakeFaceAnim(Artwork.talkingHodMouthOpen4, Artwork.talkingHodMouthClosed4),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingHodEyesOpen6, Artwork.talkingHodEyesClosed6),
                        mouth: MakeFaceAnim(Artwork.talkingHodMouthOpen6, Artwork.talkingHodMouthClosed6),
                        emission: emissionMain)
                };
            }
        }

        public override string OnDrawnDialogueId => "SephirahHodDrawn";
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

        public override void OnShownForCardChoiceNode()
        {
            this.TriggerSoloDialogue("SephirahHodChoice");
            base.OnShownForCardChoiceNode();
        }
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Hod()
        {
            TalkingCardHod.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardHod>("Hod").Id;
        }
        private void Card_Hod()
        {
            List<Ability> abilities = new() { Protector.ability };

            LobotomyCardManager.CreateCard(
                "wstl_sephirahHod", "Hod",
                "The head of the Training Department. She will assist you the best she can.",
                atk: 1, hp: 2,
                blood: 0, bones: 3, energy: 0,
                null, null,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new() { LobotomyCardManager.TraitSephirah },
                appearances: new(), onePerDeck: true);
        }
    }
}