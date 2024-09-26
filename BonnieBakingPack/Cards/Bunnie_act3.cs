using BonniesBakingPack;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Helpers;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BonniesBakingPack.BakingPlugin;

namespace BonniesBakingPack
{
    public class BunnieDiskAbility : CustomDiskTalkingCard
    {
        public override bool RespondsToDealDamage(int amount, PlayableCard target)
        {
            return true;
        }
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            if (SaveManager.SaveFile.IsPart2)
            {
                AudioController.Instance.PlaySound2D("bonnie_bonk", MixerGroup.None, 0.35f);
            }
            else
            {
                AudioController.Instance.PlaySound3D("bonnie_bonk", MixerGroup.None, base.Card.transform.position);
            }
            
            return base.OnDealDamage(amount, target);
        }
        public override string CardName => "bbp_bunnie_act3";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 1f, voiceSoundPitch: 1.4f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility SpecialAbility;
        public override SpecialTriggeredAbility DialogueAbility => SpecialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
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
    public partial class BakingPlugin
    {
        private void CreateBunnieDisk()
        {
            CardManager.New(pluginPrefix, "bunnie_act3", "Bunnie", 2, 2, "")
                .SetBloodCost(1).AddP03().SetRare()
                .AddAbilities(FreshIngredients.ability)
                .AddTraits(Trait.KillsSurvivors)
                .SetOnePerDeck()
                .RemoveCardMetaCategories(CardMetaCategory.Rare);

            TalkingCardManager.New<BunnieDiskAbility>();

            DialogueManager.GenerateEvent(pluginGuid, "BunniePhotographer", new() {
                NewLine("No photographs.", Emotion.Neutral) });

            DialogueManager.GenerateEvent(pluginGuid, "BunnieArchivist", new() { NewLine("Need to dispose of evidence...", Emotion.Neutral) },
                new() {
                    new() { NewLine("No records.", Emotion.Neutral) },
                    new() { NewLine("Where are the books?", Emotion.Quiet) }
                });

            DialogueManager.GenerateEvent(pluginGuid, "BunnieTelegrapher", new() { NewLine("Just another bot.", Emotion.Neutral) },
                            new() {
                    new() { NewLine("I kinda feel bad...", Emotion.Neutral) },
                    new() { NewLine("How much is crypto worth?", Emotion.Neutral) } 
                });

            DialogueManager.GenerateEvent(pluginGuid, "BunnieCanvas", new() { NewLine("How passé.", Emotion.Neutral) },
                            new() {
                    new() { NewLine("I hope you made something good.", Emotion.Neutral) },
                    new() { NewLine("Looks like a child's painting.", Emotion.Neutral) }
                });

            DialogueManager.GenerateEvent(pluginGuid, "BunnieFinalP03", new() { NewLine("One more left.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Off with its head.", Emotion.Neutral) },
                    new() { NewLine("I'm gonna wipe that smirk from its face.", Emotion.Anger) },
                    new() { NewLine("Beep-boop.", Emotion.Neutral) }
                });

            DialogueManager.GenerateEvent(pluginGuid, "BunnieMultiverseP03", new() { NewLine("", Emotion.Neutral) },
                            new() {
                    new() { NewLine("Off with its head.", Emotion.Neutral) },
                    new() { NewLine("I'm gonna wipe that smirk from its face.", Emotion.Anger) },
                    new() { NewLine("Beep-boop.", Emotion.Neutral) }
                });
        }
    }
}