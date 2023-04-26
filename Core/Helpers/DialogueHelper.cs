using DiskCardGame;
using GBC;
using InscryptionAPI.Dialogue;
using System.Collections;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class DialogueHelper
    {
        public static CustomLine NewLine(string dialogue, Emotion emotion) => new() { text = dialogue, emotion = emotion };
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
