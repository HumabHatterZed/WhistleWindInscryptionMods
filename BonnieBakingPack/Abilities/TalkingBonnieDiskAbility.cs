using DiskCardGame;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BonniesBakingPack.BakingPlugin;
using InscryptionAPI.Helpers;

namespace BonniesBakingPack
{
    public class TalkingBonnieDiskAbility : CustomDiskTalkingCard
    {
        public override string CardName => "bbp_act3_bonnie";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 1.6f, voiceSoundPitch: 1.6f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility SpecialAbility;
        public override SpecialTriggeredAbility DialogueAbility => SpecialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
                Sprite face = GetTexture("bonnie_act3.png").ConvertTexture(new(0.5f, 0f));
                return new()
                {
                    new(emotion: Emotion.Neutral, // smile | DONE
                        face: face,
                        eyes: MakeFaceAnim("bonnie_eyes1_open_act3.png", "bonnie_eyes1_closed_act3.png"),
                        mouth: MakeFaceAnim("bonnie_mouth1_open_act3.png", "bonnie_mouth1_closed_act3.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Quiet, // no smile | DONE
                        face: face,
                        eyes: MakeFaceAnim("bonnie_eyes1_open_act3.png", "bonnie_eyes1_closed_act3.png"),
                        mouth: MakeFaceAnim("bonnie_mouth2_open_act3.png", "bonnie_mouth2_closed_act3.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Surprise, // surprise | DONE
                        face: face,
                        eyes: MakeFaceAnim("bonnie_eyes1_open_act3.png", "bonnie_eyes1_closed_act3.png"),
                        mouth: MakeFaceAnim("bonnie_mouth3_open_act3.png", "bonnie_mouth3_closed_act3.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Laughter, // smile, eyes closed | DONE
                        face: face,
                        eyes: MakeFaceAnim("bonnie_eyes2_closed_act3.png"),
                        mouth: MakeFaceAnim("bonnie_mouth1_open_act3.png", "bonnie_mouth1_closed_act3.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Anger, // shadowed face | DONE
                        face: face,
                        eyes: MakeFaceAnim("bonnie_eyes3_open_act3.png"),
                        mouth: MakeFaceAnim("bonnie_mouth2_open_act3.png", "bonnie_mouth2_closed_act3.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Curious, // injured | DONE
                        face: face,
                        eyes: MakeFaceAnim("bonnie_eyes4_open_act3.png", "bonnie_eyes4_closed_act3.png"),
                        mouth: MakeFaceAnim("bonnie_mouth4_open_act3.png", "bonnie_mouth4_closed_act3.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple)
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
            { Opponent.Type.PhotographerBoss, "BonniePhotographer" },
            { Opponent.Type.ArchivistBoss, "BonnieArchivist" },
            { Opponent.Type.TelegrapherBoss, "BonnieTelegrapher" },
            { Opponent.Type.CanvasBoss, "BonnieCanvas" },
            { ScrybeCompat.GetP03Boss("P03AscensionFinalBoss", Opponent.Type.LeshyBoss), "BonnieFinalP03" },
            { ScrybeCompat.GetP03Boss("P03MultiverseBoss", Opponent.Type.LeshyBoss), "BonnieMultiverseP03" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
}
