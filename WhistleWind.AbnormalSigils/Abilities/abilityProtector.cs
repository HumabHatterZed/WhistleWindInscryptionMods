using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Protector()
        {
            const string rulebookName = "Protector";
            const string rulebookDescription = "Creatures adjacent to this card take 1 less damage when struck.";
            const string dialogue = "Your beast shields its ally against the blow.";
            const string triggerText = "[creature] shields its friend!";
            Protector.ability = AbnormalAbilityHelper.CreateAbility<Protector>(
                "sigilProtector",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3,
                modular: false, opponent: false, canStack: true).Id;
        }
    }
    public class Protector : AbilityBehaviour, IModifyDamageTaken
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            // only respond if the target hasn't died
            if (amount > 0 && !target.Dead)
                return base.Card.Slot.GetAdjacentCards().Contains(target);

            return false;
        }
        public override IEnumerator OnOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return base.LearnAbility(0.4f);
        }

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            if (base.Card.Slot.GetAdjacentCards().Contains(target) && damage > 0)
                return attacker == null || attacker.LacksAbility(Piercing.ability);

            return false;
        }

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            damage--;
            return damage;
        }

        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => 0;
    }
}
