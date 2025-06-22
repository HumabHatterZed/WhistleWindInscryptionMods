using DiskCardGame;
using InscryptionAPI.Helpers;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BonniesBakingPack.BakingPlugin;

namespace BonniesBakingPack {
    public class TalkingBunnieDiskAbility : CustomDiskTalkingCard {
        public override string CardName => "bbp_act3_bunnie";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 1f, voiceSoundPitch: 1.4f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility SpecialAbility;
        public override SpecialTriggeredAbility DialogueAbility => SpecialAbility;

        public override List<EmotionData> Emotions {
            get {
                Sprite face = GetTexture("bunnie_act3.png").ConvertTexture(new(0.5f, 0f));
                Sprite face2 = GetTexture("bunnie_1_act3.png").ConvertTexture(new(0.5f, 0f));
                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: GeneratePortrait.EmptyPortraitTuple,
                        mouth: GeneratePortrait.EmptyPortraitTuple,
                        emission: GeneratePortrait.EmptyPortraitTuple),
                    new(emotion: Emotion.Anger, // shadowed face
                        face: face2,
                        eyes: GeneratePortrait.EmptyPortraitTuple,
                        mouth: GeneratePortrait.EmptyPortraitTuple,
                        emission: GeneratePortrait.EmptyPortraitTuple)
                };
            }
        }

        public override string OnDrawnDialogueId => "BunnieDrawn";
        public override string OnAttackedDialogueId => "BunnieHurt";
        public override string OnSacrificedDialogueId => "BunnieSacrificed";
        public override string OnPlayFromHandDialogueId => "BunniePlayed";
        public override string OnBecomeSelectableNegativeDialogueId => null;
        public override string OnBecomeSelectablePositiveDialogueId => null;
        public override string OnSelectedForCardRemoveDialogueId => null;
        public override string OnSelectedForCardMergeDialogueId => null;
        public override string OnSelectedForDeckTrialDialogueId => null;
        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new()
        {
            { Opponent.Type.PhotographerBoss, "BunniePhotographer" },
            { Opponent.Type.ArchivistBoss, "BunnieArchivist" },
            { Opponent.Type.TelegrapherBoss, "BunnieTelegrapher" },
            { Opponent.Type.CanvasBoss, "BunnieCanvas" },
            { ScrybeCompat.GetP03Boss("P03AscensionFinalBoss", Opponent.Type.LeshyBoss), "BunnieFinalP03" },
            { ScrybeCompat.GetP03Boss("P03MultiverseBoss", Opponent.Type.LeshyBoss), "BunnieMultiverseP03" }
        };
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
}
