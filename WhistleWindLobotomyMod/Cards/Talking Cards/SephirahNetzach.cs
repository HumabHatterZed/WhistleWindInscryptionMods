using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Apocalypse;
using static WhistleWind.Core.Helpers.TextureLoader;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public class TalkingCardNetzach : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_sephirahNetzach";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 0.8f, voiceSoundPitch: 0.6f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromFile("talkingNetzachBody", new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingNetzachEmission");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingNetzachEyesOpen1", "talkingNetzachEyesClosed1"),
                        mouth: MakeFaceAnim("talkingNetzachMouthOpen1", "talkingNetzachMouthClosed1"),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim("talkingNetzachEyesOpen5", "talkingNetzachEyesClosed5"),
                        mouth: MakeFaceAnim("talkingNetzachMouthOpen5", "talkingNetzachMouthClosed5"),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingNetzachEyesOpen3", "talkingNetzachEyesClosed3"),
                        mouth: MakeFaceAnim("talkingNetzachMouthOpen4", "talkingNetzachMouthClosed4"),
                        emission: emissionMain),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim("talkingNetzachEyesClosed3", "talkingNetzachEyesClosed3"),
                        mouth: MakeFaceAnim("talkingNetzachMouthOpen3", "talkingNetzachMouthClosed1"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingNetzachEyesOpen2", "talkingNetzachEyesClosed2"),
                        mouth: MakeFaceAnim("talkingNetzachMouthOpen2", "talkingNetzachMouthClosed1"),
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
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "SephirahNetzachProspector" },
            { Opponent.Type.AnglerBoss, "SephirahNetzachAngler" },
            { Opponent.Type.TrapperTraderBoss, "SephirahNetzachTrapperTrader" },
            { Opponent.Type.LeshyBoss, "SephirahNetzachLeshy" },
            { Opponent.Type.RoyalBoss, "SephirahNetzachRoyal" },
            { ApocalypseBossOpponent.ID, "SephirahNetzachApocalypse" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Netzach()
        {
            TalkingCardNetzach.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardNetzach>("Netzach").Id;
        }
        private void Card_Netzach()
        {
            NewCard("sephirahNetzach", "Netzach", "Despite his lack of motivation, he'll do his best to succeed.",
                attack: 0, health: 3, blood: 1)
                .AddAbilities(GreedyHealing.ability, Ability.WhackAMole)
                .AddTraits(Sephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}