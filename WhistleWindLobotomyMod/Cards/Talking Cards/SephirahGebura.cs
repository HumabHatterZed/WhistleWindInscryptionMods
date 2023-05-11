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
                Sprite face = LoadSpriteFromBytes(Artwork.talkingGeburaBody, new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim(Artwork.talkingGeburaEmission);

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingGeburaEyesOpen1, Artwork.talkingGeburaEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingGeburaMouthOpen1, Artwork.talkingGeburaMouthClosed1),
                        emission: emissionMain),
                    new(emotion: Emotion.Curious,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingGeburaEyesOpen2, Artwork.talkingGeburaEyesClosed2),
                        mouth: MakeFaceAnim(Artwork.talkingGeburaMouthOpen2, Artwork.talkingGeburaMouthClosed1),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingGeburaEyesOpen1, Artwork.talkingGeburaEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingGeburaMouthOpen3, Artwork.talkingGeburaMouthClosed2),
                        emission: emissionMain),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingGeburaEyesOpen3, Artwork.talkingGeburaEyesClosed3),
                        mouth: MakeFaceAnim(Artwork.talkingGeburaMouthOpen4, Artwork.talkingGeburaMouthClosed1),
                        emission: emissionMain)
                };
            }
        }

        public override string OnDrawnDialogueId => "SephirahGeburaDrawn";
        public override string OnAttackedDialogueId => "SephirahGeburaHurt";
        public override string OnSacrificedDialogueId => "SephirahGeburaSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "SephirahGeburaSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "SephirahGeburaSelectableGood";
        public override string OnPlayFromHandDialogueId => "SephirahGeburaPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "SephirahGeburaSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "SephirahGeburaGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "SephirahGeburaTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "SephirahGeburaChoice" }
        };
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Gebura()
        {
            TalkingCardGebura.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardGebura>("Gebura").Id;
        }
        private void Card_Gebura()
        {
            List<Ability> abilities = new() { Ability.GainAttackOnKill, Piercing.ability };

            LobotomyCardManager.CreateCard(
                "wstl_sephirahGebura", "Gebura",
                "Head of the Disciplinary Team. A fierce warrior and ally.",
                atk: 3, hp: 5,
                blood: 3, bones: 0, energy: 0,
                null, null,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new() { LobotomyCardManager.TraitSephirah },
                appearances: new(), onePerDeck: true);
        }
    }
}