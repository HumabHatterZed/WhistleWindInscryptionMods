using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

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

        public override string OnDrawnDialogueId => "SephirahHokmaDrawn";
        public override string OnAttackedDialogueId => "SephirahHokmaHurt";
        public override string OnSacrificedDialogueId => "SephirahHokmaSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "SephirahHokmaSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "SephirahHokmaSelectableGood";
        public override string OnPlayFromHandDialogueId => "SephirahHokmaPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "SephirahHokmaSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "SephirahHokmaGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "SephirahHokmaTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "SephirahHokmaProspector" },
            { Opponent.Type.AnglerBoss, "SephirahHokmaAngler" },
            { Opponent.Type.TrapperTraderBoss, "SephirahHokmaTrapperTrader" },
            { Opponent.Type.LeshyBoss, "SephirahHokmaLeshy" },
            { Opponent.Type.RoyalBoss, "SephirahHokmaRoyal" }
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
            NewCard("sephirahHokma", "Hokma", "All things will happen in time. Just have faith.",
                attack: 2, health: 3, blood: 2)
                .AddAbilities(NeuteredLatch.ability)
                .AddTraits(TraitSephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}