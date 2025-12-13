using Core.Helpers;
using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;

namespace Core.AbilityClasses {
    public abstract class ModifyDamageDealtAbilityBehaviour : AbilityBehaviour, IModifyDamageTaken, IPostCardGettingAttacked {
        public abstract bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage);
        public abstract int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage);
        public abstract int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker);

        public virtual bool RespondsToPostCardGettingAttacked(PlayableCard target, PlayableCard attacker) => base.Card == attacker;
        public virtual IEnumerator OnPostCardGettingAttacked(PlayableCard target, PlayableCard attacker) {
            yield return ShowModifiedAttackDamage.ShowModifiedDamage(target, attacker);
        }

        public virtual int PostCardGettingAttackedPriority(PlayableCard target, PlayableCard attacker) => 0;
    }
}
