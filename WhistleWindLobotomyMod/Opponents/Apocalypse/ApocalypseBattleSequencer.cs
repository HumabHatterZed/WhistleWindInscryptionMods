using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents.Apocalypse
{
    public class ApocalypseBattleSequencer : Part1BossBattleSequencer
    {
        public static readonly string ID = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "ApocalypseBattleSequencer", typeof(ApocalypseBattleSequencer)).Id;
        public override Opponent.Type BossType => ApocalypseBossOpponent.ID;
        public override StoryEvent DefeatedStoryEvent => LobotomyPlugin.ApocalypseBossDefeated;

        public bool BrokeBigEyes = false;
        public bool BrokeSmallBeak = false;
        public bool BrokeLongArms = false;

        private PlayableCard BigEyesCard = null;
        private PlayableCard SmallBeakCard = null;
        private PlayableCard LongArmsCard = null;

        public List<EncounterBlueprintData> EggPhases = new()
        {
            LobotomyEncounterManager.ApocalypseBossBigEyes,
            LobotomyEncounterManager.ApocalypseBossSmallBeak,
            LobotomyEncounterManager.ApocalypseBossLongArms
        };
        public override EncounterData BuildCustomEncounter(CardBattleNodeData nodeData)
        {
            int index = SeededRandom.Range(0, EggPhases.Count, base.GetRandomSeed() + 1);
            EncounterBlueprintData startingPhase = EggPhases[index];
            EggPhases.Remove(startingPhase);

            EncounterData data = new()
            {
                opponentType = ApocalypseBossOpponent.ID,
                startConditions = new(),
                Blueprint = startingPhase
            };
            data.opponentTurnPlan = DiskCardGame.EncounterBuilder.BuildOpponentTurnPlan(data.Blueprint, 20);
            return data;
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
            ApocalypseBossOpponent opponent = TurnManager.Instance.Opponent as ApocalypseBossOpponent;
            if (card.HasTrait(LobotomyPlugin.BigEgg) && !BrokeBigEyes)
            {
                BrokeBigEyes = true;
            }
            else if (card.HasTrait(LobotomyPlugin.LittleEgg) && !BrokeSmallBeak)
            {
                BrokeSmallBeak = true;
            }
            else if (card.HasTrait(LobotomyPlugin.LongEgg) && !BrokeLongArms)
            {
                BrokeLongArms = true;
            }
            // move to the next phase when an egg is broken
            opponent.NumLives--;
            yield return opponent.LifeLostSequence();
        }

        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            // if on the player side for whatever reason, return to opponent side
            if (otherCard.Slot.IsPlayerSlot)
            {
                yield return new WaitForSeconds(0.5f);
                yield return ReturnEggToBasket(otherCard);
                yield return TextDisplayer.Instance.PlayDialogueEvent("ApocalypseBossReturnEgg", TextDisplayer.MessageAdvanceMode.Input);
            }

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
            card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.45f);
            yield return BoardManager.Instance.AssignCardToSlot(card, newSlot);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
