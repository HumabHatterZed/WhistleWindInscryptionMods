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
    public class TalkingCardHokma : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_sephirahHokma";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 0.85f, voiceSoundPitch: 0.5f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromFile("talkingHokmaBody", new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingHokmaEmission");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingHokmaEyesOpen1", "talkingHokmaEyesClosed1"),
                        mouth: MakeFaceAnim("talkingHokmaMouthOpen1", "talkingHokmaMouthClosed1"),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingHokmaEyesOpen1", "talkingHokmaEyesClosed1"),
                        mouth: MakeFaceAnim("talkingHokmaMouthOpen2", "talkingHokmaMouthClosed2"),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingHokmaEyesOpen2", "talkingHokmaEyesOpen2"),
                        mouth: MakeFaceAnim("talkingHokmaMouthClosed1", "talkingHokmaMouthClosed1"),
                        emission: emissionMain)
                };
            }
        }

        public override string OnDrawnDialogueId => "HokmaDrawn";
        public override string OnAttackedDialogueId => "HokmaHurt";
        public override string OnSacrificedDialogueId => "HokmaSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "HokmaSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "HokmaSelectableGood";
        public override string OnPlayFromHandDialogueId => "HokmaPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "HokmaSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "HokmaGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "HokmaTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "HokmaProspector" },
            { Opponent.Type.AnglerBoss, "HokmaAngler" },
            { Opponent.Type.TrapperTraderBoss, "HokmaTrapperTrader" },
            { Opponent.Type.LeshyBoss, "HokmaLeshy" },
            { Opponent.Type.RoyalBoss, "HokmaRoyal" },
            { CustomOpponentUtils.ApocalypseBossID, "HokmaApocalypse" },
            { OrdealUtils.OpponentID, "HokmaOrdeal" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Hokma()
        {
            TalkingCardHokma.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardHokma>("Hokma").Id;
        }
        private void Card_Hokma()
        {
            CardManager.New(pluginPrefix, "sephirahHokma", "Hokma",
                attack: 1, health: 4, "All things will happen in time. Just have faith.")
                .SetBloodCost(2)
                .AddAbilities(NeuteredLatch.ability)
                .AddTraits(Sephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}