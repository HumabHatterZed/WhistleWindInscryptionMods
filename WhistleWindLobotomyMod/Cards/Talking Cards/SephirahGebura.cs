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
    public class TalkingCardGebura : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_sephirahGebura";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 1f, voiceSoundPitch: 0.7f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromFile("talkingGeburaBody", new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingGeburaEmission");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingGeburaEyesOpen1", "talkingGeburaEyesClosed1"),
                        mouth: MakeFaceAnim("talkingGeburaMouthOpen1", "talkingGeburaMouthClosed1"),
                        emission: emissionMain),
                    new(emotion: Emotion.Curious,
                        face: face,
                        eyes: MakeFaceAnim("talkingGeburaEyesOpen2", "talkingGeburaEyesClosed2"),
                        mouth: MakeFaceAnim("talkingGeburaMouthOpen2", "talkingGeburaMouthClosed1"),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingGeburaEyesOpen1", "talkingGeburaEyesClosed1"),
                        mouth: MakeFaceAnim("talkingGeburaMouthOpen3", "talkingGeburaMouthClosed2"),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim("talkingGeburaEyesOpen3", "talkingGeburaEyesClosed3"),
                        mouth: MakeFaceAnim("talkingGeburaMouthOpen4", "talkingGeburaMouthClosed1"),
                        emission: emissionMain)
                };
            }
        }

        public override string OnDrawnDialogueId => "GeburaDrawn";
        public override string OnAttackedDialogueId => "GeburaHurt";
        public override string OnSacrificedDialogueId => "GeburaSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "GeburaSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "GeburaSelectableGood";
        public override string OnPlayFromHandDialogueId => "GeburaPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "GeburaSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "GeburaGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "GeburaTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "GeburaProspector" },
            { Opponent.Type.AnglerBoss, "GeburaAngler" },
            { Opponent.Type.TrapperTraderBoss, "GeburaTrapperTrader" },
            { Opponent.Type.LeshyBoss, "GeburaLeshy" },
            { Opponent.Type.RoyalBoss, "GeburaRoyal" },
            { CustomOpponentUtils.ApocalypseBossID, "GeburaApocalypse" },
            { OrdealUtils.OpponentID, "GeburaOrdeal" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Gebura()
        {
            TalkingCardGebura.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardGebura>("Gebura").Id;
        }
        private void Card_Gebura()
        {
            CardManager.New(pluginPrefix, "sephirahGebura", "Gebura",
                attack: 3, health: 5, "Though not as strong as she once was, she will still make for a powerful ally.")
                .SetBloodCost(3)
                .AddAbilities(Ability.GainAttackOnKill, Persistent.ability)
                .AddTraits(Sephirah)
                .SetOnePerDeck()
                .Build();
        }
    }
}