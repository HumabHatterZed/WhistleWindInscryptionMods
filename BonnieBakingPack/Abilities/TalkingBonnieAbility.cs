using DiskCardGame;
using InscryptionAPI.Helpers;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using UnityEngine;
using static BonniesBakingPack.BakingPlugin;

namespace BonniesBakingPack {
    public class TalkingBonnieAbility : CustomPaperTalkingCard {
        public override string CardName => "bbp_act1_bonnie";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 1.6f, voiceSoundPitch: 1.6f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility SpecialAbility;
        public override SpecialTriggeredAbility DialogueAbility => SpecialAbility;

        public override List<EmotionData> Emotions {
            get {
                Sprite face = GetTexture("bonnie.png").ConvertTexture(new(0.5f, 0f));
                FaceAnim emission = MakeFaceAnim("bonnie_emission.png");
                FaceAnim emission2 = MakeFaceAnim("bonnie_emission_1.png");

                return new()
                {
                    new(emotion: Emotion.Neutral, // smile | DONE
                        face: face,
                        eyes: MakeFaceAnim("bonnie_eyes1_open.png", "bonnie_eyes1_closed.png"),
                        mouth: MakeFaceAnim("bonnie_mouth1_open.png", "bonnie_mouth1_closed.png"),
                        emission: emission),
                    new(emotion: Emotion.Quiet, // no smile | DONE
                        face: face,
                        eyes: MakeFaceAnim("bonnie_eyes1_open.png", "bonnie_eyes1_closed.png"),
                        mouth: MakeFaceAnim("bonnie_mouth2_open.png", "bonnie_mouth2_closed.png"),
                        emission: emission),
                    new(emotion: Emotion.Surprise, // surprise | DONE
                        face: face,
                        eyes: MakeFaceAnim("bonnie_eyes1_open.png", "bonnie_eyes1_closed.png"),
                        mouth: MakeFaceAnim("bonnie_mouth3_open.png", "bonnie_mouth3_closed.png"),
                        emission: emission),
                    new(emotion: Emotion.Laughter, // smile, eyes closed | DONE
                        face: face,
                        eyes: MakeFaceAnim("bonnie_eyes2_closed.png"),
                        mouth: MakeFaceAnim("bonnie_mouth1_open.png", "bonnie_mouth1_closed.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Anger, // shadowed face | DONE
                        face: face,
                        eyes: MakeFaceAnim("bonnie_eyes3_open.png"),
                        mouth: MakeFaceAnim("bonnie_mouth2_open.png", "bonnie_mouth2_closed.png"),
                        emission: emission),
                    new(emotion: Emotion.Curious, // injured | DONE
                        face: face,
                        eyes: MakeFaceAnim("bonnie_eyes4_open.png", "bonnie_eyes4_closed.png"),
                        mouth: MakeFaceAnim("bonnie_mouth4_open.png", "bonnie_mouth4_closed.png"),
                        emission: emission2)
                };
            }
        }

        public override string OnDrawnDialogueId => "BonnieDrawn";
        public override string OnAttackedDialogueId => "BonnieHurt";
        public override string OnSacrificedDialogueId => "BonnieSacrificed";
        public override string OnPlayFromHandDialogueId => "BonniePlayed";
        public override string OnBecomeSelectableNegativeDialogueId => "BonnieSelectableBad";
        public override string OnBecomeSelectablePositiveDialogueId => "BonnieSelectableGood";
        public override string OnSelectedForCardRemoveDialogueId => "BonnieSelectableBad";
        public override string OnSelectedForCardMergeDialogueId => "BonnieGivenSigil";
        public override string OnSelectedForDeckTrialDialogueId => "BonnieTrial";
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.ProspectorBoss, "BonnieProspector" },
            { Opponent.Type.AnglerBoss, "BonnieAngler" },
            { Opponent.Type.TrapperTraderBoss, "BonnieTrapperTrader" },
            { Opponent.Type.LeshyBoss, "BonnieLeshy" },
            { Opponent.Type.RoyalBoss, "BonnieRoyal" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
}
