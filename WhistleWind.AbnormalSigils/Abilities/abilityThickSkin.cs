using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_ThickSkin()
        {
            const string rulebookName = "Thick Skin";
            const string rulebookDescription = "Whenever [creature] takes damage, reduce that damage by 1.";
            const string dialogue = "Your creature's hide absorbs the blow.";
            const string triggerText = "[creature] absorbs the blow.";
            ThickSkin.ability = AbnormalAbilityHelper.CreateAbility<ThickSkin>(
                "sigilThickSkin",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: true, opponent: true, canStack: true).Id;
        }
    }
    public class ThickSkin : AbilityBehaviour, IModifyDamageTaken
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTakeDamage(PlayableCard source) => source != null;
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return base.LearnAbility(0.4f);
        }

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            if (base.Card == target && damage > 0)
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
