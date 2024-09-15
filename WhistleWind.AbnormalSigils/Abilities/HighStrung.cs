using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_HighStrung()
        {
            const string rulebookName = "High-Strung";
            const string rulebookDescription = "At the end of the owner's turn, [creature] gains Haste equal to the opposing creature's power level.";
            HighStrung.ability = AbnormalAbilityHelper.CreateAbility<HighStrung>(
                "sigilHighStrung",
                rulebookName, rulebookDescription, powerLevel: 2,
                modular: true, opponent: true, canStack: false)
                .SetAbilityRedirect("Haste", Haste.iconId, GameColors.Instance.orange)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class HighStrung : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => !playerTurnEnd && base.Card.OpposingCard() != null;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            int stacks = base.Card.OpposingCard().PowerLevel;
            if (stacks == 0)
                yield break;

            yield return base.PreSuccessfulTriggerSequence();
            yield return AddHasteToCard(stacks);
            yield return base.LearnAbility(0.4f);
        }
        private IEnumerator AddHasteToCard(int stacks)
        {
            base.Card.Anim.LightNegationEffect();
            yield return base.Card.AddStatusEffect<Haste>(stacks, false);
            yield return new WaitForSeconds(0.1f);
        }
    }
}