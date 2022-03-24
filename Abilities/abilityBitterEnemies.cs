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
        private void Ability_BitterEnemies()
        {
            const string rulebookName = "Bitter Enemies";
            const string rulebookDescription = "A card bearing this sigil gains 1 Power when another card on this board also has this sigil.";
            const string dialogue = "A bitter grudge laid bare.";

            BitterEnemies.ability = WstlUtils.CreateAbility<BitterEnemies>(
                Resources.sigilBitterEnemies,
                rulebookName, rulebookDescription, dialogue, 2,
                addModular: true).Id;
        }
    }
    public class BitterEnemies : WstlAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool ProvidesPassiveAttackBuffAlly => true;
        public override bool ProvidesPassiveAttackBuffOpponent => true;
        public override int[] GetPassiveAttackBuffsAlly()
        {
            List<int> slots = new() { 0, 0, 0, 0 };
            foreach (CardSlot slot in (base.Card.OpponentCard ? Singleton<BoardManager>.Instance.opponentSlots : Singleton<BoardManager>.Instance.playerSlots).Where((CardSlot s) => s.Card != null))
            {
                if (slot != base.Card.Slot && slot.Card.HasAbility(BitterEnemies.ability))
                {
                    slots[slot.Index] += 1;
                }
            }
            return slots.ToArray();
        }
        public override int[] GetPassiveAttackBuffsOpponent()
        {
            List<int> slots = new() { 0, 0, 0, 0 };
            foreach (CardSlot slot in (base.Card.OpponentCard ? Singleton<BoardManager>.Instance.playerSlots : Singleton<BoardManager>.Instance.opponentSlots).Where((CardSlot s) => s.Card != null))
            {
                if (slot.Card.HasAbility(BitterEnemies.ability))
                {
                    slots[slot.Index] += 1;
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
            yield return base.LearnAbility(0.4f);
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.LearnAbility(0.4f);
        }
        public bool ActivateOnPlay()
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot != base.Card.Slot))
            {
                if (slot.Card != null && slot.Card.HasAbility(BitterEnemies.ability))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
