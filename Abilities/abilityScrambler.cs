using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Scrambler()
        {
            const string rulebookName = "Scrambler";
            const string rulebookDescription = "When this card is sacrificed, add its stats to the card it was sacrificed to, then scramble that card's stats.";
            const string dialogue = "Do you love your city?";

            Scrambler.ability = AbilityHelper.CreateAbility<Scrambler>(
                Resources.sigilScrambler, Resources.sigilScrambler_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: true, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Scrambler : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToSacrifice()
        {
            return true;
        }
        public override IEnumerator OnSacrifice()
        {
            yield return base.PreSuccessfulTriggerSequence();

            var demandingCard = Singleton<BoardManager>.Instance.currentSacrificeDemandingCard;
            CardModificationInfo mod = new(base.Card.Attack, base.Card.MaxHealth);

            demandingCard.AddTemporaryMod(mod);
            demandingCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return demandingCard.Anim.FlipInAir();
            ScrambleStats(demandingCard);
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility(0.5f);
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
