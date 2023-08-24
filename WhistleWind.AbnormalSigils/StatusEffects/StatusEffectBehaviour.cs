using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;
using WhistleWind.AbnormalSigils.Core;

namespace WhistleWind.AbnormalSigils.StatusEffects
{
    public abstract class StatusEffectBehaviour : SpecialCardBehaviour
    {
        // used for the card mod singleton IDs
        public abstract string CardModSingletonName { get; }

        public virtual bool EffectCanBeInherited => false;

        // always start with 1 stack
        public int StatusEffectCount = 1;

        // the ability used for the icon of this status effect
        public AbilityInfo IconAbilityInfo => StatusEffectManager.AllStatusEffects.Find(x => x.BehaviourType == this.GetType()).IconAbilityInfo;

        public virtual List<string> EffectDecalIds() => new();

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
