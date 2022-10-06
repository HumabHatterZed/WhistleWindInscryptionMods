using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_TargetGainStatsSigils()
        {
            const string rulebookName = "Enhance Target";
            const string rulebookDescription = "Spells only: The affected card gains this card's stats and sigils.";
            const string dialogue = "Your beast is empowered.";
            TargetGainStatsSigils.ability = AbilityHelper.CreateAbility<TargetGainStatsSigils>(
                Resources.sigilTargetGainStatsSigils, Resources.sigilTargetGainStatsSigils_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: false,
                overrideModular: true).Id;
        }
    }
    public class TargetGainStatsSigils : TargetedSpell
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool TargetAlly => true;

        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            List<Ability> abilities = base.Card.Info.Abilities;
            abilities.RemoveAll(item => item == TargetGainSigils.ability || item == TargetGainStats.ability || item == TargetGainStatsSigils.ability || item == Scrambler.ability);

            CardModificationInfo mod = new(base.Card.Attack, base.Card.Health);

            foreach (Ability item in abilities)
            {
                mod.abilities.Add(item);
            }

            slot.Card.Anim.PlayTransformAnimation();
            slot.Card.AddTemporaryMod(mod);
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();
        }
    }
}
