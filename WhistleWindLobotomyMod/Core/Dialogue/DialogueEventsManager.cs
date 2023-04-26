using InscryptionAPI.Dialogue;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Core
{
    public static class DialogueEventsManager // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static Dictionary<string, List<CustomLine>> DialogueEvents { get; internal set; }
        public static Dictionary<string, List<List<CustomLine>>> RepeatDialogueEvents { get; internal set; }

        public static void CreateDialogueEvents(string key, List<CustomLine> dialogue, List<List<CustomLine>> repeatDialogue = null)
        {
            DialogueEvents.Add(key, dialogue);
            if (repeatDialogue != null)
                RepeatDialogueEvents.Add(key, repeatDialogue);
        }
    }
}
