using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UIElements.StyleVariableResolver;

namespace WhistleWind.AbnormalSigils.StatusEffects
{
    public abstract class StatusEffectBehaviour : SpecialCardBehaviour, IOnStatusEffectAdded
    {
        private int _priority = int.MaxValue;
        public const string _DECAL = "_decal";
        public const string STATUS_ = "status_";

        public override int Priority => _priority;

        public int TurnGained = -1;
        public int EffectPotency { get; set; } = 0;
        public string ModSingletonName => STATUS_ + AbilityManager.AllAbilityInfos.AbilityByID(IconAbility).rulebookName;

        public abstract Ability IconAbility { get; }
        public abstract SpecialTriggeredAbility StatusEffect { get; }

        public virtual bool EffectCanBeInherited { get; set; } = false;
        public virtual List<string> EffectDecalIds() => new();

        public virtual bool RespondsToStatusEffectAdded(PlayableCard target, int amount, StatusEffectBehaviour statusEffect, bool alreadyHasStatus)
        {
            return false;
        }
        public virtual IEnumerator OnStatusEffectAdded(PlayableCard target, int amount, StatusEffectBehaviour statusEffect, bool alreadyHasStatus)
        {
            yield break;
        }

        public void ModifyPotency(int amount, bool updateDecals)
        {
            EffectPotency += amount;

            CardModificationInfo mod = GetStatusPotencyMod(false);
            base.PlayableCard.RemoveTemporaryMod(mod, false);
            if (EffectPotency > 0)
                base.PlayableCard.AddTemporaryMod(GetStatusPotencyMod(true));
            else
                base.PlayableCard.OnStatsChanged();

            if (updateDecals)
            {
                CardModificationInfo mod2 = GetStatusDecalsMod(false);
                base.PlayableCard.RemoveTemporaryMod(mod2, false);
                if (EffectDecalIds().Count > 0)
                    base.PlayableCard.AddTemporaryMod(GetStatusDecalsMod(true));
                else
                    base.PlayableCard.OnStatsChanged();
            }
        }
        public void SetPotency(int amount, bool updateDecals)
        {
            EffectPotency = amount;

            CardModificationInfo mod = GetStatusPotencyMod(false);
            base.PlayableCard.RemoveTemporaryMod(mod, false);

            if (EffectPotency > 0)
                base.PlayableCard.AddTemporaryMod(GetStatusPotencyMod(true));
            else
                base.PlayableCard.OnStatsChanged();

            if (updateDecals)
            {
                CardModificationInfo mod2 = base.PlayableCard.TemporaryMods.Find(x => x.singletonId == ModSingletonName + _DECAL);
                base.PlayableCard.RemoveTemporaryMod(mod2, false);
                if (EffectDecalIds().Count > 0)
                    base.PlayableCard.AddTemporaryMod(GetStatusDecalsMod(true));
                else
                    base.PlayableCard.OnStatsChanged();
            }
        }

        public CardModificationInfo GetStatusPotencyMod(bool createNew)
        {
            CardModificationInfo retval;
            if (createNew)
            {
                retval = new()
                {
                    singletonId = ModSingletonName,
                    nonCopyable = !EffectCanBeInherited
                };
                retval.specialAbilities.Add(StatusEffect);
                retval.SetStatusEffect();
                for (int i = 0; i < EffectPotency; i++)
                    retval.AddAbilities(IconAbility);
            }
            else
            {
                retval = base.PlayableCard.TemporaryMods.Find(x => x.singletonId == ModSingletonName);
            }
            return retval;
        }

        public CardModificationInfo GetStatusDecalsMod(bool createNew)
        {
            CardModificationInfo retval;
            if (createNew)
            {
                retval = new()
                {
                    singletonId = (ModSingletonName + _DECAL),
                    nonCopyable = !EffectCanBeInherited
                };
                retval.specialAbilities.Add(StatusEffect);
                retval.decalIds = EffectDecalIds();
                retval.SetStatusEffect();
            }
            retval = base.PlayableCard.TemporaryMods.Find(x => x.singletonId == (ModSingletonName + _DECAL));
            return retval;
        }


        public void DestroyStatusEffect(bool updateDisplay = true)
        {
            List<CardModificationInfo> mods = base.PlayableCard.TemporaryMods.Where(x => x.specialAbilities.Contains(this.StatusEffect)).ToList();
            foreach (CardModificationInfo mod in mods)
            {
                base.PlayableCard.RemoveTemporaryMod(mod, false);
            }
            if (updateDisplay)
                base.PlayableCard.OnStatsChanged();

            base.Destroy();
        }
    }
}
