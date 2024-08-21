using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;
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

        public override string OnDrawnDialogueId => "MalkuthDrawn";
        public override string OnAttackedDialogueId => "MalkuthHurt";
        public override string OnSacrificedDialogueId => "MalkuthSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "MalkuthSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "MalkuthSelectableGood";
        public override string OnPlayFromHandDialogueId => "MalkuthPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "MalkuthSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "MalkuthGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "MalkuthTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "MalkuthProspector" },
            { Opponent.Type.AnglerBoss, "MalkuthAngler" },
            { Opponent.Type.TrapperTraderBoss, "MalkuthTrapperTrader" },
            { Opponent.Type.LeshyBoss, "MalkuthLeshy" },
            { Opponent.Type.RoyalBoss, "MalkuthRoyal" },
            { CustomOpponentUtils.ApocalypseBossID, "MalkuthApocalypse" },
            { OrdealUtils.OpponentID, "MalkuthOrdeal" }
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
                .AddTraits(Sephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}