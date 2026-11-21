using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using static WhistleWind.Core.Helpers.TextureLoader;

namespace WhistleWindLobotomyMod {
    public class TalkingCardBongBong : CustomPaperTalkingCard {
        public override string CardName => Cards.bongbong;
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 4f, voiceSoundPitch: 1.1f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions {
            get {
                Sprite face = LoadSpriteFromFile("talkingBongBongBody.png", new Vector2(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingBongBongEmission.png");

                // neutral, laughter, curious (determined), quiet (scared), surprise (ouch)
                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingBongBongEyesOpen1.png", "talkingBongBongEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingBongBongMouthOpen1.png", "talkingBongBongMouthClosed1.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingBongBongEyesOpen2.png", "talkingBongBongEyesClosed2.png"),
                        mouth: MakeFaceAnim("talkingBongBongMouthOpen2.png", "talkingBongBongMouthClosed2.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Curious, // determined
                        face: face,
                        eyes: MakeFaceAnim("talkingBongBongEyesOpen3.png", "talkingBongBongEyesClosed3.png"),
                        mouth: MakeFaceAnim("talkingBongBongMouthOpen3.png", "talkingBongBongMouthClosed3.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim("talkingBongBongEyesOpen4.png", "talkingBongBongEyesClosed4.png"),
                        mouth: MakeFaceAnim("talkingBongBongMouthOpen4.png", "talkingBongBongMouthClosed4.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim("talkingBongBongEyesOpen5.png", "talkingBongBongEyesClosed5.png"),
                        mouth: MakeFaceAnim("talkingBongBongMouthOpen5.png", "talkingBongBongMouthClosed5.png"),
                        emission: emissionMain)
                };
            }
        }

        public override string OnDrawnDialogueId => "BongBongDetermined";
        public override string OnAttackedDialogueId => "BongBongOuch";
        public override string OnSacrificedDialogueId => "BongBongOuch";
        public override string OnBecomeSelectableNegativeDialogueId => "BongBongScared";
        public override string OnBecomeSelectablePositiveDialogueId => "BongBongHappy";
        public override string OnPlayFromHandDialogueId => "BongBongDetermined";
        public override string OnSelectedForCardRemoveDialogueId => "BongBongScared";
        public override string OnSelectedForCardMergeDialogueId => "BongBongHappy";
        public override string OnSelectedForDeckTrialDialogueId => "BongBongScared";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "BongBongHappy" },
            { Opponent.Type.AnglerBoss, "BongBongHappy" },
            { Opponent.Type.TrapperTraderBoss, "BongBongDetermined" },
            { Opponent.Type.LeshyBoss, "BongBongDetermined" },
            { Opponent.Type.RoyalBoss, "BongBongHappy" },
            { LobOpponentUtils.ApocalypseBossID, "BongBongScared" },
            { OrdealUtils.OpponentID, "BongBongDetermined" },
            { OrdealUtils.SweeperOpponentID, "BongBongScared" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class Cards {
        public const string bongbong = "wstl_bongbong";
        private static void BongBong() {
            CardManager.New(LobotomyPlugin.pluginPrefix, bongbong, "BongBong",
                attack: 2, health: 2)
                .SetCost(2, 2, 2)
                .AddAbilities(Ability.Sniper, Ability.AllStrike)
                .SetCannotGiveSigils()
                .SetCannotCopyCard()
                .SetOnePerDeck()
                .Build();
        }
    }
    public partial class Abilities {
        private static void AddSpecial_BongBong() {
            TalkingCardBongBong.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardBongBong>("BongBong").Id;
        }
    }
}