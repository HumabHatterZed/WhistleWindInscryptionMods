using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_QueenNest()
        {
            const string rulebookName = "Queen Nest";
            const string rulebookDescription = "While this card is on the board, create a Worker Bee in your hand whenever another card dies. A Worker Bee is defined as: 1 Power, 1 Health.";
            const string dialogue = "For the hive.";
            QueenNest.ability = AbnormalAbilityHelper.CreateAbility<QueenNest>(
                Artwork.sigilQueenNest, Artwork.sigilQueenNest_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
    public class QueenNest : OpponentDrawCreatedCard
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override CardInfo CardToDraw
        {
            get
            {
                CardInfo cardByName = CardLoader.GetCardByName("wstl_queenBeeWorker");
                cardByName.Mods.AddRange(base.GetNonDefaultModsFromSelf(this.Ability));
                return cardByName;
            }
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (card != base.Card)
                return base.Card.OnBoard && fromCombat && killer != null;

            return false;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (card != null && !card.Info.name.Contains("queenBeeWorker"))
            {
                yield return PreSuccessfulTriggerSequence();
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return QueueOrCreateDrawnCard();
                yield return LearnAbility(0.4f);
            }
        }
    }
}
