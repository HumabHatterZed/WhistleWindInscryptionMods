using DiskCardGame;
using GBC;
using InscryptionAPI.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.AbnormalSigils.Core
{
    public class AbnormalDialogueManager // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static Dictionary<string, List<CustomLine>> EventNames => new()
        {
            { "CopycatFail", new() { "The lie falls apart, revealing your pitiful true self." } },
            { "CourageousFail", new() { "Your creature's consitution is too weak." } },
            { "CourageousRefuse", new() { "Cowards don't get the boon of the brave." } },
            { "CowardlySelfLove", new() { "Love yourself." } },
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
            { "RegeneratorOverheal", new() { "The punishment for greed is getting everything you wanted." } },
            { "SerpentsNestInfection", new() { "A new nest is born." } }
        };

        /// <summary>
        /// Shorthand method for playing dialogue events.
        /// Checks if the inputted dialogue event has been played, then plays it if it hasn't been yet.
        /// </summary>
        /// <param name="name">Name of dialogue event to play.</param>
        public static IEnumerator PlayDialogueEvent(string name, float waitFor = 0.2f)
        {
            if (!DialogueEventsData.EventIsPlayed(name))
            {
                if (!SaveManager.SaveFile.IsPart2)
                    yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent(name, TextDisplayer.MessageAdvanceMode.Input);
                else
                    yield return Singleton<DialogueHandler>.Instance.PlayDialogueEvent(name, TextBox.Style.Neutral, GBCScrybe);

                yield return new WaitForSeconds(waitFor);
            }
        }

        public static void GenerateDialogueEvents()
        {
            foreach (KeyValuePair<string, List<CustomLine>> dialogue in EventNames)
                DialogueManager.GenerateEvent(AbnormalPlugin.pluginGuid, dialogue.Key, dialogue.Value, defaultSpeaker: DialogueEvent.Speaker.Single);
        }

        public static DialogueSpeaker GBCScrybe
        {
            get
            {
                if (SaveManager.SaveFile.IsPart2)
                {
                    if (StoryEventsData.EventCompleted(StoryEvent.GBCUndeadAmbition))
                        return Singleton<InBattleDialogueSpeakers>.Instance.GetSpeaker(DialogueSpeaker.Character.Grimora);

                    else if (StoryEventsData.EventCompleted(StoryEvent.GBCNatureAmbition))
                        return Singleton<InBattleDialogueSpeakers>.Instance.GetSpeaker(DialogueSpeaker.Character.Leshy);

                    else if (StoryEventsData.EventCompleted(StoryEvent.GBCTechAmbition))
                        return Singleton<InBattleDialogueSpeakers>.Instance.GetSpeaker(DialogueSpeaker.Character.P03);

                    else
                        return Singleton<InBattleDialogueSpeakers>.Instance.GetSpeaker(DialogueSpeaker.Character.Magnificus);
                }
                else
                    return null;
            }
        }
    }
}
