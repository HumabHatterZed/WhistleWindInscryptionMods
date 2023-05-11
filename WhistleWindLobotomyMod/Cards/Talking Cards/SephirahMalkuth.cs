using DiskCardGame;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.Core.Helpers.TextureLoader;

namespace WhistleWindLobotomyMod
{
    public class TalkingCardMalkuth : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_sephirahMalkuth";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 1.0f, voiceSoundPitch: 1.5f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromBytes(Artwork.talkingMalkuthBody, new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim(Artwork.talkingMalkuthEmission);

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingMalkuthEyesOpen1, Artwork.talkingMalkuthEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingMalkuthMouthOpen1, Artwork.talkingMalkuthMouthClosed1),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingMalkuthEyesOpen1, Artwork.talkingMalkuthEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingMalkuthMouthOpen4, Artwork.talkingMalkuthMouthClosed3),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingMalkuthEyesClosed1, Artwork.talkingMalkuthEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingMalkuthMouthOpen3, Artwork.talkingMalkuthMouthClosed3),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingMalkuthEyesOpen1, Artwork.talkingMalkuthEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingMalkuthMouthOpen2, Artwork.talkingMalkuthMouthClosed2),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingMalkuthEyesOpen2, Artwork.talkingMalkuthEyesClosed2),
                        mouth: MakeFaceAnim(Artwork.talkingMalkuthMouthOpen1, Artwork.talkingMalkuthMouthClosed1),
                        emission: emissionMain)
                };
            }
        }

        public override string OnDrawnDialogueId => "SephirahMalkuthDrawn";
        public override string OnAttackedDialogueId => "SephirahMalkuthHurt";
        public override string OnSacrificedDialogueId => "SephirahMalkuthSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "SephirahMalkuthSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "SephirahMalkuthSelectableGood";
        public override string OnPlayFromHandDialogueId => "SephirahMalkuthPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "SephirahMalkuthSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "SephirahMalkuthGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "SephirahMalkuthTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new Dictionary<Opponent.Type, string>()
        {
            { Opponent.Type.ProspectorBoss, "SephirahMalkuthChoice" }
        };
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Malkuth()
        {
            TalkingCardMalkuth.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardMalkuth>("Malkuth").Id;
        }
        private void Card_Malkuth()
        {
            List<Ability> abilities = new() { Ability.BuffNeighbours };

            LobotomyCardManager.CreateCard(
                "wstl_sephirahMalkuth", "Malkuth",
                "The head of the Control Team, here to assist you any way she can.",
                atk: 1, hp: 1,
                blood: 0, bones: 3, energy: 0,
                null, null,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new() { LobotomyCardManager.TraitSephirah },
                appearances: new(), onePerDeck: true);
        }
    }
}