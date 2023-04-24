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
    public class TalkingCardYesod : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_sephirahYesod";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 1.0f, voiceSoundPitch: 0.7f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromBytes(Artwork.talkingYesodBody, new(0.5f, 0f));
                FaceAnim emission = MakeFaceAnim(Artwork.talkingYesodEyesEmission);

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingYesodEyesOpen1, Artwork.talkingYesodEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingYesodMouthOpen1, Artwork.talkingYesodMouthClosed1),
                        emission: emission),
                    new(emotion: Emotion.Curious,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingYesodEyesOpen2, Artwork.talkingYesodEyesClosed2),
                        mouth: MakeFaceAnim(Artwork.talkingYesodMouthOpen2, Artwork.talkingYesodMouthClosed1),
                        emission: emission),
                    new(emotion: Emotion.Anger,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingYesodEyesOpen3, Artwork.talkingYesodEyesClosed3),
                        mouth: MakeFaceAnim(Artwork.talkingYesodMouthOpen2, Artwork.talkingYesodMouthClosed1),
                        emission: emission),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingYesodEyesOpen1, Artwork.talkingYesodEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingYesodMouthOpen3, Artwork.talkingYesodMouthClosed2),
                        emission: emission)
                };
            }
        }

        public override string OnDrawnDialogueId => "SephirahYesodDrawn";
        public override string OnAttackedDialogueId => "SephirahYesodHurt";
        public override string OnSacrificedDialogueId => "SephirahYesodSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "SephirahYesodSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "SephirahYesodSelectableGood";
        public override string OnPlayFromHandDialogueId => "SephirahYesodPlayed";
        public override string OnSelectedForCardRemoveDialogueId => "SephirahYesodSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "SephirahYesodGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "SephirahYesodTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new Dictionary<Opponent.Type, string>()
        {
            { Opponent.Type.ProspectorBoss, "SephirahYesodChoice" }
        };

        public override void OnShownForCardChoiceNode()
        {
            this.TriggerSoloDialogue("SephirahYesodChoice");
            base.OnShownForCardChoiceNode();
        }
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_Yesod() => TalkingCardYesod.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardYesod>("Yesod").Id;
        private void Card_Yesod()
        {
            List<Ability> abilities = new()
            {
                Ability.Tutor,
                Corrector.ability
            };
            LobotomyCardManager.CreateCard(
                "wstl_sephirahYesod", "Yesod",
                "The head of the Information Department. Incompetence is not tolerated.",
                atk: 0, hp: 1,
                blood: 2, bones: 0, energy: 0,
                null, null,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new() { LobotomyCardManager.TraitSephirah },
                appearances: new(), onePerDeck: true);
        }
    }
}