using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils.StatusEffects
{
    /// <summary>
    /// Subclass of StatusEffectBehaviour that modifies the effect's Potency on upkeep. Affects cards in the player's hand.
    /// </summary>
    public abstract class ModifyOnTurnEndStatusEffectBehaviour : StatusEffectBehaviour, IOnTurnEndInHand
    {
        public abstract int PotencyModification { get; }

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => CanModifyOnTurnEnd(playerTurnEnd);
        public bool RespondsToTurnEndInHand(bool playerTurnEnd) => CanModifyOnTurnEnd(playerTurnEnd);

        public override IEnumerator OnTurnEnd(bool playerTurnEnd) => OnModifyOnTurnEnd();
        public IEnumerator OnTurnEndInHand(bool playerTurnEnd) => OnModifyOnTurnEnd();

        public virtual bool CanModifyOnTurnEnd(bool playerTurnEnd)
        {
            return base.PlayableCard.OpponentCard != playerTurnEnd && TurnManager.Instance.TurnNumber >= TurnGained;
        }
        public virtual IEnumerator OnModifyOnTurnEnd()
        {
            //Debug.Log($"Modify: {EffectPotency} | {EffectPotency + PotencyModification}");
            yield return HelperMethods.ChangeCurrentView(base.PlayableCard.InHand ? View.Hand : View.Board);

            base.PlayableCard.Anim.LightNegationEffect();
            base.ModifyPotency(PotencyModification, true);
            if (EffectPotency <= 0)
                yield return base.RemoveFromCard(true);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
