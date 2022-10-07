using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_TargetGainStats()
        {
            const string rulebookName = "Strengthen Target";
            const string rulebookDescription = "Spells only: The affected card gains this card's stats.";
            const string dialogue = "Your beast's strength grows.";
            TargetGainStats.ability = AbilityHelper.CreateAbility<TargetGainStats>(
                Artwork.sigilTargetGainStats, Artwork.sigilTargetGainStats_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: false,
                overrideModular: true).Id;
        }
    }
    public class TargetGainStats : TargetedSpell
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool TargetAlly => true;

        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            CardModificationInfo mod = new(base.Card.Attack, base.Card.Health);

            slot.Card.Anim.PlayTransformAnimation();
            slot.Card.AddTemporaryMod(mod);
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();
        }
    }
}
