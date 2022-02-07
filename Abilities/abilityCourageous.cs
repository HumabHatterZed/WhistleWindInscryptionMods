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
        private NewAbility Ability_Courageous()
        {
            const string rulebookName = "Courageous";
            const string rulebookDescription = "Adjacent cards lose up to 2 Health but gain 1 Power for every 1 Health lost via this effect. Affected cards will not go below 1 Health.";
            const string dialogue = "Life is only given to those who don't fear death.";

            return WstlUtils.CreateAbility<Courageous>(
                Resources.sigilCourageous,
                rulebookName, rulebookDescription, dialogue, 3);
        }
    }
    public class Courageous : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public static CardModificationInfo courageMod = new CardModificationInfo(0, -1);
        public static CardModificationInfo courageMod2 = new CardModificationInfo(0, -1);

        private readonly string buffFail = "Your creature's consitution is too weak.";
        private readonly string buffRefuse = "Coward's don't get the boon of the brave.";
        private readonly string cowardKill = "A coward on the battlefield does not deserve to see its end.";

        public override bool RespondsToResolveOnBoard()
        {
            int num = 0;
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(Card.Slot).Where(slot => slot.Card != null))
            {
                num++;
            }
            return num > 0;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return PreSuccessfulTriggerSequence();

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(Card.Slot))
            {
                if (slot.Card != null)
                {
                    yield return new WaitForSeconds(0.25f);
                    if (!slot.Card.HasAbility(Ability.TailOnHit) && !slot.Card.HasAbility(Ability.Submerge) && !slot.Card.Status.hiddenAbilities.Contains(Ability.TailOnHit))
                    {
                        if (slot.Card.Health > 1 && !slot.Card.TemporaryMods.Contains(courageMod))
                        {
                            slot.Card.AddTemporaryMod(courageMod);
                            slot.Card.OnStatsChanged();
                            slot.Card.Anim.StrongNegationEffect();
                        }
                        if (slot.Card.Health > 1 && !slot.Card.TemporaryMods.Contains(courageMod2))
                        {
                            slot.Card.AddTemporaryMod(courageMod2);
                            slot.Card.OnStatsChanged();
                        }

                        if (!slot.Card.TemporaryMods.Contains(courageMod) || !slot.Card.TemporaryMods.Contains(courageMod))
                        {
                            slot.Card.Anim.StrongNegationEffect();
                            if (!PersistentValues.HasSeenCrumblingArmourFail)
                            {
                                PersistentValues.HasSeenCrumblingArmourFail = true;
                                yield return new WaitForSeconds(0.25f);
                                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(buffFail, -0.65f, 0.4f);
                            }
                        }
                        else
                        {
                            yield return new WaitForSeconds(0.25f);
                            yield return base.LearnAbility(0.4f);
                        }
                    }
                    else
                    {
                        if (base.Card.Info.name.ToLowerInvariant().Contains("crumblingarmour"))
                        {
                            base.Card.Anim.StrongNegationEffect();
                            yield return new WaitForSeconds(0.25f);
                            yield return slot.Card.Die(false, base.Card);
                            if (!PersistentValues.HasSeenCrumblingArmourKill)
                            {
                                PersistentValues.HasSeenCrumblingArmourKill = true;
                                yield return new WaitForSeconds(0.5f);
                                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(cowardKill, -0.65f, 0.4f);
                            }
                        }
                        else if (!PersistentValues.HasSeenCrumblingArmourRefuse)
                        {
                            PersistentValues.HasSeenCrumblingArmourRefuse = true;
                            yield return new WaitForSeconds(0.25f);
                            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(buffRefuse, -0.65f, 0.4f);
                            yield return new WaitForSeconds(0.25f);
                        }
                    }
                }
            }
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(Card.Slot).Where(slot => slot.Card != null))
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
            yield return PreSuccessfulTriggerSequence();

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(Card.Slot))
            {
                if (slot.Card != null)
                {
                    yield return new WaitForSeconds(0.25f);
                    if (!slot.Card.HasAbility(Ability.TailOnHit) && !slot.Card.HasAbility(Ability.Submerge) && !slot.Card.Status.hiddenAbilities.Contains(Ability.TailOnHit))
                    {
                        if (slot.Card.Health > 1 && !slot.Card.TemporaryMods.Contains(courageMod))
                        {
                            slot.Card.AddTemporaryMod(courageMod);
                            slot.Card.OnStatsChanged();
                            slot.Card.Anim.StrongNegationEffect();
                        }
                        if (slot.Card.Health > 1 && !slot.Card.TemporaryMods.Contains(courageMod2))
                        {
                            slot.Card.AddTemporaryMod(courageMod2);
                            slot.Card.OnStatsChanged();
                        }

                        if (!slot.Card.TemporaryMods.Contains(courageMod) || !slot.Card.TemporaryMods.Contains(courageMod))
                        {
                            slot.Card.Anim.StrongNegationEffect();
                            if (!PersistentValues.HasSeenCrumblingArmourFail)
                            {
                                PersistentValues.HasSeenCrumblingArmourFail = true;
                                yield return new WaitForSeconds(0.25f);
                                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(buffFail, -0.65f, 0.4f);
                            }
                        }
                        else
                        {
                            yield return new WaitForSeconds(0.25f);
                            yield return base.LearnAbility(0.4f);
                        }
                    }
                    else
                    {
                        if (base.Card.Info.name.ToLowerInvariant().Contains("crumblingarmour"))
                        {
                            base.Card.Anim.StrongNegationEffect();
                            yield return new WaitForSeconds(0.25f);
                            yield return slot.Card.Die(false, base.Card);
                            if (!PersistentValues.HasSeenCrumblingArmourKill)
                            {
                                PersistentValues.HasSeenCrumblingArmourKill = true;
                                yield return new WaitForSeconds(0.5f);
                                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(cowardKill, -0.65f, 0.4f);
                            }
                        }
                        else if (!PersistentValues.HasSeenCrumblingArmourRefuse)
                        {
                            PersistentValues.HasSeenCrumblingArmourRefuse = true;
                            yield return new WaitForSeconds(0.25f);
                            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(buffRefuse, -0.65f, 0.4f);
                            yield return new WaitForSeconds(0.25f);
                        }
                    }
                }
            }
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
                if (slot.Card != null)
                {
                    slot.Card.RemoveTemporaryMod(courageMod);
                    slot.Card.RemoveTemporaryMod(courageMod2);
                    slot.Card.OnStatsChanged();
                    slot.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.25f);
                }
            }
        }
    }
}
