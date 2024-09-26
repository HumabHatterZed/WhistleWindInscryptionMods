using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Helpers;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using UnityEngine;
using static BonniesBakingPack.BakingPlugin;

namespace BonniesBakingPack
{
    public class BonnieDiskAbility : CustomDiskTalkingCard
    {
        public override string CardName => "bbp_bonnie_act3";
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
    public partial class BakingPlugin
    {
        private void CreateBonnieDisk()
        {
            CardManager.New(pluginPrefix, "bonnie_act3", "Bonnie", 1, 1)
                .SetBloodCost(1).AddP03().SetRare()
                .SetPixelPortrait(GetTexture("bonnie_pixel.png"))
                .AddAbilities(FreshFood.ability)
                .AddTraits(Trait.KillsSurvivors)
                .SetOnePerDeck();

            TalkingCardManager.New<BonnieDiskAbility>();

            DialogueManager.GenerateEvent(pluginGuid, "BonniePhotographer", new() {
                NewLine("I should get some photos", Emotion.Neutral),
                NewLine("for the bakery.", Emotion.Neutral) },
                new() {
                    new() { NewLine("I hope my picture comes out good.", Emotion.Quiet) },
                    new() { NewLine("Make sure you get my good side!", Emotion.Surprise) },
                    new() { NewLine("I should buy a camera.", Emotion.Neutral) }
                });

            DialogueManager.GenerateEvent(pluginGuid, "BonnieArchivist", new() { NewLine("Not much of a library, huh?", Emotion.Neutral) },
                new() {
                    new() { NewLine("This librarian's pretty scary.", Emotion.Neutral) },
                    new() { NewLine("Where are the books?", Emotion.Quiet) },
                    new() { NewLine("I prefer physical media, personally.", Emotion.Neutral) }
                });

            DialogueManager.GenerateEvent(pluginGuid, "BonnieTelegrapher", new() { NewLine("This one seems nice!", Emotion.Neutral) },
                            new() {
                    new() { NewLine("Hi Golly!", Emotion.Laughter) },
                    new() { NewLine("What's crypto?", Emotion.Quiet) },
                    new() { NewLine("Maybe I should get a computer.", Emotion.Neutral) }
                });

            DialogueManager.GenerateEvent(pluginGuid, "BonnieCanvas", new() { NewLine("Ooh, I love art!", Emotion.Neutral) },
                            new() {
                    new() { NewLine("I hope you made something good.", Emotion.Neutral) },
                    new() { NewLine("Kinda looks like my kitchen.", Emotion.Laughter) },
                    new() { NewLine("Could I try next time?", Emotion.Neutral) }
                });

            DialogueManager.GenerateEvent(pluginGuid, "BonnieFinalP03", new() { NewLine("How do you kill a robot?", Emotion.Quiet) },
                new() {
                    new() { NewLine("Ugh, this meanie.", Emotion.Quiet) },
                    new() { NewLine("So smug...", Emotion.Anger) },
                    new() { NewLine("Beep-boop!", Emotion.Laughter) }
                });

            DialogueManager.GenerateEvent(pluginGuid, "BonnieMultiverseP03", new() { NewLine("What does it mean by 'mod'?", Emotion.Surprise) },
                            new() {
                    new() { NewLine("I wonder if there's another me?", Emotion.Curious) },
                    new() { NewLine("That smug face...", Emotion.Anger) },
                    new() { NewLine("Beep-boop!", Emotion.Laughter) }
                });
        }
    }
}