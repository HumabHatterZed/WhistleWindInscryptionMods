using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWindLobotomyMod.Opponents
{
    public abstract class LobotomyBattleSequencer : BossBattleSequencer, IOnPreScalesChangedRef
    {
        public virtual bool PreventScaleDamage { get; set; } = true;
        public virtual bool DirectDamageGivesBones { get; set; } = true;
        public virtual int MaxExcessBones { get; } = 8;

        public int currentExcessBones = 0;

        public bool drewInitialHand = false;

        public virtual IEnumerator PreDrawOpeningHand()
        {
            yield break;
        }
        public virtual IEnumerator PostDrawOpeningHand()
        {
            yield break;
        }

        public virtual bool RespondsToPreScalesChangedRef(int damage, int numWeights, bool toPlayer)
        {
            return !toPlayer && PreventScaleDamage;
        }
        public virtual int CollectPreScalesChangedRef(int damage, ref int numWeights, ref bool toPlayer)
        {
            if (LifeManager.Instance.DamageUntilPlayerWin == 1)
                return numWeights = 0;

            if (damage >= LifeManager.Instance.DamageUntilPlayerWin)
            {
                numWeights = Mathf.Min(LifeManager.Instance.DamageUntilPlayerWin - 1, numWeights);
                return LifeManager.Instance.DamageUntilPlayerWin - 1;
            }

            return damage;
        }

        public override IEnumerator PlayerCombatEnd()
        {
            yield return base.PlayerCombatEnd();
            currentExcessBones = 0;
        }
        public override IEnumerator OpponentCombatEnd()
        {
            yield return base.OpponentCombatEnd();
            currentExcessBones = 0;
        }
    }
}
