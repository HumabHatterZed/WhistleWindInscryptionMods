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
    public class TalkingCardTipherethA : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_sephirahTipherethA";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 0.9f, voiceSoundPitch: 1.5f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromFile("talkingTipherethABody", new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingTipherethAEmission1");
                FaceAnim emissionSurprise = MakeFaceAnim("talkingTipherethAEmission2");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingTipherethAEyesOpen1", "talkingTipherethAEyesClosed1"),
                        mouth: MakeFaceAnim("talkingTipherethAMouthOpen1", "talkingTipherethAMouthClosed1"),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingTipherethAEyesOpen1", "talkingTipherethAEyesClosed1"),
                        mouth: MakeFaceAnim("talkingTipherethAMouthOpen2", "talkingTipherethAMouthClosed2"),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim("talkingTipherethAEyesOpen2", "talkingTipherethAEyesClosed2"),
                        mouth: MakeFaceAnim("talkingTipherethAMouthOpen3", "talkingTipherethAMouthClosed3"),
                        emission: emissionSurprise),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingTipherethAEyesOpen3", "talkingTipherethAEyesClosed3"),
                        mouth: MakeFaceAnim("talkingTipherethAMouthOpen4", "talkingTipherethAMouthClosed4"),
                        emission: emissionMain),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim("talkingTipherethAEyesOpen4", "talkingTipherethAEyesClosed4"),
                        mouth: MakeFaceAnim("talkingTipherethAMouthOpen5", "talkingTipherethAMouthClosed1"),
                        emission: emissionMain),
                };
            }
        }

        public override string OnDrawnDialogueId => "TipherethADrawn";
        public override string OnAttackedDialogueId => "TipherethAHurt";
        public override string OnSacrificedDialogueId => "TipherethASacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "TipherethASelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "TipherethASelectableGood";
        public override string OnPlayFromHandDialogueId => "TipherethAPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "TipherethASelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "TipherethAGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "TipherethATrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "TipherethAProspector" },
            { Opponent.Type.AnglerBoss, "TipherethAAngler" },
            { Opponent.Type.TrapperTraderBoss, "TipherethATrapperTrader" },
            { Opponent.Type.LeshyBoss, "TipherethALeshy" },
            { Opponent.Type.RoyalBoss, "TipherethARoyal" },
            { CustomOpponentUtils.ApocalypseBossID, "TipherethAApocalypse" },
            { OrdealUtils.OpponentID, "TipherethAOrdeal" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_TipherethA()
        {
            TalkingCardTipherethA.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardTipherethA>("TipherethA").Id;
        }
        private void Card_TipherethA()
        {
            NewCard("sephirahTipherethA", "Tiphereth", "A foul-mouthed child. She's never seen without her brother.",
                attack: 1, health: 2, energy: 3)
                .AddAbilities(GiftGiver.ability)
                .AddTraits(Sephirah)
                .SetOnePerDeck()
                .SetExtendedProperty("wstl:GiftGiver", "wstl_sephirahTipherethB")
                .Build();
        }
    }
}