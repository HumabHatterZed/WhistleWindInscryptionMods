using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Scrambler()
        {
            string rulebookDescription = "When [creature] is sacrificed, give its stats to the sacrificing card then randomly scramble the result.";
            if (SpellAPI.Enabled)
                rulebookDescription = "For spells: Activate upon selecting a target.\n\n" + rulebookDescription;

            const string rulebookName = "Scrambler";
            const string dialogue = "Do you love your city?";

            Scrambler.ability = AbnormalAbilityHelper.CreateAbility<Scrambler>(
                Artwork.sigilScrambler, Artwork.sigilScrambler_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Scrambler : TargetedSpell
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool TargetAlly => true;

        private CardSlot targetSlot;
        public override IEnumerator OnSacrifice()
        {
            PlayableCard card = Singleton<BoardManager>.Instance.currentSacrificeDemandingCard;
            yield return base.PreSuccessfulTriggerSequence();

            CardModificationInfo info = new(base.Card.Attack, base.Card.Health);
            card.AddTemporaryMod(info);

            card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            ScrambleStats(card);

            yield return base.LearnAbility();
        }
        public override IEnumerator EffectOnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            targetSlot = slot;
            yield break;
        }
        public override bool ConditionForOnDie(bool wasSacrifice, PlayableCard killer)
        {
            return targetSlot != null;
        }
        public override IEnumerator EffectOnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            yield return SwitchCardView(View.Board, start: 0.75f);

            CardModificationInfo info = new(base.Card.Attack, base.Card.Health);
            targetSlot.Card.AddTemporaryMod(info);

            targetSlot.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            ScrambleStats(targetSlot.Card);

            yield return base.LearnAbility();
            yield return SwitchCardView(View.Default, start: 0.5f);
        }

        private IEnumerator ScrambleStats(PlayableCard card)
        {
            int totalStats = Mathf.Max(card.Attack + card.MaxHealth, 2);

            int newHp = 1 + SeededRandom.Range(0, totalStats, GetRandomSeed());
            int newAtk = totalStats - newHp;

            CardModificationInfo newStats = new(-card.Attack + newAtk, -card.MaxHealth + newHp);

            card.Anim.PlayTransformAnimation();
            card.AddTemporaryMod(newStats);
            card.OnStatsChanged();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
