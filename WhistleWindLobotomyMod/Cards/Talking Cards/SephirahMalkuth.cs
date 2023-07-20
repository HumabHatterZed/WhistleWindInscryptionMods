using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

using static WhistleWind.Core.Helpers.TextureLoader;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

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
                Sprite face = LoadSpriteFromFile("talkingMalkuthBody", new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingMalkuthEmission");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingMalkuthEyesOpen1", "talkingMalkuthEyesClosed1"),
                        mouth: MakeFaceAnim("talkingMalkuthMouthOpen1", "talkingMalkuthMouthClosed1"),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim("talkingMalkuthEyesOpen1", "talkingMalkuthEyesClosed1"),
                        mouth: MakeFaceAnim("talkingMalkuthMouthOpen4", "talkingMalkuthMouthClosed3"),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingMalkuthEyesClosed1", "talkingMalkuthEyesClosed1"),
                        mouth: MakeFaceAnim("talkingMalkuthMouthOpen3", "talkingMalkuthMouthClosed3"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim("talkingMalkuthEyesOpen1", "talkingMalkuthEyesClosed1"),
                        mouth: MakeFaceAnim("talkingMalkuthMouthOpen2", "talkingMalkuthMouthClosed2"),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingMalkuthEyesOpen2", "talkingMalkuthEyesClosed2"),
                        mouth: MakeFaceAnim("talkingMalkuthMouthOpen1", "talkingMalkuthMouthClosed1"),
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
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "SephirahMalkuthProspector" },
            { Opponent.Type.AnglerBoss, "SephirahMalkuthAngler" },
            { Opponent.Type.TrapperTraderBoss, "SephirahMalkuthTrapperTrader" },
            { Opponent.Type.LeshyBoss, "SephirahMalkuthLeshy" },
            { Opponent.Type.RoyalBoss, "SephirahMalkuthRoyal" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Malkuth()
        {
            TalkingCardMalkuth.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardMalkuth>("Malkuth").Id;
        }
        private void Card_Malkuth()
        {
            NewCard("sephirahMalkuth", "Malkuth", "The head of the Control Team, here to assist you any way she can.",
                attack: 1, health: 1, bones: 3)
                .AddAbilities(Ability.BuffNeighbours)
                .AddTraits(TraitSephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}