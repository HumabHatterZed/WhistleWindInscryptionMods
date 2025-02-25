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
            const string rulebookDescription = "When [creature] is sacrificed, the card it was sacrificed for will become Airborne for 3 turns.";
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

        public override bool RespondsToSacrifice() => true;
        public override IEnumerator OnSacrifice()
        {
            yield return base.PreSuccessfulTriggerSequence();
            CardModificationInfo mod = new CardModificationInfo(Ability.Flying) { singletonId = "FizzyLifted" };
            BoardManager.Instance.CurrentSacrificeDemandingCard.Status.hiddenAbilities.Add(Ability.Flying);
            BoardManager.Instance.CurrentSacrificeDemandingCard.Anim.PlayTransformAnimation();
            BoardManager.Instance.CurrentSacrificeDemandingCard.AddTemporaryMod(mod);
            yield return BoardManager.Instance.CurrentSacrificeDemandingCard.AddStatusEffect<FizzyLifterEffect>(2);
            BoardManager.Instance.CurrentSacrificeDemandingCard.GetStatusEffect<FizzyLifterEffect>().SetPotency(2, false); // reset to 3
            yield return base.LearnAbility(0.5f);
        }
    }
}
