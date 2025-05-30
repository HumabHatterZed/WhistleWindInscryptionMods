using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWindLobotomyMod.Opponents
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
            Cards.fixerRed, Cards.fixerBlack, Cards.fixerWhite, Cards.fixerPale
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
            yield return BoardManager.Instance.CreateCardInSlot(CardLoader.GetCardByName(Cards.claw), clawSlot);
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

            if (currentTier < 4 || card.Info.name != Cards.claw)
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
                0 => Cards.fixerWhite,
                1 => Cards.fixerBlack,
                _ => Cards.fixerRed
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
            possibleFixers.Remove(Cards.fixerPale);
            possibleFixers.Randomize();

            Opponent.TurnPlan.Add(new());
            Opponent.TurnPlan.Add(new() { CardLoader.GetCardByName(possibleFixers[0]) });
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
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int difficulty)
        {
            //ConstructWhiteDawn(encounterData);
            return -1;
        }
    }
}