using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public abstract class SodaEffectBehaviour : ModifyOnTurnEndStatusEffectBehaviour
    {
        public abstract string SingletonId { get; }
        public abstract Ability AbilityToAdd { get; }

        public override int PotencyModification => -1;

        public override bool RespondsToStatusEffectRemoved(PlayableCard target, StatusEffectBehaviour statusEffect)
        {
            return target == base.PlayableCard && statusEffect.GetType() == this.GetType();
        }
        public override IEnumerator OnStatusEffectRemoved(PlayableCard target, StatusEffectBehaviour statusEffect)
        {
            CardModificationInfo mod = base.PlayableCard.TemporaryMods.Find(x => HelperMethods.CompareSingleton(x.singletonId, SingletonId));
            base.PlayableCard.Status.hiddenAbilities.Remove(AbilityToAdd);
            if (mod != null)
                base.PlayableCard.RemoveTemporaryMod(mod);

            yield return base.OnStatusEffectRemoved(target, statusEffect);
        }
    }
}
