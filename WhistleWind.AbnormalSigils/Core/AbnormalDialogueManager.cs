using DiskCardGame;
using InscryptionAPI.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.AbnormalSigils.Core
{
    public static class AbnormalDialogueManager // Base code taken from GrimoraMod and SigilADay_julienperge
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
                DialogueEventGenerator.GenerateEvent(dialogue.Key, dialogue.Value, defaultSpeaker: DialogueEvent.Speaker.Single);
            }
        }

        public static Dictionary<string, List<CustomLine>> EventNames => new()
        {
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
                    "She saw her dear brothers in the distance. Her family that needed the nettle clothing to be free of the curse.",
                    "She fell to the ground, vomiting ooze like the rest of [c:bR]the City[c:]."
                }
            },
            { "NettlesFail", new() { "The lake ripples gently. As if a number of swans just took flight." } },
            { "RegeneratorOverheal", new() { "The punishment for greed is getting everything you wanted." } }
        };
    }
}
