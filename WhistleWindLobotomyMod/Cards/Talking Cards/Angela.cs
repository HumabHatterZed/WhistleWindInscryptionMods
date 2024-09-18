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
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;
using static WhistleWind.Core.Helpers.TextureLoader;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public class TalkingCardAngela : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_angela";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 0.6f, voiceSoundPitch: 1f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromFile("talkingAngelaBody", new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingAngelaEmission");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingAngelaEyesClosed1"),
                        mouth: MakeFaceAnim("talkingAngelaMouthOpen1", "talkingAngelaMouthClosed1"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingAngelaEyesClosed1"),
                        mouth: MakeFaceAnim("talkingAngelaMouthOpen2", "talkingAngelaMouthClosed2"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Curious,
                        face: face,
                        eyes: MakeFaceAnim("talkingAngelaEyesClosed2"),
                        mouth: MakeFaceAnim("talkingAngelaMouthClosed1", "talkingAngelaMouthClosed1"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim("talkingAngelaEyesOpen1", "talkingAngelaEyesClosed1"),
                        mouth: MakeFaceAnim("talkingAngelaMouthClosed1", "talkingAngelaMouthClosed1"),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingAngelaEyesClosed3"),
                        mouth: MakeFaceAnim("talkingAngelaMouthClosed1", "talkingAngelaMouthClosed1"),
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
            { CustomOpponentUtils.ApocalypseBossID, "AngelaApocalypse" },
            { OrdealUtils.OpponentID, "AngelaOrdeal" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Angela()
        {
            TalkingCardAngela.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardAngela>("Angela").Id;
        }
        private void Card_Angela()
        {
            CardManager.New(pluginPrefix, "angela", "Angela",
                attack: 2, health: 3)
                .SetEnergyCost(6)
                .AddAbilities(FrostRuler.ability, Persecutor.ability)
                .AddTraits()
                .SetOnePerDeck()
                .Build();
        }
    }
}