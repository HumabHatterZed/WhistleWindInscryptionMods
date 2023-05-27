using DiskCardGame;
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
                Sprite face = LoadSpriteFromBytes(Artwork.talkingAngelaBody, new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim(Artwork.talkingAngelaEmission);

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingAngelaEyesClosed1, Artwork.talkingAngelaEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingAngelaMouthOpen1, Artwork.talkingAngelaMouthClosed1),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingAngelaEyesClosed1, Artwork.talkingAngelaEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingAngelaMouthOpen2, Artwork.talkingAngelaMouthClosed2),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Curious,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingAngelaEyesClosed2, Artwork.talkingAngelaEyesClosed2),
                        mouth: MakeFaceAnim(Artwork.talkingAngelaMouthClosed1, Artwork.talkingAngelaMouthClosed1),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingAngelaEyesOpen1, Artwork.talkingAngelaEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingAngelaMouthClosed1, Artwork.talkingAngelaMouthClosed1),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingAngelaEyesClosed3, Artwork.talkingAngelaEyesClosed3),
                        mouth: MakeFaceAnim(Artwork.talkingAngelaMouthClosed1, Artwork.talkingAngelaMouthClosed1),
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
            { Opponent.Type.RoyalBoss, "AngelaRoyal" }
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
            List<Ability> abilities = new() { FrostRuler.ability };

            LobotomyCardManager.CreateCard(
                "wstl_angela", "Angela",
                "A trustworthy AI assistant.",
                atk: 3, hp: 3,
                blood: 0, bones: 0, energy: 6,
                null, null,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new() { LobotomyCardManager.TraitSephirah },
                appearances: new(), onePerDeck: true);
        }
    }
}