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
    public class TalkingCardChesed : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_sephirahChesed";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 0.9f, voiceSoundPitch: 0.7f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromFile("talkingChesedBody", new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingChesedEmission");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingChesedEyesOpen1", "talkingChesedEyesClosed1"),
                        mouth: MakeFaceAnim("talkingChesedMouthOpen1", "talkingChesedMouthClosed1"),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim("talkingChesedEyesOpen3", "talkingChesedEyesClosed3"),
                        mouth: MakeFaceAnim("talkingChesedMouthOpen2", "talkingChesedMouthClosed2"),
                        emission: emissionMain),
                    new(emotion: Emotion.Curious,
                        face: face,
                        eyes: MakeFaceAnim("talkingChesedEyesOpen2", "talkingChesedEyesClosed2"),
                        mouth: MakeFaceAnim("talkingChesedMouthOpen2", "talkingChesedMouthClosed2"),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingChesedEyesOpen1", "talkingChesedEyesClosed1"),
                        mouth: MakeFaceAnim("talkingChesedMouthOpen2", "talkingChesedMouthClosed2"),
                        emission: emissionMain),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim("talkingChesedEyesClosed1", "talkingChesedEyesClosed1"),
                        mouth: MakeFaceAnim("talkingChesedMouthOpen2", "talkingChesedMouthClosed2"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingChesedEyesOpen2", "talkingChesedEyesClosed2"),
                        mouth: MakeFaceAnim("talkingChesedMouthOpen3", "talkingChesedMouthClosed1"),
                        emission: emissionMain)
                };
            }
        }

        public override string OnDrawnDialogueId => "SephirahChesedDrawn";
        public override string OnAttackedDialogueId => "SephirahChesedHurt";
        public override string OnSacrificedDialogueId => "SephirahChesedSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "SephirahChesedSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "SephirahChesedSelectableGood";
        public override string OnPlayFromHandDialogueId => "SephirahChesedPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "SephirahChesedSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "SephirahChesedGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "SephirahChesedTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "SephirahChesedProspector" },
            { Opponent.Type.AnglerBoss, "SephirahChesedAngler" },
            { Opponent.Type.TrapperTraderBoss, "SephirahChesedTrapperTrader" },
            { Opponent.Type.LeshyBoss, "SephirahChesedLeshy" },
            { Opponent.Type.RoyalBoss, "SephirahChesedRoyal" },
            { ApocalypseBossOpponent.ID, "SephirahChesedApocalypse" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Chesed()
        {
            TalkingCardChesed.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardChesed>("Chesed").Id;
        }
        private void Card_Chesed()
        {
            NewCard("sephirahChesed", "Chesed", "Nothing like a fresh cup of coffee to start your day.",
                attack: 0, health: 4, blood: 1)
                .AddAbilities(Healer.ability, ThickSkin.ability)
                .AddTraits(TraitSephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}