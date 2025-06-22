using DiskCardGame;
using InscryptionAPI.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core.Helpers {
    public static class ShowModifiedAttackDamage {
        /// <summary>
        /// Modifies the attacker's displayed Power value to indicate the actual damage being dealt.
        /// </summary>
        /// <param name="value">Optional argument that bypasses the damage calculation.</param>
        public static IEnumerator ShowModifiedDamage(PlayableCard target, PlayableCard attacker, int value = -1) {
            int damage = value == -1 ? SimulateModifyDamageTaken(target, attacker) : value;
            if (damage > attacker.RenderInfo.attack) {
                attacker.RenderInfo.attackTextColor = GameColors.Instance.darkBlue;
            }
            else if (damage < attacker.RenderInfo.attack) {
                attacker.RenderInfo.attackTextColor = GameColors.Instance.darkFuschia;
            }
            else {
                attacker.RenderInfo.attackTextColor = Color.black;
            }

            attacker.RenderInfo.attack = damage;
            attacker.RenderCard();
            yield return new WaitForSeconds(0.2f);
            BoardManager.Instance.StartCoroutine(DelayUpdateStatsText(attacker));
        }
        private static IEnumerator DelayUpdateStatsText(PlayableCard attacker) {
            yield return new WaitUntil(() => !attacker.Anim.DoingAttackAnimation);
            attacker.OnStatsChanged();
        }

        public static int SimulateModifyDamageTaken(PlayableCard target, PlayableCard attacker) {
            int originalDamage = attacker.Attack;
            int damage = originalDamage;
            List<IModifyDamageTaken> modifyTakeDamage = CustomTriggerFinder.FindGlobalTriggers<IModifyDamageTaken>(true).ToList();
            modifyTakeDamage.Sort((a, b) => b.TriggerPriority(target, originalDamage, attacker) - a.TriggerPriority(target, originalDamage, attacker));
            foreach (IModifyDamageTaken modify in modifyTakeDamage) {
                if (modify != null && modify.RespondsToModifyDamageTaken(target, damage, attacker, originalDamage))
                    damage = modify.OnModifyDamageTaken(target, damage, attacker, originalDamage);
            }

            return damage > -1 ? damage : 0;
        }
    }
}
