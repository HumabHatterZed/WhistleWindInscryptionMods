using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;
using static WhistleWind.Core.Helpers.TextureLoader;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

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
                Sprite face = LoadSpriteFromFile("talkingHodBody", new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingHodEmission1");
                FaceAnim emissionLaugh = MakeFaceAnim("talkingHodEmission2");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingHodEyesOpen1", "talkingHodEyesClosed1"),
                        mouth: MakeFaceAnim("talkingHodMouthOpen1", "talkingHodMouthClosed1"),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim("talkingHodEyesOpen2", "talkingHodEyesClosed2"),
                        mouth: MakeFaceAnim("talkingHodMouthOpen2", "talkingHodMouthClosed2"),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingHodEyesOpen3", "talkingHodEyesClosed1"),
                        mouth: MakeFaceAnim("talkingHodMouthOpen3", "talkingHodMouthClosed3"),
                        emission: emissionLaugh),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim("talkingHodEyesOpen4", "talkingHodEyesClosed4"),
                        mouth: MakeFaceAnim("talkingHodMouthOpen4", "talkingHodMouthClosed4"),
                        emission: emissionMain),
                    new(emotion: Emotion.Curious,
                        face: face,
                        eyes: MakeFaceAnim("talkingHodEyesOpen5", "talkingHodEyesClosed5"),
                        mouth: MakeFaceAnim("talkingHodMouthOpen4", "talkingHodMouthClosed4"),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingHodEyesOpen6", "talkingHodEyesClosed6"),
                        mouth: MakeFaceAnim("talkingHodMouthOpen6", "talkingHodMouthClosed4"),
                        emission: emissionMain)
                };
            }
        }

        public override string OnDrawnDialogueId => "HodDrawn";
        public override string OnAttackedDialogueId => "HodHurt";
        public override string OnSacrificedDialogueId => "HodSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "HodSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "HodSelectableGood";
        public override string OnPlayFromHandDialogueId => "HodPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "HodSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "HodGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "HodTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "HodProspector" },
            { Opponent.Type.AnglerBoss, "HodAngler" },
            { Opponent.Type.TrapperTraderBoss, "HodTrapperTrader" },
            { Opponent.Type.LeshyBoss, "HodLeshy" },
            { Opponent.Type.RoyalBoss, "HodRoyal" },
            { CustomOpponentUtils.ApocalypseBossID, "HodApocalypse" },
            { OrdealUtils.OpponentID, "HodOrdeal" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Hod()
        {
            TalkingCardHod.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardHod>("Hod").Id;
        }
        private void Card_Hod()
        {
            NewCard("sephirahHod", "Hod", "Timid she may be, she will still try her hardest.",
                attack: 1, health: 2, bones: 3)
                .AddAbilities(Protector.ability)
                .AddTraits(Sephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}