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
                Sprite face = LoadSpriteFromBytes(Artwork.talkingBinahBody, new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim(Artwork.talkingBinahEmission);

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingBinahEyesOpen1, Artwork.talkingBinahEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingBinahMouthOpen1, Artwork.talkingBinahMouthClosed1),
                        emission: emissionMain),
                    new(emotion: Emotion.Surprise,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingBinahEyesOpen3, Artwork.talkingBinahEyesClosed3),
                        mouth: MakeFaceAnim(Artwork.talkingBinahMouthOpen2, Artwork.talkingBinahMouthClosed2),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingBinahEyesOpen1, Artwork.talkingBinahEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingBinahMouthOpen2, Artwork.talkingBinahMouthClosed2),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingBinahEyesOpen2, Artwork.talkingBinahEyesOpen2),
                        mouth: MakeFaceAnim(Artwork.talkingBinahMouthClosed1, Artwork.talkingBinahMouthClosed1),
                        emission: emissionMain)
                };
            }
        }

        public override string OnDrawnDialogueId => "SephirahBinahDrawn";
        public override string OnAttackedDialogueId => "SephirahBinahHurt";
        public override string OnSacrificedDialogueId => "SephirahBinahSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "SephirahBinahSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "SephirahBinahSelectableGood";
        public override string OnPlayFromHandDialogueId => "SephirahBinahPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "SephirahBinahSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "SephirahBinahGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "SephirahBinahTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "SephirahBinahProspector" },
            { Opponent.Type.AnglerBoss, "SephirahBinahAngler" },
            { Opponent.Type.TrapperTraderBoss, "SephirahBinahTrapperTrader" },
            { Opponent.Type.LeshyBoss, "SephirahBinahLeshy" },
            { Opponent.Type.RoyalBoss, "SephirahBinahRoyal" }
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
            List<Ability> abilities = new() { Ability.Sniper, Piercing.ability };

            LobotomyCardManager.CreateCard(
                "wstl_sephirahBinah", "Binah",
                "Though not as powerful as she once was, she will still make a great ally.",
                atk: 3, hp: 5,
                blood: 3, bones: 0, energy: 0,
                null, null,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new() { LobotomyCardManager.TraitSephirah },
                appearances: new(), onePerDeck: true);
        }
    }
}