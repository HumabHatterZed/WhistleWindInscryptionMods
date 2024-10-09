using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_InfiniteShield()
        {
            const string rulebookName = "Unbreakable Defence";
            const string rulebookDescription = "[creature] prevents all damage dealt to it. All cards behave as if this card took damage.";
            InfiniteShield.ability = AbnormalAbilityHelper.CreateAbility<InfiniteShield>(
                "sigilInfiniteShield",
                rulebookName, rulebookDescription, powerLevel: 5,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    [HarmonyPatch]
    public class InfiniteShield : DamageShieldBehaviour, IShieldPreventedDamage
    {
        public static Ability ability;
        public override Ability Ability => ability;

        // maybe not actually infinite
        public override int StartingNumShields => 9000;
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => playerTurnEnd != base.Card.OpponentCard;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            numShields = StartingNumShields;
            return base.OnTurnEnd(playerTurnEnd);
        }

        public bool RespondsToShieldPreventedDamage(PlayableCard target, int damage, PlayableCard attacker) => target == base.Card;
        public IEnumerator OnShieldPreventedDamage(PlayableCard target, int damage, PlayableCard attacker)
        {
            numShields = StartingNumShields;

            if (target.TriggerHandler.RespondsToTrigger(Trigger.TakeDamage, attacker))
                yield return target.TriggerHandler.OnTrigger(Trigger.TakeDamage, attacker);

            if (target.Health <= 0)
                yield return target.Die(wasSacrifice: false, attacker);

            if (attacker != null)
            {
                if (attacker.TriggerHandler.RespondsToTrigger(Trigger.DealDamage, damage, target))
                {
                    yield return attacker.TriggerHandler.OnTrigger(Trigger.DealDamage, damage, target);
                }
                yield return Singleton<GlobalTriggerHandler>.Instance.TriggerCardsOnBoard(Trigger.OtherCardDealtDamage, false, attacker, attacker.Attack, target);
            }
            yield return CustomTriggerFinder.TriggerInHand((IOnOtherCardDealtDamageInHand x) => x.RespondsToOtherCardDealtDamageInHand(attacker, attacker.Attack, target), (IOnOtherCardDealtDamageInHand x) => x.OnOtherCardDealtDamageInHand(attacker, attacker.Attack, target));
        }

        public int ShieldPreventedDamagePriority(PlayableCard target, int damage, PlayableCard attacker) => 0;
    }
}
