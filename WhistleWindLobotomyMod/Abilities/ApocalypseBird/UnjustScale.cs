using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddUnjustScale() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Unjust Scale";
            info.rulebookDescription = "At the end of the owner's turn, all other cards gain 1 Sin. At the start of the owner's turn, cards with 3+ Sin will perish. If Long Arms is defeated, this effect changes.";
            info.powerLevel = 0;
            info.passive = true;
            UnjustScale.ID = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, null, TextureLoader.LoadTextureFromFile("sigilUnjustScale.png"))
                .SetAbilityRedirect("Sin", Sin.iconId, GameColors.Instance.red).Id;
        }
    }

    public class UnjustScale {
        public static Ability ID { get; internal set; }
    }
}
