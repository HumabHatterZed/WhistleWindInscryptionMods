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
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.Core.Helpers.TextureLoader;

namespace WhistleWindLobotomyMod
{
    public class TalkingCardTipherethA : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_sephirahTipherethA";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 0.9f, voiceSoundPitch: 1.5f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromBytes(Artwork.talkingTipherethABody, new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim(Artwork.talkingTipherethAEmission1);
                FaceAnim emissionSurprise = MakeFaceAnim(Artwork.talkingTipherethAEmission2);

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingTipherethAEyesOpen1, Artwork.talkingTipherethAEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingTipherethAMouthOpen1, Artwork.talkingTipherethAMouthClosed1),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingTipherethAEyesOpen1, Artwork.talkingTipherethAEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingTipherethAMouthOpen2, Artwork.talkingTipherethAMouthClosed2),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingTipherethAEyesOpen2, Artwork.talkingTipherethAEyesClosed2),
                        mouth: MakeFaceAnim(Artwork.talkingTipherethAMouthOpen3, Artwork.talkingTipherethAMouthClosed3),
                        emission: emissionSurprise),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingTipherethAEyesOpen3, Artwork.talkingTipherethAEyesClosed3),
                        mouth: MakeFaceAnim(Artwork.talkingTipherethAMouthOpen4, Artwork.talkingTipherethAMouthClosed4),
                        emission: emissionMain),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingTipherethAEyesOpen4, Artwork.talkingTipherethAEyesClosed4),
                        mouth: MakeFaceAnim(Artwork.talkingTipherethAMouthOpen5, Artwork.talkingTipherethAMouthClosed1),
                        emission: emissionMain),
                };
            }
        }

        public override string OnDrawnDialogueId => "SephirahTipherethADrawn";
        public override string OnAttackedDialogueId => "SephirahTipherethAHurt";
        public override string OnSacrificedDialogueId => "SephirahTipherethASacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "SephirahTipherethASelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "SephirahTipherethASelectableGood";
        public override string OnPlayFromHandDialogueId => "SephirahTipherethAPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "SephirahTipherethASelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "SephirahTipherethAGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "SephirahTipherethATrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "SephirahTipherethAProspector" },
            { Opponent.Type.AnglerBoss, "SephirahTipherethAAngler" },
            { Opponent.Type.TrapperTraderBoss, "SephirahTipherethATrapperTrader" },
            { Opponent.Type.LeshyBoss, "SephirahTipherethALeshy" },
            { Opponent.Type.RoyalBoss, "SephirahTipherethARoyal" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_TipherethA()
        {
            TalkingCardTipherethA.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardTipherethA>("TipherethA").Id;
        }
        private void Card_TipherethA()
        {
            List<Ability> abilities = new() { GiftGiver.ability };

            LobotomyCardManager.CreateCard(
                "wstl_sephirahTipherethA", "Tiphereth",
                "One of a pair of twins. Don't underestimate her capabilities.",
                atk: 1, hp: 2,
                blood: 0, bones: 0, energy: 4,
                null, null,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new() { LobotomyCardManager.TraitSephirah },
                appearances: new(), onePerDeck: true)
                .SetExtendedProperty("wstl:GiftGiver", "wstl_sephirahTipherethB");
        }
    }
}