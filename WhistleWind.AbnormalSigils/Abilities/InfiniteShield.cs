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
    public class InfiniteShield : DamageShieldBehaviour
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

        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.TakeDamage))]
        private static IEnumerator TriggerDamageOnShieldBreak(IEnumerator result, PlayableCard __instance, int damage, PlayableCard attacker)
        {
            if (__instance.HasAbility(InfiniteShield.ability))
            {
                if (__instance.TriggerHandler.RespondsToTrigger(Trigger.TakeDamage, attacker))
                {
                    yield return __instance.TriggerHandler.OnTrigger(Trigger.TakeDamage, attacker);
                }
                if (attacker != null)
                {
                    if (attacker.TriggerHandler.RespondsToTrigger(Trigger.DealDamage, damage, __instance))
                    {
                        yield return attacker.TriggerHandler.OnTrigger(Trigger.DealDamage, damage, __instance);
                    }
                    yield return Singleton<GlobalTriggerHandler>.Instance.TriggerCardsOnBoard(Trigger.OtherCardDealtDamage, false, attacker, attacker.Attack, __instance);
                }
                yield return CustomTriggerFinder.TriggerInHand((IOnOtherCardDealtDamageInHand x) => x.RespondsToOtherCardDealtDamageInHand(attacker, attacker.Attack, __instance), (IOnOtherCardDealtDamageInHand x) => x.OnOtherCardDealtDamageInHand(attacker, attacker.Attack, __instance));
            }
            yield return result;
        }
    }
}
