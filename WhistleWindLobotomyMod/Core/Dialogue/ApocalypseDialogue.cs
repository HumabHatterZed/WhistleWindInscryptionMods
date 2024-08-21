using DiskCardGame;
using HarmonyLib;
using Sirenix.Utilities;
using System.Linq;
using static WhistleWind.Core.Helpers.DialogueHelper;
using static WhistleWindLobotomyMod.Core.DialogueEventsManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyDialogue
    {
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
                new() { " The Long Bird condemned the wicked..." });
            CreateDialogueEvents("ApocalypseBossArmsPostAttack",
                new() { "...yet sins continue without end." });

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
                    "I will reduce their cost over time, until they become [c:bR]free[c:] to replay." },
                new()
                {
                    new() { "Use these to reposition your creatures, for a cost." }
                });
            CreateDialogueEvents("ApocalypseBossExhausted",
                new() { "Why don't we continue a little longer?" });
            CreateDialogueEvents("ApocalypseBossBoneGain",
                new() {
                    "The beast is immune to mortal damage.",
                    "Excess injury will instead yield you up to [c:bR]2[c:] bones.",
                    "Don't worry about running dry, the ground you walk on is full of them."
                },
                new()
                {
                    new() { "The ground is full of bones." }
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
    }
}
