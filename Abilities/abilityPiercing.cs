using APIPlugin;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Piercing()
        {
            const string rulebookName = "Piercing";
            const string rulebookDescription = "When this card strikes a card, deal 1 overkill damage if applicable.";
            const string dialogue = "Your beast runs mine through.";

            return WstlUtils.CreateAbility<Piercing>(
                Resources.sigilPiercing,
                rulebookName, rulebookDescription, dialogue, 2, addModular: true);
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

            PlayableCard playableCard = Singleton<Opponent>.Instance.Queue.Find((PlayableCard x) => x.QueuedSlot == target.Slot);
            if (playableCard != null && !playableCard.Dead)
            {
                playableCard.TakeDamage(1, base.Card);
                yield return LearnAbility();
            }
        }
    }
}
