using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Lonely()
        {
            const string rulebookName = "Pebble Giver";
            const string rulebookDescription = "Choose one of your cards to gain Pebble unless a card with Pebble already exists, then return this card to your hand.";
            const string dialogue = "A friend to stay.";
            Lonely.ability = AbnormalAbilityHelper.CreateAbility<Lonely>(
                "sigilLonely",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class Lonely : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) => base.Card == attacker && CheckValid(slot);
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            yield return slot.Card.AddStatusEffectToFaceDown<Pebble>();
            if (base.Card.OpponentCard)
            {
                List<CardSlot> slots = BoardManager.Instance.OpponentSlotsCopy.FindAll(x => !TurnManager.Instance.Opponent.QueuedSlots.Contains(x));
                if (slots.Count == 0)
                    yield break;

                yield return HelperMethods.ChangeCurrentView(View.OpponentQueue);
                yield return TurnManager.Instance.Opponent.QueueCard(base.Card.Info.Clone() as CardInfo, slots[SeededRandom.Range(0, slots.Count, base.GetRandomSeed())]);
                yield return new WaitForSeconds(0.4f);
            }
            else
            {
                yield return HelperMethods.ChangeCurrentView(View.Hand);
                yield return CardSpawner.Instance.SpawnCardToHand(base.Card.Info.Clone() as CardInfo);
                yield return new WaitForSeconds(0.4f);
            }

            yield return HelperMethods.ChangeCurrentView(View.Board);
            yield return base.LearnAbility(0.4f);
        }

        public bool HasFriend
        {
            get
            {
                bool isOpponent = base.Card.OpponentCard || base.Card.QueuedSlot != null;
                if (BoardManager.Instance.GetCards(!isOpponent).Exists(x => x.HasStatusEffect<Pebble>()))
                    return true;

                if (isOpponent)
                    return TurnManager.Instance.Opponent.Queue.Exists(x => x.HasStatusEffect<Pebble>());
                else
                    return PlayerHand.Instance.CardsInHand.Exists(x => x.HasStatusEffect<Pebble>());


            }
        }
        private bool CheckValid(CardSlot target) => target.IsOpponentSlot() == base.Card.OpponentCard && target.Card != null && !HasFriend;
    }
}
