using System.Linq;
using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Scrambler()
        {
            const string rulebookName = "Scrambler";
            const string rulebookDescription = "When this card is sacrificed, add its stats onto the card it was sacrificed to, then scramble the card's stats.";
            const string dialogue = "Do you love your city?";

            return WstlUtils.CreateAbility<Scrambler>(
                Resources.sigilScrambler,
                rulebookName, rulebookDescription, dialogue, 3, true);
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
            CardModificationInfo mod = new CardModificationInfo(base.Card.Attack, base.Card.MaxHealth);

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

            CardModificationInfo newStats = new CardModificationInfo(-card.Attack + newAtk, -card.MaxHealth + newHp);
            card.AddTemporaryMod(newStats);
            card.OnStatsChanged();
        }
    }
}
