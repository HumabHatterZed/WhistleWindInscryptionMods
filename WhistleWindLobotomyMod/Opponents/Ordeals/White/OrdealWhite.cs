using DiskCardGame;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// Boss Ordeal accessible via challenge.
    /// Operates as a gauntlet, with all tiers playing out one after the other from Dawn to Midnight.
    /// Difficulty range: [20] +[1,3]
    /// Cards required: 1, 2, 4, 1
    /// Valid regions: 3
    /// </summary>
    public class OrdealWhite : OrdealBattleSequencer, IPlayerTurnEnd {
        private string chosenWhiteDawnFixer;
        private readonly List<string> allPossibleFixers = new()
        {
            Cards.fixerRed, Cards.fixerBlack, Cards.fixerWhite, Cards.fixerPale
        };
        private CardSlot clawSlot = null;

        private bool playedTheClaw = false;

        public bool RespondsToPlayerTurnEnd() => Opponent.NumLives == 1 && !playedTheClaw && clawSlot != null;
        public int PlayerTurnEndPriority() => 0;
        public IEnumerator OnPlayerTurnEnd() {
            CleanupTargetIcons();

            if (clawSlot.Card != null) {
                yield return clawSlot.Card.DieTriggerless();
            }

            CameraEffects.Instance.Shake(1f, 0.75f);
            yield return BoardManager.Instance.CreateCardInSlot(CardLoader.GetCardByName(Cards.claw), clawSlot);
            yield return new WaitForSeconds(0.2f);
            AudioController.Instance.PlaySound3D("map_slam", MixerGroup.TableObjectsSFX, Singleton<BoardManager>.Instance.transform.position);
            OrdealBannerManager.Instance.DisplayBanner(ordealType, true);
            ViewManager.Instance.SwitchToView(View.Default);
            yield return new WaitUntil(() => !OrdealBannerManager.Instance.Displaying);
            yield return HelperMethods.ChangeCurrentView(OrdealUtils.ViewCounter, endDelay: 0.5f);
            OrdealCounterManager.Instance.EnableConsole(false);
            yield return new WaitForSeconds(0.3f);
            OrdealCounterManager.Instance.SetTextColour(Color.black);
            OrdealCounterManager.Instance.UpdateConsole(ordealTier, MinNumCardsRequired);
            OrdealCounterManager.Instance.EnableConsole(true);
            yield return new WaitForSeconds(0.3f);
            playedTheClaw = true;
        }

        //public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        //{
        //    LobotomyPlugin.Log.LogDebug("[WhiteOrdeal] OnTurnEnd");
        //    currentTier++;
        //    switch (currentTier)
        //    {
        //        case 1:
        //            ConstructWhiteNoon();
        //            break;
        //        case 2:
        //            ConstructWhiteDusk();
        //            break;
        //        case 3:
        //            break;
        //        case 4:
        //            yield return base.OnTurnEnd(playerTurnEnd);
        //            break;
        //    }
        //}

        public override IEnumerator OpponentLifeLost() {
            LobotomyPlugin.Log.LogDebug($"[WhiteOrdeal] OpponentLifeLost: numLives: {Opponent.NumLives}");
            yield return new WaitUntil(() => !OrdealBannerManager.Instance.Displaying);
            if (Opponent.NumLives == 0) {
                yield return HelperMethods.ChangeCurrentView(View.Default);
                OrdealCounterManager.Instance.EnableConsole(false);
                yield return new WaitForSeconds(0.25f);
                OrdealCounterManager.Instance.SetShown(false);
                yield return new WaitForSeconds(1.5f);
                yield return Opponent.DefeatedFinalBossSequence();
                yield break;
            }

            ordealTier++;
            defeated = false;
            OrdealBannerManager.Instance.UpdateBanner(ordealType, ordealTier);

            yield return HelperMethods.ChangeCurrentView(View.OpponentQueue);
            Opponent.StartCoroutine(Opponent.ClearBoard());
            yield return Opponent.ClearQueue();

            switch (Opponent.NumLives) {
                case 3:
                    OrdealBannerManager.Instance.DisplayBanner(ordealType, true);
                    MinNumCardsRequired = ConstructWhiteNoon();
                    yield return Opponent.QueueNewCards();
                    break;
                case 2:
                    OrdealBannerManager.Instance.DisplayBanner(ordealType, true);
                    MinNumCardsRequired = ConstructWhiteDusk();
                    yield return Opponent.QueueNewCards();
                    break;
                case 1:
                    ValidCards.Add(Cards.claw);
                    OrdealCounterManager.Instance.amountLeft = MinNumCardsRequired = 1;
                    EncounterBluePrint = new();
                    InitiateWhiteMidnight();
                    yield break;
            }
            yield return new WaitForSeconds(0.75f);
            yield return HelperMethods.ChangeCurrentView(OrdealUtils.ViewCounter);
            OrdealCounterManager.Instance.EnableConsole(false);
            yield return new WaitForSeconds(0.3f);
            OrdealCounterManager.Instance.SetTextColour(Color.black);
            OrdealCounterManager.Instance.UpdateConsole(ordealTier, MinNumCardsRequired);
            OrdealCounterManager.Instance.EnableConsole(true);
            yield return new WaitForSeconds(0.3f);
        }

        private void InitiateWhiteMidnight() {
            LobotomyPlugin.Log.LogInfo("[WhiteOrdeal] Midnight");
            clawSlot = BoardManager.Instance.OpponentSlotsCopy[UnityEngine.Random.RandomRangeInt(0, BoardManager.Instance.OpponentSlotsCopy.Count)];
            CreateTargetIcon(clawSlot, GameColors.Instance.gold);
        }
        private int ConstructWhiteDusk() {
            LobotomyPlugin.Log.LogInfo("[WhiteOrdeal] Dusk");
            List<string> possibleFixers = new(allPossibleFixers);
            possibleFixers.Randomize();

            List<List<CardInfo>> newPlan = new() {
                new() { CardLoader.GetCardByName(possibleFixers[1]), CardLoader.GetCardByName(possibleFixers[2]) },
                new(),
                new() { CardLoader.GetCardByName(possibleFixers[0]) },
                new(),
                new() { CardLoader.GetCardByName(possibleFixers[3]) }
            };

            EncounterBluePrint = newPlan;
            Opponent.ReplaceAndAppendTurnPlan(newPlan);
            return 4;
        }
        private int ConstructWhiteNoon() {
            LobotomyPlugin.Log.LogInfo("[WhiteOrdeal] Noon");
            List<string> possibleFixers = new(allPossibleFixers);
            possibleFixers.Remove(chosenWhiteDawnFixer);
            possibleFixers.Remove(Cards.fixerPale);
            possibleFixers.Randomize();

            List<List<CardInfo>> newPlan = new() {
                new() { CardLoader.GetCardByName(possibleFixers[0]), CardLoader.GetCardByName(possibleFixers[1]) }
            };

            EncounterBluePrint = newPlan;
            Opponent.ReplaceAndAppendTurnPlan(newPlan);
            return 2;
        }
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty) {
            ordealTier = 0;
            chosenWhiteDawnFixer = UnityEngine.Random.RandomRangeInt(0, 3) switch {
                0 => Cards.fixerWhite,
                1 => Cards.fixerBlack,
                _ => Cards.fixerRed
            };
            List<EncounterBlueprintData.CardBlueprint> turn = new() {
                EncounterManager.NewCardBlueprint(chosenWhiteDawnFixer)
            };
            encounterData.Blueprint.AddTurn(turn);
            return 1;
        }

        public override void ModifyQueuedCard(PlayableCard card) {
            // fixers shouldn't gain random buffs
        }
    }
}