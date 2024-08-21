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
        public static IEnumerator PlayDialogueEvent(string name, float waitFor = 0.2f, PlayableCard card = null, bool repeatLines = false)
        {
            if (!DialogueEventsData.EventIsPlayed(name) || repeatLines)
            {
                if (!SaveManager.SaveFile.IsPart2)
                    yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent(name, TextDisplayer.MessageAdvanceMode.Input);
                else
                {
                    CardTemple temple = card != null ? card.Info.temple : CardTemple.NUM_TEMPLES;
                    yield return Singleton<DialogueHandler>.Instance.PlayDialogueEvent(
                        name, (TextBox.Style)temple, GBCScrybe(temple));
                }

                if (waitFor > 0f)
                    yield return new WaitForSeconds(waitFor);
            }
        }

        public static IEnumerator PlayAlternateDialogue(params string[] dialogue)
        {
            foreach (string s in dialogue)
            {
                yield return ShowUntilInput(s, Emotion.Neutral, DialogueEvent.Speaker.Leshy, style: TextBox.Style.Nature, screenPosition: TextBox.ScreenPosition.ForceBottom, delay: 0.2f);
            }
        }

        public static IEnumerator PlayAlternateDialogue(
            Emotion emotion = Emotion.Neutral,
            DialogueEvent.Speaker speaker = DialogueEvent.Speaker.Leshy,
            float delay = 0.2f,
            params string[] dialogue)
        {
            foreach (string s in dialogue)
            {
                yield return ShowUntilInput(s, emotion, speaker, style: TextBox.Style.Nature, screenPosition: TextBox.ScreenPosition.ForceBottom, delay: delay);
            }
        }

        public static IEnumerator ShowUntilInput(
            string message,
            Emotion emotion = Emotion.Neutral,
            DialogueEvent.Speaker speaker = DialogueEvent.Speaker.Leshy,
            float effectFOVOffset = -2.5f,
            float effectEyelidIntensity = 0.5f,
            TextBox.Style style = TextBox.Style.Neutral,
            TextBox.ScreenPosition screenPosition = TextBox.ScreenPosition.OppositeOfPlayer,
            float delay = 0.2f)
        {
            yield return new WaitForSeconds(delay);
            if (SaveManager.SaveFile.IsPart2)
            {
                yield return Singleton<TextBox>.Instance.ShowUntilInput(message, style, null, screenPosition);
            }
            else
            {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(message, effectFOVOffset, effectEyelidIntensity, emotion, speaker: speaker);
            }
            yield return new WaitForSeconds(delay);
        }

        public static DialogueSpeaker.Character GetGBCCharacter(DialogueEvent.Speaker speaker)
        {
            return speaker switch
            {
                DialogueEvent.Speaker.Leshy => DialogueSpeaker.Character.Leshy,
                DialogueEvent.Speaker.Stoat => DialogueSpeaker.Character.Stoat,
                DialogueEvent.Speaker.Stinkbug => DialogueSpeaker.Character.Grimora,
                DialogueEvent.Speaker.Mushroom => DialogueSpeaker.Character.Mycologists,
                DialogueEvent.Speaker.P03 => DialogueSpeaker.Character.P03,
                DialogueEvent.Speaker.Goo => DialogueSpeaker.Character.GreenWizard,
                DialogueEvent.Speaker.Trader => DialogueSpeaker.Character.Trader,
                DialogueEvent.Speaker.P03Archivist => DialogueSpeaker.Character.P03,
                DialogueEvent.Speaker.P03Photographer => DialogueSpeaker.Character.P03,
                DialogueEvent.Speaker.P03Telegrapher => DialogueSpeaker.Character.P03,
                DialogueEvent.Speaker.P03Canvas => DialogueSpeaker.Character.P03,
                DialogueEvent.Speaker.P03Librarians => DialogueSpeaker.Character.P03,
                DialogueEvent.Speaker.P03BountyHunter => DialogueSpeaker.Character.P03,
                DialogueEvent.Speaker.P03CanvasAlt => DialogueSpeaker.Character.P03,
                DialogueEvent.Speaker.Grimora => DialogueSpeaker.Character.Grimora,
                DialogueEvent.Speaker.Magnificus => DialogueSpeaker.Character.Magnificus,
                DialogueEvent.Speaker.AnglerTalkingCard => DialogueSpeaker.Character.Angler,
                DialogueEvent.Speaker.P03MycologistMain => DialogueSpeaker.Character.Mycologists,
                DialogueEvent.Speaker.P03MycologistSide => DialogueSpeaker.Character.Mycologists,
                DialogueEvent.Speaker.Bonelord => DialogueSpeaker.Character.BonelordDark,
                DialogueEvent.Speaker.PirateSkull => DialogueSpeaker.Character.GhoulRoyal,
                _ => DialogueSpeaker.Character.Leshy
            };
        }
        public static DialogueSpeaker GetGBCSpeaker(DialogueEvent.Speaker speaker)
        {
            return InBattleDialogueSpeakers.Instance.GetSpeaker(GetGBCCharacter(speaker));
        }

        public static DialogueSpeaker GBCScrybe(CardTemple temple)
        {
            return Singleton<InBattleDialogueSpeakers>.Instance.GetSpeaker(temple switch
            {
                CardTemple.Undead => DialogueSpeaker.Character.Grimora,
                CardTemple.Wizard => DialogueSpeaker.Character.Magnificus,
                CardTemple.Tech => DialogueSpeaker.Character.P03,
                _ => DialogueSpeaker.Character.Leshy
            });
        }
        public static DialogueSpeaker GBCScrybe()
        {
            if (!SaveManager.SaveFile.IsPart2)
                return null;

            if (StoryEventsData.EventCompleted(StoryEvent.GBCUndeadAmbition))
            {
                return Singleton<InBattleDialogueSpeakers>.Instance.GetSpeaker(DialogueSpeaker.Character.Grimora);
            }
            else if (StoryEventsData.EventCompleted(StoryEvent.GBCNatureAmbition))
            {
                return Singleton<InBattleDialogueSpeakers>.Instance.GetSpeaker(DialogueSpeaker.Character.Leshy);
            }
            else if (StoryEventsData.EventCompleted(StoryEvent.GBCTechAmbition))
            {
                return Singleton<InBattleDialogueSpeakers>.Instance.GetSpeaker(DialogueSpeaker.Character.P03);
            }
            else
            {
                return Singleton<InBattleDialogueSpeakers>.Instance.GetSpeaker(DialogueSpeaker.Character.Magnificus);
            }
        }
    }
}
