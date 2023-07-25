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
        public abstract string SingletonName { get; }
        public virtual bool EffectCanBeInherited => false;

        // always start with 1 stack
        public int effectCount = 1;

        // the ability used for the icon of this status effect
        public AbilityInfo IconAbilityInfo => StatusEffectManager.AllStatusEffects[this.GetType()];

        public virtual List<string> EffectDecalIds() => new();

        public CardModificationInfo GetEffectDecalMod()
        {
            CardModificationInfo result = StatusEffectManager.StatusMod(SingletonName + "_decal", IconAbilityInfo.PositiveEffect, EffectCanBeInherited);
            result.decalIds = EffectDecalIds();

            return result;
        }
        public CardModificationInfo GetEffectCountMod()
        {
            CardModificationInfo result = StatusEffectManager.StatusMod(SingletonName, IconAbilityInfo.PositiveEffect, EffectCanBeInherited);
            for (int i = 0; i < effectCount; i++)
                result.AddAbilities(IconAbilityInfo.ability);

            return result;
        }
    }
}
