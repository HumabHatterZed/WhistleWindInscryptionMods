using DiskCardGame;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.Core.Helpers.TextureLoader;

namespace WhistleWindLobotomyMod
{
    public class TalkingCardHokma : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_sephirahHokma";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 0.85f, voiceSoundPitch: 0.6f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromBytes(Artwork.talkingHokmaBody, new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim(Artwork.talkingHokmaEmission);

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingHokmaEyesOpen1, Artwork.talkingHokmaEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingHokmaMouthOpen1, Artwork.talkingHokmaMouthClosed1),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingHokmaEyesOpen3, Artwork.talkingHokmaEyesClosed3),
                        mouth: MakeFaceAnim(Artwork.talkingHokmaMouthOpen2, Artwork.talkingHokmaMouthClosed2),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingHokmaEyesOpen1, Artwork.talkingHokmaEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingHokmaMouthOpen2, Artwork.talkingHokmaMouthClosed2),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingHokmaEyesOpen2, Artwork.talkingHokmaEyesOpen2),
                        mouth: MakeFaceAnim(Artwork.talkingHokmaMouthClosed1, Artwork.talkingHokmaMouthClosed1),
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
            List<Ability> abilities = new() { Protector.ability };

            LobotomyCardManager.CreateCard(
                "wstl_sephirahHokma", "Hokma",
                "All things will happen in time. Just have faith.",
                atk: 1, hp: 2,
                blood: 0, bones: 3, energy: 0,
                null, null,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new() { LobotomyCardManager.TraitSephirah },
                appearances: new(), onePerDeck: true);
        }
    }
}