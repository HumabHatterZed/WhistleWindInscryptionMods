using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public abstract class TargetedSpell : AbilityBehaviour
    {
        public abstract bool TargetAlly { get; }

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            if (slot.Card != null)
                return TargetAlly ? !slot.Card.OpponentCard : slot.Card.OpponentCard;
            return false;
        }

        public override bool RespondsToPlayFromHand()
        {
            return true;
        }
        public override IEnumerator OnPlayFromHand()
        {
            yield return SwitchCardView(View.Board);
            yield return EffectOnPlayFromHand();
        }
        public virtual IEnumerator EffectOnPlayFromHand()
        {
            yield break;
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return true;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return SwitchCardView(View.Default, start: 0.75f);
            yield return EffectOnDie(wasSacrifice, killer);
        }
        public virtual IEnumerator EffectOnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield break;
        }

        public IEnumerator SwitchCardView(View view, float start = 0.2f, float end = 0.2f)
        {
            if (Singleton<ViewManager>.Instance.CurrentView != view)
            {
                yield return new WaitForSeconds(start);
                Singleton<ViewManager>.Instance.SwitchToView(view);
                yield return new WaitForSeconds(end);
            }
        }
    }
}
