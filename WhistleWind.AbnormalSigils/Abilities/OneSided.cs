using Core.AbilityClasses;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_OneSided()
        {
            const string rulebookName = "Opportunistic";
            const string rulebookDescription = "[creature] deals 1 additional damage when striking injured cards.";
            const string dialogue = "Blood in the water.";
            OneSided.ability = AbnormalAbilityHelper.CreateAbility<OneSided>(
                "sigilOneSided",
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: true, opponent: true, canStack: true)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook()
                .Info.SetFlipYIfOpponent().ability;
        }
    }
    public class OneSided : ModifyDamageDealtAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool activate = false;

        public override bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            return base.Card == attacker && target.Health < target.MaxHealth;
        }
        public override int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            activate = true;
            return ++damage;
        }
        public override int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => 0;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => target != null && amount > 0;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            if (activate)
            {
                activate = false;
                yield return base.LearnAbility(0.4f);
            }
        }
    }
}
