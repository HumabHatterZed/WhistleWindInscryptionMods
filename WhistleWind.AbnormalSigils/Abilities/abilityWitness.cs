using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Witness()
        {
            const string rulebookName = "Witness";
            const string rulebookDescription = "Pay 2 Bones to increase the selected creature's Health by 2 and their taken damage by 1. This effect stacks up to 3 times.";
            const string dialogue = "The truth will set you free.";
            const string triggerText = "The chosen beholds [creature] and is reborn.";
            Witness.ability = AbnormalAbilityHelper.CreateActivatedAbility<Witness>(
                "sigilWitness",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 1).Id;
        }
    }
    public class Witness : ActivatedSelectSlotBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override string NoTargetsDialogue => "There's no one to hear your message.";
        public override string InvalidTargetDialogue => "You must choose one of your other cards to proselytise.";
        public override int StartingBonesCost => 2;
        public override bool IsInvalidTarget(CardSlot slot)
        {
            if (base.IsInvalidTarget(slot))
                return true;

            if (slot.Card.OpponentCard == base.Card.OpponentCard)
            {
                var component = slot.Card.GetStatusEffect<Prudence>();
                if (component != null)
                    return component.EffectSeverity > 3;

                return false;
            }
            return true;
        }

        public override bool CanActivate()
        {
            // If there's a valid target on the owner's side
            foreach (var slot in BoardManager.Instance.GetSlotsCopy(!base.Card.OpponentCard))
            {
                if (!IsInvalidTarget(slot))
                    return true;
            }
            return false;
        }

        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            slot.Card.Anim.StrongNegationEffect();
            slot.Card.HealDamage(2);

            if (slot.Card.HasStatusEffect<Prudence>())
                slot.Card.UpdateStatusEffectCount<Prudence>(1, false);
            else
                slot.Card.AddStatusEffectToCard<Prudence>();

            yield return new WaitForSeconds(0.4f);
            yield return base.LearnAbility();
        }
    }
}
