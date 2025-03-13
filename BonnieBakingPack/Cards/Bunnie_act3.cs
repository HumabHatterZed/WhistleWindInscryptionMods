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
    public partial class BakingPlugin
    {
        private void CreateBunnieDisk()
        {
            CardManager.New(pluginPrefix3, "bunnie", "Bunnie", 2, 2, "")
                .SetBloodCost(1).SetRare().AddP03()
                .AddAbilities(FreshIngredients.ability)
                .AddTraits(Trait.KillsSurvivors)
                .AddSpecialAbilities(BunnieAttackAbility.SpecialAbility)
                .SetOnePerDeck()
                .RemoveCardMetaCategories(CardMetaCategory.Rare);

            TalkingCardManager.NewDisk<TalkingBunnieDiskAbility>();

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