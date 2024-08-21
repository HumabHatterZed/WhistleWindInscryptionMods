using DiskCardGame;
using static WhistleWind.Core.Helpers.DialogueHelper;
using static WhistleWindLobotomyMod.Core.DialogueEventsManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyDialogue
    {
        private void Dialogue_MapNodes()
        {
            CreateDialogueEvents("AbnormalChoiceNodeIntro", new() {
                "You enter a clearing surrounded by dark, twisting trees.",
                "What looks like a well rises up from its centre, but you wisely choose not to look inside.",
                "You hear voices call out, crying, screaming, wishing and wanting to be drawn up.",
                "There are things down there. Unnatural, otherwordly,",
                "[c:bR]abnormal[c:]."
                });
            CreateDialogueEvents("SefirotChoiceNodeIntro", new() {
                "As you journey through the wilderness, you come across a small group of humans.",
                "They're dressed oddly for this climate, and even odder, they seem to recognise you.",
                "They appear quite capable. Perhaps you can convince them to aid you on your travels."
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
                "[c:bR]1[c:] of your creatures must stay behind to operate the machine." },
                new() {
                    new() { "[c:bR]1[c:] of your creatures must stay behind to operate the machine." }
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
