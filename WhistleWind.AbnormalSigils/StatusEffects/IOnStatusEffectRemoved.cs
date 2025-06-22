using DiskCardGame;
using System.Collections;

namespace WhistleWind.AbnormalSigils.StatusEffects {
    /// <summary>
    /// Trigger interface that activates whenever PlayableCard.AddStatusEffect is called.
    /// </summary>
    public interface IOnStatusEffectRemoved {
        public bool RespondsToStatusEffectRemoved(PlayableCard target, StatusEffectBehaviour statusEffect);
        public IEnumerator OnStatusEffectRemoved(PlayableCard target, StatusEffectBehaviour statusEffect);
    }
}
