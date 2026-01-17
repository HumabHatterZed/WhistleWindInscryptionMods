using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddGodWhite() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "The God White";
            info.rulebookDescription = "axe";
            info.powerLevel = 5;

            GodWhite.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(GodWhite), TextureLoader.LoadTextureFromFile("sigilGodWhite.png")).Id;
        }
    }

    public class GodWhite : GodColourAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override IEnumerator PreActivate() {
            throw new System.NotImplementedException();
        }
        public override IEnumerator Activate() {
            throw new System.NotImplementedException();
        }
    }
}
