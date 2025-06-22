using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.TalkingCards;

namespace BonniesBakingPack {
    public partial class BakingPlugin {
        private void CreateBonnieDisk() {
            CardManager.New(pluginPrefix3, "bonnie", "Bonnie", 1, 1)
                .SetBloodCost(1).SetRare().AddP03()
                .SetPixelPortrait(GetTexture("bonnie_pixel.png"))
                .AddAbilities(FreshFood.ability)
                .SetExtendedProperty("IsBonnie", true)
                .AddTraits(Trait.KillsSurvivors)
                .SetOnePerDeck();

            TalkingCardManager.NewDisk<TalkingBonnieDiskAbility>();

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