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
        private NewAbility Ability_Piercing()
        {
            const string rulebookName = "Piercing";
            const string rulebookDescription = "When this card strikes an opposing card, deal 1 overkill damage to the opposing queued card. If there is no queued card, deal 1 direct damage instead.";
            const string dialogue = "Your beast runs mine through.";

            return WstlUtils.CreateAbility<Piercing>(
                Resources.sigilPiercing,
                rulebookName, rulebookDescription, dialogue, 2);
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
            //yield return Singleton<CombatPhaseManager>.Instance.DealOverkillDamage(1, base.Card.Slot, base.Card.Slot.opposingSlot);
            yield return base.PreSuccessfulTriggerSequence();
            PlayableCard queuedCard = Singleton<BoardManager>.Instance.GetCardQueuedForSlot(base.Card.Slot.opposingSlot);
            PlayableCard playableCard = Singleton<Opponent>.Instance.Queue.Find((PlayableCard x) => x.QueuedSlot == target.Slot);
            if (playableCard != null)
            {
                playableCard.TakeDamage(1, base.Card);
            }
            else
            {
                yield return Singleton<LifeManager>.Instance.ShowDamageSequence(1, 1, toPlayer: false, 0.25f, ResourceBank.Get<GameObject>("Prefabs/Environment/ScaleWeights/Weight_RealTooth"));
            }

            yield return LearnAbility();
        }
    }
}
