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
            const string rulebookDescription = "At the end of the owner's turn, this card moves in the sigil's direction and Blooms its old space.";
            const string dialogue = "From fertile flesh, a garden will soon bloom.";
            FlowerQueen.ability = AbnormalAbilityHelper.CreateAbility<FlowerQueen>(
                "sigilFlowerQueen",
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: true, canStack: false)
                .SetSlotRedirect("Blooms", BloomingSlot.Id, Color.green)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class FlowerQueen : Strafe
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override IEnumerator PostSuccessfulMoveSequence(CardSlot oldSlot)
        {
            if (oldSlot.GetSlotModification() == BloomingSlot.Id)
                yield break;

            yield return base.PreSuccessfulTriggerSequence();
            oldSlot.SetSlotModification(BloomingSlot.Id);
            yield return base.LearnAbility(0.5f);
        }
    }
}
