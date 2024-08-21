using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_HighStrung()
        {
            const string rulebookName = "High-Strung";
            const string rulebookDescription = "At the end of the owner's turn, [creature] gains Haste equal to twice the opposing card's Power.";
            HighStrung.ability = AbnormalAbilityHelper.CreateAbility<HighStrung>(
                "sigilHighStrung",
                rulebookName, rulebookDescription, powerLevel: 2,
                modular: true, opponent: true, canStack: false).Id;
        }
    }
    public class HighStrung : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => !playerTurnEnd && base.Card.OpposingCard() != null;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            int stacks = base.Card.OpposingCard().Attack * 2;
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