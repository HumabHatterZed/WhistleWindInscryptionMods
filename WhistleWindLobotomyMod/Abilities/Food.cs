using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class Food : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool hasResolved = false;

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard()
        {
            hasResolved = true;
            return base.OnResolveOnBoard();
        }
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) => hasResolved && otherCard == base.Card;
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            return base.OnOtherCardAssignedToSlot(otherCard);
        }
    }

    public partial class LobotomyPlugin
    {
        private void Ability_Food()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Food";
            info.rulebookDescription = "Whenever [creature] moves to a new space, create a Perfect Food in the old space. [define:wstl_perfectFood]";
            info.powerLevel = 3;
            Food.ability = AbilityManager.Add(pluginGuid, info, typeof(Food), TextureLoader.LoadTextureFromFile("sigilFood.png")).Id;
        }
    }
}
