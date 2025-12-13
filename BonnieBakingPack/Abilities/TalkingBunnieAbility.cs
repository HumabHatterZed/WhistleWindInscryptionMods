using DiskCardGame;
using InscryptionAPI.Helpers;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using UnityEngine;
using static BonniesBakingPack.BakingPlugin;

namespace BonniesBakingPack {
    public class TalkingBunnieAbility : CustomPaperTalkingCard {
        public override string CardName => "bbp_act1_bunnie";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 1f, voiceSoundPitch: 1.4f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility SpecialAbility;
        public override SpecialTriggeredAbility DialogueAbility => SpecialAbility;

        public override List<EmotionData> Emotions {
            get {
                Sprite face = GetTexture("bunnie.png").ConvertTexture(new(0.5f, 0f));
                Sprite face2 = GetTexture("bunnie_1.png").ConvertTexture(new(0.5f, 0f));
                FaceAnim emission = MakeFaceAnim("bunnie_emission.png");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: GeneratePortrait.EmptyPortraitTuple,
                        mouth: GeneratePortrait.EmptyPortraitTuple,
                        emission: emission),
                    new(emotion: Emotion.Anger, // shadowed face
                        face: face2,
                        eyes: GeneratePortrait.EmptyPortraitTuple,
                        mouth: GeneratePortrait.EmptyPortraitTuple,
                        emission: emission)
                };
            }
        }

        public override string OnDrawnDialogueId => "BunnieDrawn";
        public override string OnAttackedDialogueId => "BonnieHurt";
        public override string OnSacrificedDialogueId => "BonnieSacrificed";
        public override string OnPlayFromHandDialogueId => "BunniePlayed";
        public override string OnBecomeSelectableNegativeDialogueId => null;
        public override string OnBecomeSelectablePositiveDialogueId => null;
        public override string OnSelectedForCardRemoveDialogueId => null;
        public override string OnSelectedForCardMergeDialogueId => null;
        public override string OnSelectedForDeckTrialDialogueId => null;
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "BunnieProspector" },
            { Opponent.Type.AnglerBoss, "BunnieAngler" },
            { Opponent.Type.TrapperTraderBoss, "BunnieTrapperTrader" },
            { Opponent.Type.LeshyBoss, "BunnieLeshy" },
            { Opponent.Type.RoyalBoss, "BunnieRoyal" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
}
