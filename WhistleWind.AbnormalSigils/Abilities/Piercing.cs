using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Piercing() {
            const string rulebookName = "Piercing";
            const string rulebookDescription = "[creature] can strike face down cards and through shields. Opposing cards cannot reduce damage dealt by this card.";
            const string dialogue = "Even the thickest hide can be run through.";

            Piercing.ability = AbnormalAbilityHelper.CreateAbility<Piercing>(
                "sigilPiercing",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: false)


.Id;
        }
    }
    /// <summary>
    /// [creature] can strike face down cards and through shields. Opposing cards cannot reduce damage dealt by this card.
    /// </summary>
    public class Piercing : AbilityBehaviour, IModifyDamageTaken, IShieldPreventedDamage {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool forcedFaceUp = false;
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            return attacker == base.Card && slot.Card != null && slot.Card.FaceDown;
        }

        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            forcedFaceUp = true;
            yield return slot.Card.FlipFaceUp(true);
        }

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => forcedFaceUp || CardTriggersPiercingDialogue(target);

        public override IEnumerator OnDealDamage(int amount, PlayableCard target) {
            yield return LearnAbility(0.25f);
            if (forcedFaceUp && !target.Dead) {
                yield return new WaitForSeconds(0.5f);
                yield return target.FlipFaceDown(true);
                forcedFaceUp = false;
            }
        }

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
            => attacker == base.Card && damage < originalDamage;
        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) {
            return originalDamage;
        }
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => -9000;

        public bool RespondsToShieldPreventedDamage(PlayableCard target, int damage, PlayableCard attacker) =>
            attacker == base.Card && target.LacksAbility(InfiniteShield.ability);

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

        public static bool CardTriggersPiercingDialogue(PlayableCard card) {
            if (card != null) {
                return card.FaceDown
                    || card.HasAnyOfAbilities(Ability.DeathShield, ThickSkin.ability)
                    || card.Slot.GetAdjacentCards().Exists(x => x.HasAbility(Protector.ability));
            }
            return false;
        }
    }
}
