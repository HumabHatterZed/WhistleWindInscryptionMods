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
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Ability_TimeMachine()
        {
            const string rulebookName = "Time Machine";
            const string rulebookDescription = "End the current battle then remove this card from the player's deck. Choose an additional card to remove from your deck. Effect differs during certain battles.";
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

        private CardInfo chosenCardInfo = null;
        private PlayableCard chosenCard = null;

        // Failsafe that prevents ability from being used multiple times per run
        public override bool CanActivate()
        {
            if (SaveManager.SaveFile.CurrentDeck.Cards.Count - 1 > 0)
            {
                if (SaveManager.SaveFile.IsPart2)
                    return !LobotomySaveManager.UsedBackwardClockGBC;
                
                return !LobotomySaveManager.UsedBackwardClock;
            }
            return false;
        }

        public override IEnumerator Activate()
        {
            if (TurnManager.Instance.Opponent is LobotomyBossOpponent opp && opp.PreventInstantWin(true, base.Card.Slot))
            {
                base.Card.Anim.StrongNegationEffect();
                yield return opp.OnInstantWinPrevented(true,base.Card.Slot);
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

            base.SetLearned();
            AudioController.Instance.PlaySound2D("antigravity_elevator_down");
            base.Card.Anim.LightNegationEffect();
            ChangeToActivePortrait();
            yield return new WaitForSeconds(0.4f);
            ViewManager.Instance.SwitchToView(View.Default);

            yield return DialogueManager.PlayDialogueEventSafe("BackwardClockStart", advanceMode: TextDisplayer.MessageAdvanceMode.Input, speaker: DialogueHelper.GBCScrybe());
            yield return BackwardSequence();
            yield return EndBattle();
        }

        private List<CardInfo> GetCardChoices()
        {
            List<CardInfo> choices = new();
            List<CardInfo> cardsInDeck = new(SaveManager.SaveFile.CurrentDeck.Cards);
            cardsInDeck.RemoveAll(x => x.HasAbility(this.Ability));
            cardsInDeck.Sort((a, b) => b.PowerLevel - a.PowerLevel);

            int randomSeed = base.GetRandomSeed();
            List<CardInfo> strongestCards = cardsInDeck.GetRange(0, (cardsInDeck.Count + 1) / 3);
            while (cardsInDeck.Count > 0)
            {
                CardInfo choice;
                if (strongestCards.Count > 0 && SeededRandom.Bool(randomSeed++))
                {
                    choice = strongestCards.GetSeededRandom(randomSeed++);
                    strongestCards.Remove(choice);
                }
                else
                {
                    choice = cardsInDeck.GetSeededRandom(randomSeed++);
                }
                choices.Add(choice);
                cardsInDeck.Remove(choice);

                if (choices.Count > 2)
                    break;
            }

            return choices;
        }

        private IEnumerator ChooseCardForClock()
        {
            List<CardInfo> choices = GetCardChoices();
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
                SelectableCard selectedCard = null;
                Singleton<ViewManager>.Instance.SwitchToView(View.DeckSelection, immediate: false, lockAfter: true);
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
        private IEnumerator GetChosenCardToRemove(CardInfo deckCardToRemove)
        {
            PlayableCard backwardClock = BoardManager.Instance.CardsOnBoard.Find(x => HelperMethods.IsCardInfoOrCopy(x.Info, deckCardToRemove));
            if (backwardClock == null)
            {
                backwardClock = PlayerHand.Instance.CardsInHand.Find(x => HelperMethods.IsCardInfoOrCopy(x.Info, deckCardToRemove));
                if (backwardClock == null)
                {
                    CardInfo cardToDraw = CardDrawPiles.Instance.Deck.Cards.Find(x => HelperMethods.IsCardInfoOrCopy(x, deckCardToRemove));
                    if (cardToDraw != null)
                    {
                        if (!SaveManager.SaveFile.IsPart2)
                            (CardDrawPiles.Instance as CardDrawPiles3D).pile.Draw();

                        yield return CardDrawPiles.Instance.DrawCardFromDeck(cardToDraw);
                    }

                    backwardClock = PlayerHand.Instance.CardsInHand.Find(x => x.Info == deckCardToRemove);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else
            {
                backwardClock.UnassignFromSlot();
            }
            chosenCard = backwardClock;
        }

        private IEnumerator BackwardSequence()
        {
            yield return DialogueManager.PlayDialogueEventSafe("BackwardClockOperate", TextDisplayer.MessageAdvanceMode.Input, speaker: DialogueHelper.GBCScrybe());
            yield return ChooseCardForClock();
            yield return new WaitForSeconds(0.4f);
            yield return GetChosenCardToRemove(chosenCardInfo);

            CardInfo machineInfo = SaveManager.SaveFile.CurrentDeck.Cards.Find(x => HelperMethods.IsCardInfoOrCopy(x, base.Card.Info));
            base.Card.UnassignFromSlot();
            HelperMethods.RemoveCardFromDeck(machineInfo);
            HelperMethods.RemoveCardFromDeck(chosenCardInfo);
            GlitchOutAssetEffect.GlitchModel(base.Card.StatsLayer.transform);
            GlitchOutAssetEffect.GlitchModel(chosenCard.StatsLayer.transform);
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.ShowUntilInput("The machine and your [c:bR]" + chosenCardInfo.DisplayedNameLocalized + "[c:] will remain in that abandoned time.", effectFOVOffset: -0.65f, effectEyelidIntensity: 0.4f);
        }

        private IEnumerator EndBattle()
        {
            if (TurnManager.Instance.Opponent is LobotomyBossOpponent opp)
            {
                yield return opp.OnInstantWinTriggered(true, base.Card.Slot);
            }
            else
            {
                int damage = Singleton<CombatPhaseManager>.Instance.DamageDealtThisPhase = Singleton<LifeManager>.Instance.DamageUntilPlayerWin;
                yield return Singleton<LifeManager>.Instance.ShowDamageSequence(damage, damage, toPlayer: false);
            }

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
                clone.SetPixelPortrait(TextureLoader.LoadSpriteFromFile($"backwardClock_pixel_{rand}"));
            else
            {
                clone.SetEmissivePortrait(TextureLoader.LoadTextureFromFile($"backwardClock_emission_{rand}"));
                base.Card.RenderInfo.forceEmissivePortrait = true;
            }
            
            base.Card.SetInfo(clone);
        }
    }
}
