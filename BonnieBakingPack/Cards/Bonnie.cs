using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.TalkingCards;

namespace BonniesBakingPack {
    public partial class BakingPlugin {
        private void CreateBonnie() {
            CardManager.New(pluginPrefix, "bonnie", "Bonnie", 1, 1)
                .SetBloodCost(1)
                .SetPixelPortrait(GetTexture("bonnie_pixel.png"))
                .AddAbilities(FreshFood.ability)
                .SetExtendedProperty("IsBonnie", true)
                .AddTraits(Trait.KillsSurvivors)
                .SetOnePerDeck();

            TalkingCardManager.New<TalkingBonnieAbility>();

            DialogueManager.GenerateEvent(pluginGuid, "BonnieDrawn", new() {
                NewLine("Hello there!", Emotion.Laughter ),
                NewLine("Not sure what's happening...", Emotion.Quiet ) },
            new() {
                new() { NewLine("Hello again!", Emotion.Neutral) },
                new() { NewLine("Good morning!", Emotion.Laughter) },
                new() { NewLine("", Emotion.Quiet) }
            });

            DialogueManager.GenerateEvent(pluginGuid, "BonniePlayed", new() {
                NewLine("Combat?!", Emotion.Surprise) },
            new() {
                new() { NewLine("I'm not dressed for this...", Emotion.Quiet) },
                new() { NewLine("Can I sit this out?", Emotion.Curious) },
                new() { NewLine("Cute animals!!!", Emotion.Laughter) }
            });

            DialogueManager.GenerateEvent(pluginGuid, "BonnieHurt", new() {
                NewLine("Ow!", Emotion.Curious) },
                new() {
                    new() { NewLine("Ah!", Emotion.Curious) },
                    new() { NewLine("Ouch!", Emotion.Curious) },
                    new() { NewLine("Ow!", Emotion.Curious) }
                });

            DialogueManager.GenerateEvent(pluginGuid, "BonnieSacrificed", new() {
                NewLine("...", Emotion.Anger) },
                new() {
                    new() { NewLine("...", Emotion.Anger) }
                });

            DialogueManager.GenerateEvent(pluginGuid, "BonnieSelectableGood", new() {
                NewLine("What's this?", Emotion.Neutral) },
                new() {
                    new() { NewLine("You can pick someone else, I won't mind!", Emotion.Neutral) },
                    new() { NewLine("So mysterious...", Emotion.Quiet) },
                    new() { NewLine("I hope something good happens!", Emotion.Neutral) },
                    new() { NewLine("Won't be as good as my food, hehe!", Emotion.Laughter) }
                });

            DialogueManager.GenerateEvent(pluginGuid, "BonnieSelectableGood", new() {
                NewLine("...What's this?", Emotion.Quiet) },
                new() {
                    new() { NewLine("You can pick someone else.", Emotion.Quiet),
                            NewLine("I won't mind.", Emotion.Neutral) },
                    new() { NewLine("...", Emotion.Anger) },
                    new() { NewLine("You won't do anything bad, right?", Emotion.Neutral) },
                    new() { NewLine("Why this way?", Emotion.Quiet) }
                });

            //DialogueManager.GenerateEvent(pluginGuid, "BonnieGivenSigil", new(), new() { new() });
            DialogueManager.GenerateEvent(pluginGuid, "BonnieTrial", new() {
                NewLine("A trial, huh...", Emotion.Quiet),
                NewLine("I don't think I'll be much help.", Emotion.Neutral) },
                new() {
                    new() { NewLine("At least it's not a court trial, haha!", Emotion.Laughter) },
                    new() { NewLine("I don't think I'll be much help.", Emotion.Neutral) },
                    new() { NewLine("Maybe I can do it?", Emotion.Curious) }
                });
            DialogueManager.GenerateEvent(pluginGuid, "BonnieProspector", new() {
                NewLine("What a funny old dog!", Emotion.Laughter) },
                new() {
                    new() { NewLine("Dogs owning dogs?", Emotion.Neutral),
                            NewLine("What a world!", Emotion.Laughter) },
                    new() { NewLine("What a funny old dog!", Emotion.Laughter) },
                    new() { NewLine("Careful, his pickaxe is sharp!", Emotion.Neutral) },
                    new() { NewLine("Git 'em!", Emotion.Laughter) }
                });
            DialogueManager.GenerateEvent(pluginGuid, "BonnieAngler", new() {
                NewLine("Ew, it smells!", Emotion.Curious),
                NewLine("Clean your workstation!", Emotion.Curious) },
                new() {
                    new() { NewLine("I've never made fish pastries before...", Emotion.Neutral),
                            NewLine("...or any other kind of meat pastry.", Emotion.Anger) },
                    new() { NewLine("That's the biggest hook I've seen!", Emotion.Surprise) },
                    new() { NewLine("Can I debone him when we're done?", Emotion.Neutral) }
                });
            DialogueManager.GenerateEvent(pluginGuid, "BonnieTrapperTrader", new() {
                NewLine("I can't imagine skinning animals.", Emotion.Surprise),
                NewLine("What an awful bear!", Emotion.Surprise) },
                new() {
                    new() { NewLine("Just another butcher...", Emotion.Anger) },
                    new() { NewLine("...am I like this?", Emotion.Anger)},
                    new() { NewLine("Maybe he's just protecting his dream.", Emotion.Quiet) }
                });

            DialogueManager.GenerateEvent(pluginGuid, "BonnieLeshy", new() {
                NewLine("So he's the one controlling all this?", Emotion.Quiet) },
                new() {
                    new() { NewLine("Let me help!", Emotion.Neutral) },
                    new() { NewLine("So he's, like, a tree?", Emotion.Quiet) },
                    new() {NewLine("Let's kick the moon's butt!", Emotion.Laughter) }
            });

            DialogueManager.GenerateEvent(pluginGuid, "BonnieRoyal", new() { NewLine("A...pirate?", Emotion.Quiet) },
                new() {
                    new() { NewLine("What big cannons...", Emotion.Quiet) },
                    new() { NewLine("No meat, huh?", Emotion.Anger),
                            NewLine("Uh, I mean...!", Emotion.Surprise) },
                    new() { NewLine("Yo-ho-ho!", Emotion.Laughter) }
                });

            //DialogueManager.GenerateEvent(pluginGuid, "Bonnie", new(), new() { new() });

            /*DialogueManager.GenerateEvent(pluginGuid, "BonnieStatBoost", new() {
                "Shortly after leaving the camp, you hear a familiar voice behind you.",
                "Bonnie rejoins your caravan, only slightly singed.",
                "You question how she survived, but the scent of iron suggests an answer." },
                new() {
                    new() { "Bonnie rejoins you, no worse for wear." },
                    new() { "Bonnie returns from the camp with a grumpy expression." },
                    new() { "The smell of ash and iron reveals Bonnie's survival and return." }
                });*/
        }
    }
}