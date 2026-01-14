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
        private static void AddGodRed() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "The God Red";
            info.rulebookDescription = "";
            info.powerLevel = 5;

            GodRed.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(GodRed), TextureLoader.LoadTextureFromFile("sigilGodRed.png")).Id;
        }
    }

    public class GodRed : GodColourAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override IEnumerator PreActivate() {
            throw new System.NotImplementedException();
        }
        public override IEnumerator Activate() {
            throw new System.NotImplementedException();
        }

        public void OnDestroy() {

        }
    }
}
