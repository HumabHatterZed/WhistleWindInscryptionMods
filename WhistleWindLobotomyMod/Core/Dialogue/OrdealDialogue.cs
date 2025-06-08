using System.Collections.Generic;
using WhistleWindLobotomyMod.Opponents;
using static WhistleWindLobotomyMod.Core.DialogueEventsManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyDialogue
    {
        private void Dialogue_FirstOrdeal()
        {
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

        public static Dictionary<OrdealType, List<string>> BannerIntroDescriptions = new() {
            {
                OrdealType.Green, new() {
                    "One day, a question crossed through my mind: Where do we come from? We were given life and left in this world against our own volition.",
                    "We will understand life and the soul with our own hands.",
                    "We constructed a looming tower to return whence we came.",
                    "The tower is touched by the sky, and it will leave nothing on the earth."
                }},
            {
                OrdealType.Violet, new() {
                    "To gain an understanding of what is incomprehensible, they dream, staring.",
                    "We could only hear the weakest and faintest of their acts. We sought for love and compassion from them.",
                    null,
                    "We incessantly tried to accept it. We wanted to understand them in our heads by any means, regardless of the consequences."
                }},
            {
                OrdealType.Crimson, new() {
                    "Let us light a flame yet more radiant in our lives; for life is a candlelight, destined to snuff out one day.",
                    "We marched from time to time, and we would share our pleasure.",
                    "Throwing away our old bodies, we all become one, infinitely continuing the red march.",
                    null
                }},
            {
                OrdealType.Amber, new() {
                    "A perfect meal, an excellent substitute.",
                    null,
                    "To accustom oneself to the taste was an inevitable process.",
                    "They fought amongst themselves to eat the others."
                }},
            {
                OrdealType.Indigo, new() {
                    null,
                    "When night falls in the Backstreets, they will come.",
                    null,
                    null
                }},
            {
                OrdealType.White, new() {
                    "From meaningless errands, to exploration, to contract killing; they will do whatever you wish, so long as you pay them sufficiently.",
                    "They search constantly, be it for the Backers of the Wings, the Inventions of the Backstreets, the Relics of the Outskirts, the Artefacts of the Ruins...",
                    "The colossal tower of light was titled the Library. It is only natural for the Fixers to be drawn to such a mystic place of life and death.",
                    "To know and manipulate all the secrets of the world: that is the privilege of the Head, the Eye, and the Claws. It is their honour and absolute power."
                }},
        };

        public static Dictionary<OrdealType, List<string>> BannerOutroDescriptions = new() {
            {
                OrdealType.Green, new() {
                    "To live was a process full of pain.",
                    "In the end, they were bound to life. We existed only to express despair and ire.",
                    "There wasn't an answer. We didn't find a single thing we wanted. We only witnessed the death of life itself.",
                    "Who pays for the suffering and regret of the lives given to us?"
                }},
            {
                OrdealType.Violet, new() {
                    "They complied with nothing in their bid to understand. They simply did so.",
                    "We cannot understand them, nor will they understand us.",
                    null,
                    "For the sake of not crumbling in on oneself. The idea that they may impossibly exist, or that they are unreachable and forever enigmatic no matter the path. Unacceptable..."
                }},
            {
                OrdealType.Crimson, new() {
                    "To live is to yearn and fight for our desires.",
                    "The collision of one life with another, skin harmonising, painting a yet more beautiful appearance.",
                    "One day we will know, and tomorrow we will march hand in hand.",
                    null
                }},
            {
                OrdealType.Amber, new() {
                    "We ate incessantly to live. The inevitable diminution, the waste...",
                    null,
                    "We could live. We could continue eating.",
                    "And the stronger side survived. That, simply, is the story."
                }},
            {
                OrdealType.Indigo, new() {
                    null,
                    "When the sun rises anew, not a scrap will remain.",
                    null,
                    null
                }},
            {
                OrdealType.White, new() {
                    "They work in the Offices, Syndicates, and the Wings. Their work varies from banal things to something truly sublime.",
                    "As they have always done, they will overcome all that impedes them, weapons in hand.",
                    "Bookhunters... One day they will rummage through the Library reigned over by the Pale Librarian. They are what will become of the Fixers.",
                    "No one dares to stand against them. As long as they exist, the tale of the Nest will never reach its close."
                }},
        };

    }
}
