using DiskCardGame;
using InscryptionAPI.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWindLobotomyMod.Core
{
    public static class DialogueEventsManager // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        /// <summary>
        /// Shorthand method for playing dialogue events.
        /// Checks if the inputted dialogue event has been played, then plays it if it hasn't been yet.
        /// </summary>
        /// <param name="name">Name of dialogue event to play.</param>
        public static IEnumerator PlayDialogueEvent(string name, float waitFor = 0.2f)
        {
            if (!DialogueEventsData.EventIsPlayed(name))
            {
                yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent(name, TextDisplayer.MessageAdvanceMode.Input);
                yield return new WaitForSeconds(waitFor);
            }
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

                DialogueEventGenerator.GenerateEvent(dialogue.Key, dialogue.Value, defaultSpeaker: speaker);
            }
        }

        public static Dictionary<string, List<CustomLine>> EventNames => new()
        {
            {
                "AbnormalChoiceNodeIntro", new()
                {
                    "You enter a clearing surrounded by dark, twisting trees.",
                    "The moon leers down at you as the trees claw at the sky.",
                    "[c:bR]3[c:] creatures emerge from the shadows before you. Powerful, mysterious...",
                    "[c:bR]abnormal[c:]."
                }
            },
            { "CopycatFail", new() { "The lie falls apart, revealing your pitiful true self." } },
            { "CourageousFail", new() { "Your creature's consitution is too weak." } },
            { "CourageousRefuse", new() { "Cowards don't get the boon of the brave." } },
            { "CowardlySelfLove", new() { "There is strength in self-love." } },
            { "CowardlyWeaken", new() { "Your beast's moxie withers away." } },
            { "FrostRulerKiss", new() { "With a single kiss, the Snow Queen froze their hearts." } },
            { "FrostRulerFail", new() { "The snow melts away. Perhaps spring is coming." } },
            {
                "NettlesDie", new()
                {
                    "She saw her dear brothers in the distance.Her family that needed the nettle clothing to be free of the curse.",
                    "She fell to the ground, vomiting ooze like the rest of [c:bR]the City[c:]."
                }
            },
            { "NettlesFail", new() { "The lake ripples gently. As if a number of swans just took flight." } },
            { "RegeneratorOverheal", new() { "The punishment for greed is getting everything you wanted." } },
            { "ApocalypseBirdIntro", new() { "Let me tell you a story. The story of the [c:bR]Black Forest[c:]." } },
            {
                "ApocalypseBirdOutro", new()
                {
                    "The three birds, [c:bR]now one[c:] looked around for [c:bR]the Beast[c:]. But there was nothing.",
                    "No creatures. No beast. No sun or moon or stars. Only a single bird, alone in an empty forest."
                }
            },
            {
                "ApocalypseBirdStory1", new()
                {
                    "Once upon a time, [c:bR]three birds[c:] lived happily in the lush Forest with their fellow animals.",
                    "One day a stranger arrived at the Forest.He proclaimed that the Forest would soon be ensared in a bitter conflict.",
                    "One that would only end when everything was devoured by a[c: bR]terrible Beast[c:].",
                    "The birds, frightened by this doomsay, sought to prevent conflict from ever breaking out."
                }
            },
            {
                "ApocalypseBirdStory2", new()
                {
                    "Fights began to break out. More and more creatures left the Forest, no matter how hard the birds worked.",
                    "They decided to combine their powers. This way, they could better protect their home.",
                    "This way they could better return the peace."
                }
            },
            { "ApocalypseBirdStory3", new() { "Darkness fell upon the forest. Mayhem ran amok as creatures screamed in terror at the towering bird." } },
            { "ApocalypseBirdStory4", new() { "Someone yelled out [c:bB]'It's the Beast! A big, scary monster lives in the Black Forest!'[c:]" } },
            { "ApocalypseBirdStoryBig", new() { "With his many eyes, [c:bG]Big Bird[c:] kept constant watch over the entire Forest." } },
            { "ApocalypseBirdStoryLong", new() { "[c:bB]Long Bird[c:] weighed the sins of all creatures in the forest with his scales." } },
            { "ApocalypseBirdStorySmall", new() { "[c:bR]Small Bird[c:] punished wrongdoers with his beak." } },
            { "ArmyInBlackTransform", new() { "The human heart is black, and must be cleaned." } },
            { "Bloodbath1", new() { "A hand rises from the scarlet pool." } },
            { "Bloodbath2", new() { "Another pale hand emerges." } },
            { "Bloodbath3", new() { "A third hand reaches out, as if asking for help." } },
            { "CENSOREDKilledCard", new() { "What have you done to my beast?" } },
            {
                "DerFreischutzSeventhBullet", new()
                {
                    "The Devil proposed a childist contract.",
                    "The seventh bullet would pierce the heart of his most beloved.",
                    "On hearing this, the hunter sought and shot everyone he loved."
                }
            },
            { "GiantTreeSapExplode", new() { "A strange gurgling sound comes from your beast's stomach." } },
            {
                "JesterOfNihilIntro", new()
                {
                    "Watch them carefully.",
                    "They're calling - no, praying for something."
                }
            },
            {
                "JesterOfNihilOutro", new()
                {
                    "There was no way to know if they had gathered to become [c:gray]The jester[c:],",
                    "or if [c:gray]The jester[c:] had come to resemble them."
                }
            },
            {
                "JesterOfNihilStory", new()
                {
                    "[c:gray]The jester[c:] retraced the steps of a path everyone would've taken.",
                    "No matter what it did, the jester always found itself at the end of that road."
                }
            },
            { "KingOfGreedTransform", new() { "Desire unfulfilled, the koi continues for Eden." } },
            { "KnightOfDespairTransform", new() { "Having failed to protect again, the knight fell once more to despair." } },
            { "MagicalGirlHeartTransform", new() { "The balance must be maintained. Good cannot exist without evil." } },
            { "MeltingLoveAbsorb", new() { "They give themselves lovingly." } },
            { "MountainOfBodiesGrow", new() { "With each body added, its appetite grows." } },
            { "MountainOfBodiesShrink", new() { "Don't worry, bodies are in no short supply." } },
            { "NamelessFetusAwake", new() { "As you cut into the beast's flesh, it lets out a piercing cry." } },
            { "NothingThereReveal", new() { "What is that thing?" } },
            { "NothingThereTransformEgg", new() { "It seems to be trying to mimic you. 'Trying' is the key word." } },
            { "NothingThereTransformTrue", new() { "What is it doing?" } },
            { "QueenOfHatredExhaust", new() { "A formidable attack. Shame it has left her too tired to defend herself." } },
            { "QueenOfHatredRecover", new() { "The monster returns to full strength." } },
            { "TodaysShyLookAngry", new() { "Some days you don't feel like smiling." } },
            { "TodaysShyLookHappy", new() { "There was no place for frowns in the City." } },
            { "TodaysShyLookNeutral", new() { "Unable to decide what face to wear, she became shy again." } },
            {
                "WhiteNightEventIntro", new()
                {
                    "[c:bR]The time has come. A new world will come.[c:]",
                    "[c:bR]I am death and life. Darkness and light.[c:]"
                }
            },
            { "WhiteNightApostleHeretic", new() { "[c:bR]Have I not chosen you, the Twelve? Yet one of you is [c:][c:bG]a devil[c:][c:bR].[c:]" } },
            { "WhiteNightApostleDowned", new() { "[c:bR]None of you can leave my side until I permit you.[c:]" } },
            { "WhiteNightApostleKilledByNull", new() { "[c:bR]Be at ease. No calamity shall be able to trouble you.[c:]" } },
            { "WhiteNightKilledByNull", new() { "[c:bR]I shall not leave thee until I have completed my mission.[c:]" } },
            { "WhiteNightMakeRoom", new() { "[c:bR]What did you do?[c:]" } },
            { "YinDragonIntro", new() { "Now you become the sky, and I the land." } }
        };
    }
}
