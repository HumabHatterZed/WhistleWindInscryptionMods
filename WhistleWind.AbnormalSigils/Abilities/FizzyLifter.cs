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
            const string rulebookDescription = "The selected card gains Airborne for the next 3 turns.";
            FizzyLifter.ability = AbnormalAbilityHelper.CreateAbility<FizzyLifter>(
                "sigilFizzyLifter",
                rulebookName, rulebookDescription, powerLevel: 4,
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

        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) => base.Card.Info.IsTargetedSpell() && base.Card == attacker && slot.Card != null;

        public override IEnumerator OnResolveOnBoard()
        {
            foreach (PlayableCard card in BoardManager.Instance.GetCards())
            {
                yield return OnSlotTargetedForAttack(card.Slot, base.Card);
            }
        }

        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            yield return base.PreSuccessfulTriggerSequence();
            CardModificationInfo mod = new CardModificationInfo(Ability.Flying) { singletonId = "FizzyLifted" };
            slot.Card.AddTemporaryMod(mod);
            yield return slot.Card.AddStatusEffect<FizzyLifterEffect>(3);
            slot.Card.GetStatusEffect<FizzyLifterEffect>().SetPotency(3, false); // reset to 3
            yield return base.LearnAbility();
        }

        public override bool RespondsToSacrifice() => true;
        public override IEnumerator OnSacrifice()
        {
            yield return base.PreSuccessfulTriggerSequence();
            CardModificationInfo mod = new CardModificationInfo(Ability.Flying) { singletonId = "FizzyLifted" };
            BoardManager.Instance.CurrentSacrificeDemandingCard.AddTemporaryMod(mod);
            yield return BoardManager.Instance.CurrentSacrificeDemandingCard.AddStatusEffect<FizzyLifterEffect>(3);
            BoardManager.Instance.CurrentSacrificeDemandingCard.GetStatusEffect<FizzyLifterEffect>().SetPotency(3, false); // reset to 3
            yield return base.LearnAbility();
        }
    }
}
