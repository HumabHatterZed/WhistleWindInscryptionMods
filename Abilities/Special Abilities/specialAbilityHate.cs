﻿using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_Hate()
        {
            const string rulebookName = "Hate";
            const string rulebookDescription = "Transforms when the balance has shifted too far. Enters a weakened forme every other turn.";
            MagicalGirlHeart.specialAbility = AbilityHelper.CreateSpecialAbility<MagicalGirlHeart>(rulebookName, rulebookDescription).Id;
        }
    }
    public class MagicalGirlHeart : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        private int allyDeaths;
        private int opponentDeaths;
        private bool IsMagical => base.PlayableCard.Info.name == ("wstl_magicalGirlH");
        private readonly string dialogue = "The balance must be maintained. Good cannot exist without evil.";
        private readonly string altDialogue = "Good cannot exist without evil.";

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return CheckSum();
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return base.PlayableCard.OnBoard && IsMagical && fromCombat && killer != null;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            // does not increment if Magical Girl H is the killer
            if (killer != base.PlayableCard)
            {
                if (card.OpponentCard)
                {
                    opponentDeaths++;
                }
                else
                {
                    allyDeaths++;
                }
            }
            yield break;
        }
        
        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return base.PlayableCard.OnBoard && IsMagical && base.PlayableCard.OpponentCard != playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            // 2 more player cards have died than opponent cards
            if (allyDeaths > opponentDeaths + 1)
            {
                CardInfo evolution = GetEvolve(base.PlayableCard);

                yield return PerformTransformation(evolution);

                // If on opponent's side, move to player's if there's room, otherwise create in hand
                if (base.PlayableCard.OpponentCard)
                {
                    if (base.PlayableCard.Slot.opposingSlot.Card != null)
                    {
                        base.PlayableCard.RemoveFromBoard();
                        yield return new WaitForSeconds(0.5f);
                        yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(evolution, null, 0.25f, null);
                    }
                    else
                    {
                        yield return MoveToSlot(false, base.PlayableCard.Slot.opposingSlot);
                    }
                    yield return new WaitForSeconds(0.25f);
                }
                yield return PlayDialogue();
            }

            // 2 more Leshy card deaths than player card deaths
            if (allyDeaths + 1 < opponentDeaths)
            {
                CardInfo evolution = GetEvolve(base.PlayableCard);

                yield return PerformTransformation(evolution);

                List<CardSlot> giantCheck = Singleton<BoardManager>.Instance.OpponentSlotsCopy.Where(s => s.Card != null && s.Card.Info.HasTrait(Trait.Giant)).ToList();
                // if owned by the player and there are no giant cards, go to the opponent's side
                if (!base.PlayableCard.OpponentCard && giantCheck.Count() == 0)
                {
                    CardSlot opposingSlot = base.PlayableCard.Slot.opposingSlot;
                    List<CardSlot> queuedSlots = Singleton<TurnManager>.Instance.Opponent.QueuedSlots;

                    // if the opposing slot is empty, move over to it
                    if (base.PlayableCard.Slot.opposingSlot.Card == null)
                    {
                        yield return MoveToSlot(true, opposingSlot);
                    }
                    // if the opposing slot is occupied but the opposing queue is empty
                    else if (!queuedSlots.Contains(opposingSlot))
                    {
                        // if it's not Uncuttable, return to queue than move to slot
                        if (!opposingSlot.Card.Info.HasTrait(Trait.Uncuttable))
                        {
                            yield return Singleton<TurnManager>.Instance.Opponent.ReturnCardToQueue(opposingSlot.Card, 0.25f);
                            yield return MoveToSlot(true, opposingSlot);
                        }
                        // otherwise add this card to queue
                        else
                        {
                            base.PlayableCard.RemoveFromBoard();
                            yield return new WaitForSeconds(0.5f);
                            yield return Singleton<TurnManager>.Instance.Opponent.QueueCard(evolution, opposingSlot);
                        }
                    }
                    // if there are no available queues, opposing etc., add to turnplan
                    else
                    {
                        base.PlayableCard.RemoveFromBoard();
                        yield return new WaitForSeconds(0.5f);
                        List<List<CardInfo>> turnPlan = Singleton<TurnManager>.Instance.Opponent.TurnPlan;
                        List<CardInfo> addInfo = new() { evolution };
                        turnPlan.Add(addInfo);
                        yield return Singleton<TurnManager>.Instance.Opponent.ModifyTurnPlan(turnPlan);
                    }
                    yield return new WaitForSeconds(0.25f);
                }
                yield return PlayDialogue();
            }
        }

        private CardInfo GetEvolve(PlayableCard card)
        {
            CardInfo evolution = CardLoader.GetCardByName("wstl_queenOfHatred");
            foreach (CardModificationInfo item in card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            return evolution;
        }
        private IEnumerator PlayDialogue()
        {
            if (!WstlSaveManager.HasSeenHatredTransformation)
            {
                WstlSaveManager.HasSeenHatredTransformation = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(altDialogue, -0.65f, 0.4f);
                yield return new WaitForSeconds(0.5f);
            }
        }
        private IEnumerator PerformTransformation(CardInfo evolution)
        {
            yield return new WaitForSeconds(0.15f);
            base.PlayableCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return base.PlayableCard.TransformIntoCard(evolution);
            yield return new WaitForSeconds(0.5f);
        }
        private IEnumerator MoveToSlot(bool opponent, CardSlot slot)
        {
            base.PlayableCard.SetIsOpponentCard(opponent);
            base.PlayableCard.transform.eulerAngles += new Vector3(0f, 0f, -180f);
            yield return Singleton<BoardManager>.Instance.AssignCardToSlot(base.PlayableCard, slot, 0.25f);
        }
        private IEnumerator CheckSum()
        {
            bool Greed = false;
            bool Despair = false;
            CardSlot greedSlot = null;
            CardSlot despairSlot = null;
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(!base.PlayableCard.OpponentCard).Where((CardSlot s) => s.Card != null))
            {
                if (slot != base.PlayableCard.Slot)
                {
                    string slotName = slot.Card.Info.name;
                    if (slotName == "wstl_magicalGirlD" || slotName == "wstl_kingOfGreed")
                    {
                        Greed = true;
                        despairSlot = slot;
                    }
                    if (slotName == "wstl_magicalGirlS" || slotName == "wstl_knightOfDespair")
                    {
                        Despair = true;
                        despairSlot = slot;
                    }
                }
            }
            if (Greed && Despair)
            {
                if (!WstlSaveManager.HasSeenPlaceholder)
                {
                    WstlSaveManager.HasSeenPlaceholder = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("The absence of [c:bB]the fourth[c:] has rendered their purpose null.", -0.65f, 0.4f);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else
            {
                yield break;
            }
        }
    }
}
