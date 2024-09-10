using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.AbilityClasses;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_QueenNest()
        {
            const string rulebookName = "Queen Nest";
            const string rulebookDescription = "While [creature] is on the board, create a Worker Bee in your hand whenever another card perishes. [define:wstl_queenBeeWorker]";
            const string dialogue = "For the hive.";
            const string triggerText = "Another worker is born to serve [creature].";
            QueenNest.ability = AbnormalAbilityHelper.CreateAbility<QueenNest>(
                "sigilQueenNest",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: true, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class QueenNest : OpponentDrawCreatedCard
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override CardInfo CardToDraw => CardLoader.GetCardByName("wstl_queenBeeWorker");

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
