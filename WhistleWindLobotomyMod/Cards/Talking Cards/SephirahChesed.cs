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
                Sprite face = LoadSpriteFromBytes(Artwork.talkingChesedBody, new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim(Artwork.talkingChesedEmission);

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingChesedEyesOpen1, Artwork.talkingChesedEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingChesedMouthOpen1, Artwork.talkingChesedMouthClosed1),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingChesedEyesOpen3, Artwork.talkingChesedEyesClosed3),
                        mouth: MakeFaceAnim(Artwork.talkingChesedMouthOpen2, Artwork.talkingChesedMouthClosed2),
                        emission: emissionMain),
                    new(emotion: Emotion.Curious,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingChesedEyesOpen2, Artwork.talkingChesedEyesClosed2),
                        mouth: MakeFaceAnim(Artwork.talkingChesedMouthOpen2, Artwork.talkingChesedMouthClosed2),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingChesedEyesOpen1, Artwork.talkingChesedEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingChesedMouthOpen2, Artwork.talkingChesedMouthClosed2),
                        emission: emissionMain),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingChesedEyesClosed1, Artwork.talkingChesedEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingChesedMouthOpen2, Artwork.talkingChesedMouthClosed2),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingChesedEyesOpen2, Artwork.talkingChesedEyesClosed2),
                        mouth: MakeFaceAnim(Artwork.talkingChesedMouthOpen3, Artwork.talkingChesedMouthClosed1),
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
            { Opponent.Type.RoyalBoss, "SephirahChesedRoyal" }
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
            List<Ability> abilities = new() { Healer.ability, ThickSkin.ability };

            LobotomyCardManager.CreateCard(
                "wstl_sephirahChesed", "Chesed",
                "Nothing like a fresh cup of coffee to start your day.",
                atk: 0, hp: 5,
                blood: 1, bones: 0, energy: 0,
                null, null,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new() { LobotomyCardManager.TraitSephirah },
                appearances: new(), onePerDeck: true);
        }
    }
}