using DiskCardGame;
using InscryptionAPI.Dialogue;
using System.Collections;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Core
{
    public static class DialogueEventsManager // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static Dictionary<string, List<CustomLine>> EventNames => new()
        {
            { "AbnormalChoiceNodeIntro", new() {
                "You enter a clearing surrounded by dark, twisting trees.",
                "Their branches claw at the leering moon as you approach the well at the clearing's centre.",
                "Unearthly voices arise from the its inky depths, pleading, screaming, wishing and wanting.",
                "There are things in the well, waiting to be drawn up. Powerful, mysterious,",
                "[c:bR]abnormal[c:]." }},
            #region Apocalypse Bird
            { "ApocalypseBirdIntro", new() { "Let me tell you a story. The story of the [c:bR]Black Forest[c:]." }},
            { "ApocalypseBirdOutro", new() {
                "The three birds, [c:bR]now one[c:] looked around for [c:bR]the Beast[c:]. But there was nothing.",
                "No creatures. No beast. No sun or moon or stars. Only a single bird, alone in an empty forest." }},
            { "ApocalypseBirdStory1", new() {
                "Once upon a time, [c:bR]three birds[c:] lived happily in the lush Forest with their fellow animals.",
                "One day a stranger arrived at the Forest.He proclaimed that the Forest would soon be ensared in a bitter conflict.",
                "One that would only end when everything was devoured by a[c: bR]terrible Beast[c:].",
                "The birds, frightened by this doomsay, sought to prevent conflict from ever breaking out." }},
            { "ApocalypseBirdStory2", new() {
                "Fights began to break out. More and more creatures left the Forest, no matter how hard the birds worked.",
                "They decided to combine their powers. This way, they could better protect their home.",
                "This way they could better return the peace." }},
            { "ApocalypseBirdStory3", new() { "Darkness fell upon the forest. Mayhem ran amok as creatures screamed in terror at the towering bird." }},
            { "ApocalypseBirdStory4", new() { "Someone yelled out [c:bB]'It's the Beast! A big, scary monster lives in the Black Forest!'[c:]" }},
            { "ApocalypseBirdStoryBig", new() { "With his many eyes, [c:bG]Big Bird[c:] kept constant watch over the entire Forest." }},
            { "ApocalypseBirdStoryLong", new() { "[c:bSG]Long Bird[c:] weighed the sins of all creatures in the forest with his scales." }},
            { "ApocalypseBirdStorySmall", new() { "[c:bR]Small Bird[c:] punished wrongdoers with his beak." }},
            #endregion
            { "ArmyInBlackTransform", new() { "The human heart is black, and must be cleaned." }},
            { "Bloodbath1", new() { "A hand rises from the scarlet pool." }},
            { "Bloodbath2", new() { "Another pale hand emerges." }},
            { "Bloodbath3", new() { "A third hand reaches out, as if asking for help." }},
            { "CENSOREDKilledCard", new() { "What have you done to my beast?" }},
            { "DerFreischutzSeventhBullet", new() {
                "The Devil proposed a childist contract.",
                "The seventh bullet would pierce the heart of his most beloved.",
                "On hearing this, the hunter sought and shot everyone he loved." }},
            { "ExpressHellTrainWipe", new() { "The train sounds its mighty horn." }},
            { "GiantTreeSapExplode", new() { "A strange gurgling sound comes from your beast's stomach." }},
            #region Jester of Nihil
            { "JesterOfNihilIntro", new() {
                "Watch them carefully.",
                "They're calling - no, praying for something." }},
            { "JesterOfNihilOutro", new() {
                "There was no way to know if they had gathered to become [c:gray]The jester[c:],",
                "or if [c:gray]The jester[c:] had come to resemble them." }},
            { "JesterOfNihilStory", new() {
                "[c:gray]The jester[c:] retraced the steps of a path everyone would've taken.",
                "No matter what it did, the jester always found itself at the end of that road." }},
            #endregion
            { "KingOfGreedTransform", new() { "Desire unfulfilled, the koi continues for Eden." }},
            { "KnightOfDespairTransform", new() { "Having failed to protect again, the knight fell once more to despair." }},
            #region Lying Adult
            { "LyingAdultIntro", new() {
                "Long ago, there was a city made of shining emerald.",
                "Ruled by the princess [c:bR]Ozma[c:], it was home to many otherworldly splendors." }},
            { "LyingAdultIntro2", new() {
                "One day, an adult named [c:g1]Oz[c:] arrived to the Emerald City.",
                "Looking upon the brilliant city, she came up with a brilliant plan."}},
            { "LyingAdultIntro3", new() { "Soon, [c:g1]Oz[c:] came to be known as a wizard by the people." }},
            { "LyingAdultIntro4", new() { "Nothing is as deceptive as evident truth." }},
            { "LyingAdultOutro", new() { "Unable to ever go back home, they began an endless journey." }},
            { "LyingAdultOzma", new() { "[c:g1]I’ll make your wishes come true. What do you wish for?[c:]" }},
            { "LyingAdultOzma2", new() { "Poor [c:bR]Ozma[c:] didn’t know that lying adults don’t approach others without reason." }},
            { "LyingAdultRoadHome", new() { "[c:g1]Last of all, road that is lost. I will send you home.[c:]" }},
            { "LyingAdultRoadHome2", new() { "[c:g1]What are you fighting for so fiercely when you have nowhere to go back to?[c:]" }},
            { "LyingAdultScarecrow", new() { "[c:g1]Poor stuffing of straw. I'll give you the wisdom to ponder over anything.[c:]" }},
            { "LyingAdultScarecrow2", new() { "[c:g1]Do you think jabbering away with your oh-so smart mouth is all that matters?[c:]" }},
            { "LyingAdultScaredyCat", new() { "[c:g1]Cowardly kitten, I'll give you the courage to stand up to anything and everything.[c:]" }},
            { "LyingAdultScaredyCat2", new() { "[c:g1]What are you even going to do when you lack the bravery to face anything head-on?[c:]" }},
            { "LyingAdultWoodsman", new() { "[c:g1]Tin-cold woodsman. I'll give you the heart to forgive and love anyone.[c:]" }},
            { "LyingAdultWoodsman2", new() { "[c:g1]Who do you possibly expect to understand with that ice-cold heart of yours?[c:]" }},
            #endregion
            { "MagicalGirlHeartTransform", new() { "The balance must be maintained. Good cannot exist without evil." }},
            { "MeltingLoveAbsorb", new() { "They give themselves lovingly." }},
            { "MountainOfBodiesGrow", new() { "With each body added, its appetite grows." }},
            { "MountainOfBodiesShrink", new() { "Don't worry, bodies are in no short supply." }},
            { "NamelessFetusAwake", new() { "As you cut into the beast's flesh, it lets out a piercing cry." }},
            { "NothingThereReveal", new() { "What is that thing?" }},
            { "NothingThereTransformEgg", new() { "It seems to be trying to mimic you. 'Trying' is the key word." }},
            { "NothingThereTransformTrue", new() { "What is it doing?" }},
            { "QueenOfHatredExhaust", new() { "A formidable attack. Shame it has left her too tired to defend herself." }},
            { "QueenOfHatredRecover", new() { "The monster returns to full strength." }},
            #region Binah WIP
            { "SephirahBinahChoice", new() {
                DialogueWithEmotion("Oh! Um...", Emotion.Quiet),
                DialogueWithEmotion("Hello manager.", Emotion.Laughter) }},
            { "SephirahBinahDrawn", new() {
                DialogueWithEmotion("I'm not that skilled...", Emotion.Quiet),
                DialogueWithEmotion("but I'll do my best!", Emotion.Anger) }},
            { "SephirahBinahGivenSigil", new() {
                DialogueWithEmotion("I still have a lot to improve on...", Emotion.Quiet) }},
            { "SephirahBinahHurt", new() {
                DialogueWithEmotion("Ah!", Emotion.Surprise) }},
            { "SephirahBinahPlayed", new() {
                DialogueWithEmotion("I’m nervous, but...", Emotion.Surprise),
                DialogueWithEmotion("I won't let you down...!", Emotion.Anger) }},
            { "SephirahBinahSacrificed", new() {
                DialogueWithEmotion("I was nothing but trouble...", Emotion.Quiet) }},
            { "SephirahBinahSelectableBad", new()
            { DialogueWithEmotion("I don't like this...", Emotion.Curious) }},
            { "SephirahBinahSelectableGood", new() {
                DialogueWithEmotion("Let's think about this...", Emotion.Surprise) }},
            { "SephirahBinahTrial", new() {
                DialogueWithEmotion("A trial?", Emotion.Curious) }},
            #endregion
            #region Chesed WIP
            { "SephirahChesedChoice", new() {
                DialogueWithEmotion("Oh! Um...", Emotion.Quiet),
                DialogueWithEmotion("Hello manager.", Emotion.Laughter) }},
            { "SephirahChesedDrawn", new() {
                DialogueWithEmotion("I'm not that skilled...", Emotion.Quiet),
                DialogueWithEmotion("but I'll do my best!", Emotion.Anger) }},
            { "SephirahChesedGivenSigil", new() {
                DialogueWithEmotion("I still have a lot to improve on...", Emotion.Quiet) }},
            { "SephirahChesedHurt", new() {
                DialogueWithEmotion("Ah!", Emotion.Surprise) }},
            { "SephirahChesedPlayed", new() {
                DialogueWithEmotion("I’m nervous, but...", Emotion.Surprise),
                DialogueWithEmotion("I won't let you down...!", Emotion.Anger) }},
            { "SephirahChesedSacrificed", new() {
                DialogueWithEmotion("I was nothing but trouble...", Emotion.Quiet) }},
            { "SephirahChesedSelectableBad", new()
            { DialogueWithEmotion("I don't like this...", Emotion.Curious) }},
            { "SephirahChesedSelectableGood", new() {
                DialogueWithEmotion("Let's think about this...", Emotion.Surprise) }},
            { "SephirahChesedTrial", new() {
                DialogueWithEmotion("Oh~? What's this~", Emotion.Curious) }},
            #endregion
            { "SefirotChoiceNodeIntro", new() {
                "As you journey through the wilderness, you come across a small group of humans.",
                "They're dressed oddly for this climate, and even odder, [c:bR]they seem to recognise you[c:].",
                "They are on an expedition of some kind, in search of [c:bR]strange[c:] beasts.",
                "Their appear quite knowledgeable. Perhaps you can convince them to aid you on your journey." }},
            #region Geburah WIP
            { "SephirahGeburaChoice", new() {
                DialogueWithEmotion("Oh! Um...", Emotion.Quiet),
                DialogueWithEmotion("Hello manager.", Emotion.Laughter) }},
            { "SephirahGeburaDrawn", new() {
                DialogueWithEmotion("I'm not that skilled...", Emotion.Quiet),
                DialogueWithEmotion("but I'll do my best!", Emotion.Anger) }},
            { "SephirahGeburaGivenSigil", new() {
                DialogueWithEmotion("I still have a lot to improve on...", Emotion.Quiet) }},
            { "SephirahGeburaHurt", new() {
                DialogueWithEmotion("Ah!", Emotion.Surprise) }},
            { "SephirahGeburaPlayed", new() {
                DialogueWithEmotion("I’m nervous, but...", Emotion.Surprise),
                DialogueWithEmotion("I won't let you down...!", Emotion.Anger) }},
            { "SephirahGeburaSacrificed", new() {
                DialogueWithEmotion("I was nothing but trouble...", Emotion.Quiet) }},
            { "SephirahGeburaSelectableBad", new()
            { DialogueWithEmotion("I don't like this...", Emotion.Curious) }},
            { "SephirahGeburaSelectableGood", new() {
                DialogueWithEmotion("Let's think about this...", Emotion.Surprise) }},
            { "SephirahGeburaTrial", new() {
                DialogueWithEmotion("A trial?", Emotion.Curious) }},
            #endregion
            #region Hod
            { "SephirahHodChoice", new() {
                DialogueWithEmotion("Oh! Um...", Emotion.Quiet),
                DialogueWithEmotion("Hello manager.", Emotion.Laughter) }},
            { "SephirahHodDrawn", new() {
                DialogueWithEmotion("I'm not that skilled...", Emotion.Quiet),
                DialogueWithEmotion("but I'll do my best!", Emotion.Anger) }},
            { "SephirahHodGivenSigil", new() {
                DialogueWithEmotion("I still have a lot to improve on...", Emotion.Quiet) }},
            { "SephirahHodHurt", new() {
                DialogueWithEmotion("Ah!", Emotion.Surprise) }},
            { "SephirahHodPlayed", new() {
                DialogueWithEmotion("I’m nervous, but...", Emotion.Surprise),
                DialogueWithEmotion("I won't let you down...!", Emotion.Anger) }},
            { "SephirahHodSacrificed", new() {
                DialogueWithEmotion("I was nothing but trouble...", Emotion.Quiet) }},
            { "SephirahHodSelectableBad", new() {
                DialogueWithEmotion("I don't like this...", Emotion.Curious) }},
            { "SephirahHodSelectableGood", new() {
                DialogueWithEmotion("Let's think about this...", Emotion.Surprise) }},
            { "SephirahHodTrial", new() {
                DialogueWithEmotion("A trial?", Emotion.Curious) }},
            #endregion
            #region Hokma WIP
            { "SephirahHokmaChoice", new() {
                DialogueWithEmotion("Oh! Um...", Emotion.Quiet),
                DialogueWithEmotion("Hello manager.", Emotion.Laughter) }},
            { "SephirahHokmaDrawn", new() {
                DialogueWithEmotion("I'm not that skilled...", Emotion.Quiet),
                DialogueWithEmotion("but I'll do my best!", Emotion.Anger) }},
            { "SephirahHokmaGivenSigil", new() {
                DialogueWithEmotion("I still have a lot to improve on...", Emotion.Quiet) }},
            { "SephirahHokmaHurt", new() {
                DialogueWithEmotion("Ah!", Emotion.Surprise) }},
            { "SephirahHokmaPlayed", new() {
                DialogueWithEmotion("I’m nervous, but...", Emotion.Surprise),
                DialogueWithEmotion("I won't let you down...!", Emotion.Anger) }},
            { "SephirahHokmaSacrificed", new() {
                DialogueWithEmotion("I was nothing but trouble...", Emotion.Quiet) }},
            { "SephirahHokmaSelectableBad", new()
            { DialogueWithEmotion("I don't like this...", Emotion.Curious) }},
            { "SephirahHokmaSelectableGood", new() {
                DialogueWithEmotion("Let's think about this...", Emotion.Surprise) }},
            { "SephirahHokmaTrial", new() {
                DialogueWithEmotion("A trial?", Emotion.Curious) }},
            #endregion
            #region Malkuth WIP
            { "SephirahMalkuthChoice", new() {
                DialogueWithEmotion("Oh! Um...", Emotion.Quiet),
                DialogueWithEmotion("Hello manager.", Emotion.Laughter) }},
            { "SephirahMalkuthDrawn", new() {
                DialogueWithEmotion("I'm not that skilled...", Emotion.Quiet),
                DialogueWithEmotion("but I'll do my best!", Emotion.Anger) }},
            { "SephirahMalkuthGivenSigil", new() {
                DialogueWithEmotion("I still have a lot to improve on...", Emotion.Quiet) }},
            { "SephirahMalkuthHurt", new() {
                DialogueWithEmotion("Ah!", Emotion.Surprise) }},
            { "SephirahMalkuthPlayed", new() {
                DialogueWithEmotion("I’m nervous, but...", Emotion.Surprise),
                DialogueWithEmotion("I won't let you down...!", Emotion.Anger) }},
            { "SephirahMalkuthSacrificed", new() {
                DialogueWithEmotion("I was nothing but trouble...", Emotion.Quiet) }},
            { "SephirahMalkuthSelectableBad", new()
            { DialogueWithEmotion("I don't like this...", Emotion.Curious) }},
            { "SephirahMalkuthSelectableGood", new() {
                DialogueWithEmotion("Let's think about this...", Emotion.Surprise) }},
            { "SephirahMalkuthTrial", new() {
                DialogueWithEmotion("A trial?", Emotion.Curious) }},
            #endregion
            #region Netzach WIP
            { "SephirahNetzachChoice", new() {
                DialogueWithEmotion("Oh! Um...", Emotion.Quiet),
                DialogueWithEmotion("Hello manager.", Emotion.Laughter) }},
            { "SephirahNetzachDrawn", new() {
                DialogueWithEmotion("I'm not that skilled...", Emotion.Quiet),
                DialogueWithEmotion("but I'll do my best!", Emotion.Anger) }},
            { "SephirahNetzachGivenSigil", new() {
                DialogueWithEmotion("I still have a lot to improve on...", Emotion.Quiet) }},
            { "SephirahNetzachHurt", new() {
                DialogueWithEmotion("Ah!", Emotion.Surprise) }},
            { "SephirahNetzachPlayed", new() {
                DialogueWithEmotion("I’m nervous, but...", Emotion.Surprise),
                DialogueWithEmotion("I won't let you down...!", Emotion.Anger) }},
            { "SephirahNetzachSacrificed", new() {
                DialogueWithEmotion("I was nothing but trouble...", Emotion.Quiet) }},
            { "SephirahNetzachSelectableBad", new()
            { DialogueWithEmotion("I don't like this...", Emotion.Curious) }},
            { "SephirahNetzachSelectableGood", new() {
                DialogueWithEmotion("Let's think about this...", Emotion.Surprise) }},
            { "SephirahNetzachTrial", new() {
                DialogueWithEmotion("A trial?", Emotion.Curious) }},
            #endregion
            #region Tiphereth WIP
            { "SephirahTipherethChoice", new() {
                DialogueWithEmotion("Oh! Um...", Emotion.Quiet),
                DialogueWithEmotion("Hello manager.", Emotion.Laughter) }},
            { "SephirahTipherethDrawn", new() {
                DialogueWithEmotion("I'm not that skilled...", Emotion.Quiet),
                DialogueWithEmotion("but I'll do my best!", Emotion.Anger) }},
            { "SephirahTipherethGivenSigil", new() {
                DialogueWithEmotion("I still have a lot to improve on...", Emotion.Quiet) }},
            { "SephirahTipherethHurt", new() {
                DialogueWithEmotion("Ah!", Emotion.Surprise) }},
            { "SephirahTipherethPlayed", new() {
                DialogueWithEmotion("I’m nervous, but...", Emotion.Surprise),
                DialogueWithEmotion("I won't let you down...!", Emotion.Anger) }},
            { "SephirahTipherethSacrificed", new() {
                DialogueWithEmotion("I was nothing but trouble...", Emotion.Quiet) }},
            { "SephirahTipherethSelectableBad", new()
            { DialogueWithEmotion("I don't like this...", Emotion.Curious) }},
            { "SephirahTipherethSelectableGood", new() {
                DialogueWithEmotion("Let's think about this...", Emotion.Surprise) }},
            { "SephirahTipherethTrial", new() {
                DialogueWithEmotion("A trial?", Emotion.Curious) }},
            #endregion
            #region Yesod WIP
            { "SephirahYesodChoice", new() {
                DialogueWithEmotion("Hello manager.", Emotion.Neutral) }},
            { "SephirahYesodDrawn", new() {
                DialogueWithEmotion("Hello manager.", Emotion.Neutral) }},
            { "SephirahYesodGivenSigil", new() {
                DialogueWithEmotion("I will fulfill my responsibility.", Emotion.Neutral) }},
            { "SephirahYesodHurt", new() {
                DialogueWithEmotion("Kh.", Emotion.Curious) }},
            { "SephirahYesodPlayed", new() {
                DialogueWithEmotion("If there's no opportunity to strike,", Emotion.Neutral),
                DialogueWithEmotion("we'll simply make one.", Emotion.Anger) }},
            { "SephirahYesodSacrificed", new() {
                DialogueWithEmotion("There is no need to pity me.", Emotion.Neutral) }},
            { "SephirahYesodSelectableBad", new() {
                DialogueWithEmotion("This isn't particularly delightful.", Emotion.Neutral) }},
            { "SephirahYesodSelectableGood", new() {
                DialogueWithEmotion("Hm...", Emotion.Curious),
                DialogueWithEmotion("I trust you will choose correctly.", Emotion.Neutral) }},
            { "SephirahYesodTrial", new() {
                DialogueWithEmotion("It seems to be a test of some kind.", Emotion.Curious),
                DialogueWithEmotion("I hope you're prepared.", Emotion.Anger) }},
            #endregion
            { "ServantOfWrathTransform", new() { "Abandoned and betrayed, she rages against her own failure." }},
            { "TodaysShyLookAngry", new() { "Some days you don't feel like smiling." }},
            { "TodaysShyLookHappy", new() { "There was no place for frowns in the City." }},
            { "TodaysShyLookNeutral", new() { "Unable to decide what face to wear, she became shy again." }},
            #region WhiteNight
            { "WhiteNightEventIntro", new() {
                "[c:bR]The time has come. A new world will come.[c:]",
                "[c:bR]I am death and life. Darkness and light.[c:]" }},
            { "WhiteNightApostleHeretic", new() { "[c:bR]Have I not chosen you, the Twelve? Yet one of you is [c:][c:bG]a devil[c:][c:bR].[c:]" }},
            { "WhiteNightApostleDowned", new() { "[c:bR]None of you can leave my side until I permit you.[c:]" }},
            { "WhiteNightApostleKilledByNull", new() { "[c:bR]Be at ease. No calamity shall be able to trouble you.[c:]" }},
            { "WhiteNightKilledByNull", new() { "[c:bR]I shall not leave thee until I have completed my mission.[c:]" }},
            { "WhiteNightMakeRoom", new() { "[c:bR]What did you do?[c:]" }},
            #endregion
            { "YinDragonIntro", new() { "Now you become the sky, and I the land." }}
        };

        private static CustomLine DialogueWithEmotion(string dialogue, Emotion emotion) => new() { text = dialogue, emotion = emotion };
        public static Dictionary<string, List<List<CustomLine>>> RepeatEventNames => new()
        {
            #region Hod
            { "SephirahHodDrawn", new(){
                new() {
                    DialogueWithEmotion("I'm not that skilled...", Emotion.Quiet),
                    DialogueWithEmotion("but I'll do my best!", Emotion.Anger) },
                new() {
                    DialogueWithEmotion("Stay calm...", Emotion.Curious),
                    DialogueWithEmotion("...let's do this.", Emotion.Neutral) },
                new() { DialogueWithEmotion("I'll try not to be a hindrance.", Emotion.Surprise) },
                new() { DialogueWithEmotion("If we prepare, we can overcome this.", Emotion.Neutral) },
                new() { DialogueWithEmotion("I'm here to do my best.", Emotion.Neutral) },
                new() { DialogueWithEmotion("Hello manager.", Emotion.Laughter) }}
            },
            { "SephirahHodGivenSigil", new(){
                new() { DialogueWithEmotion("I still have a lot to improve on...", Emotion.Quiet) },
                new() { DialogueWithEmotion("I gotta get on everyone else's level.", Emotion.Anger) },
                new() { DialogueWithEmotion("With this I can better help everyone.", Emotion.Neutral) },
                new() { DialogueWithEmotion("I hope this helps.", Emotion.Surprise) }}
            },
            { "SephirahHodHurt", new(){
                new() { DialogueWithEmotion("Ah!", Emotion.Surprise) },
                new() { DialogueWithEmotion("Ow!", Emotion.Surprise) },
                new() { DialogueWithEmotion("I-I can take it...", Emotion.Surprise) },
                new() { DialogueWithEmotion("I can't let them down...", Emotion.Surprise) }}
            },
            { "SephirahHodPlayed", new(){
                new() {
                    DialogueWithEmotion("I’m nervous, but...", Emotion.Surprise),
                    DialogueWithEmotion("I won't let you down...!", Emotion.Anger) },
                new() { DialogueWithEmotion("I hope nothing bad happens.", Emotion.Curious) },
                new() { DialogueWithEmotion("Alright, here I come!", Emotion.Curious) },
                new() {
                    DialogueWithEmotion("Everything will work out...", Emotion.Neutral),
                    DialogueWithEmotion("Maybe...", Emotion.Curious) },
                new() { DialogueWithEmotion("If we brace ourselves, we can win this.", Emotion.Anger) },
                new() { DialogueWithEmotion("I can do this.", Emotion.Anger) }}
            },
            { "SephirahHodSacrificed", new(){
                new() { DialogueWithEmotion("I was nothing but trouble...", Emotion.Surprise) },
                new() { DialogueWithEmotion("My body feels numb...", Emotion.Surprise) },
                new() { DialogueWithEmotion("I should've done a better job.", Emotion.Surprise) }}
            },
            { "SephirahHodSelectableBad", new(){
                new() { DialogueWithEmotion("I don't like this...", Emotion.Curious) },
                new() { DialogueWithEmotion("Maybe someone else can go?", Emotion.Curious) },
                new() { DialogueWithEmotion("I can't...not again...", Emotion.Quiet) },
                new() { DialogueWithEmotion("You need me, right?", Emotion.Quiet) }}
            },
            { "SephirahHodSelectableGood", new(){
                new() { DialogueWithEmotion("Let's think about this...", Emotion.Surprise) },
                new() { DialogueWithEmotion("If it will make me more helpful...", Emotion.Neutral) },
                new() { DialogueWithEmotion("Will this give me my courage?", Emotion.Curious) }}
            },
            { "SephirahHodTrial", new(){
                new() { DialogueWithEmotion("A trial?", Emotion.Curious) },
                new() { DialogueWithEmotion("I hope I can help.", Emotion.Neutral) },
                new() { DialogueWithEmotion("I hope I'm prepared.", Emotion.Neutral) }}
            },
            #endregion
            #region Yesod WIP
            { "SephirahYesodDrawn", new(){
                new() { DialogueWithEmotion("Hello manager.", Emotion.Neutral) },
                new() { DialogueWithEmotion("Recklessly charging is not an effecient combat method.", Emotion.Neutral) },
                new() { DialogueWithEmotion("We must prioritise concluding this fight quickly.", Emotion.Curious) },
                new() { DialogueWithEmotion("I take it you're prepared for any outcome.", Emotion.Neutral) },
                new() {
                    DialogueWithEmotion("In a situation such as this,", Emotion.Curious),
                    DialogueWithEmotion("we must be calm and make sound judgements.", Emotion.Curious) },
                new() { DialogueWithEmotion("Hello manager.", Emotion.Laughter) }}
            },
            { "SephirahYesodGivenSigil", new(){
                new() { DialogueWithEmotion("I will fulfill my responsibility.", Emotion.Neutral) },
                new() { DialogueWithEmotion("I accept this with grace.", Emotion.Neutral) },
                new() { DialogueWithEmotion("Thank you.", Emotion.Laughter) }}
            },
            { "SephirahYesodHurt", new(){
                new() { DialogueWithEmotion("Kh.", Emotion.Curious) },
                new() { DialogueWithEmotion("Ah!", Emotion.Anger) },
                new() { DialogueWithEmotion("Pay no mind to me.", Emotion.Curious) },
                new() { DialogueWithEmotion("Focus on the goal.", Emotion.Curious) }}
            },
            { "SephirahYesodPlayed", new(){
                new() {
                    DialogueWithEmotion("If there's no opportunity to strike,", Emotion.Neutral),
                    DialogueWithEmotion("we'll simply make one.", Emotion.Anger) },
                new() { DialogueWithEmotion("Let's end this quickly.", Emotion.Curious) },
                new() {  DialogueWithEmotion("Very well.", Emotion.Neutral) },
                new() { DialogueWithEmotion("Let's not push ourselves too carelessly.", Emotion.Curious) },
                new() { DialogueWithEmotion("I don't want to waste any time.", Emotion.Curious) }}
            },
            { "SephirahYesodSacrificed", new(){
                new() { DialogueWithEmotion("I couldn't see the path ahead.", Emotion.Curious) },
                new() { DialogueWithEmotion("What did I do wrong?", Emotion.Anger) },
                new() { DialogueWithEmotion("It's entirely my fault.", Emotion.Curious) }}
            },
            { "SephirahYesodSelectableBad", new(){
                new() { DialogueWithEmotion("This isn't particularly delightful.", Emotion.Neutral) },
                new() { DialogueWithEmotion("This death must not be in vain.", Emotion.Neutral) },
                new() { DialogueWithEmotion("I suppose I should just accept this.", Emotion.Neutral) },
                new() {
                    DialogueWithEmotion("Frankly,", Emotion.Curious),
                    DialogueWithEmotion("I dislike how numb I've become to death.", Emotion.Anger) }}
            },
            { "SephirahYesodSelectableGood", new(){
                new() { DialogueWithEmotion("I trust you will choose correctly.", Emotion.Neutral) },
                new() { DialogueWithEmotion("Hasty actions will bring more harm than good.", Emotion.Curious) },
                new() { DialogueWithEmotion("Be logical about this.", Emotion.Neutral) }}
            },
            { "SephirahYesodTrial", new(){
                new() { DialogueWithEmotion("I hope you're prepared.", Emotion.Neutral) },
                new() { DialogueWithEmotion("A single mistake could cause disaster.", Emotion.Curious) },
                new() { DialogueWithEmotion("We must always proceed with discretion.", Emotion.Neutral) }}
            }
            #endregion
        };

        public static IEnumerator PlayDialogueEvent(string name, float waitFor = 0.2f)
        {
            yield return WhistleWind.AbnormalSigils.Core.AbnormalDialogueManager.PlayDialogueEvent(name, waitFor);
        }
        public static void GenerateDialogueEvents()
        {
            foreach (KeyValuePair<string, List<CustomLine>> dialogue in EventNames)
            {
                DialogueEvent.Speaker speaker = DialogueEvent.Speaker.Single;

                if (dialogue.Key.StartsWith("NothingThere"))
                    speaker = DialogueEvent.Speaker.Leshy;
                else if (dialogue.Key.StartsWith("WhiteNight"))
                    speaker = DialogueEvent.Speaker.Bonelord;

                // if there are no repeat lines, set repeatLines to null
                if (!RepeatEventNames.TryGetValue(dialogue.Key, out List<List<CustomLine>> repeatLines))
                    repeatLines = null;

                DialogueManager.GenerateEvent(LobotomyPlugin.pluginGuid, dialogue.Key, dialogue.Value, repeatLines, defaultSpeaker: speaker);
            }
        }
    }
}
