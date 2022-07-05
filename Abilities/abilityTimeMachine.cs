using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_TimeMachine()
        {
            const string rulebookName = "Time Machine";
            const string rulebookDescription = "Activate: End the current battle or phase and remove this card from the player's deck. Remove an additional card from the deck at random based on their power level.";
            const string dialogue = "Close your eyes, and count to ten.";

            TimeMachine.ability = WstlUtils.CreateActivatedAbility<TimeMachine>(
                Resources.sigilTimeMachine,
                rulebookName, rulebookDescription, dialogue, 5).Id;
        }
    }
    public class TimeMachine : ActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly int min = 8;

        // Failsafe that prevents ability from being used multiple times per run
        public override bool CanActivate()
        {
            return !PersistentValues.HasUsedBackwardClock;
        }

        // Ends the battle
        public override IEnumerator Activate()
        {
            PersistentValues.HasUsedBackwardClock = true;
            AudioController.Instance.PlaySound2D("antigravity_elevator_down");
            yield return base.Card.RenderInfo.forceEmissivePortrait = true;
            yield return new WaitForSeconds(0.4f);
            yield return base.LearnAbility();
            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("When you open them, you will be standing at the exact moment you wished to be in.", -0.65f, 0.4f);
            // Gets a list of all cards in the player's deck, minus Backward Clock
            List<CardInfo> deckInfo = new(RunState.DeckList);
            deckInfo.Remove(base.Card.Info);
            yield return RemoveCard(base.Card);
            // Sort by highest powerlevel if there are cards in the deck
            // Then get a list of all cards above-equal-to the min powerlevel (currently 8)
            // If there is more than 1 card in highInfo, choose a random one from highInfo
            // Else choose the first item in deckInfo (highest powerlevel
            if (deckInfo != null)
            {
                bool removed = false;
                CardInfo cardInfo = null;
                int randomSeed = base.GetRandomSeed();
                List<CardInfo> highInfo = deckInfo.FindAll((CardInfo i) => i.PowerLevel >= min);
                deckInfo.Sort((CardInfo a, CardInfo b) => b.PowerLevel - a.PowerLevel);

                if (highInfo.Count > 1)
                {
                    highInfo.Sort((CardInfo a, CardInfo b) => b.PowerLevel - a.PowerLevel);
                    cardInfo = (!(SeededRandom.Value(randomSeed++) > 0.85f)) ? highInfo[SeededRandom.Range(0, deckInfo.Count, randomSeed++)] : highInfo[0];
                }
                else
                {
                    cardInfo = deckInfo[0];
                }
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("[c:bR]One of your creatures[c:] must stay behind to operate [c:bR]the Clock[c:].", -0.65f, 0.4f);
                // Check board first, then dead cards, player's hand, then player's deck
                if (Singleton<BoardManager>.Instance.PlayerSlotsCopy.FindAll((CardSlot slot) => slot.Card != null && slot.Card.Info == cardInfo).Count == 1)
                {
                    foreach (CardSlot slot in Singleton<BoardManager>.Instance.PlayerSlotsCopy.Where(s => s.Card != null && s.Card.Info == cardInfo))
                    {
                        removed = true;
                        yield return new WaitForSeconds(0.25f);
                        yield return RemoveCard(slot.Card);
                    }
                }
                else
                {
                    if (Singleton<Deck>.Instance.Cards.FindAll((CardInfo info) => info == cardInfo).Count == 1)
                    {
                        yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo);
                    }
                    Singleton<ViewManager>.Instance.SwitchToView(View.Hand);
                    removed = true;
                    foreach (PlayableCard card in Singleton<PlayerHand>.Instance.CardsInHand.Where(c => c.Info == cardInfo))
                    {
                        yield return new WaitForSeconds(0.25f);
                        yield return RemoveCard(card);
                    }
                }
                if (!removed)
                {
                    RunState.Run.playerDeck.RemoveCard(cardInfo);
                }
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("[c:bR]The Clock[c:] and your [c:bR]" + cardInfo.DisplayedNameLocalized + "[c:] will remain in that abandoned time.", -0.65f, 0.4f);
                yield return new WaitForSeconds(0.2f);
            }
            yield return EndBattle();
        }

        private IEnumerator RemoveCard(PlayableCard item)
        {
            RunState.Run.playerDeck.RemoveCard(item.Info);
            item.UnassignFromSlot();
            SpecialCardBehaviour[] components = item.GetComponents<SpecialCardBehaviour>();
            for (int i = 0; i < components.Length; i++)
            {
                components[i].OnCleanUp();
            }
            item.ExitBoard(0.3f, Vector3.zero);
            yield return new WaitForSeconds(0.5f);
        }
        private IEnumerator EndBattle()
        {
            int damage = Singleton<LifeManager>.Instance.DamageUntilPlayerWin;
            yield return Singleton<CombatPhaseManager>.Instance.DamageDealtThisPhase = damage;
            yield return Singleton<LifeManager>.Instance.ShowDamageSequence(damage, damage, toPlayer: false);
        }
    }
}
