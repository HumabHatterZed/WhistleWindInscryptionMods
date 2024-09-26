﻿using DiskCardGame;
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
                Sprite face = LoadSpriteFromFile("talkingChesedBody.png", new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingChesedEmission.png");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingChesedEyesOpen1.png", "talkingChesedEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingChesedMouthOpen1.png", "talkingChesedMouthClosed1.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim("talkingChesedEyesOpen3.png", "talkingChesedEyesClosed3.png"),
                        mouth: MakeFaceAnim("talkingChesedMouthOpen2.png", "talkingChesedMouthClosed2.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Curious,
                        face: face,
                        eyes: MakeFaceAnim("talkingChesedEyesOpen2.png", "talkingChesedEyesClosed2.png"),
                        mouth: MakeFaceAnim("talkingChesedMouthOpen2.png", "talkingChesedMouthClosed2.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingChesedEyesOpen1.png", "talkingChesedEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingChesedMouthOpen2.png", "talkingChesedMouthClosed2.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim("talkingChesedEyesClosed1.png", "talkingChesedEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingChesedMouthOpen2.png", "talkingChesedMouthClosed2.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingChesedEyesOpen2.png", "talkingChesedEyesClosed2.png"),
                        mouth: MakeFaceAnim("talkingChesedMouthOpen3.png", "talkingChesedMouthClosed1.png"),
                        emission: emissionMain)
                };
            }
        }

        public override string OnDrawnDialogueId => "ChesedDrawn";
        public override string OnAttackedDialogueId => "ChesedHurt";
        public override string OnSacrificedDialogueId => "ChesedSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "ChesedSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "ChesedSelectableGood";
        public override string OnPlayFromHandDialogueId => "ChesedPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "ChesedSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "ChesedGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "ChesedTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "ChesedProspector" },
            { Opponent.Type.AnglerBoss, "ChesedAngler" },
            { Opponent.Type.TrapperTraderBoss, "ChesedTrapperTrader" },
            { Opponent.Type.LeshyBoss, "ChesedLeshy" },
            { Opponent.Type.RoyalBoss, "ChesedRoyal" },
            { CustomOpponentUtils.ApocalypseBossID, "ChesedApocalypse" },
            { OrdealUtils.OpponentID, "ChesedOrdeal" }
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
            CardManager.New(pluginPrefix, "sephirahChesed", "Chesed",
                attack: 1, health: 4, "Nothing like a fresh cup of coffee to start your day.")
                .SetEnergyCost(4)
                .AddAbilities(Healer.ability, Regenerator.ability)
                .AddTraits(Sephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}