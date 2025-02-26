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
            const string rulebookDescription = "When [creature] is struck, deal an equal amount of damage to its owner.";
            const string dialogue = "So this is what they feel...";
            SoulboundFlesh.ability = AbnormalAbilityHelper.CreateAbility<SoulboundFlesh>(
                "sigilSoulboundFlesh",
                rulebookName, rulebookDescription, dialogue, powerLevel: -5,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class SoulboundFlesh : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            return target == base.Card && amount > 0;
        }
        public override IEnumerator OnOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return LifeManager.Instance.ShowDamageSequence(amount, amount, !base.Card.OpponentCard, changeView: false);
            yield return new WaitForSeconds(0.3f);
            yield return base.LearnAbility(0.4f);
        }
    }
}