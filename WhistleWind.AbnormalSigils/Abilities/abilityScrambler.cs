using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Scrambler()
        {
            string rulebookDescription = "When [creature] is sacrificed, give its stats to the sacrificing card then scramble its new stats.";
            if (SpellAPI.Enabled)
                rulebookDescription = "For spells: Activate upon selecting a target.\n" + rulebookDescription;

            const string rulebookName = "Scrambler";
            const string dialogue = "Do you love your city?";

            Scrambler.ability = AbnormalAbilityHelper.CreateAbility<Scrambler>(
                Artwork.sigilScrambler, Artwork.sigilScrambler_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: false).Id;
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
            CardModificationInfo info = new(base.Card.Attack, base.Card.Health);

            yield return base.PreSuccessfulTriggerSequence();

            card.Anim.LightNegationEffect();
            card.AddTemporaryMod(info);
            yield return ScrambleStats(card);

            yield return base.LearnAbility();
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            targetSlot = slot;
            yield break;
        }
        public override bool ConditionForOnDie(bool wasSacrifice, PlayableCard killer)
        {
            return targetSlot != null;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            yield return HelperMethods.ChangeCurrentView(View.Board, endDelay: 0.75f);

            CardModificationInfo info = new(base.Card.Attack, base.Card.Health);
            targetSlot.Card.AddTemporaryMod(info);

            targetSlot.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            yield return ScrambleStats(targetSlot.Card);

            yield return base.LearnAbility();
            yield return HelperMethods.ChangeCurrentView(View.Board, startDelay: 0.5f);
        }

        private IEnumerator ScrambleStats(PlayableCard card, bool inHand = false)
        {
            int randomSeed = base.GetRandomSeed();
            int totalStats = card.Attack + card.MaxHealth;

            int newHp = 1 + SeededRandom.Range(0, totalStats, randomSeed++);
            int newAtk = totalStats - newHp;

            CardModificationInfo newStats = new(-card.Attack + newAtk, -card.MaxHealth + newHp);

            if (!inHand)
                card.Anim.PlayTransformAnimation();

            card.AddTemporaryMod(newStats);
            card.OnStatsChanged();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
