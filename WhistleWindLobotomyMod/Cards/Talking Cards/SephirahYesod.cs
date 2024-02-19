using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.TalkingCards;
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
                Sprite face = LoadSpriteFromFile("talkingYesodBody", new(0.5f, 0f));
                FaceAnim emission = MakeFaceAnim("talkingYesodEyesEmission");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingYesodEyesOpen1", "talkingYesodEyesClosed1"),
                        mouth: MakeFaceAnim("talkingYesodMouthOpen1", "talkingYesodMouthClosed1"),
                        emission: emission),
                    new(emotion: Emotion.Curious,
                        face: face,
                        eyes: MakeFaceAnim("talkingYesodEyesOpen2", "talkingYesodEyesClosed2"),
                        mouth: MakeFaceAnim("talkingYesodMouthOpen2", "talkingYesodMouthClosed1"),
                        emission: emission),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingYesodEyesOpen3", "talkingYesodEyesClosed3"),
                        mouth: MakeFaceAnim("talkingYesodMouthOpen2", "talkingYesodMouthClosed1"),
                        emission: emission),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingYesodEyesOpen1", "talkingYesodEyesClosed1"),
                        mouth: MakeFaceAnim("talkingYesodMouthOpen3", "talkingYesodMouthClosed2"),
                        emission: emission)
                };
            }
        }

        public override string OnDrawnDialogueId => "SephirahYesodDrawn";
        public override string OnAttackedDialogueId => "SephirahYesodHurt";
        public override string OnSacrificedDialogueId => "SephirahYesodSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "SephirahYesodSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "SephirahYesodSelectableGood";
        public override string OnPlayFromHandDialogueId => "SephirahYesodPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "SephirahYesodSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "SephirahYesodGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "SephirahYesodTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "SephirahYesodProspector" },
            { Opponent.Type.AnglerBoss, "SephirahYesodAngler" },
            { Opponent.Type.TrapperTraderBoss, "SephirahYesodTrapperTrader" },
            { Opponent.Type.LeshyBoss, "SephirahYesodLeshy" },
            { Opponent.Type.RoyalBoss, "SephirahYesodRoyal" },
            { ApocalypseBossOpponent.ID, "SephirahYesodApocalypse" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Yesod() => TalkingCardYesod.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardYesod>("Yesod").Id;
        private void Card_Yesod()
        {
            NewCard("sephirahYesod", "Yesod", "The head of the Information Department. Incompetence will not be tolerated.",
                attack: 2, health: 3, blood: 2)
                .AddAbilities(Ability.Tutor)
                .AddTraits(Sephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}