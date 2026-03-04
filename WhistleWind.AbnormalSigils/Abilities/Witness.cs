using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Witness() {
            const string rulebookName = "Witness";
            const string rulebookDescription = "Pay 1 Bone to inflict 1 Flagellation and increase a chosen card's Health by 2. This effect can stack up to 3 times.";
            const string dialogue = "The truth will set you free.";
            const string triggerText = "Behold [creature] and be reborn.";
            Witness.ID = AbnormalAbilityHelper.CreateActivatedAbility<Witness>(
                "sigilWitness",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2)
                .SetAbilityRedirect("Flagellation", Prudence.iconId, GameColors.Instance.red)
                .Id;
        }
    }
    /// <summary>
    /// Pay 1 Bone to inflict 1 Flagellation and increase a chosen card's Health by 2. This effect can stack up to 3 times.
    /// </summary>
    public class Witness : ActivatedSelectSlotBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        public override string InvalidTargetDialogue(CardSlot slot) => "You must choose one of your other cards to proselytise.";
        public override int StartingBonesCost => 1;
        public override bool IsValidTarget(CardSlot slot) {
            // card has less than 3 Prudence
            return base.IsValidTarget(slot) && slot.Card.GetStatusEffectPotency<Prudence>() < 3;
        }

        public override bool CanActivate() => BoardManager.Instance.CardsOnBoard.Exists(x => IsValidTarget(x.Slot));
        public override IEnumerator OnValidTargetSelected(CardSlot slot) {
            if (!slot.Card.FaceDown)
                slot.Card.Anim.StrongNegationEffect();

            slot.Card.HealDamage(2);
            yield return slot.Card.AddStatusEffectToFaceDown<Prudence>(1, false);
            yield return base.LearnAbility(0.4f);
        }
    }
}
