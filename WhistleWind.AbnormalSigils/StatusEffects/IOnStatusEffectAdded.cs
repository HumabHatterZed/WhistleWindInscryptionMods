using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
