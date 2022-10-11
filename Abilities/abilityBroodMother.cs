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
        private void Ability_BroodMother()
        {
            const string rulebookName = "Broodmother";
            const string rulebookDescription = "When [creature] is struck, create a Spiderling in your hand. A Spiderling is defined as: 0 Power, 1 Health, Fledgling.";
            const string dialogue = "A small spider takes refuge in your hand.";
            BroodMother.ability = AbilityHelper.CreateAbility<BroodMother>(
                Artwork.sigilBroodMother, Artwork.sigilBroodMother_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                addModular: true, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class BroodMother : OpponentDrawCreatedCard
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override CardInfo CardToDraw
        {
            get
            {
                CardInfo cardByName = CardLoader.GetCardByName("wstl_spiderling");
                cardByName.Mods.AddRange(base.GetNonDefaultModsFromSelf(this.Ability));
                return cardByName;
            }
        }
        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            return true;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return base.QueueOrCreateDrawnCard();
            yield return base.LearnAbility(0.5f);
        }
    }
}
