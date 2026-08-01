using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public abstract class PanicBase : ModifyOnTurnEndStatusEffectBehaviour {
        public override int PotencyModification => -EffectPotency;

        public override bool RespondsToStatusEffectRemoved(PlayableCard target, StatusEffectBehaviour statusEffect) {
            return target == base.PlayableCard && statusEffect.IconAbility == this.IconAbility;
        }
        public override IEnumerator OnStatusEffectRemoved(PlayableCard target, StatusEffectBehaviour statusEffect) {
            // remove all Sinking from this card when ending the panic
            yield return target.RemoveStatusEffect<Sinking>();
        }

        public static bool IsPanicking(PlayableCard card) {
            return card.HasStatusEffect<PanicBase>();
        }
    }
}
