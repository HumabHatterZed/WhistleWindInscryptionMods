using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Courageous()
        {
            const string rulebookName = "Courageous";
            const string rulebookDescription = "Adjacent creatures lose up to 2 Health. For each point of Heath lost this way, the affected creature gains 1 Power. This effect cannot kill cards.";
            const string dialogue = "Life is only given to those who don't fear death.";

            Courageous.ability = AbnormalAbilityHelper.CreateAbility<Courageous>(
                Artwork.sigilCourageous, Artwork.sigilCourageous_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class Courageous : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public static CardModificationInfo courageMod = new(1, -1);
        public static CardModificationInfo courageMod2 = new(1, -1);

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
                return Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Contains(otherCard.Slot);

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
                yield return AbnormalDialogueManager.PlayDialogueEvent("CourageousFail");
                yield break;
            }
            if (card.HasAnyOfAbilities(Ability.TailOnHit, Ability.Submerge, Ability.SubmergeSquid) || card.Status.hiddenAbilities.Contains(Ability.TailOnHit))
            {
                yield return AbnormalDialogueManager.PlayDialogueEvent("CourageousRefuse");
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