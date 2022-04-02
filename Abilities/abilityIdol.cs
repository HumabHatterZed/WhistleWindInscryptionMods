using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Idol()
        {
            const string rulebookName = "Idol";
            const string rulebookDescription = "While this card is on the board, all opposing cards lose 1 Power.";
            const string dialogue = "My beasts defer to you.";

            Idol.ability = WstlUtils.CreateAbility<Idol>(
                Resources.sigilIdol,
                rulebookName, rulebookDescription, dialogue, 5).Id;
        }
    }
    public class Idol : WstlAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool ProvidesPassiveAttackBuffOpponent => true;
        public override int[] GetPassiveAttackBuffsOpponent()
        {
            List<int> slots = new() { 0, 0, 0, 0 };
            foreach (CardSlot slot in (base.Card.OpponentCard ? Singleton<BoardManager>.Instance.playerSlots : Singleton<BoardManager>.Instance.opponentSlots))
            {
                if (slot.Card != null)
                {
                    slots[slot.Index] -= 1;
                }
            }
            return slots.ToArray();
        }
        public override bool RespondsToResolveOnBoard()
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.LearnAbility(0.5f);
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.LearnAbility(0.5f);
        }
        public bool ActivateOnPlay()
        {
            int num = 0;
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(!base.Card.Slot.IsPlayerSlot))
            {
                if (slot.Card != null)
                {
                    num++;
                }
            }
            return num > 0;
        }
    }
}
