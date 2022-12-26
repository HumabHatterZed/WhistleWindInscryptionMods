using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Ability_TimeMachine()
        {
            const string rulebookName = "Time Machine";
            const string rulebookDescription = "End the current battle or phase and remove this card from the player's deck. Remove an additional card from the deck at random based on their power level.";
            const string dialogue = "Close your eyes and count to ten.";

            TimeMachine.ability = LobotomyAbilityHelper.CreateActivatedAbility<TimeMachine>(
                Artwork.sigilTimeMachine, Artwork.sigilTimeMachine_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 5).Id;
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
            return !WstlSaveManager.UsedBackwardClock;
        }

        // Ends the battle
        public override IEnumerator Activate()
        {
            if (!base.HasLearned)
                yield return HelperMethods.PlayAlternateDialogue(Emotion.Anger, DialogueEvent.Speaker.Leshy, 0.2f, "Have I backed you into a corner? Or am I simply boring you?", "I suppose it doesn't matter. I will honour your request.");
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            AudioController.Instance.PlaySound2D("antigravity_elevator_down");
            base.Card.Anim.LightNegationEffect();
            RandomEmission();
            yield return new WaitForSeconds(0.4f);
            if (!base.HasLearned)
                yield return base.LearnAbility();
            else
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Close your eyes and count to ten.", -0.65f, 0.4f);
            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("When you open them, you will be standing at the exact moment you wish to be in.", -0.65f, 0.4f);
            // Gets a list of all cards in the player's deck, minus Backward Clock
            List<CardInfo> cardsInDeck = new(RunState.DeckList);
            cardsInDeck.Remove(base.Card.Info);
            base.Card.RemoveFromBoard(true);
            // Sort by highest powerlevel if there are cards in the deck
            // Then get a list of all cards above-or-equal-to the min powerlevel (currently 8)
            // If there is more than 1 card in highInfo, choose a random one from highInfo
            // Else choose the first item in deckInfo (highest powerlevel
            if (cardsInDeck != null)
            {
                bool removed = false;
                CardInfo cardInfo = null;
                int randomSeed = base.GetRandomSeed();
                List<CardInfo> cardsAboveMin = cardsInDeck.FindAll((CardInfo i) => i.PowerLevel >= min);

                cardsInDeck.Sort((CardInfo a, CardInfo b) => b.PowerLevel - a.PowerLevel);

                if (cardsAboveMin.Count > 1)
                {
                    cardsAboveMin.Sort((CardInfo a, CardInfo b) => b.PowerLevel - a.PowerLevel);
                    cardInfo = SeededRandom.Value(randomSeed++) <= 0.85f ? cardsAboveMin[SeededRandom.Range(0, cardsAboveMin.Count, randomSeed++)] : cardsAboveMin[0];
                }
                else
                    cardInfo = cardsInDeck[0];

                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("[c:bR]One of your creatures[c:] must stay behind to operate [c:bR]the Clock[c:].", -0.65f, 0.4f);
                // Check board first, then dead cards, player's hand, then player's deck
                if (Singleton<BoardManager>.Instance.PlayerSlotsCopy.FindAll((CardSlot slot) => slot.Card != null && slot.Card.Info == cardInfo).Count == 1)
                {
                    foreach (CardSlot slot in Singleton<BoardManager>.Instance.PlayerSlotsCopy.Where(s => s.Card != null && s.Card.Info == cardInfo))
                    {
                        removed = true;
                        yield return new WaitForSeconds(0.25f);
                        slot.Card.RemoveFromBoard(true);
                        yield return new WaitForSeconds(0.5f);
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
                        yield return new WaitForSeconds(0.5f);
                        card.RemoveFromBoard(true);
                        yield return new WaitForSeconds(0.5f);
                    }
                }
                if (!removed)
                {
                    RunState.Run.playerDeck.RemoveCard(cardInfo);
                }
                if (Singleton<ViewManager>.Instance.CurrentView != View.Default)
                {
                    Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                    yield return new WaitForSeconds(0.2f);
                }
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("[c:bR]The Clock[c:] and your [c:bR]" + cardInfo.DisplayedNameLocalized + "[c:] will remain in that abandoned time.", -0.65f, 0.4f);
                yield return new WaitForSeconds(0.2f);
            }
            yield return EndBattle();
        }

        private IEnumerator EndBattle()
        {
            int damage = Singleton<LifeManager>.Instance.DamageUntilPlayerWin;
            yield return Singleton<CombatPhaseManager>.Instance.DamageDealtThisPhase = damage;
            yield return Singleton<LifeManager>.Instance.ShowDamageSequence(damage, damage, toPlayer: false);
            WstlSaveManager.UsedBackwardClock = true;
        }

        private void RandomEmission()
        {
            int rand = new System.Random().Next(4);
            byte[] resource = rand switch
            {
                0 => Artwork.backwardClock_emission,
                1 => Artwork.backwardClock_emission_1,
                2 => Artwork.backwardClock_emission_2,
                _ => Artwork.backwardClock_emission_3
            };
            base.Card.ClearAppearanceBehaviours();
            base.Card.ApplyAppearanceBehaviours(new() { CardAppearanceBehaviour.Appearance.RareCardBackground });
            base.Card.Info.SetEmissivePortrait(TextureLoader.LoadTextureFromBytes(resource));
            base.Card.RenderInfo.forceEmissivePortrait = true;
            base.Card.UpdateStatsText();
        }
    }
}
