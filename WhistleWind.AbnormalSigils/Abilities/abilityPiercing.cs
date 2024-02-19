using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Piercing()
        {
            const string rulebookName = "Piercing";
            const string rulebookDescription = "Damage dealt by this card cannot be negated or reduced by sigils such as Armoured or Thick Skin.";
            const string dialogue = "Your beast runs mine through.";

            Piercing.ability = AbnormalAbilityHelper.CreateAbility<Piercing>(
                "sigilPiercing",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: false).Id;
        }
    }
    public class Piercing : AbilityBehaviour
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
    }
}
