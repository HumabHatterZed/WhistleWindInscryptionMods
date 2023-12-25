using InscryptionAPI.Dialogue;
using System.Collections.Generic;

namespace WhistleWind.AbnormalSigils.Core
{
    public class AbnormalDialogueManager // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static Dictionary<string, List<CustomLine>> EventNames => new()
        {
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
                new() {
                    "She saw her dear brothers in the distance.",
                    "Her family that needed the nettle clothing to be free of the curse.",
                    "She fell to the ground, vomiting ooze like the rest of [c:bR]the City[c:]." }},
            { "NettlesFail",
                new() { "The lake ripples gently. As if a number of swans just took flight." }},
            { "RegeneratorOverheal",
                new() { "The punishment for greed is getting everything you wanted." }},
            { "SerpentsNestInfection",
                new() { "A new nest is born." }}
        };

        public static void GenerateDialogueEvents()
        {
            foreach (KeyValuePair<string, List<CustomLine>> dialogue in EventNames)
                DialogueManager.GenerateEvent(AbnormalPlugin.pluginGuid, dialogue.Key, dialogue.Value, defaultSpeaker: DialogueEvent.Speaker.Single);
        }
    }
}
