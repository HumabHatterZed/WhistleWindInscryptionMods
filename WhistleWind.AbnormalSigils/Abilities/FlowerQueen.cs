using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Slots;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_FlowerQueen()
        {
            const string rulebookName = "Flower Queen";
            const string rulebookDescription = "At the end of the owner's turn, [creature] Blooms the opposing space.";
            const string dialogue = "From fertile flesh, a garden will soon bloom.";
            FlowerQueen.ability = AbnormalAbilityHelper.CreateAbility<FlowerQueen>(
                "sigilFlowerQueen",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: false, opponent: true, canStack: false)
                .SetSlotRedirect("Blooms", BloomingSlot.Id, Color.green)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class FlowerQueen : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (base.Card.OpposingSlot().GetSlotModification() == BloomingSlot.Id)
                yield break;

            yield return base.PreSuccessfulTriggerSequence();
            yield return base.Card.OpposingSlot().SetSlotModification(BloomingSlot.Id);
            yield return base.LearnAbility(0.5f);
        }
    }
}
