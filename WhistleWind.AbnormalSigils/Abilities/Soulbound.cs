using DiskCardGame;
using InscryptionAPI.Triggers;
using System;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_SoulboundFlesh()
        {
            const string rulebookName = "Soulbound";
            const string rulebookDescription = "Whenever [creature] takes damage, its owner also takes damage.";
            const string dialogue = "So this is what they feel...";
            Soulbound.ability = AbnormalAbilityHelper.CreateAbility<Soulbound>(
                "sigilSoulboundFlesh",
                rulebookName, rulebookDescription, dialogue, powerLevel: -5,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Soulbound : AbilityBehaviour, IPreTakeDamage
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public bool RespondsToPreTakeDamage(PlayableCard source, int damage) => damage > 0;
        public IEnumerator OnPreTakeDamage(PlayableCard source, int damage)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return LifeManager.Instance.ShowDamageSequence(damage, damage, !base.Card.OpponentCard, changeView: false);
            yield return new WaitForSeconds(0.3f);
            yield return base.LearnAbility(0.4f);
        }
    }
}