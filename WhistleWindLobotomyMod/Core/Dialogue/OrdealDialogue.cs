using DiskCardGame;
using static WhistleWind.Core.Helpers.DialogueHelper;
using static WhistleWindLobotomyMod.Core.DialogueEventsManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyDialogue
    {
        private void XDialogue_WhiteOrdeal()
        {
            CreateDialogueEvents("OrdealWhiteDawnIntro", new() // A Request
            {
                "From meaningless errands, to exploration, to contract killing; they will do whatever you wish, so long as you pay them sufficiently."
            }, new() {  new() {
                "From meaningless errands, to exploration, to contract killing; they will do whatever you wish, so long as you pay them sufficiently."
                }});
            CreateDialogueEvents("OrdealWhiteDawnOutro", new()
            {
                "They work in the Offices, Syndicates, and the Wings. Their work varies from banal things to something truly sublime."
            }, new() { new() {
                "They work in the Offices, Syndicates, and the Wings. Their work varies from banal things to something truly sublime."
            }});
            CreateDialogueEvents("OrdealWhiteNoonIntro", new() // Armaments
            {
                "They search constantly, be it for the Backers of the Wings, the Inventions of the Backstreets, the Relics of the Outskirts, the Artefacts of the Ruins..."
            }, new() { new() {
                "They search constantly, be it for the Backers of the Wings, the Inventions of the Backstreets, the Relics of the Outskirts, the Artefacts of the Ruins..."
            }});
            CreateDialogueEvents("OrdealWhiteNoonOutro", new()
            {
                "As they have always done, they will overcome all that impedes them, weapons in hand."
            }, new() { new() {
                "As they have always done, they will overcome all that impedes them, weapons in hand."
            }});
            CreateDialogueEvents("OrdealWhiteDuskIntro", new() // The Fixers
            {
                "The colossal tower of light was titled the Library. It is only natural for the Fixers to be drawn to such a mystic place of life and death."
            }, new() { new() {
                "The colossal tower of light was titled the Library. It is only natural for the Fixers to be drawn to such a mystic place of life and death."
            }});
            CreateDialogueEvents("OrdealWhiteDuskOutro", new()
            {
                "Bookhunters... One day they will rummage through the Library reigned over by the Pale Librarian. They are what will become of the Fixers."
            }, new() { new() {
                "Bookhunters... One day they will rummage through the Library reigned over by the Pale Librarian. They are what will become of the Fixers."
            }});
            CreateDialogueEvents("OrdealWhiteMidnightIntro", new() // The Claw
            {
                "To know and manipulate all the secrets of the world; that is the privilege of the Head, the Eye, and the Claws.  It is their honour and absolute power."
            }, new() { new() {
                "To know and manipulate all the secrets of the world; that is the privilege of the Head, the Eye, and the Claws.  It is their honour and absolute power."
            }});
            CreateDialogueEvents("OrdealWhiteMidnightOutro", new()
            {
                "No one dares to stand against them. As long as they exist, the tale of the Nest will never reach its close."
            }, new() { new() {
                "No one dares to stand against them. As long as they exist, the tale of the Nest will never reach its close."
            }});
        }
        private void XDialogue_IndigoOrdeal()
        {
            CreateDialogueEvents("IntroOrdealOpponent", new()
            {
                "The stream of creatures thins."
            });

            CreateDialogueEvents("DefeatedOrdealOpponent", new()
            {
                "The tide of monsters is stemmed.",
                "."
            });
            CreateDialogueEvents("OrdealIndigoNoonIntro", new()
            {
                "When night falls in the Backstreets, they will come."
            }, new() {  new() {
                "When night falls in the Backstreets, they will come."
                }});
            CreateDialogueEvents("OrdealIndigoNoonOutro", new()
            {
                "When the sun rises anew, not a scrap will remain."
            }, new() { new() {
                "When the sun rises anew, not a scrap will remain."
            }});
        }
        private void XDialogue_VioletOrdeal()
        {
            CreateDialogueEvents("OrdealVioletDawnIntro", new()
            {
                "To gain an understanding of what is incomprehensible, they dream, staring."
            }, new() {  new() {
                "To gain an understanding of what is incomprehensible, they dream, staring."
                }});
            CreateDialogueEvents("OrdealVioletDawnOutro", new()
            {
                "They complied with nothing in their bid to understand. They simply did so."
            }, new() { new() {
                "They complied with nothing in their bid to understand. They simply did so."
            }});
            CreateDialogueEvents("OrdealVioletNoonIntro", new()
            {
                "We could only hear the weakest and faintest of their acts. We sought for love and compassion from them."
            }, new() { new() {
                "We could only hear the weakest and faintest of their acts. We sought for love and compassion from them."
            }});
            CreateDialogueEvents("OrdealVioletNoonOutro", new()
            {
                "We cannot understand them, nor will they understand us."
            }, new() { new() {
                "We cannot understand them, nor will they understand us."
            }});
            CreateDialogueEvents("OrdealVioletMidnightIntro", new()
            {
                "We incessantly tried to accept it. We wanted to understand them in our heads by any means, regardless of the consequences."
            }, new() { new() {
                "We incessantly tried to accept it. We wanted to understand them in our heads by any means, regardless of the consequences."
            }});
            CreateDialogueEvents("OrdealVioletMidnightOutro", new()
            {
                "For the sake of not crumbling in on oneself. The idea that they may impossibly exist, or that they are unreachable and forever enigmatic no matter the path. Unacceptable..."
            }, new() { new() {
                "For the sake of not crumbling in on oneself. The idea that they may impossibly exist, or that they are unreachable and forever enigmatic no matter the path. Unacceptable..."
            }});
        }
        private void XDialogue_CrimsonOrdeal()
        {
            CreateDialogueEvents("OrdealCrimsonDawnIntro", new()
            {
                "Let us light a flame yet more radiant in our lives; for life is a candlelight, destined to snuff out one day."
            }, new() {  new() {
                "Let us light a flame yet more radiant in our lives; for life is a candlelight, destined to snuff out one day."
                }});
            CreateDialogueEvents("OrdealCrimsonDawnOutro", new()
            {
                "To live is to yearn and fight for our desires."
            }, new() { new() {
                "To live is to yearn and fight for our desires."
            }});
            CreateDialogueEvents("OrdealCrimsonNoonIntro", new()
            {
                "We marched from time to time, and we would share our pleasure."
            }, new() { new() {
                "We marched from time to time, and we would share our pleasure."
            }});
            CreateDialogueEvents("OrdealCrimsonNoonOutro", new()
            {
                "The collision of one life with another, skin harmonising, painting a yet more beautiful appearance."
            }, new() { new() {
                "The collision of one life with another, skin harmonising, painting a yet more beautiful appearance."
            }});
            CreateDialogueEvents("OrdealCrimsonDuskIntro", new()
            {
                "Throwing away our old bodies, we all become one, infinitely continuing the red march."
            }, new() { new() {
                "Throwing away our old bodies, we all become one, infinitely continuing the red march."
            }});
            CreateDialogueEvents("OrdealCrimsonDuskOutro", new()
            {
                "One day we will know, and tomorrow we will march hand in hand."
            }, new() { new() {
                "One day we will know, and tomorrow we will march hand in hand."
            }});
        }
        private void XDialogue_AmberOrdeal()
        {
            CreateDialogueEvents("OrdealAmberDawnIntro", new()
            {
                "A perfect meal, an excellent substitute."
            }, new() {  new() {
                "A perfect meal, an excellent substitute."
                }});
            CreateDialogueEvents("OrdealAmberDawnOutro", new()
            {
                "We ate incessantly to live. The inevitable diminution, the waste..."
            }, new() { new() {
                "We ate incessantly to live. The inevitable diminution, the waste..."
            }});
            CreateDialogueEvents("OrdealAmberDuskIntro", new()
            {
                "To accustom oneself to the taste was an inevitable process."
            }, new() { new() {
                "To accustom oneself to the taste was an inevitable process."
            }});
            CreateDialogueEvents("OrdealAmberDuskOutro", new()
            {
                "We could live. We could continue eating."
            }, new() { new() {
                "We could live. We could continue eating."
            }});
            CreateDialogueEvents("OrdealAmberMidnightIntro", new()
            {
                "They fought amongst themselves to eat the others."
            }, new() { new() {
                "They fought amongst themselves to eat the others."
            }});
            CreateDialogueEvents("OrdealAmberMidnightOutro", new()
            {
                "And the stronger side survived. That, simply, is the story."
            }, new() { new() {
                "And the stronger side survived. That, simply, is the story."
            }});
        }
        private void XDialogue_GreenOrdeal()
        {
            CreateDialogueEvents("OrdealGreenDawnIntro", new()
            {
                "One day, a question crossed through my mind. Where do we come from? We were given life and left in this world against our own volition."
            }, new() {  new() {
                "One day, a question crossed through my mind. Where do we come from? We were given life and left in this world against our own volition."
                }});
            CreateDialogueEvents("OrdealGreenDawnOutro", new()
            {
                "To live was a process full of pain."
            }, new() { new() {
                "To live was a process full of pain."
            }});
            CreateDialogueEvents("OrdealGreenNoonIntro", new()
            {
                "We will understand life and the soul with our own hands."
            }, new() { new() {
                "We will understand life and the soul with our own hands."
            }});
            CreateDialogueEvents("OrdealGreenNoonOutro", new()
            {
                "In the end, they were bound to life. We existed only to express despair and ire."
            }, new() { new() {
                "In the end, they were bound to life. We existed only to express despair and ire."
            }});
            CreateDialogueEvents("OrdealGreenDuskIntro", new()
            {
                "We constructed a looming tower to return whence we came."
            }, new() { new() {
                "We constructed a looming tower to return whence we came."
            }});
            CreateDialogueEvents("OrdealGreenDuskOutro", new()
            {
                "There wasn't an answer. We didn't find a single thing we wanted. We only witnessed the death of life itself."
            }, new() { new() {
                "There wasn't an answer. We didn't find a single thing we wanted. We only witnessed the death of life itself."
            }});
            CreateDialogueEvents("OrdealGreenMidnightIntro", new()
            {
                "The tower is touched by the sky, and it will leave nothing on the earth."
            }, new() { new() {
                "The tower is touched by the sky, and it will leave nothing on the earth."
            }});
            CreateDialogueEvents("OrdealGreenMidnightOutro", new()
            {
                "Who pays for the suffering and regret of the lives given to us?"
            }, new() { new() {
                "Who pays for the suffering and regret of the lives given to us?"
            }});
        }
    }
}
