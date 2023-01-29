﻿using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Courageous()
        {
            const string rulebookName = "Courageous";
            const string rulebookDescription = "If an adjacent card has more than 1 Health, it loses 1 Health and gains 1 Power. This effect can activate twice for a maximum of -2 Health and +2 Power. Stat changes persist until battle's end.";
            const string dialogue = "Life is only given to those who don't fear death.";

            Courageous.ability = AbilityHelper.CreateAbility<Courageous>(
                Resources.sigilCourageous, Resources.sigilCourageous_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Courageous : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool IsArmour => base.Card.Info.name.ToLowerInvariant().Contains("crumblingarmour");

        public static CardModificationInfo courageMod = new(1, -1);
        public static CardModificationInfo courageMod2 = new(1, -1);

        private readonly string buffFail = "Your creature's consitution is too weak.";
        private readonly string buffRefuse = "Cowards don't get the boon of the brave.";
        private readonly string cowardKill = "A coward on the battlefield does not deserve to see its end.";

        public override bool RespondsToResolveOnBoard()
        {
            return Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null).Count() > 0;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return PreSuccessfulTriggerSequence();
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null))
            {
                yield return ApplyEffect(slot.Card);
            }
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null).Count() > 0)
            {
                return Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Contains(otherCard.Slot);
            }
            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return PreSuccessfulTriggerSequence();
            yield return ApplyEffect(otherCard);
        }

        private IEnumerator ApplyEffect(PlayableCard card)
        {
            if (card.Health == 1)
            {
                card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                if (!WstlSaveManager.HasSeenCrumblingArmourFail)
                {
                    WstlSaveManager.HasSeenCrumblingArmourFail = true;
                    yield return new WaitForSeconds(0.25f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(buffFail, -0.65f, 0.4f);
                }
                yield break;
            }
            if (card.HasAnyOfAbilities(Ability.TailOnHit, Ability.Submerge, Ability.SubmergeSquid) || card.Status.hiddenAbilities.Contains(Ability.TailOnHit))
            {
                if (IsArmour)
                {
                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.4f);
                    yield return card.Die(false, base.Card);
                    if (!WstlSaveManager.HasSeenCrumblingArmourKill)
                    {
                        WstlSaveManager.HasSeenCrumblingArmourKill = true;
                        yield return new WaitForSeconds(0.5f);
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(cowardKill, -0.65f, 0.4f);
                    }
                }
                else if (!WstlSaveManager.HasSeenCrumblingArmourRefuse)
                {
                    WstlSaveManager.HasSeenCrumblingArmourRefuse = true;
                    yield return new WaitForSeconds(0.25f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(buffRefuse, -0.65f, 0.4f);
                    yield return new WaitForSeconds(0.25f);
                }
                yield break;
            }

            if (!card.TemporaryMods.Contains(courageMod))
            {
                card.AddTemporaryMod(courageMod);
                card.OnStatsChanged();
            }
            if (!card.TemporaryMods.Contains(courageMod2) && card.Health > 1)
            {
                card.AddTemporaryMod(courageMod2);
                card.OnStatsChanged();
            }

            card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return base.LearnAbility();
        }
    }
}