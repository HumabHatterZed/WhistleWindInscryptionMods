using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.AbnormalSigils.StatusEffects
{
    public abstract class ReduceStatusEffectBehaviour : StatusEffectBehaviour, IOnUpkeepInHand
    {
        public abstract int SeverityReduction { get; }

        public override bool RespondsToUpkeep(bool playerUpkeep) => CanReduceOnUpkeep(playerUpkeep);
        public bool RespondsToUpkeepInHand(bool playerUpkeep) => CanReduceOnUpkeep(playerUpkeep);

        public override IEnumerator OnUpkeep(bool playerUpkeep) => OnReduceOnUpkeep();
        public IEnumerator OnUpkeepInHand(bool playerUpkeep) => OnReduceOnUpkeep();

        public virtual bool CanReduceOnUpkeep(bool playerUpkeep)
        {
            //Debug.Log($"Reduce? {base.PlayableCard.Info.name} {base.PlayableCard.InHand}: {base.PlayableCard.OpponentCard != playerUpkeep} {TurnManager.Instance.TurnNumber > TurnGained}");
            return base.PlayableCard.OpponentCard != playerUpkeep && TurnManager.Instance.TurnNumber > TurnGained;
        }
        public virtual IEnumerator OnReduceOnUpkeep()
        {
            //Debug.Log($"Reduce: {EffectSeverity} | {EffectSeverity - SeverityReduction}");
            yield return new WaitForSeconds(0.3f);
            base.PlayableCard.Anim.LightNegationEffect();
            ViewManager.Instance.SwitchToView(base.PlayableCard.InHand ? View.Hand : View.Board);
            yield return new WaitForSeconds(0.2f);
            AddSeverity(-SeverityReduction, true);
            if (EffectSeverity <= 0)
                Destroy();

            yield return new WaitForSeconds(0.3f);
        }
    }

    public abstract class StatusEffectBehaviour : SpecialCardBehaviour
    {
        // used for the card mod singleton IDs
        public abstract string CardModSingletonName { get; }

        public virtual bool EffectCanBeInherited => false;

        public int EffectSeverity = 0;
        public int TurnGained = -1;
        // the ability used for the icon of this status effect
        public AbilityInfo IconAbilityInfo => StatusEffectManager.AllStatusEffects.Find(x => x.BehaviourType == this.GetType())?.IconAbilityInfo;

        public virtual List<string> EffectDecalIds() => new();

        // allows for adding extra stacks of an effect to a card
        private void Start()
        {
            if (base.PlayableCard != null)
            {
                EffectSeverity = base.PlayableCard.GetAbilityStacks(IconAbilityInfo.ability);

                base.PlayableCard.AddTemporaryMod(EffectCountMod());
                if (EffectDecalIds().Count > 0)
                    base.PlayableCard.AddTemporaryMod(EffectDecalMod());
            }
        }

        public void AddSeverity(int amount, bool updateDecals)
        {
            EffectSeverity += amount;
            CardModificationInfo mod = base.PlayableCard.TemporaryMods.Find(x => x.singletonId == CardModSingletonName);
            base.PlayableCard.RemoveTemporaryMod(mod);
            if (EffectSeverity > 0)
                base.PlayableCard.AddTemporaryMod(EffectCountMod());

            if (updateDecals)
            {
                mod = base.PlayableCard.TemporaryMods.Find(x => x.singletonId == CardModSingletonName + "_decal");
                base.PlayableCard.RemoveTemporaryMod(mod);
                if (EffectDecalIds().Count > 0)
                    base.PlayableCard.AddTemporaryMod(EffectDecalMod());
            }
        }
        public void SetSeverity(int amount, bool updateDecals)
        {
            EffectSeverity = amount;
            CardModificationInfo mod = base.PlayableCard.TemporaryMods.Find(x => x.singletonId == CardModSingletonName);
            base.PlayableCard.RemoveTemporaryMod(mod);
            if (EffectSeverity > 0)
                base.PlayableCard.AddTemporaryMod(EffectCountMod());

            if (updateDecals)
            {
                mod = base.PlayableCard.TemporaryMods.Find(x => x.singletonId == CardModSingletonName + "_decal");
                base.PlayableCard.RemoveTemporaryMod(mod);
                if (EffectDecalIds().Count > 0)
                    base.PlayableCard.AddTemporaryMod(EffectDecalMod());
            }
        }

        public CardModificationInfo EffectDecalMod()
        {
            CardModificationInfo result = StatusEffectManager.StatusMod(CardModSingletonName + "_decal", IconAbilityInfo.PositiveEffect, EffectCanBeInherited);
            result.decalIds = EffectDecalIds();
            result.nonCopyable = true;
            return result;
        }
        public CardModificationInfo EffectCountMod()
        {
            CardModificationInfo result = StatusEffectManager.StatusMod(CardModSingletonName, IconAbilityInfo.PositiveEffect, EffectCanBeInherited);
            for (int i = 0; i < EffectSeverity; i++)
                result.AddAbilities(IconAbilityInfo.ability);

            result.nonCopyable = true;
            return result;
        }
    }
}
