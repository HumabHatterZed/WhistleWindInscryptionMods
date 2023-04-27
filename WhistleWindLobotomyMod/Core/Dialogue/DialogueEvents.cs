using DiskCardGame;
using static WhistleWind.Core.Helpers.DialogueHelper;
using static WhistleWindLobotomyMod.Core.DialogueEventsManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        private void Dialogue_MapNodes()
        {
            CreateDialogueEvents("AbnormalChoiceNodeIntro", new() {
                "You enter a clearing surrounded by dark, twisting trees.",
                "Their branches claw at the leering moon as you approach the well at the clearing's centre.",
                "Unearthly voices arise from the its inky depths, pleading, screaming, wishing and wanting.",
                "There are things in the well, waiting to be drawn up. Powerful, mysterious,",
                "[c:bR]abnormal[c:]."
                });
            CreateDialogueEvents("SefirotChoiceNodeIntro", new() {
                "As you journey through the wilderness, you come across a small group of humans.",
                "They're dressed oddly for this climate, and even odder, [c:bR]they seem to recognise you[c:].",
                "They are on an expedition of some kind, in search of [c:bR]strange[c:] beasts.",
                "Their appear quite knowledgeable. Perhaps you can convince them to aid you on your journey."
                });
        }
        private void Dialogue_ApocalypseBird()
        {
            CreateDialogueEvents("ApocalypseBirdIntro", new() {
                "Let me tell you a story. The story of the [c:bR]Black Forest[c:]."
                });
            CreateDialogueEvents("ApocalypseBirdOutro", new() {
                "The three birds, [c:bR]now one[c:] looked around for [c:bR]the Beast[c:]. But there was nothing.",
                "No creatures. No beast. No sun or moon or stars. Only a single bird, alone in an empty forest."
                });
            CreateDialogueEvents("ApocalypseBirdStory1", new() {
                "Once upon a time, [c:bR]three birds[c:] lived happily in the lush Forest with their fellow animals.",
                "One day a stranger arrived at the Forest.He proclaimed that the Forest would soon be ensared in a bitter conflict.",
                "One that would only end when everything was devoured by a[c: bR]terrible Beast[c:].",
                "The birds, frightened by this doomsay, sought to prevent conflict from ever breaking out."
                });
            CreateDialogueEvents("ApocalypseBirdStory2", new() {
                "Fights began to break out. More and more creatures left the Forest, no matter how hard the birds worked.",
                "They decided to combine their powers. This way, they could better protect their home.",
                "This way they could better return the peace."
                });
            CreateDialogueEvents("ApocalypseBirdStory3", new() {
                "Darkness fell upon the forest. Mayhem ran amok as creatures screamed in terror at the towering bird."
                });
            CreateDialogueEvents("ApocalypseBirdStory4", new() {
                "Someone yelled out [c:bB]'It's the Beast! A big, scary monster lives in the Black Forest!'[c:]"
                });
            CreateDialogueEvents("ApocalypseBirdBig", new() {
                "[c:bG]Big Bird[c:] with his many eyes kept constant watch over the entire Forest."
                });
            CreateDialogueEvents("ApocalypseBirdLong", new() {
                "[c:bSG]Long Bird[c:] weighed the sins of all creatures in the forest with his scales."
                });
            CreateDialogueEvents("ApocalypseBirdSmall", new() {
                "[c:bR]Small Bird[c:] punished wrongdoers with his beak."
                });
        }
        private void Dialogue_Abnormalities()
        {
            CreateDialogueEvents("ArmyInBlackTransform", new() {
                "The human heart is black, and must be cleaned."
                });
            CreateDialogueEvents("Bloodbath1", new() {
                "A hand rises from the scarlet pool."
                });
            CreateDialogueEvents("Bloodbath2", new() {
                "Another pale hand emerges."
                });
            CreateDialogueEvents("Bloodbath3", new() {
                "A third hand reaches out, as if asking for help."
                });
            CreateDialogueEvents("CENSOREDKilledCard", new() {
                "What have you done to my beast?"
                });
            CreateDialogueEvents("DerFreischutzSeventhBullet", new() {
                "The Devil proposed a childist contract.",
                "The seventh bullet would pierce the heart of his most beloved.",
                "On hearing this, the hunter sought and shot everyone he loved."
                });
            CreateDialogueEvents("ExpressHellTrainWipe", new() {
                "The train sounds its mighty horn."
                });
            CreateDialogueEvents("GiantTreeSapExplode", new() {
                "A strange gurgling sound comes from your beast's stomach."
                });
            CreateDialogueEvents("KingOfGreedTransform", new() {
                "Desire unfulfilled, the koi continues for Eden."
                });
            CreateDialogueEvents("KnightOfDespairTransform", new() {
                "Having failed to protect again, the knight fell once more to despair."
                });
            CreateDialogueEvents("MagicalGirlHeartTransform", new() {
                "The balance must be maintained. Good cannot exist without evil."
                });
            CreateDialogueEvents("MeltingLoveAbsorb", new() {
                "They give themselves lovingly."
                });
            CreateDialogueEvents("MountainOfBodiesGrow", new() {
                "With each body added, its appetite grows."
                });
            CreateDialogueEvents("MountainOfBodiesShrink", new() {
                "Don't worry, bodies are in no short supply."
                });
            CreateDialogueEvents("NamelessFetusAwake", new() {
                "As you cut into the beast's flesh, it lets out a piercing cry."
                });
            CreateDialogueEvents("NothingThereReveal", new() {
                "What is that thing?"
                });
            CreateDialogueEvents("NothingThereTransformEgg", new() {
                "It seems to be trying to mimic you. 'Trying' is the key word."
                });
            CreateDialogueEvents("NothingThereTransformTrue", new() {
                "What is it doing?"
                });
            CreateDialogueEvents("QueenOfHatredExhaust", new() {
                "A formidable attack. Shame it has left her too tired to defend herself."
                });
            CreateDialogueEvents("QueenOfHatredRecover", new() {
                "The monster returns to full strength."
                });
            CreateDialogueEvents("ServantOfWrathTransform", new() {
                "Abandoned and betrayed, she rages against her own failure."
                });
            CreateDialogueEvents("TodaysShyLookAngry", new() {
                "Some days you don't feel like smiling."
                });
            CreateDialogueEvents("TodaysShyLookHappy", new() {
                "There was no place for frowns in the City."
                });
            CreateDialogueEvents("TodaysShyLookNeutral", new() {
                "Unable to decide what face to wear, she became shy again."
                });
            CreateDialogueEvents("YinDragonIntro", new() {
                "Now you become the sky, and I the land."
                });
        }
        private void Dialogue_JesterOfNihil()
        {
            CreateDialogueEvents("JesterOfNihilIntro", new() {
                "Watch them carefully.",
                "They're calling - no, praying for something."
                });
            CreateDialogueEvents("JesterOfNihilOutro", new() {
                "There was no way to know if they had gathered to become [c:gray]The jester[c:],",
                "or if [c:gray]The jester[c:] had come to resemble them."
                });
            CreateDialogueEvents("JesterOfNihilStory", new() {
                "[c:gray]The jester[c:] retraced the steps of a path everyone would've taken.",
                "No matter what it did, the jester always found itself at the end of that road."
                });
        }
        private void Dialogue_LyingAdult()
        {
            CreateDialogueEvents("LyingAdultIntro", new() {
                "Long ago, there was a city made of shining emerald.",
                "Ruled by the princess [c:bR]Ozma[c:], it was home to many otherworldly splendors."
                });
            CreateDialogueEvents("LyingAdultIntro2", new() {
                "One day, an adult named [c:g1]Oz[c:] arrived to the Emerald City.",
                "Looking upon the brilliant city, she came up with a brilliant plan."
                });
            CreateDialogueEvents("LyingAdultIntro3", new() {
                "Soon, [c:g1]Oz[c:] came to be known as a wizard by the people."
                });
            CreateDialogueEvents("LyingAdultIntro4", new() {
                "Nothing is as deceptive as evident truth."
                });
            CreateDialogueEvents("LyingAdultOutro", new() {
                "Unable to ever go back home, they began an endless journey."
                });
            CreateDialogueEvents("LyingAdultOzma", new() {
                "[c:g1]I’ll make your wishes come true. What do you wish for?[c:]"
                });
            CreateDialogueEvents("LyingAdultOzma2", new() {
                "Poor [c:bR]Ozma[c:] didn’t know that lying adults don’t approach others without reason."
                });
            CreateDialogueEvents("LyingAdultRoadHome", new() {
                "[c:g1]Last of all, road that is lost. I will send you home.[c:]"
                });
            CreateDialogueEvents("LyingAdultRoadHome2", new() {
                "[c:g1]What are you fighting for so fiercely when you have nowhere to go back to?[c:]"
                });
            CreateDialogueEvents("LyingAdultScarecrow", new() {
                "[c:g1]Poor stuffing of straw. I'll give you the wisdom to ponder over anything.[c:]"
                });
            CreateDialogueEvents("LyingAdultScarecrow2", new() {
                "[c:g1]Do you think jabbering away with your oh-so smart mouth is all that matters?[c:]"
                });
            CreateDialogueEvents("LyingAdultScaredyCat", new() {
                "[c:g1]Cowardly kitten, I'll give you the courage to stand up to anything and everything.[c:]"
                });
            CreateDialogueEvents("LyingAdultScaredyCat2", new() {
                "[c:g1]What are you even going to do when you lack the bravery to face anything head-on?[c:]"
                });
            CreateDialogueEvents("LyingAdultWoodsman", new() {
                "[c:g1]Tin-cold woodsman. I'll give you the heart to forgive and love anyone.[c:]"
                });
            CreateDialogueEvents("LyingAdultWoodsman2", new() {
                "[c:g1]Who do you possibly expect to understand with that ice-cold heart of yours?[c:]"
                });
        }
        private void Dialogue_Binah()
        {
            CreateDialogueEvents("SephirahBinahChoice", new()
            {

            });
            CreateDialogueEvents("SephirahBinahDrawn", new()
            {

            });
            CreateDialogueEvents("SephirahBinahGivenSigil", new()
            {

            });
            CreateDialogueEvents("SephirahBinahHurt", new()
            {

            });
            CreateDialogueEvents("SephirahBinahPlayed", new()
            {

            });
            CreateDialogueEvents("SephirahBinahSacrificed", new()
            {

            });
            CreateDialogueEvents("SephirahBinahSelectableBad", new()
            {

            });
            CreateDialogueEvents("SephirahBinahSelectableGood", new()
            {

            });
            CreateDialogueEvents("SephirahBinahTrial", new()
            {

            });
        }
        private void Dialogue_Chesed()
        {
            CreateDialogueEvents("SephirahChesedChoice", new()
            {

            });
            CreateDialogueEvents("SephirahChesedDrawn", new()
            {

            });
            CreateDialogueEvents("SephirahChesedGivenSigil", new()
            {

            });
            CreateDialogueEvents("SephirahChesedHurt", new()
            {

            });
            CreateDialogueEvents("SephirahChesedPlayed", new()
            {

            });
            CreateDialogueEvents("SephirahChesedSacrificed", new()
            {

            });
            CreateDialogueEvents("SephirahChesedSelectableBad", new()
            {

            });
            CreateDialogueEvents("SephirahChesedSelectableGood", new()
            {

            });
            CreateDialogueEvents("SephirahChesedTrial", new()
            {

            });
        }
        private void Dialogue_Gebura()
        {
            CreateDialogueEvents("SephirahGeburaChoice", new()
            {

            });
            CreateDialogueEvents("SephirahGeburaDrawn", new()
            {

            });
            CreateDialogueEvents("SephirahGeburaGivenSigil", new()
            {

            });
            CreateDialogueEvents("SephirahGeburaHurt", new()
            {

            });
            CreateDialogueEvents("SephirahGeburaPlayed", new()
            {

            });
            CreateDialogueEvents("SephirahGeburaSacrificed", new()
            {

            });
            CreateDialogueEvents("SephirahGeburaSelectableBad", new()
            {

            });
            CreateDialogueEvents("SephirahGeburaSelectableGood", new()
            {

            });
            CreateDialogueEvents("SephirahGeburaTrial", new()
            {

            });
        }
        private void Dialogue_Hod()
        {
            CreateDialogueEvents("SephirahHodChoice", new() {
                NewLine("Oh! Um...", Emotion.Quiet),
                NewLine("Hello manager.", Emotion.Laughter)
            });
            CreateDialogueEvents("SephirahHodDrawn", new() {
                NewLine("I'm not that skilled...", Emotion.Quiet),
                NewLine("but I'll do my best!", Emotion.Anger) },
                new() {
                    new() { NewLine("I'll try not to be a hindrance.", Emotion.Surprise) },
                    new() { NewLine("If we prepare, we can overcome this.", Emotion.Neutral) },
                    new() { NewLine("I'm here to do my best.", Emotion.Neutral) },
                    new() { NewLine("Hello manager.", Emotion.Laughter) }
                });
            CreateDialogueEvents("SephirahHodGivenSigil", new() {
                NewLine("I still have a lot to improve on...", Emotion.Quiet) },
                new() {
                    new() { NewLine("I still have a lot to improve on...", Emotion.Quiet) },
                    new() { NewLine("I gotta get on everyone else's level.", Emotion.Anger) },
                    new() { NewLine("With this I can better help everyone.", Emotion.Neutral) },
                    new() { NewLine("I hope this helps.", Emotion.Surprise) }
                });
            CreateDialogueEvents("SephirahHodHurt", new() {
                NewLine("Ah!", Emotion.Surprise) },
                new() {
                    new() { NewLine("Ah!", Emotion.Surprise) },
                    new() { NewLine("Ow!", Emotion.Surprise) },
                    new() { NewLine("I-I can take it...", Emotion.Surprise) },
                    new() { NewLine("I can't let them down...", Emotion.Surprise) }
                });
            CreateDialogueEvents("SephirahHodPlayed", new() {
                NewLine("I’m nervous, but...", Emotion.Surprise),
                NewLine("I won't let you down...!", Emotion.Anger) },
                new(){
                    new() { NewLine("I’m nervous, but...", Emotion.Surprise),
                            NewLine("I won't let you down...!", Emotion.Anger) },
                    new() { NewLine("I hope nothing bad happens.", Emotion.Curious) },
                    new() { NewLine("Alright, here I come!", Emotion.Curious) },
                    new() { NewLine("Everything will work out...", Emotion.Neutral),
                            NewLine("Maybe...", Emotion.Curious) },
                    new() { NewLine("If we brace ourselves, we can win this.", Emotion.Anger) },
                    new() { NewLine("I can do this.", Emotion.Anger) }
                });
            CreateDialogueEvents("SephirahHodSacrificed", new() {
                NewLine("I was nothing but trouble...", Emotion.Quiet)},
                new() {
                    new() { NewLine("I was nothing but trouble...", Emotion.Surprise) },
                    new() { NewLine("My body feels numb...", Emotion.Surprise) },
                    new() { NewLine("I should've done a better job.", Emotion.Surprise) }
                });
            CreateDialogueEvents("SephirahHodSelectableBad", new() {
                NewLine("I don't like this...", Emotion.Curious) },
                new() {
                    new() { NewLine("I don't like this...", Emotion.Curious) },
                    new() { NewLine("Maybe someone else can go?", Emotion.Curious) },
                    new() { NewLine("I can't...not again...", Emotion.Quiet) },
                    new() { NewLine("You need me, right?", Emotion.Quiet) }
                });
            CreateDialogueEvents("SephirahHodSelectableGood", new() {
                NewLine("Let's think about this...", Emotion.Surprise) },
                new() {
                    new() { NewLine("Let's think about this...", Emotion.Surprise) },
                    new() { NewLine("If it will make me more helpful...", Emotion.Neutral) },
                    new() { NewLine("Will this give me my courage?", Emotion.Curious) }
                });
            CreateDialogueEvents("SephirahHodTrial", new() {
                NewLine("A trial?", Emotion.Curious) },
                new() {
                    new() { NewLine("A trial?", Emotion.Curious) },
                    new() { NewLine("I hope I can help.", Emotion.Neutral) },
                    new() { NewLine("I hope I'm prepared.", Emotion.Neutral) }
                });
        }
        private void Dialogue_Hokma()
        {
            CreateDialogueEvents("SephirahHokmaChoice", new()
            {

            });
            CreateDialogueEvents("SephirahHokmaDrawn", new()
            {

            });
            CreateDialogueEvents("SephirahHokmaGivenSigil", new()
            {

            });
            CreateDialogueEvents("SephirahHokmaHurt", new()
            {

            });
            CreateDialogueEvents("SephirahHokmaPlayed", new()
            {

            });
            CreateDialogueEvents("SephirahHokmaSacrificed", new()
            {

            });
            CreateDialogueEvents("SephirahHokmaSelectableBad", new()
            {

            });
            CreateDialogueEvents("SephirahHokmaSelectableGood", new()
            {

            });
            CreateDialogueEvents("SephirahHokmaTrial", new()
            {

            });
        }
        private void Dialogue_Malkuth()
        {

            CreateDialogueEvents("SephirahMalkuthChoice", new() {
                NewLine("Nice to meet you, manager!", Emotion.Neutral)
            });
            CreateDialogueEvents("SephirahMalkuthDrawn", new() {
                NewLine("Greetings Manager!", Emotion.Laughter) },
                new() {
                    new() { NewLine("Greetings Manager!", Emotion.Laughter) },
                    new() { NewLine("I promise to succeed!", Emotion.Anger) },
                    new() { NewLine("Trust me and follow my lead!", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahMalkuthGivenSigil", new() {
                NewLine("I'm fully prepared now!", Emotion.Anger) },
                new() {
                    new() { NewLine("I'm fully prepared now!", Emotion.Anger) },
                    new() { NewLine("Winning is just a matter of time!", Emotion.Anger) },
                    new() { NewLine("I'll do my best within my ability!", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahMalkuthHurt", new() {
                NewLine("Don't panic.", Emotion.Quiet) },
                new() {
                    new() { NewLine("Don't panic.", Emotion.Quiet) },
                    new() { NewLine("Stay calm.", Emotion.Quiet) },
                    new() { NewLine("I'll fight harder, don't worry!", Emotion.Anger) }
                });
            CreateDialogueEvents("SephirahMalkuthPlayed", new() {
                NewLine("I've got a good feeling about this!", Emotion.Anger) },
                new() {
                    new() { NewLine("I've got a good feeling about this!", Emotion.Anger) },
                    new() { NewLine("Trust me and follow my lead!", Emotion.Anger) },
                    new() { NewLine("Let's stay sharp, shall we?", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahMalkuthSacrificed", new() {
                NewLine("Not like this...", Emotion.Quiet) },
                new() {
                    new() { NewLine("Not like this...", Emotion.Quiet) },
                    new() { NewLine("...Don't give up.", Emotion.Quiet) },
                    new() { NewLine("I talked big and failed, again.", Emotion.Quiet) }
                });
            CreateDialogueEvents("SephirahMalkuthSelectableBad", new() {
                NewLine("Stay calm, everyone.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Stay calm, everyone.", Emotion.Neutral) },
                    new() { NewLine("I have faith in your decision.", Emotion.Quiet) },
                    new() { NewLine("Why don't we take a deep breath?", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahMalkuthSelectableGood", new() {
                NewLine("Things are going smoothly!", Emotion.Surprise) },
                new() {
                    new() { NewLine("Things are going smoothly!", Emotion.Surprise) },
                    new() { NewLine("Let's make the most of this!", Emotion.Neutral) },
                    new() { NewLine("I trust your judgement!", Emotion.Laughter) }
                });
            CreateDialogueEvents("SephirahMalkuthTrial", new() {
                NewLine("Have faith in me!", Emotion.Neutral) },
                new() {
                    new() { NewLine("We can do it!", Emotion.Anger) },
                    new() { NewLine("Have faith in me!", Emotion.Anger) },
                    new() { NewLine("You've got this manager!", Emotion.Neutral) }
                });
        }
        private void Dialogue_Netzach()
        {
            CreateDialogueEvents("SephirahNetzachChoice", new() {
                NewLine("Hey.", Emotion.Neutral)
                });
            CreateDialogueEvents("SephirahNetzachDrawn", new() {
                NewLine("Hey.", Emotion.Quiet) },
                new() {
                    new() { NewLine("Hey.", Emotion.Neutral) },
                    new() { NewLine("Know what I need most? Retreat and sleep.", Emotion.Neutral) },
                    new() { NewLine("Sigh...", Emotion.Quiet),
                            NewLine("I'm too tired for this.", Emotion.Neutral) },
                    new() { NewLine("This battlefield atmosphere...", Emotion.Quiet),
                            NewLine("Not a fan of it.", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahNetzachGivenSigil", new() {
                NewLine("That should be enough, right?", Emotion.Quiet) },
                new() {
                    new() { NewLine("That should be enough, right?", Emotion.Quiet) },
                    new() { NewLine("Waste of effort.", Emotion.Neutral) },
                    new() { NewLine("I'll try not to disappoint.", Emotion.Laughter) }
                });
            CreateDialogueEvents("SephirahNetzachHurt", new() {
                NewLine("Tch.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Tch.", Emotion.Anger) }
                });
            CreateDialogueEvents("SephirahNetzachPlayed", new() {
                NewLine("I just wanna nap in the back...", Emotion.Neutral) },
                new() {
                    new() { NewLine("How did I end up doing things like this?", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahNetzachSacrificed", new() {
                NewLine("Guess I can sleep now.", Emotion.Surprise) },
                new() {
                    new() { NewLine("Sorry I couldn't help.", Emotion.Neutral) },
                    new() { NewLine("Guess I can sleep now.", Emotion.Surprise) },
                    new() { NewLine("Why is the end always futile?", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahNetzachSelectableBad", new() {
                NewLine("...I just wanna end this quickly.", Emotion.Quiet) },
                new() {
                    new() { NewLine("...I just wanna end this quickly.", Emotion.Quiet) },
                    new() { NewLine("Can I opt out?", Emotion.Neutral) },
                    new() { NewLine("Again?", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahNetzachSelectableGood", new() {
                NewLine("Choose whomever.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Choose whomever.", Emotion.Neutral) },
                    new() { NewLine("Why with all the tests?", Emotion.Neutral) },
                    new() { NewLine("Can I opt out?", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahNetzachTrial", new() {
                NewLine("Can I opt out?", Emotion.Neutral) },
                new() {
                    new() { NewLine("Can I opt out?", Emotion.Neutral) },
                    new() { NewLine("Why with all the tests?", Emotion.Neutral) }
                });
        }
        private void Dialogue_Tiphereth()
        {
            CreateDialogueEvents("SephirahTipherethChoice", new()
            {

            });
            CreateDialogueEvents("SephirahTipherethDrawn", new()
            {

            });
            CreateDialogueEvents("SephirahTipherethGivenSigil", new()
            {

            });
            CreateDialogueEvents("SephirahTipherethHurt", new()
            {

            });
            CreateDialogueEvents("SephirahTipherethPlayed", new()
            {

            });
            CreateDialogueEvents("SephirahTipherethSacrificed", new()
            {

            });
            CreateDialogueEvents("SephirahTipherethSelectableBad", new()
            {

            });
            CreateDialogueEvents("SephirahTipherethSelectableGood", new()
            {

            });
            CreateDialogueEvents("SephirahTipherethTrial", new()
            {

            });
        }
        private void Dialogue_Yesod()
        {
            CreateDialogueEvents("SephirahYesodChoice", new() {
                NewLine("Hello manager.", Emotion.Neutral)
                });
            CreateDialogueEvents("SephirahYesodDrawn", new() {
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
            CreateDialogueEvents("SephirahYesodGivenSigil", new() {
                NewLine("I will fulfill my responsibility.", Emotion.Neutral) },
                new() {
                    new() { NewLine("I will fulfill my responsibility.", Emotion.Neutral) },
                    new() { NewLine("I accept this with grace.", Emotion.Neutral) },
                    new() { NewLine("Thank you.", Emotion.Laughter) }
                });
            CreateDialogueEvents("SephirahYesodHurt", new() {
                NewLine("Kh.", Emotion.Curious) },
                new() {
                    new() { NewLine("Kh.", Emotion.Curious) },
                    new() { NewLine("Ah!", Emotion.Anger) },
                    new() { NewLine("Pay no mind to me.", Emotion.Curious) },
                    new() { NewLine("Focus on the goal.", Emotion.Curious) }
                });
            CreateDialogueEvents("SephirahYesodPlayed", new() {
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
            CreateDialogueEvents("SephirahYesodSacrificed", new() {
                NewLine("There is no need to pity me.", Emotion.Neutral) },
                new() {
                    new() { NewLine("I couldn't see the path ahead.", Emotion.Curious) },
                    new() { NewLine("What did I do wrong?", Emotion.Anger) },
                    new() { NewLine("It's entirely my fault.", Emotion.Curious) }
                });
            CreateDialogueEvents("SephirahYesodSelectableBad", new() {
                NewLine("This isn't particularly delightful.", Emotion.Neutral) },
                new() {
                    new() { NewLine("This isn't particularly delightful.", Emotion.Neutral) },
                    new() { NewLine("This death must not be in vain.", Emotion.Neutral) },
                    new() { NewLine("I suppose I should just accept this.", Emotion.Neutral) },
                    new() { NewLine("Frankly,", Emotion.Curious),
                            NewLine("I dislike how numb I've become to death.", Emotion.Anger) }
                });
            CreateDialogueEvents("SephirahYesodSelectableGood", new() {
                NewLine("Hm...", Emotion.Curious),
                NewLine("I trust you will choose correctly.", Emotion.Neutral) },
                new() {
                    new() { NewLine("I trust you will choose correctly.", Emotion.Neutral) },
                    new() { NewLine("Hasty actions will bring more harm than good.", Emotion.Curious) },
                    new() { NewLine("Be logical about this.", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahYesodTrial", new() {
                NewLine("It seems to be a test of some kind.", Emotion.Curious),
                NewLine("I hope you're prepared.", Emotion.Anger) },
                new() {
                    new() { NewLine("I hope you're prepared.", Emotion.Neutral) },
                    new() { NewLine("A single mistake could cause disaster.", Emotion.Curious) },
                    new() { NewLine("We must always proceed with discretion.", Emotion.Neutral) }
                });
        }
        private void Dialogue_WhiteNight()
        {
            CreateDialogueEvents("WhiteNightEventIntro", new() {
                "[c:bR]The time has come. A new world will come.[c:]",
                "[c:bR]I am death and life. Darkness and light.[c:]" });
            CreateDialogueEvents("WhiteNightApostleHeretic", new() {
                "[c:bR]Have I not chosen you, the Twelve? Yet one of you is [c:][c:bG]a devil[c:][c:bR].[c:]"
                });
            CreateDialogueEvents("WhiteNightApostleDowned", new() {
                "[c:bR]None of you can leave my side until I permit you.[c:]"
                });
            CreateDialogueEvents("WhiteNightApostleKilledByNull", new() {
                "[c:bR]Be at ease. No calamity shall be able to trouble you.[c:]"
                });
            CreateDialogueEvents("WhiteNightKilledByNull", new() {
                "[c:bR]I shall not leave thee until I have completed my mission.[c:]"
                });
            CreateDialogueEvents("WhiteNightMakeRoom", new() {
                "[c:bR]What did you do?[c:]"
                });
        }
    }
}
