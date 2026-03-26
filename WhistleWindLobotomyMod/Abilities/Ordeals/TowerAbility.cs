using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddTower() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "The Tower";
            info.rulebookDescription = "While this card is in an active state, the Light of the End will target spaces on the player's side of the board.";
            info.powerLevel = 5;
            info.passive = true;

            TowerAbility.ID = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, null, TextureLoader.LoadTextureFromFile("sigilTower.png")).Id;
            // cannot add redirects until after the info's been added to the API
            info.SetUniqueRedirect("Light of the End", "wstl:Ordeals_Light of the End", Color.green);
        }
    }

    public class TowerAbility {
        public static Ability ID { get; internal set; }
    }
}
