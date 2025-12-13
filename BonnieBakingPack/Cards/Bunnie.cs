using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.TalkingCards;

namespace BonniesBakingPack {
    public partial class BakingPlugin {
        private void CreateBunnie() {
            CardManager.New(pluginPrefix, "bunnie", "Bunnie", 2, 2, "The hunt begins.")
                .SetBloodCost(1)
                .AddAbilities(FreshIngredients.ability)
                .AddTraits(Trait.KillsSurvivors)
                .AddSpecialAbilities(BunnieAttackAbility.SpecialAbility)
                .SetOnePerDeck();

            TalkingCardManager.New<TalkingBunnieAbility>();

            DialogueManager.GenerateEvent(pluginGuid, "BunnieDrawn", new() {
                NewLine("Hello ag- for the first time!", Emotion.Neutral ) },
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