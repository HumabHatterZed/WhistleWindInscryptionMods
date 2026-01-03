using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddTower() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "The Tower";
            info.rulebookDescription = "This card enters an active state every few turns. While active, two Helix Lights will appear on the player's side of the board.";
            info.powerLevel = 5;
            info.passive = true;

            Tower.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(Tower), TextureLoader.LoadTextureFromFile("sigilTower.png")).Id;
            info.SetUniqueRedirect("Helix Lights", "wstl:Ordeals_Helix Light", Color.green);
        }
    }

    public class Tower : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
