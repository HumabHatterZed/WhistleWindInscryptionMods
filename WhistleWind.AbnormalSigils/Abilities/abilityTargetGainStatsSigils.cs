﻿using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_TargetGainStatsSigils()
        {
            string rulebookDescription = "When [creature] is sacrificed, give its stats and sigils to the sacrificing card.";
            if (SpellAPI.Enabled)
                rulebookDescription = "For spells: Activate upon selecting a target.\n" + rulebookDescription;

            const string rulebookName = "Enhance Target";
            const string dialogue = "Your beast is empowered.";
            TargetGainStatsSigils.ability = AbnormalAbilityHelper.CreateAbility<TargetGainStatsSigils>(
                Artwork.sigilTargetGainStatsSigils, Artwork.sigilTargetGainStatsSigils_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class TargetGainStatsSigils : TargetedSpell
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool TargetAlly => true;

        public override IEnumerator OnSacrifice()
        {
            PlayableCard card = Singleton<BoardManager>.Instance.currentSacrificeDemandingCard;
            CardModificationInfo mod = GetCardMod();

            yield return base.PreSuccessfulTriggerSequence();
            card.Anim.LightNegationEffect();
            card.AddTemporaryMod(mod);
            yield return new WaitForSeconds(0.2f);
            yield return base.LearnAbility();

        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            CardModificationInfo mod = GetCardMod();

            slot.Card.Anim.PlayTransformAnimation();
            slot.Card.AddTemporaryMod(mod);
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();
        }
        private CardModificationInfo GetCardMod()
        {
            CardModificationInfo mod = new(base.Card.Attack, base.Card.Health);

            List<Ability> abilities = base.Card.Info.Abilities;
            abilities.RemoveAll(item => item == TargetGainSigils.ability ||
            item == TargetGainStats.ability || item == TargetGainStatsSigils.ability || item == Scrambler.ability);

            foreach (Ability item in abilities)
            {
                mod.abilities.Add(item);
            }

            return mod;
        }
    }
}
