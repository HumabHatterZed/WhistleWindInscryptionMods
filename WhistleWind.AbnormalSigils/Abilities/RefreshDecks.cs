using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    /// <summary>
    /// When this card is played, discard your current hand and reshuffle both draw piles, then draw a new opening hand based on the number of turns that have passed.
    /// </summary>
    public class RefreshDecks : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public const string REMOVE_ON_REFRESH_ID = "wstl:RemoveOnRefresh";
        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();
        public override IEnumerator OnResolveOnBoard() {
            if (!SaveManager.SaveFile.IsPart2)
                yield return HelperMethods.ChangeCurrentView(View.Hand, 0.2f, 0.4f);

            yield return Singleton<PlayerHand>.Instance.CleanUp();
            yield return new WaitForSeconds(0.2f);

            if (!SaveManager.SaveFile.IsPart2)
                yield return HelperMethods.ChangeCurrentView(View.CardPiles, 0.2f, 0.4f);

            Singleton<PlayerHand>.Instance.Initialize();
            Singleton<CardDrawPiles>.Instance.CleanUp();
            yield return new WaitForSeconds(0.4f);
            yield return Singleton<CardDrawPiles>.Instance.Initialize();
            CardDrawPiles.Instance.Deck.randomSeed *= 2;
            yield return new WaitForSeconds(0.4f);

            ViewManager.Instance.SwitchToView(View.Hand);
            yield return new WaitForSeconds(0.1f);

            yield return DrawImprovedOpeningHoof();

            ViewManager.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.1f);

            foreach (PlayableCard card in BoardManager.Instance.CardsOnBoard.Concat(PlayerHand.Instance.CardsInHand).Concat(TurnManager.Instance.Opponent.Queue)) {
                if (card != null) {
                    CardModificationInfo mod = card.TemporaryMods.Find(x => x.singletonId == REMOVE_ON_REFRESH_ID);
                    if (mod != null) {
                        card.RemoveTemporaryMod(mod);
                    }
                }
            }

            if (!base.Card.Info.IsSpell()) {
                yield return base.Card.Die(false, null);
            }
        }

        private IEnumerator DrawImprovedOpeningHoof() {
            AbnormalPlugin.Log.LogDebug($"[RefreshDecks] TurnNum: {TurnManager.Instance.TurnNumber}");
            bool hasPiles = Singleton<CardDrawPiles3D>.Instance != null;

            if (hasPiles && TurnManager.Instance.TurnNumber > 3) {
                // max out at 3 additional side draws
                int numAdditionalSide = TurnManager.Instance.TurnNumber > 11 ? 3 : (int)Mathf.Sqrt(TurnManager.Instance.TurnNumber - 3);
                AbnormalPlugin.Log.LogDebug($"[RefreshDecks] Side: {numAdditionalSide}");
                for (int i = 0; i < numAdditionalSide; i++) {
                    Singleton<CardDrawPiles3D>.Instance.sidePile.Draw();
                    yield return Singleton<CardDrawPiles3D>.Instance.DrawFromSidePile();
                }
            }

            yield return Singleton<CardDrawPiles>.Instance.DrawOpeningHand(TurnManager.Instance.GetFixedHand());

            if (TurnManager.Instance.TurnNumber > 3) {
                // max out at 3 additional main draws
                int numAdditionalMain = TurnManager.Instance.TurnNumber > 14 ? 3 : (int)Mathf.Sqrt(3 * TurnManager.Instance.TurnNumber / 4 - 2);
                AbnormalPlugin.Log.LogDebug($"[RefreshDecks] Main: {numAdditionalMain}");
                if (hasPiles) {
                    for (int i = 0; i < numAdditionalMain; i++) {
                        Singleton<CardDrawPiles3D>.Instance.pile.Draw();
                        yield return Singleton<CardDrawPiles>.Instance.DrawCardFromDeck();
                    }
                }
                else {
                    for (int i = 0; i < numAdditionalMain; i++) {
                        yield return Singleton<CardDrawPiles>.Instance.DrawCardFromDeck();
                    }
                }
            }

            CardDrawPiles.Instance.turnsSinceExhausted = 0;
        }
    }

    public partial class AbnormalPlugin {
        private void Ability_RefreshDecks() {
            const string rulebookName = "Grand Reopening";
            RefreshDecks.ability = AbnormalAbilityHelper.CreateAbility<RefreshDecks>(
                "sigilRefreshDecks", rulebookName, "When this card is played, discard your current hand and reshuffle both draw piles, then draw a new opening hand based on the number of turns that have passed.",
                null, powerLevel: 0, canStack: false).Id;
        }
    }
}