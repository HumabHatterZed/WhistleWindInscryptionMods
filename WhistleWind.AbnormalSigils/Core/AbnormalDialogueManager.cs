using InscryptionAPI.Dialogue;
using System.Collections.Generic;

namespace WhistleWind.AbnormalSigils.Core
{
    public class AbnormalDialogueManager // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static Dictionary<string, List<CustomLine>> EventNames => new()
        {
            { "LearnStatusEffects",
                new() {
                    "Ah, this beast has been afflicted with a [c:bR]status effect[c:].",
                    "When this battle ends, it will be cleared from its host." }
            },
            { "LearnStatusEffectsOverflow",
                new() {
                    "It seems this beast is laden with too many [c:bR]status effects[c:].",
                    "You can see the excess by interacting with the rightmost status." }
            },

            { "CopycatFail",
                new() { "Some things in this world are too unique to replicate." }},
            { "CopycatDead",
                new() { "The lie falls apart, revealing your pitiful true self." }},
            { "CourageousFail",
                new() { "Your creature's consitution is too weak." }},
            { "CourageousRefuse",
                new() { "Cowards don't get the boon of the brave." }},
            { "CowardlySelfLove",
                new() { "Love yourself." }},
            { "CowardlyWeaken",
                new() { "Your beast's moxie withers away." }},
            { "CursedFail",
                new() { "The curse abates." }},
            { "FrostRulerKiss",
                new() { "With a single kiss, the Snow Queen froze their hearts." }},
            { "FrostRulerFail",
                new() { "The snow melts away. Perhaps spring is coming." }},
            { "LonelyDie",
                new() { "Why do they always leave?" }},
            { "NettlesDie",
                new() { "These clothes were meant to free us of the curse." }},
            { "RegeneratorOverheal",
                new() { "The punishment for greed is getting everything you wanted." }},
            { "SerpentsNestInfection",
                new() { "A new nest is born." }},
            { "ScorchingExtinguished",
                new() { "The flood waters extinguish the raging fire." }},
            { "FloodedSlotDried",
                new() { "The water evaporates in the scorching heat." }}
        };

        public static void GenerateDialogueEvents()
        {
            foreach (KeyValuePair<string, List<CustomLine>> dialogue in EventNames)
                DialogueManager.GenerateEvent(AbnormalPlugin.pluginGuid, dialogue.Key, dialogue.Value, defaultSpeaker: DialogueEvent.Speaker.Single);
        }
    }
}
