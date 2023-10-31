using DiskCardGame;
using GBC;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Helpers.Extensions;
using InscryptionCommunityPatch.PixelTutor;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Ability_TimeMachine()
        {
            const string rulebookName = "Time Machine";
            const string rulebookDescription = "End the current battle or phase then remove this card from the player's deck. The player must choose an additional card from a selection of 3 to remove from their deck. Selection is based on card power.";
            const string dialogue = "Close your eyes and count to ten.";

            TimeMachine.ability = LobotomyAbilityHelper.CreateActivatedAbility<TimeMachine>(
                "sigilTimeMachine",
                rulebookName, rulebookDescription, dialogue, powerLevel: 5).Id;
        }
    }
    public class TimeMachine : ActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private const int min = 8;

        // Failsafe that prevents ability from being used multiple times per run
        public override bool CanActivate() => SaveManager.SaveFile.IsPart2 ? !LobotomySaveManager.UsedBackwardClockGBC : !LobotomySaveManager.UsedBackwardClock;

        public override IEnumerator Activate()
        {
            if (LobotomyPlugin.PreventOpponentDamage)
            {
                DialogueHelper.ShowUntilInput("Something prevents its function. You cannot escape this one.");
                yield break;
            }

            // prevent bell-ringing
            TurnManager.Instance.PlayerCanInitiateCombat = false;
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            if (SaveManager.SaveFile.IsPart2)
            {
                PixelCombatBell bell = (PixelCombatBell)Resources.FindObjectsOfTypeAll(typeof(PixelCombatBell)).FirstOrDefault();
                bell?.SetEnabled(false);
            }
            if (!base.HasLearned)
                yield return DialogueHelper.PlayAlternateDialogue(
                    Emotion.Anger,
                    DialogueEvent.Speaker.Leshy,
                    0.2f,
                    "Have I backed you into a corner? Or am I simply boring you?",
                    "I suppose it doesn't matter. I will honour your request.");

            HelperMethods.ChangeCurrentView(View.Default);
            AudioController.Instance.PlaySound2D("antigravity_elevator_down");
            base.Card.Anim.LightNegationEffect();
            ChangeToActivePortrait();
            yield return new WaitForSeconds(0.4f);

            base.SetLearned();
            yield return DialogueManager.PlayDialogueEventSafe("BackwardClockStart", advanceMode: TextDisplayer.MessageAdvanceMode.Input, speaker: DialogueHelper.GBCScrybe());
            yield return BackwardSequence();
            yield return EndBattle();
        }

        private List<CardInfo> GetCardChoices()
        {
            List<CardInfo> choices = new();
            int randomSeed = base.GetRandomSeed();
            List<CardInfo> cardsInDeck = new(SaveManager.SaveFile.IsPart2 ? SaveManager.SaveFile.gbcData.deck.Cards : RunState.DeckList);
            cardsInDeck.Remove(base.Card.Info);
            cardsInDeck.Sort((a, b) => b.PowerLevel - a.PowerLevel);

            List<CardInfo> strongCards = cardsInDeck.FindAll(x => x.PowerLevel >= min);
            while (choices.Count < 3)
            {
                int index = 0;
                if (strongCards.Count > 0)
                {
                    index = SeededRandom.Range(0, strongCards.Count, randomSeed++);
                    choices.Add(strongCards[index]);
                    strongCards.RemoveAt(index);
                }
                else if (cardsInDeck.Count > 0)
                {
                    index = SeededRandom.Range(0, cardsInDeck.Count, randomSeed++);
                    choices.Add(cardsInDeck[0]);
                    cardsInDeck.RemoveAt(index);
                }
                else
                    break;
            }

            return choices;
        }
        private IEnumerator RemoveChosenCardFromBoard(CardInfo cardToRemove)
        {
            if (BoardManager.Instance.PlayerSlotsCopy.Exists(x => x.Card?.Info == cardToRemove || x.Card?.Info.name == cardToRemove.name))
            {
                // find an exact match with the info; if one doesn't exist, find the name
                CardSlot boardSlot = BoardManager.Instance.PlayerSlotsCopy.Find(x => x.Card?.Info == cardToRemove);
                boardSlot ??= BoardManager.Instance.PlayerSlotsCopy.Find(x => x.Card?.Info.name == cardToRemove.name);
                chosenCard = boardSlot?.Card;
            }
            else
            {
                yield return HelperMethods.ChangeCurrentView(View.Hand);
                if (PlayerHand.Instance.CardsInHand.Exists(x => x.Info == cardToRemove || x.Info.name == cardToRemove.name))
                    chosenCard = PlayerHand.Instance.CardsInHand.Find(x => x.Info == cardToRemove);

                else if (CardDrawPiles.Instance.Deck.Cards.Exists(x => x == cardToRemove || x.name == cardToRemove.name))
                {
                    CardInfo drawToHand = CardDrawPiles.Instance.Deck.Cards.Find(x => x == cardToRemove);

                    yield return CardSpawner.Instance.SpawnCardToHand(drawToHand);
                    chosenCard = PlayerHand.Instance.CardsInHand.Find(x => x.Info == drawToHand);
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        private IEnumerator ChooseCardForClock(List<CardInfo> choices)
        {
            if (SaveManager.SaveFile.IsPart2)
            {
                PixelPlayableCard selectedCard = null;
                yield return PixelBoardManager.Instance.GetComponent<PixelPlayableCardArray>().SelectPixelCardFrom(choices, delegate (PixelPlayableCard x)
                {
                    selectedCard = x;
                });

                Tween.Position(selectedCard.transform, selectedCard.transform.position + Vector3.back * 4f, 0.1f, 0f, Tween.EaseIn);
                Destroy(selectedCard.gameObject, 0.1f);
                chosenCardInfo = selectedCard.Info;
            }
            else
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.DeckSelection, immediate: false, lockAfter: true);

                SelectableCard selectedCard = null;
                yield return BoardManager.Instance.CardSelector.SelectCardFrom(choices, (CardDrawPiles.Instance as CardDrawPiles3D).Pile, delegate (SelectableCard x)
                {
                    selectedCard = x;
                });

                Tween.Position(selectedCard.transform, selectedCard.transform.position + Vector3.back * 4f, 0.1f, 0f, Tween.EaseIn);
                Destroy(selectedCard.gameObject, 0.1f);
                chosenCardInfo = selectedCard.Info;

                Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            }
        }
        private CardInfo chosenCardInfo = null;
        private PlayableCard chosenCard = null;
        private IEnumerator BackwardSequence()
        {
            List<CardInfo> choices = GetCardChoices();
            
            //LobotomyPlugin.Log.LogInfo($"Start {cardToRemove != null} {cardToRemove?.name}");
            yield return DialogueManager.PlayDialogueEventSafe("BackwardClockOperate", speaker: DialogueHelper.GBCScrybe());

            yield return ChooseCardForClock(choices);
            yield return RemoveChosenCardFromBoard(chosenCardInfo);

            base.Card.RemoveFromBoard(true);
            if (chosenCard != null)
                chosenCard.RemoveFromBoard(true);
            else
                HelperMethods.RemoveCardFromDeck(chosenCardInfo);

            yield return new WaitForSeconds(0.5f);
            HelperMethods.ChangeCurrentView(View.Default, 0f);
            yield return DialogueHelper.ShowUntilInput("[c:bR]The Clock[c:] and your [c:bR]" + chosenCardInfo.DisplayedNameLocalized + "[c:] will remain in that abandoned time.", effectFOVOffset: -0.65f, effectEyelidIntensity: 0.4f);
            yield return new WaitForSeconds(0.2f);
        }

        private IEnumerator EndBattle()
        {
            int damage = Singleton<LifeManager>.Instance.DamageUntilPlayerWin;

            yield return Singleton<CombatPhaseManager>.Instance.DamageDealtThisPhase = damage;
            yield return Singleton<LifeManager>.Instance.ShowDamageSequence(damage, damage, toPlayer: false);

            if (SaveManager.SaveFile.IsPart2)
                LobotomySaveManager.UsedBackwardClockGBC = true;
            else
                LobotomySaveManager.UsedBackwardClock = true;

            if (!TurnManager.Instance.GameEnding && !TurnManager.Instance.GameEnded)
            {
                yield return new WaitForSeconds(0.4f);
                TurnManager.Instance.PlayerCanInitiateCombat = true;
            }
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }

        private void ChangeToActivePortrait()
        {
            int rand = new System.Random().Next(4);
            CardInfo clone = base.Card.Info.Clone() as CardInfo;

            if (SaveManager.SaveFile.IsPart2)
            {
                string resource = rand switch
                {
                    0 => "backwardClock_pixel_0",
                    1 => "backwardClock_pixel_1",
                    2 => "backwardClock_pixel_2",
                    _ => "backwardClock_pixel_3"
                };
                clone.SetPixelPortrait(TextureLoader.LoadSpriteFromFile(resource));
            }
            else
            {
                string resource = rand switch
                {
                    0 => "backwardClock_emission",
                    1 => "backwardClock_emission_1",
                    2 => "backwardClock_emission_2",
                    _ => "backwardClock_emission_3"
                };
                clone.SetEmissivePortrait(TextureLoader.LoadTextureFromFile(resource));
            }

            base.Card.RenderInfo.forceEmissivePortrait = true;
            base.Card.SetInfo(clone);
        }
    }
}
