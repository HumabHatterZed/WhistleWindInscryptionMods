using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Encounters;
using InscryptionAPI.Guid;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents.Apocalypse
{
    public class ApocalypseBattleSequencer : Part1BossBattleSequencer
    {
        public static readonly string ID = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "ApocalypseBattleSequencer", typeof(ApocalypseBattleSequencer)).Id;
        public override Opponent.Type BossType => ApocalypseBossOpponent.ID;
        public override StoryEvent DefeatedStoryEvent => LobotomyPlugin.ApocalypseBossDefeated;

        private bool BrokeBigEyes = false;
        private bool BrokeSmallBeak = false;
        private bool BrokeLongArms = false;

        private PlayableCard BigEyesCard = null;
        private PlayableCard SmallBeakCard = null;
        private PlayableCard LongArmsCard = null;

        // use an empty encounter plan
        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
        {
            return new EncounterData
            {
                opponentTurnPlan = new(),
                Blueprint = new()
                {
                    turns = new(),
                    dominantTribes = new List<Tribe> { Tribe.Bird }
                },
                opponentType = ApocalypseBossOpponent.ID,
                startConditions = new()
            };
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return card.HasAnyOfTraits(LobotomyPlugin.LittleEgg, LobotomyPlugin.BigEgg, LobotomyPlugin.LongEgg);
        }
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            return otherCard.HasAnyOfTraits(LobotomyPlugin.LittleEgg, LobotomyPlugin.BigEgg, LobotomyPlugin.LongEgg);
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            ApocalypseBossOpponent opponent = TurnManager.Instance?.Opponent as ApocalypseBossOpponent;
            if (card.HasTrait(LobotomyPlugin.LittleEgg))
            {
                BrokeSmallBeak = true;
                yield return opponent?.ShutBeakSequence();
            }
            else if (card.HasTrait(LobotomyPlugin.BigEgg))
            {
                BrokeBigEyes = true;
                yield return opponent?.CloseEyesSequence();
            }
            else if (card.HasTrait(LobotomyPlugin.LongEgg))
            {
                BrokeLongArms = true;
                yield return opponent?.BreakArmsSequence();
            }
            yield break;
        }

        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            // if on the player side for whatever reason, return to opponent side
            if (otherCard.Slot.IsPlayerSlot)
                yield return ReturnEggToBasket(otherCard);

            // keep track of the egg cards
            if (otherCard != null && (!SmallBeakCard || !BigEyesCard || !LongArmsCard))
            {
                if (!BrokeSmallBeak && otherCard.HasTrait(LobotomyPlugin.LittleEgg))
                    SmallBeakCard = otherCard;
                else if (!BrokeBigEyes && otherCard.HasTrait(LobotomyPlugin.BigEgg))
                    BigEyesCard = otherCard;
                else if (!BrokeLongArms && otherCard.HasTrait(LobotomyPlugin.LongEgg))
                    LongArmsCard = otherCard;
            }
        }
        private IEnumerator ReturnEggToBasket(PlayableCard card)
        {
            List<CardSlot> opponentSlots = BoardManager.Instance.GetSlotsCopy(false).FindAll(x => x.Card == null);
            CardSlot newSlot;
            if (opponentSlots.Count == 0)
            {
                // grab the first slot occupied by a non-Egg
                newSlot = BoardManager.Instance.GetSlotsCopy(false)
                    .Find(x => x.Card != null && x.Card.LacksAllTraits(
                        LobotomyPlugin.LittleEgg, LobotomyPlugin.BigEgg, LobotomyPlugin.LongEgg
                        ));

                // somehow we have too many eggs; kill this one
                if (newSlot == null)
                {
                    yield return card.Die(false);
                    yield return new WaitForSeconds(0.5f);
                    yield break;
                }
                yield return newSlot.Card.Die(false);
            }
            else
            {
                int randomSeed = base.GetRandomSeed();
                newSlot = opponentSlots[SeededRandom.Range(0, opponentSlots.Count, randomSeed++)];
            }

            yield return BoardManager.Instance.AssignCardToSlot(card, newSlot);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
