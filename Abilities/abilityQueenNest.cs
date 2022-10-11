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
        private void Ability_QueenNest()
        {
            const string rulebookName = "Queen Nest";
            const string rulebookDescription = "While this card is on the board, create a Worker Bee in your hand whenever another card dies. A Worker Bee is defined as: 1 Power, 1 Health.";
            const string dialogue = "For the hive.";
            QueenNest.ability = AbilityHelper.CreateAbility<QueenNest>(
                Artwork.sigilQueenNest, Artwork.sigilQueenNest_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: true, opponent: true, canStack: false, isPassive: false).Id;
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
            {
                return base.Card.OnBoard && fromCombat && killer != null;
            }
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
