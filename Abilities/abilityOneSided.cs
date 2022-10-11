using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_OneSided()
        {
            const string rulebookName = "One-Sided Strike";
            const string rulebookDescription = "When [creature] strikes a card, deal 1 additional damage if the struck card cannot attack this card.";
            const string dialogue = "Catch them unawares.";
            OneSided.ability = AbilityHelper.CreateAbility<OneSided>(
                Artwork.sigilOneSided, Artwork.sigilOneSided_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                addModular: true, opponent: true, canStack: true, isPassive: false).Id;
        }
    }
    public class OneSided : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target)
        {
            if (amount > 0 && target != null && !target.Dead)
            {
                return CheckValid(target);
            }
            return false;
        }

        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            target.Status.damageTaken++;
            if (target.Health <= 0)
            {
                yield return target.Die(wasSacrifice: false, base.Card);
            }
            yield return base.LearnAbility(0.4f);
        }
        private bool CheckValid(PlayableCard target)
        {
            // if this card can submerge, return true by default
            if (base.Card.HasAnyOfAbilities(Ability.Submerge, Ability.SubmergeSquid))
                return true;

            // if this card doesn't have Sniper or Marksman
            if (!base.Card.HasAnyOfAbilities(Ability.Sniper, Marksman.ability))
            {
                // if this card has Bi or Tri Strike, check whether the opponent has it too
                if (base.Card.HasAbility(Ability.SplitStrike) || base.Card.HasTriStrike())
                    return !(target.HasAbility(Ability.SplitStrike) || target.HasTriStrike());

                // otherwise, return whether the opponent can attack this card (won't attack directly or is blocked)
                return target.CanAttackDirectly(base.Card.Slot) || target.AttackIsBlocked(base.Card.Slot);
            }
            // if the target is opposing this card
            if (target.Slot == base.Card.Slot.opposingSlot)
                return target.CanAttackDirectly(base.Card.Slot) || target.AttackIsBlocked(base.Card.Slot);

            // if the target is in an opposing adjacent slot
            if (Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot.opposingSlot).Contains(target.Slot))
                return !(target.HasAbility(Ability.SplitStrike) || target.HasTriStrike());

            return true;
        }
    }
}
