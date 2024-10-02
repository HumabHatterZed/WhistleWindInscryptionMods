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

        public virtual bool RespondsToPreScalesChanged(int damage, int numWeights, bool toPlayer) => !toPlayer && PreventScaleDamage;
        public virtual int OnPreScalesChanged(int damage, ref int numWeights, ref bool toPlayer)
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
        public bool RespondsToPreScalesChangedRef(int damage, int numWeights, bool toPlayer) => RespondsToPreScalesChanged(damage, numWeights, toPlayer);
        public int CollectPreScalesChangedRef(int damage, ref int numWeights, ref bool toPlayer) => OnPreScalesChanged(damage, ref numWeights, ref toPlayer);
    }
}
