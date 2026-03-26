using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddLongArms() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Long Arms";
            info.rulebookDescription = "[creature] is immune to status ailments. While this card is on the board, time cannot be altered.";
            info.powerLevel = 0;
            info.passive = true;
            LongArms.ID = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, null, TextureLoader.LoadTextureFromFile("sigilLongArms.png"))
                .SetItemRedirect("time cannot be altered", "Hourglass", GameColors.Instance.red)
                .Id;
        }
    }

    public class LongArms {
        public static Ability ID { get; internal set; }
    }
}
