using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UIElements.StyleVariableResolver;

namespace WhistleWind.AbnormalSigils.StatusEffects
{
    /// <summary>
    /// Subclass of StatusEffectBehaviour that modifies the effect's Potency on upkeep. Affects cards in the player's hand.
    /// </summary>
    public abstract class ModifyOnUpkeepStatusEffectBehaviour : StatusEffectBehaviour, IOnUpkeepInHand
    {
        public abstract int PotencyModification { get; }

        public override bool RespondsToUpkeep(bool playerUpkeep) => CanModifyOnUpkeep(playerUpkeep);
        public bool RespondsToUpkeepInHand(bool playerUpkeep) => CanModifyOnUpkeep(playerUpkeep);

        public override IEnumerator OnUpkeep(bool playerUpkeep) => OnModifyOnUpkeep();
        public IEnumerator OnUpkeepInHand(bool playerUpkeep) => OnModifyOnUpkeep();

        public virtual bool CanModifyOnUpkeep(bool playerUpkeep)
        {
            return base.PlayableCard.OpponentCard != playerUpkeep && TurnManager.Instance.TurnNumber > TurnGained;
        }
        public virtual IEnumerator OnModifyOnUpkeep()
        {
            //Debug.Log($"Modify: {EffectPotency} | {EffectPotency + PotencyModification}");
            yield return new WaitForSeconds(0.3f);
            base.PlayableCard.Anim.LightNegationEffect();
            ViewManager.Instance.SwitchToView(base.PlayableCard.InHand ? View.Hand : View.Board);
            yield return new WaitForSeconds(0.2f);

            base.ModifyPotency(PotencyModification, true);
            if (EffectPotency <= 0)
                base.DestroyStatusEffect();

            yield return new WaitForSeconds(0.3f);
        }
    }
}
