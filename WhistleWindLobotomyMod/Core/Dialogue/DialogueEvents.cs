using DiskCardGame;
using static WhistleWind.Core.Helpers.DialogueHelper;
using static WhistleWindLobotomyMod.Core.DialogueEventsManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
        private void Dialogue_BackwardClock()
        {
            CreateDialogueEvents("BackwardClockStart", new() {
                "Close your eyes and count to ten.",
                "When you open them, you will be standing at the exact moment you wish to be in." },
                new() {
                    new() { "Close your eyes and count to ten." }
                });

            CreateDialogueEvents("BackwardClockOperate", new() {
                "[c:bR]One of your creatures[c:] must stay behind to operate [c:bR]the Clock[c:]." },
                new() {
                    new() { "[c:bR]One of your creatures[c:] must stay behind to operate [c:bR]the Clock[c:]." }
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
            CreateDialogueEvents("CrimsonScarHood", new() {
                "At long last, she has found him."
                });
            CreateDialogueEvents("CrimsonScarWolf", new() {
                "That familiar red cloth..."
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
            CreateDialogueEvents("PlagueDoctorBless", new() {
                "The hands of the Clock move towards salvation."
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
                "One day a stranger arrived at the Forest. He proclaimed that the Forest would soon be ensared in a bitter conflict.",
                "One that would only end when everything was devoured by a [c:bR]terrible Beast[c:].",
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
            // boss stuff
            CreateDialogueEvents("ApocalypseBossPreIntro", new() {
                "You walk down into a large clearing, the black trees crowding around you.",
                "An otherworldly roar brings you to attention. The very earth quails in fear as something approaches you.",
                "Lumbering yet impossibly fast, you find yourself face-to-face with..."
                },
                new() {
                    new() {
                        "An otherworldly roar shakes you to your core. The forest floor quakes as something approaches you.",
                        "Lumbering yet impossibly fast, you find yourself face-to-face with..." },
                });
            CreateDialogueEvents("ApocalypseBossIntro",
                new() { "[c:bR]The Beast[c:]." },
                new() {
                    new() { "[c:bR]The Beast[c:]." }
                });
            CreateDialogueEvents("ApocalypseBossBrokenEggBig",
                new() { "The far-sighted eyes have been blinded." });
            CreateDialogueEvents("ApocalypseBossBrokenEggLong",
                new() { "The head that looked to the cosmos has been lowered." });
            CreateDialogueEvents("ApocalypseBossBrokenEggSmall",
                new() { "The mouth that devours everything has been shut." });
            CreateDialogueEvents("ApocalypseBossTryDirectDamage",
                new() { "[c:bR]The monster[c:] is not so easily tamed." });
            CreateDialogueEvents("ApocalypseBossReturnEgg",
                new() { "Your efforts prove futile." });
            CreateDialogueEvents("ApocalypseBossBendScales1",
                new() {
                    "The Beast is [c:bR]immune to mortal damage[c:].",
                    "Yet even it has its weaknesses."
                });
            CreateDialogueEvents("ApocalypseBossBendScales2",
                new() { "Gather your courage, and march onward." });
            CreateDialogueEvents("ApocalypseBossPrelude",
                new() { "[c:bSG]Into the twilight.[c:]" });

            CreateDialogueEvents("ApocalypseBossMouthPreAttack",
                new() { "The mouth that devours everything opens." });
            CreateDialogueEvents("ApocalypseBossMouthPostAttack",
                new() { "The Small Bird's beak whispered endlessly." });
            CreateDialogueEvents("ApocalypseBossMouthFailAttack",
                new() { "The beast's maw snaps shut on empty air." });

            CreateDialogueEvents("ApocalypseBossEyePreAttack",
                new() { "The Big Bird's eyes imprisoned light..." });
            CreateDialogueEvents("ApocalypseBossEyePostAttack",
                new() { "...and they burned like stars." });
            CreateDialogueEvents("ApocalypseBossEyeFailAttack",
                new() { "...but there was nothing to behold." });

            CreateDialogueEvents("ApocalypseBossArmsPreAttack",
                new() { "The Long Bird's arms concealed time..." });
            CreateDialogueEvents("ApocalypseBossArmsPostAttack",
                new() { "...yet sins continued day after day." });

            CreateDialogueEvents("ApocalypseBossCardsExhausted",
                new() { "The Beast senses your hunger, and grows more daring." });

            CreateDialogueEvents("ApocalypseBossFinalPhase",
                new() { "The Beast begins to grow desperate." });

            CreateDialogueEvents("ApocalypseBossReactive1",
                new() { "Your power angers the Beast." });
            CreateDialogueEvents("ApocalypseBossReactive2",
                new() { "The Beast grows more aggressive." });
            CreateDialogueEvents("ApocalypseBossReactive3",
                new() { "The Beast's strength grows beyond imagination..." });

            CreateDialogueEvents("ApocalypseBossReactiveSkin",
                new() { "The Beast's hide hardens under your creatures' claws." });

            CreateDialogueEvents("ApocalypseBossRecall",
                new() {
                    "The Beast and its ilk move quickly and frequently. Use these to reposition your creatures.",
                    "Returned creatures will have their cost changed based on how recently you played them.",
                    "Newly played creatures will cost [c:bR]2 Bones[c:] to play again.",
                    "I will reduce their recall cost over time, until they become [c:bR]free[c:] to replay." },
                new()
                {
                    new() { "Use these to reposition your creatures, for a cost." }
                });
            CreateDialogueEvents("ApocalypseBossExhausted",
                new() { "Why don't we continue a little longer?" });
            CreateDialogueEvents("ApocalypseBossBoneGain",
                new() {
                    "The beast is immune to mortal damage.",
                    "Excess injury will instead yield you up to [c:bR]3[c:] bones.",
                    "Don't worry about running dry, the ground you walk on is full of them."
                });
            CreateDialogueEvents("ApocalypseBossFinal",
                new() {
                    "Backed into a corner, the Beast releases one final burst of strength."
                });
            CreateDialogueEvents("ApocalypseBossFinalTargets",
                new() {
                    "When the Beast attacks a space marked in [c:bR]red[c:], it will deal twice the normal damage.",
                    "When the Beast attacks a space marked in [c:bSG]white[c:], it will deal half the normal damage, but regain vitality."
                });
        }
        private void Dialogue_JesterOfNihil()
        {
            CreateDialogueEvents("JesterOfNihilIntro", new() {
                "Watch them carefully.",
                "They're calling - no, praying for something."
                });
            CreateDialogueEvents("JesterOfNihilOutro", new() {
                "There was no way to know if they had gathered to become [c:gray]the Jester[c:],",
                "or if [c:gray]the Jester[c:] had come to resemble them."
                });
            CreateDialogueEvents("JesterOfNihilStory", new() {
                "[c:gray]The Jester[c:] retraced the steps of a path everyone would've taken.",
                "No matter what it did, the Jester always found itself at the end of that road."
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
                new () {
                    new() {
                        NewLine("We kept them separated for", Emotion.Neutral),
                        NewLine("this reason, manager.", Emotion.Neutral) },
                    new() { NewLine("A final trial for you, manager.", Emotion.Neutral) },
                    new() { NewLine("It has been quite the journey, yes?", Emotion.Neutral) }
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
            CreateDialogueEvents("SephirahBinahProspector", new()
            {
                NewLine("Deal with those beasts quickly.", Emotion.Neutral) },
                new() {
                    new() { NewLine("A small obstacle.", Emotion.Neutral) },
                    new() { NewLine("Let us begin.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahBinahAngler", new()
            {
                NewLine("Don't lose your appetite now.", Emotion.Surprise) },
                new() {
                    new() { NewLine("I grow tired of fish.", Emotion.Neutral) },
                    new() { NewLine("You must enjoy the stench.", Emotion.Laughter) }
            });
            CreateDialogueEvents("SephirahBinahTrapperTrader", new()
            {
                NewLine("Another two-faced deceiver.", Emotion.Laughter) },
                new() {
                    new() { NewLine("No one helps you without a price.", Emotion.Neutral) },
                    new() { NewLine("Be wary of where your foot falls.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahBinahLeshy", new()
            {
                NewLine("It seems we have reached the conclusion.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Are we doing this again?", Emotion.Neutral) },
                    new() { NewLine("I have fought stronger foes.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahBinahRoyal", new()
            {
                NewLine("What is this?", Emotion.Neutral) },
                new() {
                    new() { NewLine("A captain of a sunken vessel.", Emotion.Laughter) },
                    new() { NewLine("His crew has no loyalty.", Emotion.Laughter) }
            });
            CreateDialogueEvents("SephirahBinahApocalypse", new()
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
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("SephirahBinahChoice", new()
            {
                NewLine("Well look who it is.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Well look who it is.", Emotion.Neutral) },
                    new() { NewLine("Choose.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahBinahDrawn", new()
            {
                NewLine("Be calm.", Emotion.Laughter) },
                new() {
                    new() { NewLine("Miss me?", Emotion.Laughter) },
                    new() { NewLine("Countless stars light the sky today.", Emotion.Neutral) },
                    new() { NewLine("Your outlook is what decides things.", Emotion.Neutral) },
                    new() { NewLine("Be calm.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahBinahGivenSigil", new()
            {
                NewLine("I am curious to see", Emotion.Neutral),
                NewLine("what memories you yielded.", Emotion.Surprise)},
                new() {
                    new() { NewLine("Excellent.", Emotion.Surprise) },
                    new() { NewLine("You have done well.", Emotion.Laughter) },
                    new() { NewLine("This power is incomplete.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahBinahHurt", new()
            {
                NewLine("...", Emotion.Anger) },
                new() {
                    new() { NewLine("...", Emotion.Anger) },
                    new() { NewLine("", Emotion.Anger) }
            });
            CreateDialogueEvents("SephirahBinahPlayed", new()
            {
                NewLine("Let us begin.", Emotion.Neutral) },
                new() {
                    new() { NewLine("More bodies to clean.", Emotion.Neutral) },
                    new() { NewLine("Let us begin.", Emotion.Neutral) },
                    new() { NewLine("The closer to death, the freer you are.", Emotion.Laughter) }
            });
            CreateDialogueEvents("SephirahBinahSacrificed", new()
            {
                NewLine("...", Emotion.Anger) },
                new() {
                    new() { NewLine("...", Emotion.Anger) },
                    new() { NewLine("", Emotion.Anger) }
            });
            CreateDialogueEvents("SephirahBinahSelectableBad", new()
            {
                NewLine("Did you think you could avoid it?", Emotion.Neutral) },
                new() {
                    new() { NewLine("Did you think you could avoid it?", Emotion.Neutral) },
                    new() { NewLine("This is nearing the end.", Emotion.Neutral) },
                    new() { NewLine("Your breath is fading.", Emotion.Surprise) },
                    new() { NewLine("No one will blame you.", Emotion.Laughter) }
            });
            CreateDialogueEvents("SephirahBinahSelectableGood", new()
            {
                NewLine("Excellent.", Emotion.Laughter) },
                new() {
                    new() { NewLine("Excellent.", Emotion.Laughter) },
                    new() { NewLine("Inching closer to wholeness.", Emotion.Neutral) },
                    new() { NewLine("I leave it to your discretion.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahBinahTrial", new()
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
            CreateDialogueEvents("SephirahChesedProspector", new()
            {
                NewLine("So many animals~", Emotion.Laughter) },
                new() {
                    new() { NewLine("So much dust.", Emotion.Neutral) },
                    new() { NewLine("Do we have to fight?", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahChesedAngler", new()
            {
                NewLine("That's an impressive hook there~.", Emotion.Laughter) },
                new() {
                    new() { NewLine("Are you feeling hungry?", Emotion.Laughter) },
                    new() { NewLine("I'm used to the stench of decay by now.", Emotion.Laughter) }
            });
            CreateDialogueEvents("SephirahChesedTrapperTrader", new()
            {
                NewLine("What an odd fellow.", Emotion.Neutral) },
                new() {
                    new() { NewLine("What tricks are up their sleeve?", Emotion.Neutral) },
                    new() {
                        NewLine("Nothing like a cup of coffee", Emotion.Laughter),
                        NewLine("to keep you warm~", Emotion.Laughter)}
            });
            CreateDialogueEvents("SephirahChesedLeshy", new()
            {
                NewLine("We've come so far...", Emotion.Neutral),
                NewLine("just a little more now.", Emotion.Laughter) },
                new() {
                    new() { NewLine("Are you prepared manager?", Emotion.Laughter) },
                    new() { NewLine("We can't hesitate now.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahChesedRoyal", new()
            {
                NewLine("A pirate? How fun~", Emotion.Laughter) },
                new() {
                    new() { NewLine("Watch your head now.", Emotion.Laughter) }
            });
            CreateDialogueEvents("SephirahChesedApocalypse", new()
            {
                NewLine("This one...", Emotion.Neutral),
                NewLine("I hope you're prepared.", Emotion.Neutral) },
                new() {
                    new() { NewLine("I worry you have a death wish.", Emotion.Neutral) },
                    new() { NewLine("Are we really doing this?", Emotion.Neutral) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("SephirahChesedChoice", new() {
                NewLine("Hey~ manager.", Emotion.Laughter),
                NewLine("Off on a nice stroll?", Emotion.Laughter) },
                new() {
                    new() { NewLine("Hey~ manager.", Emotion.Laughter) },
                    new() { NewLine("Off on a nice stroll?", Emotion.Laughter) },
                    new() { NewLine("Who will it be this time~?", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahChesedDrawn", new() {
                NewLine("Nothing like a fresh cup of coffee to start your day.", Emotion.Laughter) },
                new() {
                    new() { NewLine("I'm sure we can do this well.", Emotion.Laughter) },
                    new() { NewLine("Let's try our best, yeah?", Emotion.Laughter) },
                    new() { NewLine("Don't overwork yourself~", Emotion.Laughter) }
                });
            CreateDialogueEvents("SephirahChesedGivenSigil", new() {
                NewLine("Hey, thanks~", Emotion.Laughter) },
                new() {
                    new() { NewLine("That was smooth~", Emotion.Laughter) },
                    new() { NewLine("Hey thanks~", Emotion.Laughter) },
                    new() {
                        NewLine("Guess I gotta work harder", Emotion.Surprise),
                        NewLine("in their stead, huh?", Emotion.Surprise) },
                    new() { NewLine("How about a nice cup of coffee?", Emotion.Laughter) },
                });
            CreateDialogueEvents("SephirahChesedHurt", new() {
                NewLine("Ah!", Emotion.Surprise) },
                new() {
                    new() { NewLine("Ah!", Emotion.Surprise) },
                    new() { NewLine("Hm.", Emotion.Quiet) },
                    new() { NewLine("Tch.", Emotion.Curious) }
            });
            CreateDialogueEvents("SephirahChesedPlayed", new() {
                NewLine("I don't want anyone to die here.", Emotion.Surprise) },
                new() {
                    new() { NewLine("I don't want anyone to die here.", Emotion.Surprise) },
                    new() { NewLine("If there's a meaning to this fight...", Emotion.Neutral) },
                    new() { NewLine("No choice but to stand our ground.", Emotion.Curious) },
                    new() { NewLine("All this effort wasted on fighting.", Emotion.Surprise) }
            });
            CreateDialogueEvents("SephirahChesedSacrificed", new() {
                NewLine("I guess this is as far as I can go...", Emotion.Surprise) },
                new() {
                    new() { NewLine("Ah...", Emotion.Surprise) },
                    new() { NewLine("Heh...", Emotion.Surprise) },
                    new() { NewLine("See you in Hell.", Emotion.Anger) }
            });
            CreateDialogueEvents("SephirahChesedSelectableBad", new() {
                NewLine("Hmm...", Emotion.Neutral) },
                new() {
                    new() { NewLine("This again?", Emotion.Neutral) },
                    new() {
                        NewLine("We can lose everything", Emotion.Curious),
                        NewLine("if we hesitate.", Emotion.Curious) },
                    new() { NewLine("I'd hate for us to suffer even more.", Emotion.Surprise) }
            });
            CreateDialogueEvents("SephirahChesedSelectableGood", new()
            {
                NewLine("This looks promising~", Emotion.Laughter) },
                new() {
                    new() { NewLine("Oh, this again~", Emotion.Laughter) },
                    new() { NewLine("I wouldn't mind a nice of cup of coffee now.", Emotion.Laughter) },
                    new() { NewLine("Who will it be~?", Emotion.Laughter) },
                    new() { NewLine("This looks promising~", Emotion.Laughter) }
            });
            CreateDialogueEvents("SephirahChesedTrial", new() {
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
            CreateDialogueEvents("SephirahGeburaProspector", new()
            {
                NewLine("Who's this geezer?", Emotion.Neutral) },
                new() {
                    new() { NewLine("This guy...", Emotion.Anger) },
                    new() { NewLine("What a pain.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahGeburaAngler", new()
            {
                NewLine("Let's make quick work of him.", Emotion.Neutral) },
                new() {
                    new() { NewLine("This stench takes me back.", Emotion.Neutral) },
                    new() { NewLine("Watch that hook there.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahGeburaTrapperTrader", new()
            {
                NewLine("Keep on your toes now.", Emotion.Neutral) },
                new() {
                    new() { NewLine("These traps are annoying.", Emotion.Anger) },
                    new() { NewLine("Clear a path and strike cleanly.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahGeburaLeshy", new()
            {
                NewLine("Nothing left but to continue forward.", Emotion.Neutral) },
                new() {
                    new() { NewLine("We've got this.", Emotion.Neutral) },
                    new() { NewLine("One more enemy left.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahGeburaRoyal", new()
            {
                NewLine("Not the strangest thing I've seen.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Be quick and avoid those cannons.", Emotion.Neutral) },
                    new() { NewLine("His goons are tougher than they look.", Emotion.Anger) }
            });
            CreateDialogueEvents("SephirahGeburaApocalypse", new()
            {
                NewLine("Why have you come here?", Emotion.Neutral),
                NewLine("You trying to die?", Emotion.Anger) },
                new() {
                    new() { NewLine("Let me handle this.", Emotion.Neutral) },
                    new() { NewLine("These stupid birds...", Emotion.Anger) },
                    new() { NewLine("Don't chicken out now.", Emotion.Neutral) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("SephirahGeburaChoice", new() {
                NewLine("Manager.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Manager.", Emotion.Neutral) },
                    new() { NewLine("Hey manager.", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahGeburaDrawn", new() {
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
            CreateDialogueEvents("SephirahGeburaGivenSigil", new() {
                NewLine("I'm starting to get accustomed", Emotion.Neutral),
                NewLine("to this new body.", Emotion.Neutral) },
                new() {
                    new() {
                        NewLine("I'm starting to get accustomed", Emotion.Neutral),
                        NewLine("to this new body.", Emotion.Neutral) },
                    new() { NewLine("I won't waste this strength.", Emotion.Neutral) },
                    new() { NewLine("Thanks.", Emotion.Laughter) }
            });
            CreateDialogueEvents("SephirahGeburaHurt", new() {
                NewLine("Kch.", Emotion.Anger) },
                new() {
                    new() { NewLine("Kch.", Emotion.Anger) },
                    new() { NewLine("Bastard.", Emotion.Anger) },
                    new() { NewLine("That all?", Emotion.Anger) },
                    new() { NewLine("I wasn't so feeble before...", Emotion.Anger) }
            });
            CreateDialogueEvents("SephirahGeburaPlayed", new() {
                NewLine("Let's beat these guys into submission.", Emotion.Anger) },
                new() {
                    new() { NewLine("Let's beat these guys into submission.", Emotion.Anger) },
                    new() { NewLine("Let's do this.", Emotion.Neutral) },
                    new() { NewLine("We mustn't falter just yet.", Emotion.Neutral) },
                    new() { NewLine("We'll talk after everything's over.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahGeburaSacrificed", new() {
                NewLine("Bastard.", Emotion.Anger) },
                new() {
                    new() { NewLine("Bastard.", Emotion.Anger) },
                    new() { NewLine("Damn it.", Emotion.Anger) },
                    new() { NewLine("Don't let it be in vain, yeah?", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahGeburaSelectableBad", new() {
                NewLine("Don't like this...", Emotion.Neutral) },
                new() {
                    new() { NewLine("Wayward wrath will only ruin yourself.", Emotion.Neutral) },
                    new() { NewLine("You know this isn't just a silly game, right?", Emotion.Curious) },
                    new() { NewLine("Mind where you point your blade.", Emotion.Neutral) },
                    new() { NewLine("Not a chance.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahGeburaSelectableGood", new() {
                NewLine("This looks like a good opportunity.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Guess I'll cop a nap.", Emotion.Neutral) },
                    new() { NewLine("This looks like a good opportunity.", Emotion.Laughter) },
                    new() { NewLine("Another opportunity, huh?", Emotion.Neutral) },
                    new() { NewLine("Don't take it for granted.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahGeburaTrial", new() {
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
            CreateDialogueEvents("SephirahHodProspector", new()
            {
                NewLine("Watch out for that pickaxe.", Emotion.Neutral) },
                new() {
                    new() { NewLine("We've prepared for this.", Emotion.Neutral) },
                    new() { NewLine("I'll follow your lead.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahHodAngler", new()
            {
                NewLine("Urk, the smell...", Emotion.Neutral) },
                new() {
                    new() { NewLine("Let's get out of here quickly...", Emotion.Neutral) },
                    new() { NewLine("Don't knock over those buckets.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahHodTrapperTrader", new()
            {
                NewLine("A betrayal...", Emotion.Curious) },
                new() {
                    new() { NewLine("I'll trust you, manager.", Emotion.Neutral) },
                    new() { NewLine("So many pelts...", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahHodLeshy", new()
            {
                NewLine("The moon's so large...", Emotion.Curious) },
                new() {
                    new() { NewLine("We can do this.", Emotion.Anger) },
                    new() { NewLine("We're a lot stronger than before.", Emotion.Anger) }
            });
            CreateDialogueEvents("SephirahHodRoyal", new()
            {
                NewLine("A pirate...skeleton?", Emotion.Neutral) },
                new() {
                    new() { NewLine("Those cannons hurt a lot...", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahHodApocalypse", new()
            {
                NewLine("That monster...", Emotion.Neutral),
                NewLine("Remember our training, please.", Emotion.Curious) },
                new() {
                    new() { NewLine("Keep calm...keep calm...", Emotion.Curious) },
                    new() { NewLine("It's tough, but not immortal.", Emotion.Anger) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("SephirahHodChoice", new() {
                NewLine("Oh! Um...", Emotion.Quiet),
                NewLine("Hello manager.", Emotion.Laughter) },
                new() {
                    new() { NewLine("Hello manager.", Emotion.Laughter) },
                    new() { NewLine("I won't be a hindrance!", Emotion.Anger) },
                    new() { NewLine("Who do you choose?", Emotion.Neutral) }
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
                    new() { NewLine("I won't let you down!", Emotion.Anger) },
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
            #endregion
        }
        private void Dialogue_Hokma()
        {
            #region Boss Dialogue
            CreateDialogueEvents("SephirahHokmaProspector", new()
            {
                NewLine("This is no different from anything else.", Emotion.Neutral) },
                new() {
                    new() { NewLine("We've done this before.", Emotion.Neutral) },
                    new() { NewLine("We must move onward.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahHokmaAngler", new()
            {
                NewLine("We can endlessly question", Emotion.Neutral),
                NewLine("why we must endure this endeavour.", Emotion.Neutral) },
                new() {
                    new() {
                        NewLine("Take all possibilities", Emotion.Neutral),
                        NewLine("into consideration.", Emotion.Neutral) },
                    new() { NewLine("Let's work quickly now.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahHokmaTrapperTrader", new()
            {
                NewLine("His madness can be used to our advantage.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Bend the rules to your favour.", Emotion.Neutral) },
                    new() { NewLine("Don't let the snow blind you.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahHokmaLeshy", new()
            {
                NewLine("We are all bound by something greater.", Emotion.Neutral),
                NewLine("He is no exception.", Emotion.Neutral) },
                new() {
                    new() { NewLine("If we focus, victory is assured.", Emotion.Neutral) },
                    new() { NewLine("Everything will happen as it should.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahHokmaRoyal", new()
            {
                NewLine("...I admit I am caught off-guard.", Emotion.Neutral) },
                new() {
                    new() { NewLine("In the end, this is all a pointless errand.", Emotion.Neutral) },
                    new() {
                        NewLine("The cannons are slow.", Emotion.Neutral),
                        NewLine("We can easily avoid them.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahHokmaApocalypse", new()
            {
                NewLine("Every Goliath has its David.", Emotion.Neutral),
                NewLine("Have faith in yourself, and in us.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Have faith in yourself and us.", Emotion.Curious) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("SephirahHokmaChoice", new()
            {
                NewLine("Greetings manager.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Greetings manager.", Emotion.Neutral) },
                    new() { NewLine("There is work to be done.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahHokmaDrawn", new()
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
            CreateDialogueEvents("SephirahHokmaGivenSigil", new()
            {
                NewLine("Thank you.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Thank you.", Emotion.Neutral) },
                    new() { NewLine("Thank you.", Emotion.Laughter) },
                    new() { NewLine("This will help greatly.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahHokmaHurt", new()
            {
                NewLine("...", Emotion.Anger) },
                new() {
                    new() { NewLine("...", Emotion.Anger) },
                    new() { NewLine("", Emotion.Anger) }
            });
            CreateDialogueEvents("SephirahHokmaPlayed", new()
            {
                NewLine("Let us begin the day.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Overconfidence is the greatest enemy.", Emotion.Neutral) },
                    new() { NewLine("Let us begin the day.", Emotion.Neutral) },
                    new() { NewLine("Let's hurry to finish this work.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahHokmaSacrificed", new()
            {
                NewLine("A pointless errand.", Emotion.Anger) },
                new() {
                    new() { NewLine("A pointless errand.", Emotion.Anger) },
                    new() { NewLine("", Emotion.Neutral) },
                    new() { NewLine("", Emotion.Anger) }
            });
            CreateDialogueEvents("SephirahHokmaSelectableBad", new()
            {
                NewLine("A pointless errand.", Emotion.Anger) },
                new() {
                    new() { NewLine("A pointless errand.", Emotion.Anger) },
                    new() { NewLine("", Emotion.Neutral) },
                    new() { NewLine("", Emotion.Anger) }
            });
            CreateDialogueEvents("SephirahHokmaSelectableGood", new()
            {
                NewLine("An opportunity presents itself.", Emotion.Neutral) },
                new() {
                    new() { NewLine("An opportunity presents itself.", Emotion.Neutral) },
                    new() { NewLine("Be prudent about this.", Emotion.Neutral) },
                    new() { NewLine("I have faith in your decision.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahHokmaTrial", new()
            {
                NewLine("Boundless pride and ambition", Emotion.Neutral),
                NewLine("will bring about a bitter end...", Emotion.Neutral) },
                new() {
                    new() { NewLine("Do not be hasty.", Emotion.Neutral) },
                    new() { NewLine("There is no halting now.", Emotion.Neutral) },
                    new() { NewLine("An endless series of trials.", Emotion.Neutral) }
            });
            #endregion
        }
        private void Dialogue_Malkuth()
        {
            #region Boss Dialogue
            CreateDialogueEvents("SephirahMalkuthProspector", new()
            {
                NewLine("Heads up manager!", Emotion.Neutral) },
                new() {
                    new() { NewLine("We've got this!", Emotion.Anger) },
                    new() { NewLine("Let's stay sharp, now!", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahMalkuthAngler", new()
            {
                NewLine("The familiar smell of death.", Emotion.Quiet) },
                new() {
                    new() { NewLine("Winning's just a matter of time!", Emotion.Anger) },
                    new() { NewLine("Watch out for those bait buckets!", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahMalkuthTrapperTrader", new()
            {
                NewLine("Be on the look-out!", Emotion.Neutral) },
                new() {
                    new() { NewLine("Watch out for traps!", Emotion.Neutral) },
                    new() { NewLine("We need to be smart about this.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahMalkuthLeshy", new()
            {
                NewLine("This guy looks tough.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Take a deep breath, and focus.", Emotion.Neutral) },
                    new() {
                        NewLine("We've made this far.", Emotion.Neutral),
                        NewLine("We can't lose now!", Emotion.Anger) }
            });
            CreateDialogueEvents("SephirahMalkuthRoyal", new()
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
            CreateDialogueEvents("SephirahMalkuthApocalypse", new()
            {
                NewLine("This thing is huge...", Emotion.Quiet),
                NewLine("Uh, don't worry!", Emotion.Neutral),
                NewLine("We can do it!", Emotion.Laughter) },
                new() {
                    new() { NewLine("We just need to outlast it...", Emotion.Quiet) },
                    new() { NewLine("Manage your resources carefully!", Emotion.Anger) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("SephirahMalkuthChoice", new() {
                NewLine("Nice to meet you, manager!", Emotion.Neutral) },
                new() {
                    new() { NewLine("Hello again!", Emotion.Neutral) },
                    new() { NewLine("I'm prepared to help!", Emotion.Neutral) },
                    new() { NewLine("I promise to succeed!", Emotion.Anger) }
                });
            CreateDialogueEvents("SephirahMalkuthDrawn", new() {
                NewLine("Greetings Manager!", Emotion.Laughter) },
                new() {
                    new() { NewLine("Greetings Manager!", Emotion.Laughter) },
                    new() { NewLine("I promise to succeed!", Emotion.Anger) },
                    new() { NewLine("Trust me and follow my lead!", Emotion.Neutral) },
                    new() { NewLine("Let's do this!", Emotion.Anger) }
                });
            CreateDialogueEvents("SephirahMalkuthGivenSigil", new() {
                NewLine("I'm fully prepared now!", Emotion.Anger) },
                new() {
                    new() { NewLine("I'm fully prepared now!", Emotion.Anger) },
                    new() { NewLine("Winning is just a matter of time!", Emotion.Anger) },
                    new() { NewLine("I'll do my best within my ability!", Emotion.Neutral) },
                    new() { NewLine("Thank you manager!", Emotion.Laughter) }
                });
            CreateDialogueEvents("SephirahMalkuthHurt", new() {
                NewLine("Don't panic.", Emotion.Quiet) },
                new() {
                    new() { NewLine("Don't panic.", Emotion.Quiet) },
                    new() { NewLine("Stay calm.", Emotion.Quiet) },
                    new() { NewLine("I'll fight harder, don't worry!", Emotion.Anger) },
                    new() { NewLine("...", Emotion.Quiet) },
                    new() { NewLine("...", Emotion.Quiet) }
                });
            CreateDialogueEvents("SephirahMalkuthPlayed", new() {
                NewLine("I've got a good feeling about this!", Emotion.Anger) },
                new() {
                    new() { NewLine("I've got a good feeling about this!", Emotion.Anger) },
                    new() { NewLine("Trust me and follow my lead!", Emotion.Anger) },
                    new() { NewLine("Let's stay sharp, shall we?", Emotion.Neutral) },
                    new() { NewLine("Focus on the path forward!", Emotion.Anger) }
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
                    new() { NewLine("You've got this manager!", Emotion.Neutral) },
                    new() { NewLine("Nothing we haven't solved before!", Emotion.Neutral) }
                });
            #endregion
        }
        private void Dialogue_Netzach()
        {
            #region Boss Dialogue
            CreateDialogueEvents("SephirahNetzachProspector", new()
            {
                NewLine("That banging's annoying...", Emotion.Neutral) },
                new() {
                    new() { NewLine("Again?", Emotion.Neutral) },
                    new() { NewLine("Not a fan of that pickaxe.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahNetzachAngler", new()
            {
                NewLine("So many buckets of fish...", Emotion.Neutral) },
                new() {
                    new() { NewLine("The smell's making me sick.", Emotion.Neutral) },
                    new() { NewLine("Can't we take the long way around?", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahNetzachTrapperTrader", new()
            {
                NewLine("Everyone here's crazy...", Emotion.Neutral) },
                new() {
                    new() { NewLine("At least those traps are obvious.", Emotion.Laughter) },
                    new() {
                        NewLine("Just want to lie down in the snow", Emotion.Neutral),
                        NewLine("and go to sleep.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahNetzachLeshy", new()
            {
                NewLine("This is the final boss?", Emotion.Neutral) },
                new() {
                    new() { NewLine("Since we've come this far, may as well.", Emotion.Neutral) },
                    new() { NewLine("Let's finish this.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahNetzachRoyal", new()
            {
                NewLine("...am I hallucinating?", Emotion.Neutral) },
                new() {
                    new() { NewLine("I guess I've seen weirder things.", Emotion.Neutral) },
                    new() { NewLine("Those cannons are pretty slow, huh?", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahNetzachApocalypse", new()
            {
                NewLine("What is that thing?", Emotion.Neutral) },
                new() {
                    new() { NewLine("I don't need this stress...", Emotion.Neutral) },
                    new() { NewLine("Why are we back here?", Emotion.Neutral) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("SephirahNetzachChoice", new() {
                NewLine("Hey.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Hey.", Emotion.Neutral) },
                    new() { NewLine("Pick someone else?", Emotion.Neutral) }
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
                    new() { NewLine("I'll try not to disappoint.", Emotion.Laughter) },
                    new() { NewLine("Thanks, I guess.", Emotion.Quiet) }
                });
            CreateDialogueEvents("SephirahNetzachHurt", new() {
                NewLine("Tch.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Tch.", Emotion.Anger) },
                    new() { NewLine("Tch.", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahNetzachPlayed", new() {
                NewLine("I just wanna nap in the back...", Emotion.Neutral) },
                new() {
                    new() { NewLine("How did I end up doing things like this?", Emotion.Neutral) },
                    new() { NewLine("I just wanna nap in the back...", Emotion.Neutral) },
                    new() { NewLine("Let's do this...", Emotion.Neutral) },
                    new() { NewLine("Let's get this over with.", Emotion.Neutral) }
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
                    new() { NewLine("Again?", Emotion.Neutral) },
                    new() { NewLine("Is leaving an option?", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahNetzachSelectableGood", new() {
                NewLine("Choose whoever.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Choose whoever.", Emotion.Neutral) },
                    new() { NewLine("...", Emotion.Neutral) },
                    new() { NewLine("Can I opt out?", Emotion.Neutral) },
                    new() { NewLine("At your discretion, manager.", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahNetzachTrial", new() {
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
            CreateDialogueEvents("SephirahTipherethAProspector", new()
            {
                NewLine("Let's deal with this guy quickly.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Should've stayed out of the way.", Emotion.Neutral) },
                    new() { NewLine("You still haven't beat him?", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahTipherethAAngler", new()
            {
                NewLine("It smells like the Backstreets here.", Emotion.Neutral) },
                new() {
                    new() { NewLine("At least his actions are predictable.", Emotion.Neutral) },
                    new() { NewLine("Quickly now.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahTipherethATrapperTrader", new()
            {
                NewLine("Guess we're next on the chopping block.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Those knives look sharp. Careful.", Emotion.Neutral) },
                    new() { NewLine("Hold onto those pelts now.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahTipherethALeshy", new()
            {
                NewLine("We've got this manager.", Emotion.Laughter) },
                new() {
                    new() { NewLine("Don't forget, you have us to help.", Emotion.Surprise) },
                    new() { NewLine("One more foe to beat.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahTipherethARoyal", new()
            {
                NewLine("Seriously?", Emotion.Neutral) },
                new() {
                    new() { NewLine("Never was that into pirates.", Emotion.Neutral) },
                    new() { NewLine("This guy can't aim at all.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahYesodApocalypse", new()
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
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("SephirahTipherethAChoice", new() {
                NewLine("Hurry and pick one of us.", Emotion.Anger) },
                new() {
                    new() { NewLine("Hurry and pick one of us.", Emotion.Anger) },
                    new() { NewLine("Don't keep up waiting.", Emotion.Neutral) }
                });
            CreateDialogueEvents("SephirahTipherethADrawn", new() {
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
            CreateDialogueEvents("SephirahTipherethAGivenSigil", new() {
                NewLine("Think I can't do this myself?", Emotion.Anger) },
                new() {
                    new() { NewLine("Think I can't do this myself?", Emotion.Anger) },
                    new() { NewLine("Thanks.", Emotion.Neutral) },
                    new() { NewLine("Not bad.", Emotion.Neutral) },
                    new() { NewLine("This better be useful.", Emotion.Anger) }
            });
            CreateDialogueEvents("SephirahTipherethAHurt", new() {
                NewLine("Ah!", Emotion.Quiet) },
                new() {
                    new() { NewLine("Ah!", Emotion.Quiet) },
                    new() { NewLine("I was too slow...", Emotion.Quiet) }
            });
            CreateDialogueEvents("SephirahTipherethAPlayed", new() {
                NewLine("Push forward!", Emotion.Anger) },
                new() {
                    new() { NewLine("Push forward!", Emotion.Anger) },
                    new() { NewLine("Don't get cocky.", Emotion.Neutral) },
                    new() { NewLine("We can do this.", Emotion.Laughter) }
            });
            CreateDialogueEvents("SephirahTipherethASacrificed", new() {
                NewLine("...", Emotion.Anger) },
                new() {
                    new() { NewLine("...", Emotion.Anger) },
                    new() { NewLine("I can still fight!", Emotion.Anger) },
                    new() { NewLine("...Sorry", Emotion.Quiet) }
            });
            CreateDialogueEvents("SephirahTipherethASelectableBad", new() {
                NewLine("Why'd you bring us here?", Emotion.Anger) },
                new() {
                    new() { NewLine("Why'd you bring us here?", Emotion.Anger) },
                    new() { NewLine("No.", Emotion.Anger) },
                    new() { NewLine("Don't you dare.", Emotion.Anger) }
            });
            CreateDialogueEvents("SephirahTipherethASelectableGood", new() {
                NewLine("A respite?", Emotion.Neutral) },
                new() {
                    new() { NewLine("Decide for yourself.", Emotion.Neutral) },
                    new() { NewLine("Make good use of this.", Emotion.Neutral) },
                    new() { NewLine("Choose someone that needs it.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahTipherethATrial", new() {
                NewLine("A trial?", Emotion.Neutral) },
                new() {
                    new() { NewLine("We've gone through worse.", Emotion.Neutral) },
                    new() { NewLine("I can handle it.", Emotion.Neutral) },
                    new() { NewLine("I'll win this for sure.", Emotion.Laughter) }
            });
            #endregion

            #region Tiphereth B
            CreateDialogueEvents("SephirahTipherethBDrawn", new() {
                NewLine("Hello manager.", Emotion.Laughter) },
                new() {
                    new() { NewLine("Hello manager.", Emotion.Neutral) },
                    new() { NewLine("Hello manager.", Emotion.Laughter) },
                    new() { NewLine("You'll do fine, like always.", Emotion.Laughter) }
            });
            CreateDialogueEvents("SephirahTipherethBPlayed", new() {
                NewLine("Here we go.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Here we go.", Emotion.Neutral) },
                    new() { NewLine("What a strange place...", Emotion.Neutral) },
                    new() { NewLine("Time to work.", Emotion.Neutral) },
                    new() { NewLine("Let's do our best.", Emotion.Laughter) }
            });
            CreateDialogueEvents("SephirahTipherethBSacrificed", new() {
                NewLine("...", Emotion.Quiet) },
                new() {
                    new() { NewLine("...", Emotion.Quiet) }
            });
            #endregion
        }
        private void Dialogue_Yesod()
        {
            #region Boss Dialogue
            CreateDialogueEvents("SephirahYesodProspector", new()
            {
                NewLine("Don't let his age lull you into", Emotion.Neutral),
                NewLine("a false sense of security.", Emotion.Neutral) },
                new() {
                    new() { NewLine("That pickaxe is sharp. Stay vigilant.", Emotion.Neutral) },
                    new() { NewLine("Let's not waste any time.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahYesodAngler", new()
            {
                NewLine("Don't let the smell dull your mind.", Emotion.Neutral) },
                new() {
                    new() {
                        NewLine("His hook always seems to hit its mark.", Emotion.Neutral),
                        NewLine("I'm sure you know how to deal with it.", Emotion.Neutral) },
                    new() { NewLine("This isn't particularly pleasant...", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahYesodTrapperTrader", new()
            {
                NewLine("Come up with a plan, then execute it.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Don't let the cold cloud your senses, manager.", Emotion.Neutral) },
                    new() { NewLine("Hasty actions will bring more harm than good.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahYesodLeshy", new()
            {
                NewLine("We must make sound judgements", Emotion.Neutral),
                NewLine("before rushing forward.", Emotion.Neutral) },
                new() {
                    new() {
                        NewLine("Use what we've learned until now.", Emotion.Neutral),
                        NewLine("to take him down.", Emotion.Neutral) },
                    new() { NewLine("Consider this your final test.", Emotion.Neutral) }
            });
            CreateDialogueEvents("SephirahYesodRoyal", new()
            {
                NewLine("A skeleton? That's new.", Emotion.Neutral) },
                new() {
                    new() { NewLine("His crew seems persuadable.", Emotion.Neutral) },
                    new() { NewLine("Can't say I'm impressed.", Emotion.Neutral) }
            });
            #endregion

            #region Normal Dialogue
            CreateDialogueEvents("SephirahYesodChoice", new() {
                NewLine("Hello manager.", Emotion.Neutral) },
                new() {
                    new() { NewLine("Hello manager.", Emotion.Neutral) }
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
            #endregion
        }
        private void Dialogue_WhiteNight()
        {
            CreateDialogueEvents("WhiteNightEventIntro", new() {
                "[c:bR]The time has come. A new world will come.[c:]",
                "[c:bR]I am death and life. Darkness and light.[c:]" });
            CreateDialogueEvents("WhiteNightApostleHeretic", new() {
                "[c:bR]Have I not chosen you, the Twelve? Yet one of you is a devil.[c:]"
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
