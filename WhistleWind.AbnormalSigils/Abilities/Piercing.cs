using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Piercing() {
            const string rulebookName = "Piercing";
            const string rulebookDescription = "[creature] can strike through breakable shields. Opposing cards cannot reduce or prevent damage dealt by this card.";
            const string dialogue = "Even the thickest hide can be run through.";

            Piercing.ability = AbnormalAbilityHelper.CreateAbility<Piercing>(
                "sigilPiercing",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    /// <summary>
    /// [creature] can strike through breakable shields. Opposing cards cannot reduce or prevent damage dealt by this card.
    /// </summary>
    public class Piercing : AbilityBehaviour, IModifyDamageTaken, IShieldPreventedDamage {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => true;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target) {
            if (target.HasAnyOfAbilities(Ability.DeathShield, Ability.PreventAttack, ThickSkin.ability)
                || target.Slot.GetAdjacentCards().Exists(x => x.HasAbility(Protector.ability))) {
                yield return LearnAbility(0.25f);
            }
        }

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => attacker == base.Card && damage < originalDamage;
        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) {
            return originalDamage;
        }
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => -9000;

        public bool RespondsToShieldPreventedDamage(PlayableCard target, int damage, PlayableCard attacker) => attacker == base.Card && target.LacksAbility(InfiniteShield.ability);

        public IEnumerator OnShieldPreventedDamage(PlayableCard target, int damage, PlayableCard attacker) {
            // recreate TakeDamage logic
            target.Status.damageTaken += damage;
            target.UpdateStatsText();
            if (target.Health > 0)
                target.Anim.PlayHitAnimation();

            if (target.TriggerHandler.RespondsToTrigger(Trigger.TakeDamage, attacker))
                yield return target.TriggerHandler.OnTrigger(Trigger.TakeDamage, attacker);

            if (target.Health <= 0)
                yield return target.Die(wasSacrifice: false, attacker);

            if (attacker != null) {
                if (attacker.TriggerHandler.RespondsToTrigger(Trigger.DealDamage, damage, target))
                    yield return attacker.TriggerHandler.OnTrigger(Trigger.DealDamage, damage, target);

                yield return Singleton<GlobalTriggerHandler>.Instance.TriggerCardsOnBoard(Trigger.OtherCardDealtDamage, false, attacker, attacker.Attack, target);
            }

            yield return CustomTriggerFinder.TriggerInHand<IOnOtherCardDealtDamageInHand>(
                x => x.RespondsToOtherCardDealtDamageInHand(attacker, attacker.Attack, target),
                x => x.OnOtherCardDealtDamageInHand(attacker, attacker.Attack, target)
            );
        }

        public int ShieldPreventedDamagePriority(PlayableCard target, int damage, PlayableCard attacker) => 0;
    }
}
