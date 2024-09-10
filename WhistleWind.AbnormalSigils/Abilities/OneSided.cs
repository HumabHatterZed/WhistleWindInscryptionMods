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
            const string rulebookDescription = "[creature] deals 1 additional damage when striking creatures that cannot attack it.";
            const string dialogue = "A cheap hit.";
            OneSided.ability = AbnormalAbilityHelper.CreateAbility<OneSided>(
                "sigilOneSided",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: true)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook()
                .Info.SetFlipYIfOpponent().ability;
        }
    }
    public class OneSided : AbilityBehaviour, IModifyDamageTaken
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool activate = false;

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            return base.Card == attacker && AbnormalAbilityHelper.SimulateOneSidedAttack(base.Card, target);
        }
        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            activate = true;
            return damage + 1;
        }
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => 0;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => activate;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            activate = false;
            yield return base.PreSuccessfulTriggerSequence();
            yield return base.LearnAbility(0.4f);
        }
    }
}
