using System.Linq;
using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
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

        private CardModificationInfo mod = new CardModificationInfo(0, 2);
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
                if (slot.Card != null && !slot.Card.TemporaryMods.Contains(mod))
                {
                    slot.Card.AddTemporaryMod(mod);
                    slot.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.25f);
                    yield return LearnAbility(0.4f);
                }
            }
            yield break;
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null))
            {
                if (slot.Card == otherCard)
                {
                    if (!base.Card.Slot.IsPlayerSlot)
                    {
                        return !otherCard.Slot.IsPlayerSlot;
                    }
                    return otherCard.Slot.IsPlayerSlot;
                }
            }
            return false;

        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.PreSuccessfulTriggerSequence();

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(Card.Slot))
            {
                if (slot.Card != null && !slot.Card.TemporaryMods.Contains(mod))
                {
                    slot.Card.AddTemporaryMod(mod);
                    slot.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.25f);
                    yield return LearnAbility(0.4f);
                }
            }
            yield break;
        }
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return true;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            yield return new WaitForSeconds(0.25f);
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
    }
}
