using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddApocalypse() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Black Forest Guardians";
            info.rulebookDescription = "This card changes its combat pattern every three turns. At 80/60/40 Health, change pattern and the previous pattern cannot used again.";
            info.powerLevel = 0;
            info.passive = true;
            ApocalypseAbility.ID = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, null, TextureLoader.LoadTextureFromFile("sigilApocalypse.png")).Id;
        }
    }

    public class ApocalypseAbility {
        public static Ability ID { get; internal set; }
    }
}
