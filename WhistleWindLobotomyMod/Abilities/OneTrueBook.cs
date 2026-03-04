using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddOneTrueBook() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "The One True Book";
            info.rulebookDescription = "";
            info.powerLevel = 5;

            OneTrueBook.ID = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(OneTrueBook), TextureLoader.LoadTextureFromFile("sigilOneTrueBook.png")).Id;
        }
    }

    public class OneTrueBook : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

    }
}
