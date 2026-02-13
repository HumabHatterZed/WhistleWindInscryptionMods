using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddGodWhite() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "The God White";
            info.rulebookDescription = "Activate: Inflict Reverence onto a";
            info.powerLevel = 5;

            GodWhite.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(GodWhite), TextureLoader.LoadTextureFromFile("sigilGodWhite.png")).Id;
        }
    }

    public class GodWhite : GodColourAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        protected override IEnumerator PreActivate(bool halfHealth) {
            yield break;
        }
        protected override IEnumerator Activate(bool halfHealth) {
            yield break;
        }

        public override void SetUpVisualGameObject() {
            // stub
        }

        protected override IEnumerator CleanUpVisuals() {
            //ShowEye(false);
            yield return new WaitForSeconds(0.5f);
            Destroy(activateVisualGameObject);
        }
    }
}
