﻿using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_YellowBrick()
        {
            const string rulebookName = "Yellow Brick Road";
            const string rulebookDescription = "Activates when Scarecrow Searching for Wisdom, Warm-Hearted Woodsman, and Scaredy Cat are played on the same side of the board.";
            YellowBrick.specialAbility = AbilityHelper.CreateSpecialAbility<YellowBrick>(rulebookName, rulebookDescription).Id;
        }
    }

    public class YellowBrick : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public override bool RespondsToResolveOnBoard()
        {
            return true;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return CheckForOtherCards();
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return CheckForOtherCards();
        }

        private IEnumerator CheckForOtherCards()
        {
            // Break if already have Adult
            if (WstlSaveManager.HasAdult)
            {
                WstlPlugin.Log.LogDebug("Player already has Adult Who Tells Lies.");
                yield break;
            }

            CardSlot scarecrowSlot = null;
            CardSlot woodsmanSlot = null;
            CardSlot scaredySlot = null;

            foreach (CardSlot slot in CustomMethods.GetBoardSlotsCopy(base.PlayableCard.OpponentCard).Where((CardSlot s) => s.Card != null))
            {
                if (slot != base.PlayableCard.Slot)
                {
                    if (slot.Card.Info.name == "wstl_wisdomScarecrow")
                    {
                        WstlPlugin.Log.LogDebug("Player has Scarecrow Searching for Wisdom.");
                        scarecrowSlot = slot;
                        continue;
                    }
                    if (slot.Card.Info.name == "wstl_warmHeartedWoodsman")
                    {
                        WstlPlugin.Log.LogDebug("Player has Warm-Hearted Woodsman.");
                        woodsmanSlot = slot;
                        continue;
                    }
                    if (slot.Card.Info.name == "wstl_scaredyCat" || slot.Card.Info.name == "wstl_scaredyCatStrong")
                    {
                        WstlPlugin.Log.LogDebug("Player has Scaredy Cat.");
                        scaredySlot = slot;
                        continue;
                    }
                }
            }

            if (scarecrowSlot != null && woodsmanSlot != null && scaredySlot != null)
                yield return Emerald(scarecrowSlot, woodsmanSlot, scaredySlot);

            yield break;
        }

        private IEnumerator Emerald(CardSlot scarecrow, CardSlot woodsman, CardSlot scaredy)
        {
            yield return new WaitForSeconds(1f);

            // Exposit story of the Black Forest
            if (!WstlSaveManager.HasSeenApocalypse)
            {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Let me tell you a story. The story of the [c:bR]Black Forest[c:].");
            }

            AudioController.Instance.SetLoopVolume(0.5f * (Singleton<GameFlowManager>.Instance as Part1GameFlowManager).GameTableLoopVolume, 0.5f);
            AudioController.Instance.SetLoopAndPlay("red_noise", 1);
            AudioController.Instance.SetLoopVolumeImmediate(0.3f, 1);

            if (!WstlSaveManager.HasSeenApocalypse)
            {
                yield return new WaitForSeconds(0.4f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, true);
                yield return new WaitForSeconds(0.5f);

                yield return CustomMethods.PlayAlternateDialogue(Emotion.Neutral, DialogueEvent.Speaker.Leshy, 0.2f,
                    "Once upon a time, [c:bR]three birds[c:] lived happily in the lush Forest with their fellow animals.",
                    "One day a stranger arrived at the Forest.He proclaimed that the Forest would soon be ensared in a bitter conflict.",
                    "One that would only end when everything was devoured by a[c: bR]terrible Beast[c:].",
                    "The birds, frightened by this doomsay, sought to prevent conflict from ever breaking out.");
                
                // Look down at the board
                Singleton<ViewManager>.Instance.SwitchToView(View.Board);
                yield return new WaitForSeconds(0.25f);

                //smallSlot.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("[c:bR]Small Bird[c:] punished wrongdoers with his beak.");
                //longSlot.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("[c:bB]Long Bird[c:] weighed the sins of all creatures in the forest with his scales.");
                base.PlayableCard.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("With his many eyes, [c:bG]Big Bird[c:] kept constant watch over the entire Forest.");

                yield return CustomMethods.PlayAlternateDialogue(Emotion.Neutral, DialogueEvent.Speaker.Leshy, 0.2f,
                    "Fights began to break out. More and more creatures left the Forest, no matter how hard the birds worked.",
                    "They decided to combine their powers. This way, they could better protect their home.",
                    "This way they could better return the peace.");
            }

            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            yield return new WaitForSeconds(0.2f);

            // Remove cards
            //smallSlot.Card.RemoveFromBoard(true);
            yield return new WaitForSeconds(0.2f);
            //longSlot.Card.RemoveFromBoard(true);
            yield return new WaitForSeconds(0.2f);
            base.PlayableCard.RemoveFromBoard(true);
            yield return new WaitForSeconds(0.5f);

            yield return TableEffects();

            // More text
            if (!WstlSaveManager.HasSeenApocalypse)
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Darkness fell upon the forest. Mayhem ran amok as creatures screamed in terror at the towering bird.");

            // Give player Apocalypse in their deck and their hand
            Singleton<ViewManager>.Instance.SwitchToView(View.Hand);

            CardInfo info = CardLoader.GetCardByName("wstl_lyingAdult");
            RunState.Run.playerDeck.AddCard(info);
            
            // set cost to 0 for this fight (can play immediately that way)
            info.cost = 0;
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(info, null, 0.25f, null);
            WstlSaveManager.HasAdult = true;
            yield return new WaitForSeconds(0.2f);

            // Li'l text blurb
            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Someone yelled out 'It's [c:bR]the Beast[c:]! A big, scary monster lives in the Black Forest!'");

            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.15f);
            if (!WstlSaveManager.HasSeenAdult)
            {
                WstlSaveManager.HasSeenAdult = true;
                yield return CustomMethods.PlayAlternateDialogue(Emotion.Neutral, DialogueEvent.Speaker.Leshy, 0.2f,
                    "The three birds, [c:bR]now one[c:] looked around for [c:bR]the Beast[c:]. But there was nothing.",
                    "No creatures. No beast. No sun or moon or stars. Only a single bird, alone in an empty forest.");
            }
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }

        private IEnumerator TableEffects()
        {
            WstlSaveManager.HasSeenAdultEffects = true;

            Color glowRed = GameColors.Instance.glowRed;
            Color darkRed = GameColors.Instance.darkRed;
            darkRed.a = 0.5f;
            Color gray = GameColors.Instance.gray;
            gray.a = 0.5f;

            Singleton<TableVisualEffectsManager>.Instance.ChangeTableColors(GameColors.Instance.nearBlack, GameColors.Instance.brown, GameColors.Instance.gray, darkRed, darkRed, glowRed, glowRed, glowRed, glowRed);

            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(0.166f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(1.418f);

            AudioController.Instance.StopLoop(1);
            AudioController.Instance.SetLoopVolume((Singleton<GameFlowManager>.Instance as Part1GameFlowManager).GameTableLoopVolume, 0.25f);
        }
    }
}
