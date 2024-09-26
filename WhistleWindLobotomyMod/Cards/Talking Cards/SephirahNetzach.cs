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
using WhistleWindLobotomyMod.Opponents;
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
                Sprite face = LoadSpriteFromFile("talkingNetzachBody.png", new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingNetzachEmission.png");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingNetzachEyesOpen1.png", "talkingNetzachEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingNetzachMouthOpen1.png", "talkingNetzachMouthClosed1.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim("talkingNetzachEyesOpen5.png", "talkingNetzachEyesClosed5.png"),
                        mouth: MakeFaceAnim("talkingNetzachMouthOpen5.png", "talkingNetzachMouthClosed5.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingNetzachEyesOpen3.png", "talkingNetzachEyesClosed3.png"),
                        mouth: MakeFaceAnim("talkingNetzachMouthOpen4.png", "talkingNetzachMouthClosed4.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim("talkingNetzachEyesClosed3.png", "talkingNetzachEyesClosed3.png"),
                        mouth: MakeFaceAnim("talkingNetzachMouthOpen3.png", "talkingNetzachMouthClosed1.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingNetzachEyesOpen2.png", "talkingNetzachEyesClosed2.png"),
                        mouth: MakeFaceAnim("talkingNetzachMouthOpen2.png", "talkingNetzachMouthClosed1.png"),
                        emission: emissionMain)
                };
            }
        }

        public override string OnDrawnDialogueId => "NetzachDrawn";
        public override string OnAttackedDialogueId => "NetzachHurt";
        public override string OnSacrificedDialogueId => "NetzachSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "NetzachSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "NetzachSelectableGood";
        public override string OnPlayFromHandDialogueId => "NetzachPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "NetzachSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "NetzachGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "NetzachTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "NetzachProspector" },
            { Opponent.Type.AnglerBoss, "NetzachAngler" },
            { Opponent.Type.TrapperTraderBoss, "NetzachTrapperTrader" },
            { Opponent.Type.LeshyBoss, "NetzachLeshy" },
            { Opponent.Type.RoyalBoss, "NetzachRoyal" },
            { CustomOpponentUtils.ApocalypseBossID, "NetzachApocalypse" },
            { OrdealUtils.OpponentID, "NetzachOrdeal" }
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
            CardManager.New(pluginPrefix, "sephirahNetzach", "Netzach",
                attack: 0, health: 3, "Unmotivated and unwilling. Surely there are others you can choose?")
                .SetBloodCost(1)
                .AddAbilities(GreedyHealing.ability, Ability.WhackAMole)
                .AddTraits(Sephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}