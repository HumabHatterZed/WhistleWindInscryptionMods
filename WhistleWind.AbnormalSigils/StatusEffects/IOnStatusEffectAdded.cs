using DiskCardGame;
using System.Collections;

namespace WhistleWind.AbnormalSigils.StatusEffects
{
    /// <summary>
    /// Trigger interface that activates whenever PlayableCard.AddStatusEffect is called.
    /// </summary>
    public interface IOnStatusEffectAdded
    {
        public bool RespondsToStatusEffectAdded(PlayableCard target, int amount, StatusEffectBehaviour statusEffect, bool alreadyHasStatus);
        public IEnumerator OnStatusEffectAdded(PlayableCard target, int amount, StatusEffectBehaviour statusEffect, bool alreadyHasStatus);
    }
}
