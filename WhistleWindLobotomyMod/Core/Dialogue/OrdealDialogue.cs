using System.Collections.Generic;
using WhistleWindLobotomyMod.Opponents;
using static WhistleWindLobotomyMod.Core.DialogueEventsManager;

namespace WhistleWindLobotomyMod {
    public partial class LobotomyDialogue {
        private void Dialogue_FirstOrdeal() {
            CreateDialogueEvents("OrdealNonOrdealKilled", new() {
                "Abhorrent though that creature was, its death will not reduce the counter.",
                "Aim for the others. Before you're overwhelmed." });

            CreateDialogueEvents("OrdealExhausted", new() {
                "It seems you've run dry.",
                "Worry not, it's not over for you yet.",
                "But you're running out of time. And the enemy knows you're desperate." });

            CreateDialogueEvents("OrdealRecall", new() {
                "These creatures will not stay in one spot for long. Use these to reposition your creatures." });

            CreateDialogueEvents("RecallMechanic", new() {
                "Newly played creatures will cost [c:bR]2 bones[c:] to play again.",
                "I will reduce this cost over time, until they become [c:bR]free[c:] to replay." });

            CreateDialogueEvents("OrdealFirstIntro", new()
            {
                "This will be a little different than normal.",
                "The creatures you will fight do not belong to me, nor do they belong here.",
                "They are... abominations.",
                "You will need to destroy them all before they destroy you.",
                "As I am not your opponent, any attacks against me will instead replenish your life.",
                "Excess damage will reward you with up to two [c:bR]bones[c:] per turn.",
                "The monitor will show you how many foes remain.",
                "Now, prepare yourself."
            });
            CreateDialogueEvents("OrdealDefeatedCardsLeft", new()
            {
                "Your work is satisfactory.",
                "Clear the remaining creatures from the board and you may pass."
            });
        }

        // string[] format:
        // [0] title
        // [1] intro desc
        // [2] outro desc
        public static readonly Dictionary<OrdealType, List<string[]>> BannerStrings = new() {
            {
                OrdealType.Green, new() {
                    new string[] {
                        "Doubt",
                        "One day, a question crossed through my mind. Where do we come from? We were given life and left in this world against our own volition.",
                        "To live was a process full of pain."
                    },
                    new string[] {
                        "Process of Understanding",
                        "In the end, they were bound to life. We existed only to express despair and ire.",
                        "We will understand life and the soul with our own hands."
                    },
                    new string[] {
                        "Where We Must Reach",
                        "We constructed a looming tower to return whence we came.",
                        "There wasn’t an answer. We didn’t find a single thing we wanted. We only witnessed the death of life itself."
                    },
                    new string[] {
                        "Helix of the End",
                        "The tower is touched by the sky, and nothing will remain on the ground.",
                        "Who pays for the suffering and neglect of the lives given to us?"
                    }
                }
            },
            {
                OrdealType.Violet, new() {
                    new string[] {
                        "The Fruit of Understanding",
                        "To gain an understanding of what is incomprehensible, they dream, staring.",
                        "They complied with nothing in their bid to understand. They simply did so."
                    },
                    new string[] {
                        "Grant Us Love",
                        "We could only hear the weakest and faintest of their acts. We sought for love and compassion from them.",
                        "We cannot understand them, nor will they understand us."
                    },
                    null,
                    new string[] {
                        "The God Delusion",
                        "We incessantly tried to accept it. We wanted to understand them in our heads by any means, regardless of the consequences.",
                        "For the sake of not crumbling in on oneself. The idea that they may impossibly exist, or that they are unreachable and forever enigmatic no matter the path. Unacceptable…"
                    }
                }
            },
            {
                OrdealType.Crimson, new() {
                    new string[] {
                        "Cheers for the Beginning",
                        "Let us light a flame yet more radiant in our lives; for life is a candlelight, destined to snuff out one day.",
                        "To live is to yearn and fight for our desires."
                    },
                    new string[] {
                        "The Harmony of Skin",
                        "We marched from time to time, and we would share our pleasure.",
                        "The collision of one life with another, skin harmonizing, painting a yet more beautiful appearance."
                    },
                    new string[] {
                        "The Struggle at the Climax",
                        "Throwing away our old bodies, we all become one, infinitely continuing the red march.",
                        "One day we will know, and tomorrow we will march hand in hand."
                    },
                    null
                }
            },
            {
                OrdealType.Amber, new() {
                    new string[] {
                        "The Perfect Food",
                        "A perfect meal, an excellent substitute.",
                        "We ate incessantly to live. The inevitable diminution, the waste…"
                    },
                    null,
                    new string[] {
                        "The Food Chain",
                        "To accustom oneself to the taste was an inevitable process.",
                        "We could live. We could continue eating."
                    },
                    new string[] {
                        "The Eternal Meal",
                        "They fought amongst themselves to eat the others.",
                        "And the stronger side survived. That, simply, is the story."
                    }
                }
            },
            {
                OrdealType.Indigo, new() {
                    null,
                    new string[] {
                        "The Sweepers",
                        "When night falls in the Backstreets, they will come.",
                        "When the sun rises up, there will be no remains anymore."
                    },
                    null,
                    new string[] {
                        "Night in the Backstreets",
                        "From the borders of the Nest they emerge, sweeping away everything in their path.",
                        "For eighty minutes they appear to clean the Backstreets. Dead bodies, unauthorised constructions, anything and everything the City doesn't need."
                    }
                }
            },
            {
                OrdealType.White, new() {
                    new string[] {
                        "A Request",
                        "From meaningless errands, to exploration, to contract killing; they will do whatever you wish, so long as you pay them sufficiently.",
                        "They work in the Offices, Syndicates, and the Wings. Their tasks vary from the banal things to something truly sublime."
                    },
                    new string[] {
                        "Armaments",
                        "They search constantly, be it for the Backers of the Wings, the Inventions of the Backstreets, the Reliques of the Outskirts, the Artefacts of the Ruins...",
                        "As they have always done, they will overcome all that impedes them, weapons in hand."
                    },
                    new string[] {
                        "The Fixers",
                        "The colossal tower of light was titled The Library. It is only natural for the Fixers to be drawn to such a mystic place of life and death.",
                        "Bookhunters... One day they will rummage through the Library reigned over by the Pale Librarian. They are what shall become of the Fixers."
                    },
                    new string[] {
                        "The Claw",
                        "To know and manipulate all the secrets of the world; that is the privilege of the Head, the Eye, and the Claws. It is their honor and absolute power.",
                        "No one dares to stand against them. As long as they exist, the tale of the Nest will never reach its close."
                    }
                }
            }
        };
    }
}
