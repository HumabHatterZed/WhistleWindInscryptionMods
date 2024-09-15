using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_HealingStrike()
        {
            const string rulebookName = "Healing Strike";
            const string rulebookDescription = "Creatures struck by [creature] take no damage and instead gain Health equal to the damage dealt.";
            const string dialogue = "The curse continues unabated.";
            HealingStrike.ability = AbnormalAbilityHelper.CreateAbility<HealingStrike>(
                "sigilHealingStrike",
                rulebookName, rulebookDescription, dialogue, powerLevel: -2,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;

            Debug.Log($"{AbilitiesUtil.GetInfo(FlowerQueen.ability).rulebookName} {AbilitiesUtil.GetInfo(FlowerQueen.ability).HasMetaCategory(AbilityMetaCategory.AscensionUnlocked)}");
            AbilitiesUtil.GetInfo(FlowerQueen.ability).metaCategories.ForEach(x => Debug.Log(x));
        }
    }
    public class HealingStrike : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => amount > 0 && target != null;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            target.HealDamage(Mathf.Min(target.MaxHealth - target.Health, amount * 2));
            yield return base.LearnAbility(0.5f);
        }
    }
}
