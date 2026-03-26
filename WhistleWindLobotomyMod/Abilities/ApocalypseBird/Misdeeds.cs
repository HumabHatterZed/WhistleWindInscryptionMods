using DiskCardGame;
using InscryptionAPI.Card;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddMisdeeds() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Misdeeds Not Allowed!";
            info.rulebookDescription = "Whenever [creature] takes damage, it gains 1 Power until the end of the owner's turn.";
            info.powerLevel = 0;
            info.passive = true;
            Misdeeds.ID = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, null, TextureLoader.LoadTextureFromFile("sigilMisdeeds.png")).Id;

        }
    }

    public class Misdeeds {
        public static Ability ID { get; internal set; }
    }
}
