using DiskCardGame;
using EasyFeedback.APIs;
using Infiniscryption.P03KayceeRun.Encounters;
using InscryptionAPI.Encounters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    /// <summary>
    /// Appears as special boss
    /// 
    /// Combines all Ordeal tiers into one boss encounter
    /// </summary>
    public class OrdealWhite : OrdealBattleSequencer
    {
        private int currentTier = 0;
        private string chosenWhiteDawnFixer;
        private readonly List<string> allPossibleFixers = new()
        {
            "wstl_fixerRed", "wstl_fixerWhite", "wstl_fixerWhite", "wstl_fixerPale"
        };
        private CardSlot clawSlot = null;

        public override IEnumerator OpponentUpkeep()
        {
            if (clawSlot == null || Opponent.NumTurnsTaken < Opponent.TurnPlan.Count + 1)
                yield break;

            CleanupTargetIcons();

            if (clawSlot.Card != null)
                yield return clawSlot.Card.Die(false);

            CameraEffects.Instance.Shake(1f, 0.75f);
            yield return BoardManager.Instance.CreateCardInSlot(CardLoader.GetCardByName("wstl_claw"), clawSlot);
            yield return new WaitForSeconds(0.2f);
            AudioController.Instance.PlaySound3D("map_slam", MixerGroup.TableObjectsSFX, Singleton<BoardManager>.Instance.transform.position);
            yield return new WaitForSeconds(1f);
            clawSlot = null;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            LobotomyPlugin.Log.LogDebug("OnTurnEnd");
            currentTier++;
            switch (currentTier)
            {
                case 1:
                    ConstructWhiteNoon();
                    break;
                case 2:
                    ConstructWhiteDusk();
                    break;
                case 3:
                    break;
                case 4:
                    yield return base.OnTurnEnd(playerTurnEnd);
                    break;
            }
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            LobotomyPlugin.Log.LogDebug("OnOtherCardDie");
            currentTier++;
            switch (currentTier)
            {
                case 1:
                    ConstructWhiteNoon();
                    break;
                case 2:
                    ConstructWhiteDusk();
                    break;
                case 3:
                    break;
            }

            if (currentTier < 4 || card.Info.name != "wstl_claw")
                yield break;

            yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);
        }

        private void InitiateWhiteMidnight()
        {
            clawSlot = BoardManager.Instance.OpponentSlotsCopy[UnityEngine.Random.RandomRangeInt(0, BoardManager.Instance.OpponentSlotsCopy.Count)];
            CreateTargetIcon(clawSlot, GameColors.Instance.gold);
        }

        private void ConstructWhiteDawn(EncounterData encounterData)
        {
            chosenWhiteDawnFixer = UnityEngine.Random.RandomRangeInt(0, 3) switch
            {
                0 => "wstl_fixerWhite",
                1 => "wstl_fixerBlack",
                _ => "wstl_fixerRed"
            };

            encounterData.Blueprint
                .AddTurn().AddTurn()
                .AddTurn(new List<EncounterBlueprintData.CardBlueprint>() {
                    EncounterManager.NewCardBlueprint(chosenWhiteDawnFixer)
            });
        }
        private void ConstructWhiteNoon()
        {
            List<string> possibleFixers = new(allPossibleFixers);
            possibleFixers.Remove(chosenWhiteDawnFixer);
            possibleFixers.Remove("wstl_fixerPale");
            possibleFixers.Randomize();

            Opponent.TurnPlan.Add(new());
            Opponent.TurnPlan.Add(new() { CardLoader.GetCardByName(possibleFixers[0])});
            Opponent.TurnPlan.Add(new());
            Opponent.TurnPlan.Add(new());
            Opponent.TurnPlan.Add(new() { CardLoader.GetCardByName(possibleFixers[1]) });
        }
        private void ConstructWhiteDusk()
        {
            List<string> possibleFixers = new(allPossibleFixers);
            possibleFixers.Randomize();

            Opponent.TurnPlan.Add(new() { CardLoader.GetCardByName(possibleFixers[0]) });
            Opponent.TurnPlan.Add(new());
            Opponent.TurnPlan.Add(new());
            Opponent.TurnPlan.Add(new() { CardLoader.GetCardByName(possibleFixers[1]) });
            Opponent.TurnPlan.Add(new());
            Opponent.TurnPlan.Add(new());
            Opponent.TurnPlan.Add(new());
            Opponent.TurnPlan.Add(new() { CardLoader.GetCardByName(possibleFixers[2]) });
            Opponent.TurnPlan.Add(new());
            Opponent.TurnPlan.Add(new());
            Opponent.TurnPlan.Add(new() { CardLoader.GetCardByName(possibleFixers[3]) });
        }
        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            ConstructWhiteDawn(encounterData);
            return encounterData;
        }
    }
}