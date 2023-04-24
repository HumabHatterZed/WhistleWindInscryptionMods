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
    public class TalkingCardNetzach : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_sephirahNetzach";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 1.1f, voiceSoundPitch: 0.6f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromBytes(Artwork.talkingNetzachBody, new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim(Artwork.talkingNetzachEyesEmission1);

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingNetzachEyesOpen1, Artwork.talkingNetzachEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingNetzachMouthOpen1, Artwork.talkingNetzachMouthClosed1),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingNetzachEyesOpen5, Artwork.talkingNetzachEyesClosed5),
                        mouth: MakeFaceAnim(Artwork.talkingNetzachMouthOpen5, Artwork.talkingNetzachMouthClosed5),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingNetzachEyesOpen3, Artwork.talkingNetzachEyesClosed3),
                        mouth: MakeFaceAnim(Artwork.talkingNetzachMouthOpen4, Artwork.talkingNetzachMouthClosed4),
                        emission: emissionMain),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingNetzachEyesClosed3, Artwork.talkingNetzachEyesClosed3),
                        mouth: MakeFaceAnim(Artwork.talkingNetzachMouthOpen3, Artwork.talkingNetzachMouthClosed1),
                        emission: null),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingNetzachEyesOpen2, Artwork.talkingNetzachEyesClosed2),
                        mouth: MakeFaceAnim(Artwork.talkingNetzachMouthOpen2, Artwork.talkingNetzachMouthClosed1),
                        emission: emissionMain)
                };
            }
        }

        public override string OnDrawnDialogueId => "SephirahNetzachDrawn";
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

        public override void OnShownForCardChoiceNode()
        {
            this.TriggerSoloDialogue("SephirahNetzachChoice");
            base.OnShownForCardChoiceNode();
        }
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Netzach()
        {
            TalkingCardNetzach.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardNetzach>("Netzach").Id;
        }
        private void Card_Netzach()
        {
            List<Ability> abilities = new()
            {
                GreedyHealing.ability
            };

            LobotomyCardManager.CreateCard(
                "wstl_sephirahNetzach", "Netzach",
                "The head of the Training Department. She will assist you the best she can.",
                atk: 1, hp: 3,
                blood: 1, bones: 0, energy: 0,
                null, null,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new() { LobotomyCardManager.TraitSephirah },
                appearances: new(), onePerDeck: true);
        }
    }
}