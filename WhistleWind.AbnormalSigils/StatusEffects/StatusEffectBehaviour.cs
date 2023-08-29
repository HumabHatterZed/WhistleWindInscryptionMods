using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;

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
            int startingStacks = base.PlayableCard?.GetAbilityStacks(IconAbilityInfo.ability) ?? 1;

            StatusEffectCount = startingStacks;
            //AbnormalPlugin.Log.LogInfo($"Start: {StatusEffectCount}");
            CardModificationInfo decalMod = EffectDecalMod();
            if (decalMod.DecalIds.Count > 0)
                base.PlayableCard.AddTemporaryMod(decalMod);
        }

        public void UpdateStatusEffectCount(int numToAdd, bool updateDecals)
        {
            StatusEffectCount += numToAdd;
            base.PlayableCard.AddTemporaryMod(EffectCountMod());

            if (updateDecals)
            {
                CardModificationInfo decalMod = EffectDecalMod();
                if (decalMod.DecalIds.Count > 0)
                    base.PlayableCard.AddTemporaryMod(decalMod);
            }
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
