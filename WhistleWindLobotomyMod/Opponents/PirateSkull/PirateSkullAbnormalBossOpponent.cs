using DiskCardGame;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents.PirateSkull
{
    public class PirateSkullAbnormalBossOpponent : PirateSkullBossOpponent
    {
        // Change these to custom cards
        private new readonly string[] POSSIBLE_PACK_CARDS = new string[7] { "wstl_snowWhitesApple", "wstl_laetitia", "wstl_youreBald", "wstl_burrowingHeaven", "wstl_parasiteTree", "wstl_dontTouchMe", "wstl_todaysShyLook" };

        public override IEnumerator StartNewPhaseSequence()
        {
            TurnPlan.Clear();
            switch (NumLives)
            {
                case 2:
                    yield return StartPhase2();
                    break;
                case 1:
                    yield return StartGiantCardPhase();
                    break;
            }
        }

        // Identical to vanilla but with the cards replaced
        private new IEnumerator StartPhase2()
        {
            yield return ClearBoard();
            yield return ClearQueue();
            yield return new WaitForSeconds(0.5f);
            Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue, immediate: false, lockAfter: true);
            yield return new WaitForSeconds(0.25f);
            GameObject packObj = Instantiate(ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/CardPack"));
            Vector3 vector = new(0.7f, 5.05f, 0.9f);
            Vector3 position = vector + Vector3.forward * 8f;
            packObj.transform.position = position;
            Tween.Position(packObj.transform, vector, 0.5f, 0f, Tween.EaseOutStrong);
            yield return new WaitForSeconds(0.5f);
            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Yar?", -2.5f, 0.5f, Emotion.Curious, TextDisplayer.LetterAnimation.Jitter, DialogueEvent.Speaker.PirateSkull);
            yield return new WaitForSeconds(0.1f);
            List<CardInfo> list = new();
            for (int j = 0; j < 2; j++)
            {
                CardInfo item = CustomRandom.Bool() ? CardLoader.GetCardByName("Squirrel") : CardLoader.GetCardByName("Rabbit");
                list.Add(item);
            }
            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
            for (int k = 0; k < 2; k++)
            {
                CardInfo cardByName = CardLoader.GetCardByName(POSSIBLE_PACK_CARDS[SeededRandom.Range(0, POSSIBLE_PACK_CARDS.Length, randomSeed++)]);
                list.Add(cardByName);
            }
            yield return PackMule.SpawnAndOpenPack(list, packObj.transform, packObj.transform.position);
            Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
            yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("PirateSkullRodentPack", TextDisplayer.MessageAdvanceMode.Input);
            yield return new WaitForSeconds(0.1f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            yield return new WaitForSeconds(0.1f);
            List<CardSlot> openSlots = Singleton<BoardManager>.Instance.OpponentSlotsCopy;
            for (int i = 0; i < 2; i++)
            {
                int index = SeededRandom.Range(0, openSlots.Count, randomSeed++);
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName("wstl_apostleMoleman"), openSlots[index], 0.2f);
                openSlots.RemoveAt(index);
            }
            yield return this.ReplaceWithCustomBlueprint(LobotomyEncounterManager.PirateSkullAbnormalBossP2);
            Singleton<InteractionCursor>.Instance.InteractionDisabled = false;
        }
    }
}
