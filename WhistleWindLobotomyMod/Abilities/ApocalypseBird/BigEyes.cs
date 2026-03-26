using DiskCardGame;
using InscryptionAPI.Card;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddBigEyes() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Big Eyes";
            info.rulebookDescription = "While [creature] is on the board, all creatures are unaffected by Power-changing effects.";
            info.powerLevel = 0;
            info.passive = true;
            BigEyes.ID = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, null, TextureLoader.LoadTextureFromFile("sigilBigEyes.png")).Id;
        }
    }

    public class BigEyes {
        public static Ability ID { get; internal set; }
    }
}
