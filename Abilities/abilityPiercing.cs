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
        private void Ability_Piercing()
        {
            const string rulebookName = "Piercing";
            const string rulebookDescription = "When this card strikes a card and there is another card behind it, deal 1 overkill damage to that card.";
            const string dialogue = "Your beast runs mine through.";

            Piercing.ability = AbilityHelper.CreateAbility<Piercing>(
                Resources.sigilPiercing,// Resources.sigilPiercing_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                addModular: true).Id;
        }
    }
    public class Piercing : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target)
        {
            return base.Card.Slot.IsPlayerSlot;
        }
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            PlayableCard queuedCard = Singleton<BoardManager>.Instance.GetCardQueuedForSlot(target.Slot);
            if (queuedCard != null && !queuedCard.Dead)
            {
                yield return Singleton<CombatPhaseManager>.Instance.DealOverkillDamage(1, base.Card.Slot, target.Slot);
                yield return LearnAbility();
            }
            yield break;
        }
    }
}
