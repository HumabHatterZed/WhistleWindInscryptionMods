using APIPlugin;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Volatile()
        {
            const string rulebookName = "Volatile";
            const string rulebookDescription = "When this card dies, adjacent and opposing cards are dealt 10 damage.";
            const string dialogue = "An explosive finish.";

            return WstlUtils.CreateAbility<Volatile>(
                Resources.sigilVolatile,
                rulebookName, rulebookDescription, dialogue, 0, addModular: true);
        }
    }
    public class Volatile : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToPreDeathAnimation(bool wasSacrifice)
        {
            return base.Card.OnBoard && !wasSacrifice;
        }
        public override IEnumerator OnPreDeathAnimation(bool wasSacrifice)
        {
            base.Card.Anim.LightNegationEffect();
            yield return base.PreSuccessfulTriggerSequence();
            yield return this.ExplodeFromSlot(base.Card.Slot);
            yield return base.LearnAbility(0.25f);
        }

        protected IEnumerator ExplodeFromSlot(CardSlot slot)
        {
            List<CardSlot> adjacentSlots = Singleton<BoardManager>.Instance.GetAdjacentSlots(slot);
            if (adjacentSlots.Count > 0 && adjacentSlots[0].Index < slot.Index)
            {
                if (adjacentSlots[0].Card != null && !adjacentSlots[0].Card.Dead)
                {
                    yield return this.BombCard(adjacentSlots[0].Card, slot.Card);
                }
                adjacentSlots.RemoveAt(0);
            }
            if (slot.opposingSlot.Card != null && !slot.opposingSlot.Card.Dead)
            {
                yield return this.BombCard(slot.opposingSlot.Card, slot.Card);
            }
            if (adjacentSlots.Count > 0 && adjacentSlots[0].Card != null && !adjacentSlots[0].Card.Dead)
            {
                yield return this.BombCard(adjacentSlots[0].Card, slot.Card);
            }
        }

        private IEnumerator BombCard(PlayableCard target, PlayableCard attacker)
        {
            //target.Anim.PlayHitAnimation();
            yield return target.TakeDamage(10, attacker);
            yield return new WaitForSeconds(0.15f);
        }
    }
}
