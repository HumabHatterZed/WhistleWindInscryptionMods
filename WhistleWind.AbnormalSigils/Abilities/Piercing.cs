using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Piercing()
        {
            const string rulebookName = "Piercing";
            const string rulebookDescription = "[creature] will strike through shields. Damage dealt by this card cannot be reduced.";
            const string dialogue = "Your beast runs mine through.";

            Piercing.ability = AbnormalAbilityHelper.CreateAbility<Piercing>(
                "sigilPiercing",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Piercing : AbilityBehaviour, IModifyDamageTaken
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => true;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            if (target.HasAnyOfAbilities(Ability.DeathShield, Ability.PreventAttack, ThickSkin.ability)
                || target.Slot.GetAdjacentCards().Exists(x => x.HasAbility(Protector.ability)))
            {
                yield return LearnAbility(0.25f);
            }
        }

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => attacker == base.Card && damage < originalDamage;
        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => originalDamage;
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => -1000;
    }
}
