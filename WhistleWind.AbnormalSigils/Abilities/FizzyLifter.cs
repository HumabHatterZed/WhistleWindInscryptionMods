using DiskCardGame;
using HarmonyLib;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_FizzyLifter()
        {
            const string rulebookName = "Fizzy Lifter";
            const string rulebookDescription = "The selected card will become Airborne for 3 turns.";
            FizzyLifter.ability = AbnormalAbilityHelper.CreateAbility<FizzyLifter>(
                "sigilFizzyLifter",
                rulebookName, rulebookDescription, powerLevel: 0,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }

    public class FizzyLifter : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToSacrifice() => true;
        public override IEnumerator OnSacrifice()
        {
            yield return Sequence(BoardManager.Instance.CurrentSacrificeDemandingCard);
            yield return base.LearnAbility(0.5f);
        }

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
            => attacker == base.Card && base.Card.Info.IsTargetedSpell() && slot.Card != null;
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            yield return Sequence(slot.Card);
        }

        private IEnumerator Sequence(PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            CardModificationInfo mod = new CardModificationInfo(Ability.Flying) { singletonId = "FizzyLifted" };
            target.Status.hiddenAbilities.Add(Ability.Flying);
            target.Anim.PlayTransformAnimation();
            target.AddTemporaryMod(mod);
            yield return target.AddStatusEffect<FizzyLifterEffect>(2);
            target.GetStatusEffect<FizzyLifterEffect>().SetPotency(2, false); // reset to 3
        }
    }
}
