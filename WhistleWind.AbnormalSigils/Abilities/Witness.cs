using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Rulebook;
using System.Collections;
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
            const string rulebookDescription = "Pay 1 Bone to inflict 1 Flagellation and increase the selected creature's Health by 2. This effect stacks up to 3 times.";
            const string dialogue = "The truth will set you free.";
            const string triggerText = "Behold [creature] and be reborn.";
            Witness.ability = AbnormalAbilityHelper.CreateActivatedAbility<Witness>(
                "sigilWitness",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2)
                .SetAbilityRedirect("Flagellation", Prudence.iconId, GameColors.Instance.red).Id;
        }
    }
    public class Witness : ActivatedSelectSlotBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override string InvalidTargetDialogue(CardSlot slot) => "You must choose one of your other cards to proselytise.";
        public override int StartingBonesCost => 1;
        public override bool IsValidTarget(CardSlot slot)
        {
            if (!base.IsValidTarget(slot))
                return false;

            // card is on same side of board and has less than 3 Prudence
            return slot.Card.OpponentCard == base.Card.OpponentCard && (slot.Card.GetStatusEffect<Prudence>()?.EffectPotency ?? 0) < 3;
        }

        public override bool CanActivate() => BoardManager.Instance.GetSlotsCopy(!base.Card.OpponentCard).Exists(IsValidTarget);
        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            if (!slot.Card.FaceDown)
                slot.Card.Anim.StrongNegationEffect();

            slot.Card.AddStatusEffectToFaceDown<Prudence>(1, false, delegate (int i)
            {
                slot.Card.HealDamage(2);
                return i;
            });
            yield return base.LearnAbility(0.4f);
        }
    }
}
