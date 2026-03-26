using DiskCardGame;
using InscryptionAPI.Card;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddSmallBeak() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Small Beak";
            info.rulebookDescription = "At the start of the player's turn, target a random lane on the board.  At the start of the player's next turn, kill all cards in the targeted lane, excluding this card.";
            info.powerLevel = 0;
            info.passive = true;
            SmallBeak.ID = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, null, TextureLoader.LoadTextureFromFile("sigilSmallBeak.png")).Id;
        }
    }

    public class SmallBeak {
        public static Ability ID { get; internal set; }
    }
}
