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
                Sprite face = LoadSpriteFromFile("talkingMalkuthBody.png", new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingMalkuthEmission.png");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingMalkuthEyesOpen1.png", "talkingMalkuthEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingMalkuthMouthOpen1.png", "talkingMalkuthMouthClosed1.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim("talkingMalkuthEyesOpen1.png", "talkingMalkuthEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingMalkuthMouthOpen4.png", "talkingMalkuthMouthClosed3.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingMalkuthEyesClosed1.png", "talkingMalkuthEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingMalkuthMouthOpen3.png", "talkingMalkuthMouthClosed3.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim("talkingMalkuthEyesOpen1.png", "talkingMalkuthEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingMalkuthMouthOpen2.png", "talkingMalkuthMouthClosed2.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingMalkuthEyesOpen2.png", "talkingMalkuthEyesClosed2.png"),
                        mouth: MakeFaceAnim("talkingMalkuthMouthOpen1.png", "talkingMalkuthMouthClosed1.png"),
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
            CardManager.New(pluginPrefix, "sephirahMalkuth", "Malkuth",
                attack: 1, health: 1, "The head of the Control Team, here to assist you any way she can.")
                .SetBonesCost(3)
                .AddAbilities(Ability.BuffNeighbours)
                .AddTraits(Sephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}