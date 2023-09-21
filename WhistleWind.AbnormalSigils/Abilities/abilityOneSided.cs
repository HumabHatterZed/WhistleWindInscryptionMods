using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_OneSided()
        {
            const string rulebookName = "Opportunistic";
            const string rulebookDescription = "[creature] deals 1 additional damage to opposing cards that cannot attack this card during their own turn.";
            const string dialogue = "A cheap hit.";
            OneSided.ability = AbnormalAbilityHelper.CreateAbility<OneSided>(
                "sigilOneSided",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: true).Id;
        }
    }
    public class OneSided : AbilityBehaviour, IModifyDamageTaken
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool activate = false;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => activate;
        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            return base.Card == attacker && CheckValid(target);
        }

        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            activate = false;
            yield return base.PreSuccessfulTriggerSequence();
            yield return base.LearnAbility(0.4f);
        }
        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            activate = true;
            damage++;
            return damage;
        }

        private bool CheckValid(PlayableCard target)
        {
            // if target has no Power, if this card can submerge or is facedown (cannot be hit), return true by default
            if (target.Attack == 0)
                return true;

            if (target.HasAbility(Ability.Flying) && base.Card.LacksAbility(Ability.Reach))
                return true;

            // Persistent check
            if ((base.Card.HasAbility(Ability.TailOnHit) && !base.Card.Status.lostTail) ||
                base.Card.HasAbility(Ability.PreventAttack) ||
                base.Card.HasAnyOfAbilities(Ability.Submerge, Ability.SubmergeSquid) || base.Card.FaceDown)
                return target.LacksAbility(Persistent.ability);

            // Shield checks
            if (base.Card.HasShield())
                return target.LacksAllAbilities(Piercing.ability, Ability.Sharp, Reflector.ability);

            return !target.GetOpposingSlots().Contains(base.Card.Slot);
        }

        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => 0;
    }
}
