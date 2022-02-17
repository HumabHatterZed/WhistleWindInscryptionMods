using APIPlugin;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_FlagBearer()
        {
            const string rulebookName = "Flag Bearer";
            const string rulebookDescription = "Adjacent cards gain 2 Health.";
            const string dialogue = "Morale runs high.";

            return WstlUtils.CreateAbility<FlagBearer>(
                Resources.sigilFlagBearer,
                rulebookName, rulebookDescription, dialogue, 3);
        }
    }
    public class FlagBearer : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private CardModificationInfo mod = new(0, 2);
        public override bool RespondsToResolveOnBoard()
        {
            int num = 0;
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null))
            {
                num++;
            }
            return num > 0;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.PreSuccessfulTriggerSequence();

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot))
            {
                if (slot.Card.TemporaryMods.Contains(mod))
                {
                    yield break;
                }
                yield return Effect(slot.Card);
            }
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null))
            {
                if (slot.Card == otherCard)
                {
                    return true;
                }
            }
            return false;

        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.PreSuccessfulTriggerSequence();
            if (otherCard.TemporaryMods.Contains(mod))
            {
                yield break;
            }
            yield return Effect(otherCard);
        }
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return true;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(Card.Slot))
            {
                if (slot.Card != null && slot.Card.Health > 2)
                {
                    slot.Card.RemoveTemporaryMod(mod);
                    slot.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.25f);
                }
            }
        }

        private IEnumerator Effect(PlayableCard card)
        {
            card.AddTemporaryMod(mod);
            card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return LearnAbility(0.4f);
        }
    }
}
