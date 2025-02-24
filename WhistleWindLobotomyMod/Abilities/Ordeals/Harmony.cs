using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class Abilities
    {
        private static void AddHarmony()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Harmony";
            info.rulebookDescription = "When [creature] dies, two Cheers for the Beginning are created on the owner's side of the board [define:wstl_skinCheers]";
            info.powerLevel = 3;
            HarmonyAbility.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(HarmonyAbility), TextureLoader.LoadTextureFromFile("sigilHarmony.png")).Id;
        }
    }

    public class HarmonyAbility : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !wasSacrifice;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            List<CardSlot> openSlots = BoardManager.Instance.GetOpenSlots(!base.Card.OpponentCard);
            if (!openSlots.Contains(base.Card.Slot))
                openSlots.Add(base.Card.Slot);

            openSlots.Randomize();
            if (openSlots.Count > 2) openSlots.RemoveRange(2, openSlots.Count - 2);

            foreach (CardSlot slot in openSlots)
            {
                yield return BoardManager.Instance.CreateCardInSlot(CardLoader.GetCardByName(Cards.skinHarmony), slot);
            }
        }
    }
}
