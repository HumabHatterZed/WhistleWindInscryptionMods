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
        private void Ability_Grinder()
        {
            const string rulebookName = "Grinder";
            const string rulebookDescription = "This card gains the stats of the card sacrificed to play it.";
            const string dialogue = "Now everything will be just fine.";
            Grinder.ability = AbilityHelper.CreateAbility<Grinder>(
                Artwork.sigilGrinder, Artwork.sigilGrinder_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                addModular: true, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Grinder : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return !fromCombat && Singleton<BoardManager>.Instance.currentSacrificeDemandingCard == base.Card;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.LightNegationEffect();
            base.Card.AddTemporaryMod(new(card.Attack, card.Health));
            base.Card.OnStatsChanged();
            yield return new WaitForSeconds(0.25f);
            yield return base.LearnAbility(0.4f);
        }
    }
}
