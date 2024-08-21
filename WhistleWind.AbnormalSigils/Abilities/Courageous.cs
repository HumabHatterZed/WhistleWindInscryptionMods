using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Courageous()
        {
            const string rulebookName = "Courageous";
            const string rulebookDescription = "Creatures adjacent to this card lose up to 2 Health. For each point of Heath lost, the affected creature gains 1 Power. This effect cannot kill cards.";
            const string dialogue = "Life is only given to those who don't fear death.";

            Courageous.ability = AbnormalAbilityHelper.CreateAbility<Courageous>(
                "sigilCourageous",
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
    public class Courageous : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard()
        {
            return Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Exists(slot => slot.Card != null);
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
            return Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Contains(otherCard.Slot);
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return PreSuccessfulTriggerSequence();
            yield return ApplyEffect(otherCard);
        }

        private IEnumerator ApplyEffect(PlayableCard card)
        {
            if (card.Health <= 1)
            {
                card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("CourageousFail");
                yield break;
            }
            if (card.HasAnyOfAbilities(Ability.TailOnHit, Ability.Submerge, Ability.SubmergeSquid) || card.Status.hiddenAbilities.Contains(Ability.TailOnHit))
            {
                card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("CourageousRefuse");
                yield break;
            }

            if (!card.TemporaryMods.Exists(x => x.singletonId == "wstl:Courageous1"))
            {
                CardModificationInfo mod = new(1, -1)
                {
                    singletonId = "wstl:Courageous1"
                };
                card.AddTemporaryMod(mod);
            }
            if (!card.TemporaryMods.Exists(x => x.singletonId == "wstl:Courageous2"))
            {
                if (card.Health > 1)
                {
                    CardModificationInfo mod = new(1, -1)
                    {
                        singletonId = "wstl:Courageous2"
                    };
                    card.AddTemporaryMod(mod);
                }
            }

            card.OnStatsChanged();
            card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return base.LearnAbility();
        }
    }
}