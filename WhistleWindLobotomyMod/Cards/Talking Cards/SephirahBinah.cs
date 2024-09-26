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
    public class TalkingCardBinah : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_sephirahBinah";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 0.8f, voiceSoundPitch: 0.7f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromFile("talkingBinahBody.png", new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingBinahEmission.png");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingBinahEyesOpen1.png", "talkingBinahEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingBinahMouthOpen1.png", "talkingBinahMouthClosed1.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim("talkingBinahEyesOpen3.png", "talkingBinahEyesClosed3.png"),
                        mouth: MakeFaceAnim("talkingBinahMouthOpen2.png", "talkingBinahMouthClosed2.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingBinahEyesOpen1.png", "talkingBinahEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingBinahMouthOpen2.png", "talkingBinahMouthClosed2.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingBinahEyesOpen2.png", "talkingBinahEyesOpen2.png"),
                        mouth: MakeFaceAnim("talkingBinahMouthClosed1.png", "talkingBinahMouthClosed1.png"),
                        emission: emissionMain)
                };
            }
        }

        public override string OnDrawnDialogueId => "BinahDrawn";
        public override string OnAttackedDialogueId => "BinahHurt";
        public override string OnSacrificedDialogueId => "BinahSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "BinahSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "BinahSelectableGood";
        public override string OnPlayFromHandDialogueId => "BinahPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "BinahSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "BinahGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "BinahTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "BinahProspector" },
            { Opponent.Type.AnglerBoss, "BinahAngler" },
            { Opponent.Type.TrapperTraderBoss, "BinahTrapperTrader" },
            { Opponent.Type.LeshyBoss, "BinahLeshy" },
            { Opponent.Type.RoyalBoss, "BinahRoyal" },
            { CustomOpponentUtils.ApocalypseBossID, "BinahApocalypse" },
            { OrdealUtils.OpponentID, "BinahOrdeal" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Binah()
        {
            TalkingCardBinah.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardBinah>("Binah").Id;
        }
        private void Card_Binah()
        {
            CardManager.New(pluginPrefix, "sephirahBinah", "Binah",
                attack: 3, health: 5, "Callous and sadistic, she seems keen on seeing your struggles in person.")
                .SetBloodCost(3)
                .AddAbilities(Ability.Sniper, Piercing.ability)
                .AddTraits(Sephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}