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
    public class BunnieAbility : CustomPaperTalkingCard
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
        public override string CardName => "bbp_bunnie";
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 1f, voiceSoundPitch: 1.4f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility SpecialAbility;
        public override SpecialTriggeredAbility DialogueAbility => SpecialAbility;

        public override List<EmotionData> Emotions
        {
            get
            {
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
    public partial class BakingPlugin
    {
        private void CreateBunnie()
        {
            CardManager.New(pluginPrefix, "bunnie", "Bunnie", 2, 2, "")
                .SetBloodCost(1)
                .AddAbilities(FreshIngredients.ability)
                .AddTraits(Trait.KillsSurvivors)
                .SetOnePerDeck();

            TalkingCardManager.New<BunnieAbility>();

            DialogueManager.GenerateEvent(pluginGuid, "BunnieDrawn", new() {
                NewLine("Hello ag- for the time!", Emotion.Neutral ) },
            new() {
                new() { NewLine("Is it time to restock?", Emotion.Neutral) },
                new() { NewLine("Good morning!", Emotion.Neutral) },
                new() { NewLine("Hello again!", Emotion.Neutral) }
            });

            DialogueManager.GenerateEvent(pluginGuid, "BunniePlayed", new() {
                NewLine("Fresh ingredients.", Emotion.Neutral) },
            new() {
                new() { NewLine("Good thing I made this mask!", Emotion.Neutral) },
                new() { NewLine("Fresh ingredients.", Emotion.Neutral) },
                new() { NewLine("Everyone here's so mean.", Emotion.Neutral) }
            });

            DialogueManager.GenerateEvent(pluginGuid, "BunnieProspector", new() {
                NewLine("Crazy old old.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Crazy old dog.", Emotion.Neutral) },
                    new() { NewLine("What a funny old dog!", Emotion.Neutral) },
                    new() { NewLine("Watch out for that pickaxe.", Emotion.Neutral) },
                    new() { NewLine("Lemme git 'em.", Emotion.Neutral) }
                });
            DialogueManager.GenerateEvent(pluginGuid, "BunnieAngler", new() {
                NewLine("Ew, it smells!", Emotion.Neutral) },
                new() {
                    new() { NewLine("I've never made fish pastries before...", Emotion.Neutral) },
                    new() { NewLine("What a big hook.", Emotion.Neutral) },
                    new() { NewLine("Can I debone him when we're done?", Emotion.Neutral) }
                });
            DialogueManager.GenerateEvent(pluginGuid, "BunnieTrapperTrader", new() {
                NewLine("I could learn a thing or two from him.", Emotion.Neutral) },
                new() {
                    new() { NewLine("What messy cuts.", Emotion.Anger) },
                    new() { NewLine("We do what we must.", Emotion.Neutral),
                            NewLine("To protect our dream.", Emotion.Neutral) },
                    new() { NewLine("So two-faced!", Emotion.Neutral) }
                });

            DialogueManager.GenerateEvent(pluginGuid, "BunnieLeshy", new() {
                NewLine("So he's the one controlling all this?", Emotion.Neutral) },
                new() {
                    new() { NewLine("I wonder if those are tea leaves.", Emotion.Neutral) },
                    new() { NewLine("No strings on me!", Emotion.Neutral) },
                    new() { NewLine("Is the moon cuttable?", Emotion.Neutral),
                            NewLine("...well it is made of cheese.", Emotion.Neutral) }
            });

            DialogueManager.GenerateEvent(pluginGuid, "BunnieRoyal", new() { NewLine("Walk the plank.", Emotion.Neutral) },
                new() {
                    new() { NewLine("What big cannons...", Emotion.Neutral) },
                    new() { NewLine("No meat.", Emotion.Neutral),
                            NewLine("Plenty of bones, though.", Emotion.Neutral) },
                    new() { NewLine("Yo-ho-ho!", Emotion.Neutral) }
                });

            //DialogueManager.GenerateEvent(pluginGuid, "Bunnie", new(), new() { new() });
        }
    }
}