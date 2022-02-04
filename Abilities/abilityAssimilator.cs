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
        private NewAbility Ability_Assimilator()
        {
            const string rulebookName = "Assimilator";
            const string rulebookDescription = "When a card bearing this sigil kills an enemy card, this card gains 1 Power and 1 Health.";
            const string dialogue = "From the many, one.";
            return WstlUtils.CreateAbility<Assimilator>(
                Resources.sigilAssimilator,
                rulebookName, rulebookDescription, dialogue, 4);
        }
    }
    public class Assimilator : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private CardModificationInfo mod = new CardModificationInfo(1, 1);

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return killer == Card;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);
            Card.AddTemporaryMod(mod);
            Card.OnStatsChanged();
            Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.2f);
            yield return LearnAbility(0.4f);
        }
    }
}
