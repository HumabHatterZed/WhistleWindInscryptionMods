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

        private bool IsArmour => base.Card.Info.name.ToLowerInvariant().Contains("crumblingarmour");

        public static CardModificationInfo courageMod = new(0, -1);
        public static CardModificationInfo courageMod2 = new(0, -1);

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

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(Card.Slot).Where(slot => slot.Card != null))
            {
                yield return Effect(slot.Card);
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

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(Card.Slot).Where(slot => slot.Card != null))
            {
                slot.Card.RemoveTemporaryMod(courageMod);
                slot.Card.RemoveTemporaryMod(courageMod2);
                slot.Card.OnStatsChanged();
                slot.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.25f);
            }
        }

        private IEnumerator Effect(PlayableCard card)
        {
            yield return new WaitForSeconds(0.2f);
            if (card.HasAbility(Ability.TailOnHit) || card.HasAbility(Ability.Submerge) || card.Status.hiddenAbilities.Contains(Ability.TailOnHit))
            {
                if (IsArmour)
                {
                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.25f);
                    yield return card.Die(false, base.Card);
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
                yield break;
            }

            if (card.Health == 1)
            {
                card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.25f);
                if (!PersistentValues.HasSeenCrumblingArmourFail)
                {
                    PersistentValues.HasSeenCrumblingArmourFail = true;
                    yield return new WaitForSeconds(0.25f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(buffFail, -0.65f, 0.4f);
                }
                yield break;
            }

            if (!card.TemporaryMods.Contains(courageMod))
            {
                card.AddTemporaryMod(courageMod);
                card.OnStatsChanged();
            }
            if (!card.TemporaryMods.Contains(courageMod2))
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
