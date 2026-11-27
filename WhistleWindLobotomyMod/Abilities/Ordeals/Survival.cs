using Core.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using Pixelplacement;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddSurvival() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Survival";
            info.rulebookDescription = "At the start of every other turn for the owner, this card creates a Food Chain in empty adjacent spaces. [define:wstl_foodChain]";
            info.powerLevel = 4;
            info.passive = true;
            Survival.ID = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(Survival), TextureLoader.LoadTextureFromFile("sigilSurvival.png")).Id;
        }
    }

    public class Survival : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
    }
}
