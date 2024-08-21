using DiskCardGame;
using static WhistleWind.Core.Helpers.DialogueHelper;
using static WhistleWindLobotomyMod.Core.DialogueEventsManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyDialogue
    {
        private void Dialogue_Angela()
        {
            #region Boss Dialogue
            CreateDialogueEvents("AngelaProspector", new()
            {
                NewLine("A decrepit old man.", Emotion.Neutral) },
                new() {
                    new() {
                        NewLine("I believe that pack animal", Emotion.Neutral),
                        NewLine("will aid us with its death.", Emotion.Neutral) },
                    new() { NewLine("Do you wonder if he ever bathes?", Emotion.Neutral) },
                    new() { NewLine("You should know what to do by now.", Emotion.Neutral) }
            });
            CreateDialogueEvents("AngelaAngler", new()
            {
                NewLine("He appears to have a simple mind.", Emotion.Neutral),
                NewLine("We can easily manipulate him.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Fish are a lot like people, aren't they?", Emotion.Neutral) },
                    new() { NewLine("I recommend holding your breath.", Emotion.Neutral) }
            });
            CreateDialogueEvents("AngelaTrapperTrader", new()
            {
                NewLine("Everyone has a different side to them.", Emotion.Neutral),
                NewLine("Don't you think, manager?", Emotion.Laughter) },
                new() {
                    new() { NewLine("Don't die manager.", Emotion.Laughter) },
                    new() { NewLine("Watch out for traps.", Emotion.Neutral) },
                    new() { NewLine("Does the cold bother you?", Emotion.Neutral) }
            });
            CreateDialogueEvents("AngelaLeshy", new()
            {
                NewLine("We appear to have reached the end.", Emotion.Neutral) },
                new() {
                    new() {
                        NewLine("I expect you to conquer this challenge", Emotion.Neutral),
                        NewLine("quite easily, manager.", Emotion.Neutral) },
                    new() { NewLine("A final trial for you, manager.", Emotion.Neutral) },
                    new() { NewLine("It has been quite the journey, yes?", Emotion.Neutral) }
            });
            CreateDialogueEvents("AngelaRoyal", new()
            {
                NewLine("...do not expect a comment from me.", Emotion.Neutral) },
                new() {
                    new() {
                        NewLine("Do not be deceived.", Emotion.Neutral),
                        NewLine("He is not an abnormality.", Emotion.Neutral) },
                    new() { NewLine("An unloyal crew...", Emotion.Laughter) },
                    new() {
                        NewLine("He reminds me of you", Emotion.Neutral),
                        NewLine("in a way.", Emotion.Laughter) }
            });
            CreateDialogueEvents("AngelaApocalypse", new()
            {
                NewLine("This threat may be beyond you.", Emotion.Neutral) },
                new() {
                    new() {
                        NewLine("We kept them separated for", Emotion.Neutral),
                        NewLine("this reason, manager.", Emotion.Neutral) },
                    new() { NewLine("A final trial for you, manager.", Emotion.Neutral) },
                    new() { NewLine("It has been quite the journey, yes?", Emotion.Neutral) }
            });
            CreateDialogueEvents("AngelaOrdeal", new()
            {
                NewLine("Ordeals are a common occurrence", Emotion.Neutral),
                NewLine("at the facility.", Emotion.Neutral),
                NewLine("Nothing you can't handle, right?", Emotion.Laughter)},
                new() {
                    new() { NewLine("Do take care of this, yes?", Emotion.Neutral) },
                    new() { NewLine("They are the chaos factor.", Emotion.Neutral) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("AngelaChoice", new()
            {
                NewLine("Hello manager.", Emotion.Neutral),
                NewLine("Shall we get going?", Emotion.Surprise) },
                new() {
                    new() { NewLine("Hello manager.", Emotion.Neutral) },
                    new() { NewLine("I am here to assist you.", Emotion.Neutral) },
                    new() { NewLine("Shall we get going?", Emotion.Surprise) }
            });
            CreateDialogueEvents("AngelaDrawn", new()
            {
                NewLine("Hello manager.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Hello manager.", Emotion.Neutral) },
                    new() { NewLine("Gotten yourself in trouble?", Emotion.Neutral) },
                    new() { NewLine("I am here to assist you.", Emotion.Neutral) },
                    new() { NewLine("You can rely on me.", Emotion.Neutral) }
            });
            CreateDialogueEvents("AngelaGivenSigil", new()
            {
                NewLine("Thank you.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Thank you.", Emotion.Neutral) },
                    new() { NewLine("Thank you.", Emotion.Laughter) },
                    new() { NewLine("Thank you.", Emotion.Surprise) }
            });
            CreateDialogueEvents("AngelaHurt", new()
            {
                NewLine("", Emotion.Anger) },
                new() {
                    new() { NewLine("", Emotion.Anger) }
            });
            CreateDialogueEvents("AngelaPlayed", new()
            {
                NewLine("Combat?", Emotion.Neutral),
                NewLine("I see.", Emotion.Curious) },
                new() {
                    new() { NewLine("Try not to make a mess.", Emotion.Neutral) },
                    new() { NewLine("Let's hurry this up, yes?", Emotion.Surprise) },
                    new() { NewLine("Nothing I can't overcome.", Emotion.Laughter) }
            });
            CreateDialogueEvents("AngelaSacrificed", new()
            {
                NewLine("I see.", Emotion.Anger) },
                new() {
                    new() { NewLine("I see.", Emotion.Anger) },
                    new() { NewLine("", Emotion.Anger) }
            });
            CreateDialogueEvents("AngelaSelectableBad", new()
            {
                NewLine("Reminds me of the facility.", Emotion.Neutral) },
                new() {
                    new() { NewLine("No point in hesitating.", Emotion.Neutral) },
                    new() { NewLine("Sacrifices must be made.", Emotion.Neutral) },
                    new() { NewLine("Nothing you should be uncomfortable with.", Emotion.Neutral) }
            });
            CreateDialogueEvents("AngelaSelectableGood", new()
            {
                NewLine("A golden opportunity.", Emotion.Neutral) },
                new() {
                    new() { NewLine("A golden opportunity.", Emotion.Neutral) },
                    new() { NewLine("I expect you to know the best choice.", Emotion.Curious) },
                    new() { NewLine("I leave it to your discretion.", Emotion.Neutral) }
            });
            CreateDialogueEvents("AngelaTrial", new()
            {
                NewLine("We must all overcome ordeals in life.", Emotion.Neutral),
                NewLine("You taught me that.", Emotion.Surprise) },
                new() {
                    new() { NewLine("You wouldn't fail so easily, right?", Emotion.Laughter) },
                    new() { NewLine("No point in dilly-dallying now.", Emotion.Neutral) },
                    new() { NewLine("Yet another obstacle in my way...", Emotion.Anger) }
            });
            #endregion
        }
        private void Dialogue_Binah()
        {
            #region Boss Dialogue
            CreateDialogueEvents("BinahProspector", new()
            {
                NewLine("Deal with those beasts quickly.", Emotion.Neutral) },
                new() {
                    new() { NewLine("A small obstacle.", Emotion.Neutral) },
                    new() { NewLine("Let us begin.", Emotion.Neutral) }
            });
            CreateDialogueEvents("BinahAngler", new()
            {
                NewLine("Don't lose your appetite now.", Emotion.Surprise) },
                new() {
                    new() { NewLine("I grow tired of fish.", Emotion.Neutral) },
                    new() { NewLine("You must enjoy the stench.", Emotion.Laughter) }
            });
            CreateDialogueEvents("BinahTrapperTrader", new()
            {
                NewLine("Another two-faced deceiver.", Emotion.Laughter) },
                new() {
                    new() { NewLine("No one helps you without a price.", Emotion.Neutral) },
                    new() { NewLine("Be wary of where your foot falls.", Emotion.Neutral) }
            });
            CreateDialogueEvents("BinahLeshy", new()
            {
                NewLine("It seems we have reached the conclusion.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Are we doing this again?", Emotion.Neutral) },
                    new() { NewLine("I have fought stronger foes.", Emotion.Neutral) }
            });
            CreateDialogueEvents("BinahRoyal", new()
            {
                NewLine("What is this?", Emotion.Neutral) },
                new() {
                    new() { NewLine("A captain of a sunken vessel.", Emotion.Laughter) },
                    new() { NewLine("His crew has no loyalty.", Emotion.Laughter) }
            });
            CreateDialogueEvents("BinahApocalypse", new()
            {
                NewLine("You have wandered into a dangerous place.", Emotion.Laughter),
                NewLine("Do you think yourself immortal?", Emotion.Laughter) },
                new() {
                    new() { NewLine("Quite the marvelous beast, isn't it?", Emotion.Laughter) },
                    new() {
                        NewLine("You have entered its lair once more.", Emotion.Neutral),
                        NewLine("You must have a death wish.", Emotion.Laughter) },
                    new() { NewLine("I do enjoy this place.", Emotion.Surprise) },
            });
            CreateDialogueEvents("BinahOrdeal", new()
            {
                NewLine("How amusing.", Emotion.Neutral),
                NewLine("It seems the veil here is thinner", Emotion.Laughter),
                NewLine("than I thought.", Emotion.Laughter),
                NewLine("I won't elucidate on their presence.", Emotion.Neutral),
                NewLine("Just take care of them.", Emotion.Neutral)},
                new() {
                    new() { NewLine("Every action has consequences.", Emotion.Laughter) },
                    new() { NewLine("They appear wherever they please, huh?", Emotion.Neutral) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("BinahChoice", new()
            {
                NewLine("Well look who it is.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Well look who it is.", Emotion.Neutral) },
                    new() { NewLine("Choose.", Emotion.Neutral) }
            });
            CreateDialogueEvents("BinahDrawn", new()
            {
                NewLine("Be calm.", Emotion.Laughter) },
                new() {
                    new() { NewLine("Miss me?", Emotion.Laughter) },
                    new() { NewLine("Countless stars light the sky today.", Emotion.Neutral) },
                    new() { NewLine("Your outlook is what decides things.", Emotion.Neutral) },
                    new() { NewLine("Be calm.", Emotion.Neutral) }
            });
            CreateDialogueEvents("BinahGivenSigil", new()
            {
                NewLine("I am curious to see", Emotion.Neutral),
                NewLine("what memories you yielded.", Emotion.Surprise)},
                new() {
                    new() { NewLine("Excellent.", Emotion.Surprise) },
                    new() { NewLine("You have done well.", Emotion.Laughter) },
                    new() { NewLine("This power is incomplete.", Emotion.Neutral) }
            });
            CreateDialogueEvents("BinahHurt", new()
            {
                NewLine("...", Emotion.Anger) },
                new() {
                    new() { NewLine("...", Emotion.Anger) },
                    new() { NewLine("", Emotion.Anger) }
            });
            CreateDialogueEvents("BinahPlayed", new()
            {
                NewLine("Let us begin.", Emotion.Neutral) },
                new() {
                    new() { NewLine("More bodies to clean.", Emotion.Neutral) },
                    new() { NewLine("Let us begin.", Emotion.Neutral) },
                    new() { NewLine("The closer to death, the freer you are.", Emotion.Laughter) }
            });
            CreateDialogueEvents("BinahSacrificed", new()
            {
                NewLine("...", Emotion.Anger) },
                new() {
                    new() { NewLine("...", Emotion.Anger) },
                    new() { NewLine("", Emotion.Anger) }
            });
            CreateDialogueEvents("BinahSelectableBad", new()
            {
                NewLine("Did you think you could avoid it?", Emotion.Neutral) },
                new() {
                    new() { NewLine("Did you think you could avoid it?", Emotion.Neutral) },
                    new() { NewLine("This is nearing the end.", Emotion.Neutral) },
                    new() { NewLine("Your breath is fading.", Emotion.Surprise) },
                    new() { NewLine("No one will blame you.", Emotion.Laughter) }
            });
            CreateDialogueEvents("BinahSelectableGood", new()
            {
                NewLine("Excellent.", Emotion.Laughter) },
                new() {
                    new() { NewLine("Excellent.", Emotion.Laughter) },
                    new() { NewLine("Inching closer to wholeness.", Emotion.Neutral) },
                    new() { NewLine("I leave it to your discretion.", Emotion.Neutral) }
            });
            CreateDialogueEvents("BinahTrial", new()
            {
                NewLine("Tis an ordeal to overcome.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Are you up to the task?", Emotion.Laughter) },
                    new() { NewLine("Tis an ordeal to overcome.", Emotion.Neutral) },
                    new() { NewLine("There is no obstacle you can't overcome.", Emotion.Neutral) }
            });
            #endregion
        }
        private void Dialogue_Chesed()
        {
            #region Boss Dialogue
            CreateDialogueEvents("ChesedProspector", new()
            {
                NewLine("So many animals~", Emotion.Laughter) },
                new() {
                    new() { NewLine("So much dust.", Emotion.Neutral) },
                    new() { NewLine("Do we have to fight?", Emotion.Neutral) }
            });
            CreateDialogueEvents("ChesedAngler", new()
            {
                NewLine("That's an impressive hook there~.", Emotion.Laughter) },
                new() {
                    new() { NewLine("Are you feeling hungry?", Emotion.Laughter) },
                    new() { NewLine("I'm used to the stench of decay by now.", Emotion.Laughter) }
            });
            CreateDialogueEvents("ChesedTrapperTrader", new()
            {
                NewLine("What an odd fellow.", Emotion.Neutral) },
                new() {
                    new() { NewLine("What tricks are up their sleeve?", Emotion.Neutral) },
                    new() {
                        NewLine("Nothing like a cup of coffee", Emotion.Laughter),
                        NewLine("to keep you warm~", Emotion.Laughter)}
            });
            CreateDialogueEvents("ChesedLeshy", new()
            {
                NewLine("We've come so far...", Emotion.Neutral),
                NewLine("just a little more now.", Emotion.Laughter) },
                new() {
                    new() { NewLine("Are you prepared manager?", Emotion.Laughter) },
                    new() { NewLine("We can't hesitate now.", Emotion.Neutral) }
            });
            CreateDialogueEvents("ChesedRoyal", new()
            {
                NewLine("A pirate? How fun~", Emotion.Laughter) },
                new() {
                    new() { NewLine("Watch your head now.", Emotion.Laughter) }
            });
            CreateDialogueEvents("ChesedApocalypse", new()
            {
                NewLine("This one...", Emotion.Neutral),
                NewLine("I hope you're prepared.", Emotion.Neutral) },
                new() {
                    new() { NewLine("I worry you have a death wish.", Emotion.Neutral) },
                    new() { NewLine("Are we really doing this?", Emotion.Neutral) }
            });
            CreateDialogueEvents("ChesedOrdeal", new()
            {
                NewLine("Well, isn't this a surprise~", Emotion.Neutral),
                NewLine("Guess they came here too, huh?", Emotion.Neutral)},
                new() {
                    new() { NewLine("Be careful now~", Emotion.Laughter) },
                    new() { NewLine("Don't get overwhelmed", Emotion.Neutral) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("ChesedChoice", new() {
                NewLine("Hey~ manager.", Emotion.Laughter),
                NewLine("Off on a nice stroll?", Emotion.Laughter) },
                new() {
                    new() { NewLine("Hey~ manager.", Emotion.Laughter) },
                    new() { NewLine("Off on a nice stroll?", Emotion.Laughter) },
                    new() { NewLine("Who will it be this time~?", Emotion.Neutral) }
                });
            CreateDialogueEvents("ChesedDrawn", new() {
                NewLine("Nothing like a fresh cup of coffee to start your day.", Emotion.Laughter) },
                new() {
                    new() { NewLine("I'm sure we can do this well.", Emotion.Laughter) },
                    new() { NewLine("Let's try our best, yeah?", Emotion.Laughter) },
                    new() { NewLine("Don't overwork yourself~", Emotion.Laughter) }
                });
            CreateDialogueEvents("ChesedGivenSigil", new() {
                NewLine("Hey, thanks~", Emotion.Laughter) },
                new() {
                    new() { NewLine("That was smooth~", Emotion.Laughter) },
                    new() { NewLine("Hey thanks~", Emotion.Laughter) },
                    new() {
                        NewLine("Guess I gotta work harder", Emotion.Surprise),
                        NewLine("in their stead, huh?", Emotion.Surprise) },
                    new() { NewLine("How about a nice cup of coffee?", Emotion.Laughter) },
                });
            CreateDialogueEvents("ChesedHurt", new() {
                NewLine("Ah!", Emotion.Surprise) },
                new() {
                    new() { NewLine("Ah!", Emotion.Surprise) },
                    new() { NewLine("Hm.", Emotion.Quiet) },
                    new() { NewLine("Tch.", Emotion.Curious) }
            });
            CreateDialogueEvents("ChesedPlayed", new() {
                NewLine("I don't want anyone to die here.", Emotion.Surprise) },
                new() {
                    new() { NewLine("I don't want anyone to die here.", Emotion.Surprise) },
                    new() { NewLine("If there's a meaning to this fight...", Emotion.Neutral) },
                    new() { NewLine("No choice but to stand our ground.", Emotion.Curious) },
                    new() { NewLine("All this effort wasted on fighting.", Emotion.Surprise) }
            });
            CreateDialogueEvents("ChesedSacrificed", new() {
                NewLine("I guess this is as far as I can go...", Emotion.Surprise) },
                new() {
                    new() { NewLine("Ah...", Emotion.Surprise) },
                    new() { NewLine("Heh...", Emotion.Surprise) },
                    new() { NewLine("See you in Hell.", Emotion.Anger) }
            });
            CreateDialogueEvents("ChesedSelectableBad", new() {
                NewLine("Hmm...", Emotion.Neutral) },
                new() {
                    new() { NewLine("This again?", Emotion.Neutral) },
                    new() {
                        NewLine("We can lose everything", Emotion.Curious),
                        NewLine("if we hesitate.", Emotion.Curious) },
                    new() { NewLine("I'd hate for us to suffer even more.", Emotion.Surprise) }
            });
            CreateDialogueEvents("ChesedSelectableGood", new()
            {
                NewLine("This looks promising~", Emotion.Laughter) },
                new() {
                    new() { NewLine("Oh, this again~", Emotion.Laughter) },
                    new() { NewLine("I wouldn't mind a nice of cup of coffee now.", Emotion.Laughter) },
                    new() { NewLine("Who will it be~?", Emotion.Laughter) },
                    new() { NewLine("This looks promising~", Emotion.Laughter) }
            });
            CreateDialogueEvents("ChesedTrial", new() {
                NewLine("Sometimes, you have to get bold", Emotion.Laughter),
                NewLine("in order to protect something.", Emotion.Curious) },
                new() {
                    new() { NewLine("Nothing we haven't seen before.", Emotion.Laughter) },
                    new() { NewLine("I'm sure this will go well.", Emotion.Laughter) },
                    new() { NewLine("Let's do our best and finish the job~", Emotion.Laughter) },
                    new() {
                        NewLine("Sometimes, you have to get bold", Emotion.Laughter),
                        NewLine("in order to protect something.", Emotion.Curious)
                    }
            });
            #endregion
        }
        private void Dialogue_Gebura()
        {
            #region Boss Dialogue
            CreateDialogueEvents("GeburaProspector", new()
            {
                NewLine("Who's this geezer?", Emotion.Neutral) },
                new() {
                    new() { NewLine("This guy...", Emotion.Anger) },
                    new() { NewLine("What a pain.", Emotion.Neutral) }
            });
            CreateDialogueEvents("GeburaAngler", new()
            {
                NewLine("Let's make quick work of him.", Emotion.Neutral) },
                new() {
                    new() { NewLine("This stench takes me back.", Emotion.Neutral) },
                    new() { NewLine("Watch that hook there.", Emotion.Neutral) }
            });
            CreateDialogueEvents("GeburaTrapperTrader", new()
            {
                NewLine("Keep on your toes now.", Emotion.Neutral) },
                new() {
                    new() { NewLine("These traps are annoying.", Emotion.Anger) },
                    new() { NewLine("Clear a path and strike cleanly.", Emotion.Neutral) }
            });
            CreateDialogueEvents("GeburaLeshy", new()
            {
                NewLine("Nothing left but to continue forward.", Emotion.Neutral) },
                new() {
                    new() { NewLine("We've got this.", Emotion.Neutral) },
                    new() { NewLine("One more enemy left.", Emotion.Neutral) }
            });
            CreateDialogueEvents("GeburaRoyal", new()
            {
                NewLine("Not the strangest thing I've seen.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Be quick and avoid those cannons.", Emotion.Neutral) },
                    new() { NewLine("His goons are tougher than they look.", Emotion.Anger) }
            });
            CreateDialogueEvents("GeburaApocalypse", new()
            {
                NewLine("Why have you come here?", Emotion.Neutral),
                NewLine("You trying to die?", Emotion.Anger) },
                new() {
                    new() { NewLine("Let me handle this.", Emotion.Neutral) },
                    new() { NewLine("These stupid birds...", Emotion.Anger) },
                    new() { NewLine("Don't chicken out now.", Emotion.Neutral) }
            });
            CreateDialogueEvents("GeburaOrdeal", new()
            {
                NewLine("An Ordeal, huh?", Emotion.Neutral),
                NewLine("Let's hurry up and smash 'em.", Emotion.Neutral)},
                new() {
                    new() { NewLine("Let's hurry up and smash 'em.", Emotion.Neutral) },
                    new() { NewLine("Nothing I can't handle myself.", Emotion.Neutral) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("GeburaChoice", new() {
                NewLine("Manager.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Manager.", Emotion.Neutral) },
                    new() { NewLine("Hey manager.", Emotion.Neutral) }
                });
            CreateDialogueEvents("GeburaDrawn", new() {
                NewLine("Not as strong as I used to be.", Emotion.Neutral),
                NewLine("Still not used to it.", Emotion.Curious) },
                new() {
                    new() {
                        NewLine("Not as strong as I used to be.", Emotion.Neutral),
                        NewLine("Still not used to it.", Emotion.Curious) },
                    new() { NewLine("Don't let your strength get to your head.", Emotion.Neutral) },
                    new() { NewLine("Manager.", Emotion.Neutral) },
                    new() { NewLine("Always keep a cool head.", Emotion.Neutral) },
            });
            CreateDialogueEvents("GeburaGivenSigil", new() {
                NewLine("I'm starting to get accustomed", Emotion.Neutral),
                NewLine("to this new body.", Emotion.Neutral) },
                new() {
                    new() {
                        NewLine("I'm starting to get accustomed", Emotion.Neutral),
                        NewLine("to this new body.", Emotion.Neutral) },
                    new() { NewLine("I won't waste this strength.", Emotion.Neutral) },
                    new() { NewLine("Thanks.", Emotion.Laughter) }
            });
            CreateDialogueEvents("GeburaHurt", new() {
                NewLine("That all?", Emotion.Anger) },
                new() {
                    new() { NewLine("Bastard.", Emotion.Anger) },
                    new() { NewLine("That all?", Emotion.Anger) }
            });
            CreateDialogueEvents("GeburaPlayed", new() {
                NewLine("Let's beat these guys into submission.", Emotion.Anger) },
                new() {
                    new() { NewLine("Let's beat these guys into submission.", Emotion.Anger) },
                    new() { NewLine("Let's do this.", Emotion.Neutral) },
                    new() { NewLine("We mustn't falter just yet.", Emotion.Neutral) },
                    new() { NewLine("We'll talk after everything's over.", Emotion.Neutral) }
            });
            CreateDialogueEvents("GeburaSacrificed", new() {
                NewLine("Bastard.", Emotion.Anger) },
                new() {
                    new() { NewLine("Bastard.", Emotion.Anger) },
                    new() { NewLine("Damn it.", Emotion.Anger) },
                    new() { NewLine("Don't let it be in vain, yeah?", Emotion.Neutral) }
            });
            CreateDialogueEvents("GeburaSelectableBad", new() {
                NewLine("Don't like this...", Emotion.Neutral) },
                new() {
                    new() { NewLine("Wayward wrath will only ruin yourself.", Emotion.Neutral) },
                    new() { NewLine("You know this isn't just a silly game, right?", Emotion.Curious) },
                    new() { NewLine("Mind where you point your blade.", Emotion.Neutral) },
                    new() { NewLine("Not a chance.", Emotion.Neutral) }
            });
            CreateDialogueEvents("GeburaSelectableGood", new() {
                NewLine("This looks like a good opportunity.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Guess I'll cop a nap.", Emotion.Neutral) },
                    new() { NewLine("This looks like a good opportunity.", Emotion.Laughter) },
                    new() { NewLine("Another opportunity, huh?", Emotion.Neutral) },
                    new() { NewLine("Don't take it for granted.", Emotion.Neutral) }
            });
            CreateDialogueEvents("GeburaTrial", new() {
                NewLine("A test of some sort?", Emotion.Curious) },
                new() {
                    new() { NewLine("Let me have a go.", Emotion.Neutral) },
                    new() {
                        NewLine("Let's work together", Emotion.Neutral),
                        NewLine("and give it our best shot.", Emotion.Neutral) },
                    new() { NewLine("I defer to your judgement.", Emotion.Neutral) }
            });
            #endregion
        }
        private void Dialogue_Hod()
        {
            #region Boss Dialogue
            CreateDialogueEvents("HodProspector", new()
            {
                NewLine("Watch out for that pickaxe.", Emotion.Curious) },
                new() {
                    new() { NewLine("We've prepared for this.", Emotion.Anger) },
                    new() { NewLine("I'll follow your lead.", Emotion.Neutral) }
            });
            CreateDialogueEvents("HodAngler", new()
            {
                NewLine("Urk, the smell...", Emotion.Curious) },
                new() {
                    new() { NewLine("Let's get out of here quickly...", Emotion.Curious) },
                    new() { NewLine("Don't knock over those buckets.", Emotion.Curious) }
            });
            CreateDialogueEvents("HodTrapperTrader", new()
            {
                NewLine("A betrayal...", Emotion.Curious) },
                new() {
                    new() { NewLine("I'll trust you, manager.", Emotion.Neutral) },
                    new() { NewLine("So many pelts...", Emotion.Neutral) }
            });
            CreateDialogueEvents("HodLeshy", new()
            {
                NewLine("The moon's so large...", Emotion.Curious) },
                new() {
                    new() { NewLine("We can do this.", Emotion.Anger) },
                    new() { NewLine("We're a lot stronger than before.", Emotion.Anger) }
            });
            CreateDialogueEvents("HodRoyal", new()
            {
                NewLine("A pirate...skeleton?", Emotion.Neutral),
                NewLine("...skeleton?", Emotion.Curious)},
                new() {
                    new() { NewLine("Those cannons hurt a lot...", Emotion.Neutral) }
            });
            CreateDialogueEvents("HodApocalypse", new()
            {
                NewLine("That monster...", Emotion.Quiet),
                NewLine("What do we do?", Emotion.Curious) },
                new() {
                    new() { NewLine("Keep calm...keep calm...", Emotion.Curious) },
                    new() { NewLine("Remember our training.", Emotion.Anger) }
            });
            CreateDialogueEvents("HodOrdeal", new()
            {
                NewLine("How did they get outside?", Emotion.Neutral) },
                new() {
                    new() { NewLine("Did they follow us?", Emotion.Curious) },
                    new() { NewLine("Hurry, before more come.", Emotion.Neutral) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("HodChoice", new() {
                NewLine("Oh! Um...", Emotion.Quiet),
                NewLine("Hello manager.", Emotion.Laughter) },
                new() {
                    new() { NewLine("Hello manager.", Emotion.Laughter) },
                    new() { NewLine("I won't be a hindrance!", Emotion.Anger) },
                    new() { NewLine("Who do you choose?", Emotion.Neutral) }
                });
            CreateDialogueEvents("HodDrawn", new() {
                NewLine("I'm not that skilled...", Emotion.Quiet),
                NewLine("but I'll do my best!", Emotion.Anger) },
                new() {
                    new() { NewLine("I'll try not to be a hindrance.", Emotion.Surprise) },
                    new() { NewLine("If we prepare, we can overcome this.", Emotion.Neutral) },
                    new() { NewLine("I'm here to do my best.", Emotion.Neutral) },
                    new() { NewLine("Hello manager.", Emotion.Laughter) }
                });
            CreateDialogueEvents("HodGivenSigil", new() {
                NewLine("I still have a lot to improve on...", Emotion.Quiet) },
                new() {
                    new() { NewLine("I still have a lot to improve on...", Emotion.Quiet) },
                    new() { NewLine("I gotta get on everyone else's level.", Emotion.Anger) },
                    new() { NewLine("With this I can better help everyone.", Emotion.Neutral) },
                    new() { NewLine("I hope this helps.", Emotion.Surprise) }
                });
            CreateDialogueEvents("HodHurt", new() {
                NewLine("Ah!", Emotion.Surprise) },
                new() {
                    new() { NewLine("Ah!", Emotion.Surprise) },
                    new() { NewLine("Ow!", Emotion.Surprise) },
                    new() { NewLine("I-I can take it...", Emotion.Surprise) },
                    new() { NewLine("I can't let them down...", Emotion.Surprise) }
                });
            CreateDialogueEvents("HodPlayed", new() {
                NewLine("I’m nervous, but...", Emotion.Surprise),
                NewLine("I won't let you down...!", Emotion.Anger) },
                new(){
                    new() { NewLine("I won't let you down!", Emotion.Anger) },
                    new() { NewLine("I hope nothing bad happens.", Emotion.Curious) },
                    new() { NewLine("Alright, here I come!", Emotion.Curious) },
                    new() { NewLine("Everything will work out...", Emotion.Neutral),
                            NewLine("Maybe...", Emotion.Curious) },
                    new() { NewLine("If we brace ourselves, we can win this.", Emotion.Anger) },
                    new() { NewLine("I can do this.", Emotion.Anger) }
                });
            CreateDialogueEvents("HodSacrificed", new() {
                NewLine("I was nothing but trouble...", Emotion.Quiet)},
                new() {
                    new() { NewLine("I was nothing but trouble...", Emotion.Surprise) },
                    new() { NewLine("My body feels numb...", Emotion.Surprise) },
                    new() { NewLine("I should've done a better job.", Emotion.Surprise) }
                });
            CreateDialogueEvents("HodSelectableBad", new() {
                NewLine("I don't like this...", Emotion.Curious) },
                new() {
                    new() { NewLine("I don't like this...", Emotion.Curious) },
                    new() { NewLine("Maybe someone else can go?", Emotion.Curious) },
                    new() { NewLine("I can't...not again...", Emotion.Quiet) },
                    new() { NewLine("You need me, right?", Emotion.Quiet) }
                });
            CreateDialogueEvents("HodSelectableGood", new() {
                NewLine("Let's think about this...", Emotion.Surprise) },
                new() {
                    new() { NewLine("Let's think about this...", Emotion.Surprise) },
                    new() { NewLine("If it will make me more helpful...", Emotion.Neutral) },
                    new() { NewLine("Will this give me my courage?", Emotion.Curious) }
                });
            CreateDialogueEvents("HodTrial", new() {
                NewLine("A trial?", Emotion.Curious) },
                new() {
                    new() { NewLine("A trial?", Emotion.Curious) },
                    new() { NewLine("I hope I can help.", Emotion.Neutral) },
                    new() { NewLine("I hope I'm prepared.", Emotion.Neutral) }
                });
            #endregion
        }
        private void Dialogue_Hokma()
        {
            #region Boss Dialogue
            CreateDialogueEvents("HokmaProspector", new()
            {
                NewLine("This is no different from anything else.", Emotion.Neutral) },
                new() {
                    new() { NewLine("We've done this before.", Emotion.Neutral) },
                    new() { NewLine("We must move onward.", Emotion.Neutral) }
            });
            CreateDialogueEvents("HokmaAngler", new()
            {
                NewLine("We can endlessly question", Emotion.Neutral),
                NewLine("why we must endure this endeavour.", Emotion.Neutral) },
                new() {
                    new() {
                        NewLine("Take all possibilities", Emotion.Neutral),
                        NewLine("into consideration.", Emotion.Neutral) },
                    new() { NewLine("Let's work quickly now.", Emotion.Neutral) }
            });
            CreateDialogueEvents("HokmaTrapperTrader", new()
            {
                NewLine("His madness can be used to our advantage.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Bend the rules to your favour.", Emotion.Neutral) },
                    new() { NewLine("Don't let the snow blind you.", Emotion.Neutral) }
            });
            CreateDialogueEvents("HokmaLeshy", new()
            {
                NewLine("We are all bound by something greater.", Emotion.Neutral),
                NewLine("He is no exception.", Emotion.Neutral) },
                new() {
                    new() { NewLine("If we focus, victory is assured.", Emotion.Neutral) },
                    new() { NewLine("Everything will happen as it should.", Emotion.Neutral) }
            });
            CreateDialogueEvents("HokmaRoyal", new()
            {
                NewLine("...I admit I am caught off-guard.", Emotion.Neutral) },
                new() {
                    new() { NewLine("In the end, this is all a pointless errand.", Emotion.Neutral) },
                    new() {
                        NewLine("The cannons are slow.", Emotion.Neutral),
                        NewLine("We can easily avoid them.", Emotion.Neutral) }
            });
            CreateDialogueEvents("HokmaApocalypse", new()
            {
                NewLine("Every Goliath has its David.", Emotion.Neutral),
                NewLine("Have faith in yourself, and in us.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Have faith in yourself and us.", Emotion.Curious) },
                    new() { NewLine("We will make it through.", Emotion.Curious) }
            });
            CreateDialogueEvents("HokmaOrdeal", new()
            {
                NewLine("Some things cannot be controlled.", Emotion.Neutral) },
                new() {
                    new() { NewLine("We must prove ourselves once more.", Emotion.Neutral) },
                    new() { NewLine("Some things cannot be controlled.", Emotion.Neutral) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("HokmaChoice", new()
            {
                NewLine("Greetings manager.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Greetings manager.", Emotion.Neutral) },
                    new() { NewLine("There is work to be done.", Emotion.Neutral) }
            });
            CreateDialogueEvents("HokmaDrawn", new()
            {
                NewLine("What matters more than strength...", Emotion.Neutral),
                NewLine("is faith in our victory.", Emotion.Anger) },
                new() {
                    new() { NewLine("Stay true to your belief.", Emotion.Neutral) },
                    new() {
                        NewLine("You have come to the point", Emotion.Neutral),
                        NewLine("of no return.", Emotion.Neutral) },
                    new() { NewLine("It begins once more.", Emotion.Neutral) }
            });
            CreateDialogueEvents("HokmaGivenSigil", new()
            {
                NewLine("Thank you.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Thank you.", Emotion.Neutral) },
                    new() { NewLine("Thank you.", Emotion.Laughter) },
                    new() { NewLine("This will help greatly.", Emotion.Neutral) }
            });
            CreateDialogueEvents("HokmaHurt", new()
            {
                NewLine("...", Emotion.Anger) },
                new() {
                    new() { NewLine("...", Emotion.Anger) },
                    new() { NewLine("", Emotion.Anger) }
            });
            CreateDialogueEvents("HokmaPlayed", new()
            {
                NewLine("Let us begin the day.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Overconfidence is the greatest enemy.", Emotion.Neutral) },
                    new() { NewLine("Let us begin the day.", Emotion.Neutral) },
                    new() { NewLine("Let's hurry to finish this work.", Emotion.Neutral) }
            });
            CreateDialogueEvents("HokmaSacrificed", new()
            {
                NewLine("A pointless errand.", Emotion.Anger) },
                new() {
                    new() { NewLine("A pointless errand.", Emotion.Anger) },
                    new() { NewLine("", Emotion.Neutral) },
                    new() { NewLine("", Emotion.Anger) }
            });
            CreateDialogueEvents("HokmaSelectableBad", new()
            {
                NewLine("A pointless errand.", Emotion.Anger) },
                new() {
                    new() { NewLine("A pointless errand.", Emotion.Anger) },
                    new() { NewLine("", Emotion.Neutral) },
                    new() { NewLine("", Emotion.Anger) }
            });
            CreateDialogueEvents("HokmaSelectableGood", new()
            {
                NewLine("An opportunity presents itself.", Emotion.Neutral) },
                new() {
                    new() { NewLine("An opportunity presents itself.", Emotion.Neutral) },
                    new() { NewLine("Be prudent about this.", Emotion.Neutral) },
                    new() { NewLine("I have faith in your decision.", Emotion.Neutral) }
            });
            CreateDialogueEvents("HokmaTrial", new()
            {
                NewLine("Boundless pride and ambition", Emotion.Neutral),
                NewLine("will bring about a bitter end...", Emotion.Neutral) },
                new() {
                    new() { NewLine("Do not be hasty.", Emotion.Neutral) },
                    new() { NewLine("This is just another test.", Emotion.Neutral) },
                    new() { NewLine("An endless series of trials.", Emotion.Neutral) }
            });
            #endregion
        }
        private void Dialogue_Malkuth()
        {
            #region Boss Dialogue
            CreateDialogueEvents("MalkuthProspector", new()
            {
                NewLine("Heads up manager!", Emotion.Neutral) },
                new() {
                    new() { NewLine("We've got this!", Emotion.Anger) },
                    new() { NewLine("Let's stay sharp, now!", Emotion.Neutral) }
            });
            CreateDialogueEvents("MalkuthAngler", new()
            {
                NewLine("The familiar smell of death.", Emotion.Quiet) },
                new() {
                    new() { NewLine("Winning's just a matter of time!", Emotion.Anger) },
                    new() { NewLine("Watch out for those bait buckets!", Emotion.Neutral) }
            });
            CreateDialogueEvents("MalkuthTrapperTrader", new()
            {
                NewLine("Be on the look-out!", Emotion.Neutral) },
                new() {
                    new() { NewLine("Watch out for traps!", Emotion.Neutral) },
                    new() { NewLine("We need to be smart about this.", Emotion.Neutral) }
            });
            CreateDialogueEvents("MalkuthLeshy", new()
            {
                NewLine("This guy looks tough.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Take a deep breath, and focus.", Emotion.Neutral) },
                    new() {
                        NewLine("We've made this far.", Emotion.Neutral),
                        NewLine("We can't lose now!", Emotion.Anger) }
            });
            CreateDialogueEvents("MalkuthRoyal", new()
            {
                NewLine("Are those cannons?", Emotion.Quiet) },
                new() {
                    new() {
                        NewLine("We've dealt with worse.", Emotion.Neutral),
                        NewLine("No need to worry too much!", Emotion.Laughter) },
                    new() {
                        NewLine("Treat it as another abnormality", Emotion.Neutral),
                        NewLine("and act accordingly!", Emotion.Neutral) }
            });
            CreateDialogueEvents("MalkuthApocalypse", new()
            {
                NewLine("Don't worry!", Emotion.Neutral),
                NewLine("We can do it!", Emotion.Laughter) },
                new() {
                    new() { NewLine("We just need to outlast it...", Emotion.Quiet) },
                    new() { NewLine("Manage your resources carefully!", Emotion.Anger) }
            });
            CreateDialogueEvents("MalkuthOrdeal", new()
            {
                NewLine("These creepy things...", Emotion.Neutral)},
                new() {
                    new() { NewLine("These creepy things...", Emotion.Neutral) },
                    new() { NewLine("You can't control the unknown.", Emotion.Neutral) },
                    new() { NewLine("Nothing we can't handle!", Emotion.Laughter) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("MalkuthChoice", new() {
                NewLine("Nice to meet you, manager!", Emotion.Neutral) },
                new() {
                    new() { NewLine("Hello again!", Emotion.Neutral) },
                    new() { NewLine("I'm prepared to help!", Emotion.Neutral) },
                    new() { NewLine("I promise to succeed!", Emotion.Anger) }
                });
            CreateDialogueEvents("MalkuthDrawn", new() {
                NewLine("Greetings Manager!", Emotion.Laughter) },
                new() {
                    new() { NewLine("Greetings Manager!", Emotion.Laughter) },
                    new() { NewLine("I promise to succeed!", Emotion.Anger) },
                    new() { NewLine("Trust me and follow my lead!", Emotion.Neutral) },
                    new() { NewLine("Let's do this!", Emotion.Anger) }
                });
            CreateDialogueEvents("MalkuthGivenSigil", new() {
                NewLine("I'm fully prepared now!", Emotion.Anger) },
                new() {
                    new() { NewLine("I'm fully prepared now!", Emotion.Anger) },
                    new() { NewLine("Winning is just a matter of time!", Emotion.Anger) },
                    new() { NewLine("I'll do my best within my ability!", Emotion.Neutral) },
                    new() { NewLine("Thank you manager!", Emotion.Laughter) }
                });
            CreateDialogueEvents("MalkuthHurt", new() {
                NewLine("Don't panic.", Emotion.Quiet) },
                new() {
                    new() { NewLine("Don't panic.", Emotion.Quiet) },
                    new() { NewLine("Stay calm.", Emotion.Quiet) },
                    new() { NewLine("I'll fight harder, don't worry!", Emotion.Anger) },
                    new() { NewLine("...", Emotion.Quiet) },
                    new() { NewLine("...", Emotion.Quiet) }
                });
            CreateDialogueEvents("MalkuthPlayed", new() {
                NewLine("I've got a good feeling about this!", Emotion.Anger) },
                new() {
                    new() { NewLine("I've got a good feeling about this!", Emotion.Anger) },
                    new() { NewLine("Trust me and follow my lead!", Emotion.Anger) },
                    new() { NewLine("Let's stay sharp, shall we?", Emotion.Neutral) },
                    new() { NewLine("Focus on the path forward!", Emotion.Anger) }
                });
            CreateDialogueEvents("MalkuthSacrificed", new() {
                NewLine("Not like this...", Emotion.Quiet) },
                new() {
                    new() { NewLine("Not like this...", Emotion.Quiet) },
                    new() { NewLine("...Don't give up.", Emotion.Quiet) },
                    new() { NewLine("I talked big and failed, again.", Emotion.Quiet) }
                });
            CreateDialogueEvents("MalkuthSelectableBad", new() {
                NewLine("Stay calm, everyone.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Stay calm, everyone.", Emotion.Neutral) },
                    new() { NewLine("I have faith in your decision.", Emotion.Quiet) },
                    new() { NewLine("Why don't we take a deep breath?", Emotion.Neutral) }
                });
            CreateDialogueEvents("MalkuthSelectableGood", new() {
                NewLine("Things are going smoothly!", Emotion.Surprise) },
                new() {
                    new() { NewLine("Things are going smoothly!", Emotion.Surprise) },
                    new() { NewLine("Let's make the most of this!", Emotion.Neutral) },
                    new() { NewLine("I trust your judgement!", Emotion.Laughter) }
                });
            CreateDialogueEvents("MalkuthTrial", new() {
                NewLine("Have faith in me!", Emotion.Neutral) },
                new() {
                    new() { NewLine("We can do it!", Emotion.Anger) },
                    new() { NewLine("Have faith in me!", Emotion.Anger) },
                    new() { NewLine("You've got this manager!", Emotion.Neutral) },
                    new() { NewLine("Nothing we haven't solved before!", Emotion.Neutral) }
                });
            #endregion
        }
        private void Dialogue_Netzach()
        {
            #region Boss Dialogue
            CreateDialogueEvents("NetzachProspector", new()
            {
                NewLine("That banging's annoying...", Emotion.Neutral) },
                new() {
                    new() { NewLine("Again?", Emotion.Neutral) },
                    new() { NewLine("Not a fan of that pickaxe.", Emotion.Neutral) }
            });
            CreateDialogueEvents("NetzachAngler", new()
            {
                NewLine("So many buckets of fish...", Emotion.Neutral) },
                new() {
                    new() { NewLine("The smell's making me sick.", Emotion.Neutral) },
                    new() { NewLine("Can't we take the long way around?", Emotion.Neutral) }
            });
            CreateDialogueEvents("NetzachTrapperTrader", new()
            {
                NewLine("Everyone here's crazy...", Emotion.Neutral) },
                new() {
                    new() { NewLine("At least those traps are obvious.", Emotion.Laughter) },
                    new() {
                        NewLine("Just want to lie down in the snow", Emotion.Neutral),
                        NewLine("and go to sleep.", Emotion.Neutral) }
            });
            CreateDialogueEvents("NetzachLeshy", new()
            {
                NewLine("This is the final boss?", Emotion.Neutral) },
                new() {
                    new() { NewLine("Since we've come this far, may as well.", Emotion.Neutral) },
                    new() { NewLine("Let's finish this.", Emotion.Neutral) }
            });
            CreateDialogueEvents("NetzachRoyal", new()
            {
                NewLine("...am I hallucinating?", Emotion.Neutral) },
                new() {
                    new() { NewLine("I guess I've seen weirder things.", Emotion.Neutral) },
                    new() { NewLine("Those cannons are pretty slow, huh?", Emotion.Neutral) }
            });
            CreateDialogueEvents("NetzachApocalypse", new()
            {
                NewLine("Can't we do something less...", Emotion.Neutral),
                NewLine("...stressful?", Emotion.Neutral)},
                new() {
                    new() { NewLine("Back to the bird, huh?", Emotion.Neutral) },
                    new() { NewLine("I'm tired of this.", Emotion.Neutral) }
            });
            CreateDialogueEvents("NetzachOrdeal", new()
            {
                NewLine("An ordeal?", Emotion.Neutral) },
                new() {
                    new() { NewLine("They never stop...", Emotion.Neutral) },
                    new() { NewLine("Part of the routine.", Emotion.Neutral) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("NetzachChoice", new() {
                NewLine("Hey.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Hey.", Emotion.Neutral) },
                    new() { NewLine("Pick someone else?", Emotion.Neutral) }
                });
            CreateDialogueEvents("NetzachDrawn", new() {
                NewLine("Hey.", Emotion.Quiet) },
                new() {
                    new() { NewLine("Hey.", Emotion.Neutral) },
                    new() { NewLine("Know what I need most? Retreat and sleep.", Emotion.Neutral) },
                    new() { NewLine("Sigh...", Emotion.Quiet),
                            NewLine("I'm too tired for this.", Emotion.Neutral) },
                    new() { NewLine("This battlefield atmosphere...", Emotion.Quiet),
                            NewLine("Not a fan of it.", Emotion.Neutral) }
                });
            CreateDialogueEvents("NetzachGivenSigil", new() {
                NewLine("That should be enough, right?", Emotion.Quiet) },
                new() {
                    new() { NewLine("That should be enough, right?", Emotion.Quiet) },
                    new() { NewLine("Waste of effort.", Emotion.Neutral) },
                    new() { NewLine("I'll try not to disappoint.", Emotion.Laughter) },
                    new() { NewLine("Thanks, I guess.", Emotion.Quiet) }
                });
            CreateDialogueEvents("NetzachHurt", new() {
                NewLine("Tch.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Tch.", Emotion.Anger) },
                    new() { NewLine("Tch.", Emotion.Neutral) }
                });
            CreateDialogueEvents("NetzachPlayed", new() {
                NewLine("I just wanna nap in the back...", Emotion.Neutral) },
                new() {
                    new() { NewLine("How did I end up doing things like this?", Emotion.Neutral) },
                    new() { NewLine("I just wanna nap in the back...", Emotion.Neutral) },
                    new() { NewLine("Let's do this...", Emotion.Neutral) },
                    new() { NewLine("Let's get this over with.", Emotion.Neutral) }
                });
            CreateDialogueEvents("NetzachSacrificed", new() {
                NewLine("Guess I can sleep now.", Emotion.Surprise) },
                new() {
                    new() { NewLine("Sorry I couldn't help.", Emotion.Neutral) },
                    new() { NewLine("Guess I can sleep now.", Emotion.Surprise) },
                    new() { NewLine("Why is the end always futile?", Emotion.Neutral) }
                });
            CreateDialogueEvents("NetzachSelectableBad", new() {
                NewLine("...I just wanna end this quickly.", Emotion.Quiet) },
                new() {
                    new() { NewLine("...I just wanna end this quickly.", Emotion.Quiet) },
                    new() { NewLine("Can I opt out?", Emotion.Neutral) },
                    new() { NewLine("Again?", Emotion.Neutral) },
                    new() { NewLine("Is leaving an option?", Emotion.Neutral) }
                });
            CreateDialogueEvents("NetzachSelectableGood", new() {
                NewLine("Choose whoever.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Choose whoever.", Emotion.Neutral) },
                    new() { NewLine("...", Emotion.Neutral) },
                    new() { NewLine("Can I opt out?", Emotion.Neutral) },
                    new() { NewLine("At your discretion, manager.", Emotion.Neutral) }
                });
            CreateDialogueEvents("NetzachTrial", new() {
                NewLine("Can I opt out?", Emotion.Neutral) },
                new() {
                    new() { NewLine("Can I opt out?", Emotion.Neutral) },
                    new() { NewLine("Again?", Emotion.Neutral) },
                    new() { NewLine("Why with all the tests?", Emotion.Neutral) },
                    new() { NewLine("Ugh...", Emotion.Quiet) }
                });
            #endregion
        }
        private void Dialogue_Tiphereth()
        {
            #region Boss Dialogue
            CreateDialogueEvents("TipherethAProspector", new()
            {
                NewLine("Let's deal with this guy quickly.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Should've stayed out of the way.", Emotion.Neutral) },
                    new() { NewLine("You still haven't beat him?", Emotion.Neutral) }
            });
            CreateDialogueEvents("TipherethAAngler", new()
            {
                NewLine("It smells like the Backstreets here.", Emotion.Neutral) },
                new() {
                    new() { NewLine("At least his actions are predictable.", Emotion.Neutral) },
                    new() { NewLine("Quickly now.", Emotion.Neutral) }
            });
            CreateDialogueEvents("TipherethATrapperTrader", new()
            {
                NewLine("Guess we're next on the chopping block.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Those knives look sharp. Careful.", Emotion.Neutral) },
                    new() { NewLine("Hold onto those pelts now.", Emotion.Neutral) }
            });
            CreateDialogueEvents("TipherethALeshy", new()
            {
                NewLine("We've got this manager.", Emotion.Laughter) },
                new() {
                    new() { NewLine("Don't forget, you have us to help.", Emotion.Surprise) },
                    new() { NewLine("One more foe to beat.", Emotion.Neutral) }
            });
            CreateDialogueEvents("TipherethARoyal", new()
            {
                NewLine("Seriously?", Emotion.Neutral) },
                new() {
                    new() { NewLine("Never was that into pirates.", Emotion.Neutral) },
                    new() { NewLine("This guy can't aim at all.", Emotion.Neutral) }
            });
            CreateDialogueEvents("TipherethAApocalypse", new()
            {
                NewLine("It's massive...", Emotion.Neutral),
                NewLine("I hope you know", Emotion.Neutral),
                NewLine("what you're doing.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Keep your head on tight, manager.", Emotion.Neutral) },
                    new() { NewLine("This'll be tough.", Emotion.Neutral) },
                    new() { NewLine("We'll make it out for sure.", Emotion.Neutral) }
            });
            CreateDialogueEvents("TipherethAOrdeal", new()
            {
                NewLine("Ugh, these things.", Emotion.Anger) },
                new() {
                    new() { NewLine("Let's finish this quickly.", Emotion.Neutral) },
                    new() { NewLine("Such a pain.", Emotion.Neutral) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("TipherethAChoice", new() {
                NewLine("Hurry and pick one of us.", Emotion.Anger) },
                new() {
                    new() { NewLine("Hurry and pick one of us.", Emotion.Anger) },
                    new() { NewLine("Don't keep us waiting.", Emotion.Neutral) },
                    new() { NewLine("Well?", Emotion.Neutral) }
                });
            CreateDialogueEvents("TipherethADrawn", new() {
                NewLine("Buckle up manager.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Buckle up manager.", Emotion.Neutral) },
                    new() {
                        NewLine("Have faith.", Emotion.Laughter),
                        NewLine("You're not fighting alone.", Emotion.Surprise) },
                    new() {
                        NewLine("You can't plan forever.", Emotion.Neutral),
                        NewLine("Now's the time for action.", Emotion.Anger) },
                    new() { NewLine("You know what to do, right?", Emotion.Neutral) },
                    new() { NewLine("We can't let everyone die here.", Emotion.Neutral) }
            });
            CreateDialogueEvents("TipherethAGivenSigil", new() {
                NewLine("Think I can't do this myself?", Emotion.Anger) },
                new() {
                    new() { NewLine("Think I can't do this myself?", Emotion.Anger) },
                    new() { NewLine("Thanks.", Emotion.Neutral) },
                    new() { NewLine("Not bad.", Emotion.Neutral) },
                    new() { NewLine("This better be useful.", Emotion.Anger) }
            });
            CreateDialogueEvents("TipherethAHurt", new() {
                NewLine("Ah!", Emotion.Quiet) },
                new() {
                    new() { NewLine("Ah!", Emotion.Quiet) },
                    new() { NewLine("I was too slow...", Emotion.Quiet) }
            });
            CreateDialogueEvents("TipherethAPlayed", new() {
                NewLine("Push forward!", Emotion.Anger) },
                new() {
                    new() { NewLine("Push forward!", Emotion.Anger) },
                    new() { NewLine("Don't get cocky.", Emotion.Neutral) },
                    new() { NewLine("We can do this.", Emotion.Laughter) }
            });
            CreateDialogueEvents("TipherethASacrificed", new() {
                NewLine("...", Emotion.Anger) },
                new() {
                    new() { NewLine("...", Emotion.Anger) },
                    new() { NewLine("I can still fight!", Emotion.Anger) },
                    new() { NewLine("...Sorry", Emotion.Quiet) }
            });
            CreateDialogueEvents("TipherethASelectableBad", new() {
                NewLine("Why'd you bring us here?", Emotion.Anger) },
                new() {
                    new() { NewLine("Why'd you bring us here?", Emotion.Anger) },
                    new() { NewLine("No.", Emotion.Anger) },
                    new() { NewLine("Don't you dare.", Emotion.Anger) }
            });
            CreateDialogueEvents("TipherethASelectableGood", new() {
                NewLine("A respite?", Emotion.Neutral) },
                new() {
                    new() { NewLine("Decide for yourself.", Emotion.Neutral) },
                    new() { NewLine("Make good use of this.", Emotion.Neutral) },
                    new() { NewLine("Choose someone that needs it.", Emotion.Neutral) }
            });
            CreateDialogueEvents("TipherethATrial", new() {
                NewLine("A trial?", Emotion.Neutral) },
                new() {
                    new() { NewLine("We've gone through worse.", Emotion.Neutral) },
                    new() { NewLine("I can handle it.", Emotion.Neutral) },
                    new() { NewLine("I'll win this for sure.", Emotion.Laughter) }
            });
            #endregion

            #region Tiphereth B
            CreateDialogueEvents("TipherethBDrawn", new() {
                NewLine("Hello manager.", Emotion.Laughter) },
                new() {
                    new() { NewLine("Hello manager.", Emotion.Neutral) },
                    new() { NewLine("Hello manager.", Emotion.Laughter) },
                    new() { NewLine("You'll do fine, like always.", Emotion.Laughter) }
            });
            CreateDialogueEvents("TipherethBPlayed", new() {
                NewLine("Here we go.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Here we go.", Emotion.Neutral) },
                    new() { NewLine("What a strange place...", Emotion.Neutral) },
                    new() { NewLine("Time to work.", Emotion.Neutral) },
                    new() { NewLine("Let's do our best.", Emotion.Laughter) }
            });
            CreateDialogueEvents("TipherethBSacrificed", new() {
                NewLine("...", Emotion.Quiet) },
                new() {
                    new() { NewLine("...", Emotion.Quiet) }
            });
            #endregion
        }
        private void Dialogue_Yesod()
        {
            #region Boss Dialogue
            CreateDialogueEvents("YesodProspector", new()
            {
                NewLine("Don't let his age lull you into", Emotion.Neutral),
                NewLine("a false sense of security.", Emotion.Neutral) },
                new() {
                    new() { NewLine("That pickaxe is sharp. Stay vigilant.", Emotion.Neutral) },
                    new() { NewLine("Let's not waste any time.", Emotion.Neutral) }
            });
            CreateDialogueEvents("YesodAngler", new()
            {
                NewLine("Don't let the smell dull your mind.", Emotion.Neutral) },
                new() {
                    new() {
                        NewLine("His hook always seems to hit its mark.", Emotion.Neutral),
                        NewLine("I'm sure you know how to deal with it.", Emotion.Neutral) },
                    new() { NewLine("This isn't particularly pleasant...", Emotion.Neutral) }
            });
            CreateDialogueEvents("YesodTrapperTrader", new()
            {
                NewLine("Come up with a plan, then execute it.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Don't let the cold cloud your senses, manager.", Emotion.Neutral) },
                    new() { NewLine("Hasty actions will bring more harm than good.", Emotion.Neutral) }
            });
            CreateDialogueEvents("YesodLeshy", new()
            {
                NewLine("We must make sound judgements", Emotion.Neutral),
                NewLine("before rushing forward.", Emotion.Neutral) },
                new() {
                    new() {
                        NewLine("Use what we've learned until now.", Emotion.Neutral),
                        NewLine("to take him down.", Emotion.Neutral) },
                    new() { NewLine("Consider this your final test.", Emotion.Neutral) }
            });
            CreateDialogueEvents("YesodRoyal", new()
            {
                NewLine("A skeleton? That's new.", Emotion.Neutral) },
                new() {
                    new() { NewLine("His crew seems persuadable.", Emotion.Neutral) },
                    new() { NewLine("Can't say I'm impressed.", Emotion.Neutral) }
            });
            CreateDialogueEvents("YesodApocalypse", new()
            {
                NewLine("Don't be intimidated.", Emotion.Neutral),
                NewLine("Manage your creatures wisely,", Emotion.Neutral),
                NewLine("and maintain constant fire.", Emotion.Neutral) },
                new() {
                    new() {
                        NewLine("We know its tricks now.", Emotion.Neutral),
                        NewLine("We are at an advantage.", Emotion.Neutral) },
                    new() { NewLine("Don't lose your cool now.", Emotion.Neutral) }
            });
            CreateDialogueEvents("YesodOrdeal", new()
            {
                NewLine("These trials are meant to test you.", Emotion.Neutral),
                NewLine("I trust you have what it takes.", Emotion.Neutral)},
                new() {
                    new() { NewLine("Be efficient.", Emotion.Neutral) },
                    new() { NewLine("Little more than a nuisance", Emotion.Neutral) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("YesodChoice", new() {
                NewLine("Hello manager.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Hello manager.", Emotion.Neutral) }
                });
            CreateDialogueEvents("YesodDrawn", new() {
                NewLine("Hello manager.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Hello manager.", Emotion.Neutral) },
                    new() { NewLine("Recklessly charging is not an effecient combat method.", Emotion.Neutral) },
                    new() { NewLine("We must prioritise concluding this fight quickly.", Emotion.Curious) },
                    new() { NewLine("I take it you're prepared for any outcome.", Emotion.Neutral) },
                    new() { NewLine("In a situation such as this,", Emotion.Curious),
                            NewLine("we must be calm and make sound judgements.", Emotion.Curious) },
                    new() { NewLine("Hello manager.", Emotion.Laughter) }
                });
            CreateDialogueEvents("YesodGivenSigil", new() {
                NewLine("I will fulfill my responsibility.", Emotion.Neutral) },
                new() {
                    new() { NewLine("I will fulfill my responsibility.", Emotion.Neutral) },
                    new() { NewLine("I accept this with grace.", Emotion.Neutral) },
                    new() { NewLine("Thank you.", Emotion.Laughter) }
                });
            CreateDialogueEvents("YesodHurt", new() {
                NewLine("Miscalculated...", Emotion.Curious) },
                new() {
                    new() { NewLine("Pay no mind to me.", Emotion.Curious) },
                    new() { NewLine("Focus on the goal.", Emotion.Curious) }
                });
            CreateDialogueEvents("YesodPlayed", new() {
                NewLine("If there's no opportunity to strike,", Emotion.Neutral),
                NewLine("we'll simply make one.", Emotion.Anger) },
                new() {
                    new() { NewLine("If there's no opportunity to strike,", Emotion.Neutral),
                            NewLine("we'll simply make one.", Emotion.Anger) },
                    new() { NewLine("Let's end this quickly.", Emotion.Curious) },
                    new() { NewLine("Very well.", Emotion.Neutral) },
                    new() { NewLine("Let's not push ourselves too carelessly.", Emotion.Curious) },
                    new() { NewLine("I don't want to waste any time.", Emotion.Curious) }
                });
            CreateDialogueEvents("YesodSacrificed", new() {
                NewLine("There is no need to pity me.", Emotion.Neutral) },
                new() {
                    new() { NewLine("I couldn't see the path ahead.", Emotion.Curious) },
                    new() { NewLine("What did I do wrong?", Emotion.Anger) },
                    new() { NewLine("It's entirely my fault.", Emotion.Curious) }
                });
            CreateDialogueEvents("YesodSelectableBad", new() {
                NewLine("This isn't particularly delightful.", Emotion.Neutral) },
                new() {
                    new() { NewLine("This isn't particularly delightful.", Emotion.Neutral) },
                    new() { NewLine("This death must not be in vain.", Emotion.Neutral) },
                    new() { NewLine("I suppose I should just accept this.", Emotion.Neutral) },
                    new() { NewLine("Frankly,", Emotion.Curious),
                            NewLine("I dislike how numb I've become to death.", Emotion.Anger) }
                });
            CreateDialogueEvents("YesodSelectableGood", new() {
                NewLine("Hm...", Emotion.Curious),
                NewLine("I trust you will choose correctly.", Emotion.Neutral) },
                new() {
                    new() { NewLine("I trust you will choose correctly.", Emotion.Neutral) },
                    new() { NewLine("Hasty actions will bring more harm than good.", Emotion.Curious) },
                    new() { NewLine("Be logical about this.", Emotion.Neutral) }
                });
            CreateDialogueEvents("YesodTrial", new() {
                NewLine("It seems to be a test of some kind.", Emotion.Curious),
                NewLine("I hope you're prepared.", Emotion.Anger) },
                new() {
                    new() { NewLine("I hope you're prepared.", Emotion.Neutral) },
                    new() { NewLine("A single mistake could cause disaster.", Emotion.Curious) },
                    new() { NewLine("We must always proceed with discretion.", Emotion.Neutral) }
                });
            #endregion
        }
    }
}
