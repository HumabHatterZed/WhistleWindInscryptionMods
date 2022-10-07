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
        private void Ability_Scrambler()
        {
            const string rulebookName = "Scrambler";
            const string rulebookDescription = "Targeted Spell: Give the target this card's stats then scramble its stats.";
            const string dialogue = "Do you love your city?";

            Scrambler.ability = AbilityHelper.CreateAbility<Scrambler>(
                Artwork.sigilScrambler, Artwork.sigilScrambler_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: false, opponent: false, canStack: false, isPassive: false,
                overrideModular: true).Id;
        }
    }
    public class Scrambler : TargetedSpell
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool TargetAlly => true;

        private CardSlot targetSlot;
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            targetSlot = slot;
            yield break;
        }
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return targetSlot != null;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            yield return new WaitForSeconds(0.75f);

            CardModificationInfo info = new(base.Card.Attack, base.Card.Health);
            targetSlot.Card.AddTemporaryMod(info);

            targetSlot.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            targetSlot.Card.Anim.PlayTransformAnimation();
            ScrambleStats(targetSlot.Card);
            yield return new WaitForSeconds(0.5f);

            yield return base.LearnAbility();
            yield return SwitchCardView(View.Default, start: 0.5f);
        }

        private void ScrambleStats(PlayableCard card)
        {
            int totalStats = card.Attack + card.MaxHealth;

            int newHp = SeededRandom.Range(1, totalStats + 1, GetRandomSeed());
            int newAtk = totalStats - newHp;

            CardModificationInfo newStats = new(-card.Attack + newAtk, -card.MaxHealth + newHp);
            card.AddTemporaryMod(newStats);
            card.OnStatsChanged();
        }
    }
}
