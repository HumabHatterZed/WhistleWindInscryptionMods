using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddFood() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Food";
            info.rulebookDescription = "Whenever [creature] moves to a new space, create a Perfect Food in the old space. [define:wstl_foodPerfect]";
            info.powerLevel = 3;
            Food.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(Food), TextureLoader.LoadTextureFromFile("sigilFood.png")).Id;
        }
    }

    /// <summary>
    /// Whenever [creature] moves to a new space, create a Perfect Food in the old space. [define:wstl_foodPerfect]
    /// </summary>
    public class Food : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        private CardSlot oldSlot;

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            oldSlot = base.Card.Slot;
            return base.OnResolveOnBoard();
        }
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) => otherCard == base.Card && oldSlot != null;
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard) {
            if (oldSlot.Card == null)
                yield return oldSlot.CreateCardInSlot(CardLoader.GetCardByName(Cards.perfectFood));
        }
    }
}
