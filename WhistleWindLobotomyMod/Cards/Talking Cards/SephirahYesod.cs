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
    public class TalkingCardYesod : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_sephirahYesod";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 1.0f, voiceSoundPitch: 0.7f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromFile("talkingYesodBody.png", new(0.5f, 0f));
                FaceAnim emission = MakeFaceAnim("talkingYesodEyesEmission.png");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingYesodEyesOpen1.png", "talkingYesodEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingYesodMouthOpen1.png", "talkingYesodMouthClosed1.png"),
                        emission: emission),
                    new(emotion: Emotion.Curious,
                        face: face,
                        eyes: MakeFaceAnim("talkingYesodEyesOpen2.png", "talkingYesodEyesClosed2.png"),
                        mouth: MakeFaceAnim("talkingYesodMouthOpen2.png", "talkingYesodMouthClosed1.png"),
                        emission: emission),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingYesodEyesOpen3.png", "talkingYesodEyesClosed3.png"),
                        mouth: MakeFaceAnim("talkingYesodMouthOpen2.png", "talkingYesodMouthClosed1.png"),
                        emission: emission),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingYesodEyesOpen1.png", "talkingYesodEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingYesodMouthOpen3.png", "talkingYesodMouthClosed2.png"),
                        emission: emission)
                };
            }
        }

        public override string OnDrawnDialogueId => "YesodDrawn";
        public override string OnAttackedDialogueId => "YesodHurt";
        public override string OnSacrificedDialogueId => "YesodSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "YesodSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "YesodSelectableGood";
        public override string OnPlayFromHandDialogueId => "YesodPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "YesodSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "YesodGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "YesodTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "YesodProspector" },
            { Opponent.Type.AnglerBoss, "YesodAngler" },
            { Opponent.Type.TrapperTraderBoss, "YesodTrapperTrader" },
            { Opponent.Type.LeshyBoss, "YesodLeshy" },
            { Opponent.Type.RoyalBoss, "YesodRoyal" },
            { CustomOpponentUtils.ApocalypseBossID, "YesodApocalypse" },
            { OrdealUtils.OpponentID, "YesodOrdeal" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Yesod() => TalkingCardYesod.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardYesod>("Yesod").Id;
        private void Card_Yesod()
        {
            CardManager.New(pluginPrefix, "sephirahYesod", "Yesod",
                attack: 2, health: 3, "A stickler for rules, he'll ensure your beasts' compliance.")
                .SetBloodCost(2)
                .AddAbilities(Ability.Tutor)
                .AddTraits(Sephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}