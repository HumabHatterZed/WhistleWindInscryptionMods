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
            const string rulebookName = "Lonely";
            const string rulebookDescription = "Choose a card on the board to gain 1 Pebble unless a card with Pebble already exists, then return this card to your hand. If a card with Pebble dies, all ally cards receive 1 additional damage until a new card is chosen.";
            const string dialogue = "A friend to stay.";
            Lonely.ability = AbnormalAbilityHelper.CreateAbility<Lonely>(
                "sigilLonely",
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class Lonely : AbilityBehaviour, IModifyDamageTaken
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) => base.Card == attacker && CheckValid(slot);
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            friendDead = false;
            yield return slot.Card.AddStatusEffectFlipCard<Pebble>(1);
            yield return base.LearnAbility(0.4f);
            if (base.Card.OpponentCard)
            {
                List<CardSlot> slots = BoardManager.Instance.OpponentSlotsCopy.FindAll(x => !TurnManager.Instance.Opponent.QueuedSlots.Contains(x));
                if (slots.Count == 0)
                    yield break;

                yield return TurnManager.Instance.Opponent.QueueCard(base.Card.Info, slots[SeededRandom.Range(0, slots.Count, base.GetRandomSeed())]);
            }
            else
                yield return CardSpawner.Instance.SpawnCardToHand(base.Card.Info);
        }


        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return card.HasStatusEffect<Pebble>() && card.OpponentCard == base.Card.OpponentCard;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            friendDead = true;
            foreach (PlayableCard c in BoardManager.Instance.GetCards(!base.Card.OpponentCard))
            {
                if (!card.Dead)
                {
                    yield return new WaitForSeconds(0.1f);
                    c.Anim.LightNegationEffect();
                }
            }
            yield return DialogueHelper.PlayDialogueEvent("LonelyDie", card: base.Card);
        }

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            return target.OpponentCard == base.Card.OpponentCard && friendDead;
        }

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => damage + 1;
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => 0;


        private bool friendDead = false;
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
