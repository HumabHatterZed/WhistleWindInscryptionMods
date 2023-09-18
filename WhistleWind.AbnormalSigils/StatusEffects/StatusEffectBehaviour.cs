using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.AbnormalSigils.StatusEffects
{
    public abstract class StatusEffectBehaviour : SpecialCardBehaviour
    {
        // used for the card mod singleton IDs
        public abstract string CardModSingletonName { get; }

        public virtual bool EffectCanBeInherited => false;

        public int StatusEffectCount = 0;

        // the ability used for the icon of this status effect
        public AbilityInfo IconAbilityInfo => StatusEffectManager.AllStatusEffects.Find(x => x.BehaviourType == this.GetType()).IconAbilityInfo;

        public virtual List<string> EffectDecalIds() => new();

        // allows for adding extra stacks of an effect to a card
        private void Start()
        {
            int startingStacks = Mathf.Max(1, base.PlayableCard?.GetAbilityStacks(IconAbilityInfo.ability) ?? 0);

            StatusEffectCount = startingStacks;
            AbnormalPlugin.Log.LogInfo($"Start: {StatusEffectCount}");

            base.PlayableCard.AddTemporaryMod(EffectCountMod());
            if (EffectDecalIds().Count > 0)
                base.PlayableCard.AddTemporaryMod(EffectDecalMod());
        }

        public void UpdateStatusEffectCount(int numToAdd, bool updateDecals)
        {
            StatusEffectCount += numToAdd;
            base.PlayableCard.AddTemporaryMod(EffectCountMod());

            if (updateDecals)
                base.PlayableCard.AddTemporaryMod(EffectDecalMod());
        }
        public CardModificationInfo EffectDecalMod()
        {
            CardModificationInfo result = StatusEffectManager.StatusMod(CardModSingletonName + "_decal", IconAbilityInfo.PositiveEffect, EffectCanBeInherited);
            result.decalIds = EffectDecalIds();

            return result;
        }
        public CardModificationInfo EffectCountMod()
        {
            CardModificationInfo result = StatusEffectManager.StatusMod(CardModSingletonName, IconAbilityInfo.PositiveEffect, EffectCanBeInherited);
            for (int i = 0; i < StatusEffectCount; i++)
                result.AddAbilities(IconAbilityInfo.ability);

            return result;
        }
    }
}
