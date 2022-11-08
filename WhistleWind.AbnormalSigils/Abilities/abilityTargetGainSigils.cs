using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_TargetGainSigils()
        {
            const string rulebookName = "Imbue Target";

            string rulebookDescription = "When [creature] is sacrificed, give its sigils to the sacrificing card.";
            if (SpellAPI.Enabled)
                rulebookDescription = "For spells: Activate upon selecting a target.\n\n" + rulebookDescription;

            const string dialogue = "Your beast is imbued with powerful energies.";
            TargetGainSigils.ability = AbnormalAbilityHelper.CreateAbility<TargetGainSigils>(
                Artwork.sigilTargetGainSigils, Artwork.sigilTargetGainSigils_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                modular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class TargetGainSigils : TargetedSpell
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool TargetAlly => true;

        public override IEnumerator EffectOnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            List<Ability> abilities = base.Card.Info.Abilities;
            abilities.RemoveAll(item => item == TargetGainSigils.ability ||
            item == TargetGainStats.ability || item == TargetGainStatsSigils.ability || item == Scrambler.ability);

            CardModificationInfo mod = new();

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
