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
    public class TalkingCardTipherethB : CustomPaperTalkingCard
    {
        public override string CardName => "wstl_sephirahTipherethB";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 0.9f, voiceSoundPitch: 1.4f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = LoadSpriteFromBytes(Artwork.talkingTipherethBBody, new(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim(Artwork.talkingTipherethBEmission);

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingTipherethBEyesOpen1, Artwork.talkingTipherethBEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingTipherethBMouthOpen1, Artwork.talkingTipherethBMouthClosed1),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingTipherethBEyesOpen1, Artwork.talkingTipherethBEyesClosed1),
                        mouth: MakeFaceAnim(Artwork.talkingTipherethBMouthOpen2, Artwork.talkingTipherethBMouthClosed2),
                        emission: emissionMain),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim(Artwork.talkingTipherethBEyesOpen2, Artwork.talkingTipherethBEyesOpen2),
                        mouth: MakeFaceAnim(Artwork.talkingTipherethBMouthClosed1, Artwork.talkingTipherethBMouthClosed1),
                        emission: GeneratePortrait.EmptyPortraitTuple)
                };
            }
        }

        public override string OnDrawnDialogueId => "SephirahTipherethBDrawn";
        public override string OnAttackedDialogueId => "SephirahTipherethBSacrificed";
        public override string OnSacrificedDialogueId => "SephirahTipherethBSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "SephirahTipherethBSacrificed";
        public override string OnBecomeSelectablePositiveDialogueId => "SephirahTipherethBSacrificed";
        public override string OnPlayFromHandDialogueId => "SephirahTipherethBPlayed";

        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new();
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_TipherethB()
        {
            TalkingCardTipherethB.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardTipherethB>("TipherethB").Id;
        }
        private void Card_TipherethB()
        {
            List<Ability> abilities = new() { Ability.DrawCopyOnDeath, Ability.LatchDeathShield };

            LobotomyCardManager.CreateCard(
                "wstl_sephirahTipherethB", "Tiphereth",
                "",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 2,
                null, null,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new() { LobotomyCardManager.TraitSephirah },
                appearances: new(), onePerDeck: true);
        }
    }
}