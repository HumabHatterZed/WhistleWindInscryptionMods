using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Wedge() {
            const string rulebookName = "Shove Aside";
            const string rulebookDescription = "Creatures struck by [creature] are pushed to an adjacent space.";
            const string dialogue = "How rude.";
            Wedge.ability = AbnormalAbilityHelper.CreateAbility<Wedge>(
                "sigilWedge",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: false)


.Id;
        }
    }
    /// <summary>
    /// Creatures struck by [creature] are pushed to an adjacent space.
    /// </summary>
    public class Wedge : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => target != null && Unyielding.CardCanBeMoved(target);
        public override IEnumerator OnDealDamage(int amount, PlayableCard target) {
            CardSlot left = target.Slot.GetAdjacent(true);
            CardSlot right = target.Slot.GetAdjacent(false);
            bool leftOpen = left != null && left.Card == null;
            bool rightOpen = right != null && right.Card == null;

            if (!leftOpen && !rightOpen) {
                yield break;
            }

            yield return base.PreSuccessfulTriggerSequence();
            target.Anim.StrongNegationEffect();
            yield return BoardManager.Instance.AssignCardToSlot(target, leftOpen ? left : right);
            yield return new WaitForSeconds(0.3f);
            yield return base.LearnAbility();
        }
    }
}
