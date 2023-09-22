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

        public int EffectSeverity = 0;

        // the ability used for the icon of this status effect
        public AbilityInfo IconAbilityInfo => StatusEffectManager.AllStatusEffects.Find(x => x.BehaviourType == this.GetType())?.IconAbilityInfo;

        public virtual List<string> EffectDecalIds() => new();

        // allows for adding extra stacks of an effect to a card
        private void Start()
        {
            if (base.PlayableCard == null) return;

            EffectSeverity = Mathf.Max(1, base.PlayableCard.GetAbilityStacks(IconAbilityInfo.ability));

            base.PlayableCard.AddTemporaryMod(EffectCountMod());
            if (EffectDecalIds().Count > 0)
                base.PlayableCard.AddTemporaryMod(EffectDecalMod());
        }

        public void UpdateStatusEffectCount(int numToAdd, bool updateDecals)
        {
            EffectSeverity += numToAdd;
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
            for (int i = 0; i < EffectSeverity; i++)
                result.AddAbilities(IconAbilityInfo.ability);

            return result;
        }
    }
}
