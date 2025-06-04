using DiskCardGame;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Opponents
{
    /// <summary>
    /// Boss Ordeal accessible via challenge.
    /// Operates as a gauntlet, with all tiers playing out one after the other from Dawn to Midnight.
    /// Difficulty range: [20] +[1,3]
    /// Cards required: 1, 2, 4, 1
    /// Valid regions: 3
    /// </summary>
    public class OrdealWhite : OrdealBattleSequencer
    {
        private string chosenWhiteDawnFixer;
        private readonly List<string> allPossibleFixers = new()
        {
            Cards.fixerRed, Cards.fixerBlack, Cards.fixerWhite, Cards.fixerPale
        };
        private CardSlot clawSlot = null;

        public override IEnumerator OpponentUpkeep()
        {
            if (Opponent.NumLives > 1 || clawSlot == null)
                yield break;

            CleanupTargetIcons();

            if (clawSlot.Card != null)
                yield return clawSlot.Card.DieTriggerless();

            CameraEffects.Instance.Shake(1f, 0.75f);
            yield return BoardManager.Instance.CreateCardInSlot(CardLoader.GetCardByName(Cards.claw), clawSlot);
            yield return new WaitForSeconds(0.2f);
            OrdealBannerManager.Instance.DisplayBanner(ordealType, true);
            AudioController.Instance.PlaySound3D("map_slam", MixerGroup.TableObjectsSFX, Singleton<BoardManager>.Instance.transform.position);

            yield return new WaitUntil(() => !OrdealBannerManager.Instance.Displaying);
            yield return HelperMethods.ChangeCurrentView(OrdealUtils.ViewCounter, endDelay: 0.5f);
            yield return OrdealCounterManager.Instance.UpdateAmountLeft(amountKilledThisTurn, 0.25f);

            clawSlot = null;
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
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            LobotomyPlugin.Log.LogDebug("[WhiteOrdeal] OnOtherCardDie");
            yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);
            //yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);
            //ordealTier++;
            //MinNumCardsRequired = ordealTier switch { 1 => ConstructWhiteNoon(), 2 => ConstructWhiteDusk(), _ => 1 };

            //if (ordealTier < 4 || card.Info.name != Cards.claw)
            //    yield break;

            
        }

        public override IEnumerator OpponentLifeLost()
        {
            LobotomyPlugin.Log.LogDebug($"[WhiteOrdeal] OpponentLifeLost: numLives: {Opponent.NumLives}");

            if (Opponent.NumLives == 0)
                yield break;

            ordealTier++;
            OrdealBannerManager.Instance.UpdateBanner(ordealType, ordealTier);

            Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue);

            Opponent.StartCoroutine(Opponent.ClearBoard());
            yield return Opponent.ClearQueue();

            switch (Opponent.NumLives)
            {
                case 3:
                    OrdealBannerManager.Instance.DisplayBanner(ordealType, true);
                    MinNumCardsRequired = ConstructWhiteNoon();
                    break;
                case 2:
                    OrdealBannerManager.Instance.DisplayBanner(ordealType, true);
                    MinNumCardsRequired = ConstructWhiteDusk();
                    break;
                case 1:
                    MinNumCardsRequired = 1;
                    InitiateWhiteMidnight();
                    break;
            }
        }

        //private IEnumerator AdvanceToNextTier()
        //{
        //    currentTier++;
        //    MinNumCardsRequired = currentTier switch { 1 => ConstructWhiteNoon(), 2 => ConstructWhiteDusk(), _ => 1 };

        //    OrdealBannerManager.Instance.UpdateBannerOutro(ordealType, ordealTier);
        //    base.StartCoroutine(Opponent.DisplayBanner(ordealType, false));

        //    OrdealCounterManager.Instance.UpdateConsole(BattleSequencer.ordealTier, BattleSequencer.MinNumCardsRequired);
        //    AudioController.Instance.FadeOutLoop(0.1f, 0, 1);
        //}
        private void InitiateWhiteMidnight()
        {
            clawSlot = BoardManager.Instance.OpponentSlotsCopy[UnityEngine.Random.RandomRangeInt(0, BoardManager.Instance.OpponentSlotsCopy.Count)];
            CreateTargetIcon(clawSlot, GameColors.Instance.gold);
        }

        private int ConstructWhiteDawn(EncounterData encounterData)
        {
            chosenWhiteDawnFixer = UnityEngine.Random.RandomRangeInt(0, 3) switch
            {
                0 => Cards.fixerWhite,
                1 => Cards.fixerBlack,
                _ => Cards.fixerRed
            };

            encounterData.Blueprint
                .AddTurn()
                .AddTurn()
                .AddTurn(new List<EncounterBlueprintData.CardBlueprint>() {
                    EncounterManager.NewCardBlueprint(chosenWhiteDawnFixer)
            });

            return 1;
        }
        private int ConstructWhiteNoon()
        {

            List<string> possibleFixers = new(allPossibleFixers);
            possibleFixers.Remove(chosenWhiteDawnFixer);
            possibleFixers.Remove(Cards.fixerPale);
            possibleFixers.Randomize();

            List<List<CardInfo>> newPlan = new() {
                new(),
                new() { CardLoader.GetCardByName(possibleFixers[0]) },
                new(),
                new(),
                new() { CardLoader.GetCardByName(possibleFixers[1]) }
            };

            Opponent.ReplaceAndAppendTurnPlan(newPlan);
            return 2;
        }
        private int ConstructWhiteDusk()
        {
            List<string> possibleFixers = new(allPossibleFixers);
            possibleFixers.Randomize();

            List<List<CardInfo>> newPlan = new() {
                new(),
                new() { CardLoader.GetCardByName(possibleFixers[0]) },
                new(),
                new(),
                new() { CardLoader.GetCardByName(possibleFixers[1]) },
                new(),
                new(),
                new(),
                new() { CardLoader.GetCardByName(possibleFixers[2]) },
                new(),
                new() { CardLoader.GetCardByName(possibleFixers[3]) }
            };

            Opponent.ReplaceAndAppendTurnPlan(newPlan);
            return 4;
        }
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty)
        {
            Opponent.NumLives = 4;
            return ConstructWhiteDawn(encounterData);
        }
    }
}