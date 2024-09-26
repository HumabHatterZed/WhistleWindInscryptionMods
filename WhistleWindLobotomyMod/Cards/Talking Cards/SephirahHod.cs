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
                Sprite face = LoadSpriteFromFile("talkingHodBody.png", new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingHodEmission1.png");
                FaceAnim emissionLaugh = MakeFaceAnim("talkingHodEmission2.png");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingHodEyesOpen1.png", "talkingHodEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingHodMouthOpen1.png", "talkingHodMouthClosed1.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim("talkingHodEyesOpen2.png", "talkingHodEyesClosed2.png"),
                        mouth: MakeFaceAnim("talkingHodMouthOpen2.png", "talkingHodMouthClosed2.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingHodEyesOpen3.png", "talkingHodEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingHodMouthOpen3.png", "talkingHodMouthClosed3.png"),
                        emission: emissionLaugh),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim("talkingHodEyesOpen4.png", "talkingHodEyesClosed4.png"),
                        mouth: MakeFaceAnim("talkingHodMouthOpen4.png", "talkingHodMouthClosed4.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Curious,
                        face: face,
                        eyes: MakeFaceAnim("talkingHodEyesOpen5.png", "talkingHodEyesClosed5.png"),
                        mouth: MakeFaceAnim("talkingHodMouthOpen4.png", "talkingHodMouthClosed4.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingHodEyesOpen6.png", "talkingHodEyesClosed6.png"),
                        mouth: MakeFaceAnim("talkingHodMouthOpen6.png", "talkingHodMouthClosed4.png"),
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
            CardManager.New(pluginPrefix, "sephirahHod", "Hod",
                attack: 1, health: 2, "Timid she may be, she will still try her hardest.")
                .SetBonesCost(3)
                .AddAbilities(Protector.ability)
                .AddTraits(Sephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}