using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_ActivatedSniper() {
            const string rulebookName = "Gun For Hire";
            const string rulebookDescription = "Pay 2 Energy to give this card Sniper until the end of its next attack.";
            const string dialogue = "Anything for a price.";
            const string triggerText = "[creature] prepares to fire.";
            ActivatedSniper.ability = AbnormalAbilityHelper.CreateActivatedAbility<ActivatedSniper>(
                "sigilActivatedSniper",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2)
                .Id;
        }
    }
    /// <summary>
    /// Create a random card in your hand, then deactivate this sigil for 3 turns.
    /// </summary>
    public class ActivatedSniper : ActivatedAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int EnergyCost => 2;
        private bool activated = false;
        public override bool CanActivate() => base.CanActivate() && base.Card.LacksAbility(Ability.Sniper);
        public override IEnumerator Activate() {
            activated = true;
            yield return HelperMethods.ChangeCurrentView(View.Board);
            base.Card.Anim.PlayTransformAnimation();
            yield return new WaitForSeconds(0.15f);
            base.Card.Status.hiddenAbilities.Add(this.Ability);
            base.Card.AddTemporaryMod(new(Ability.Sniper) { singletonId = "wstl:ActivatedSniper" });
            yield return base.LearnAbility(0.4f);
        }

        public override bool RespondsToAttackEnded() => activated;
        public override IEnumerator OnAttackEnded() {
            CardModificationInfo mod = base.Card.TemporaryMods.Find(x => x.singletonId == "wstl:ActivatedSniper");
            if (mod != null) {
                yield return HelperMethods.ChangeCurrentView(View.Board);
                base.Card.Anim.PlayTransformAnimation();
                yield return new WaitForSeconds(0.15f);
                base.Card.Status.hiddenAbilities.Remove(this.Ability);
                base.Card.RemoveTemporaryMod(mod);
                yield return new WaitForSeconds(0.4f);
            }
            activated = false;
        }
    }
}
