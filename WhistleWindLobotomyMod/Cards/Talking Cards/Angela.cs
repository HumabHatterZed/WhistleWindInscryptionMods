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
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public class TalkingCardAngela : CustomPaperTalkingCard {
        public override string CardName => Cards.angela;
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 4f, voiceSoundPitch: 1.1f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions {
            get {
                Sprite face = LoadSpriteFromFile("talkingAngelaBody.png", new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingAngelaEmission.png");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingAngelaEyesClosed1.png", "talkingAngelaEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingAngelaMouthOpen1.png", "talkingAngelaMouthClosed1.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingAngelaEyesClosed1.png", "talkingAngelaEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingAngelaMouthOpen2.png", "talkingAngelaMouthClosed2.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Curious,
                        face: face,
                        eyes: MakeFaceAnim("talkingAngelaEyesClosed2.png", "talkingAngelaEyesClosed2.png"),
                        mouth: MakeFaceAnim("talkingAngelaMouthClosed1.png", "talkingAngelaMouthClosed1.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim("talkingAngelaEyesOpen1.png", "talkingAngelaEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingAngelaMouthClosed1.png", "talkingAngelaMouthClosed1.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingAngelaEyesClosed3.png", "talkingAngelaEyesClosed3.png"),
                        mouth: MakeFaceAnim("talkingAngelaMouthClosed1.png", "talkingAngelaMouthClosed1.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple)
                };
            }
        }

        public override string OnDrawnDialogueId => "AngelaDrawn";
        public override string OnAttackedDialogueId => "AngelaHurt";
        public override string OnSacrificedDialogueId => "AngelaSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "AngelaSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "AngelaSelectableGood";
        public override string OnPlayFromHandDialogueId => "AngelaPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "AngelaSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "AngelaGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "AngelaTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "AngelaProspector" },
            { Opponent.Type.AnglerBoss, "AngelaAngler" },
            { Opponent.Type.TrapperTraderBoss, "AngelaTrapperTrader" },
            { Opponent.Type.LeshyBoss, "AngelaLeshy" },
            { Opponent.Type.RoyalBoss, "AngelaRoyal" },
            { LobOpponentUtils.ApocalypseBossID, "AngelaApocalypse" },
            { OrdealUtils.OpponentID, "AngelaOrdeal" },
            { OrdealUtils.SweeperOpponentID, "AngelaSweeper" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class Cards {
        public const string angela = "wstl_angela";
        private static void Angela() {
            CardManager.New(LobotomyPlugin.pluginPrefix, angela, "Angela",
                attack: 3, health: 3)
                .SetEnergyCost(6)
                .AddAbilities(ScenarioOverseer.ID)
                .SetCannotGiveSigils()
                .SetCannotCopyCard()
                .SetOnePerDeck()
                .Build();
        }
    }
    public partial class Abilities {
        private static void AddSpecial_Angela() {
            TalkingCardAngela.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardAngela>("Angela").Id;
        }
    }
}