using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddTrueSaviour() {
            const string rulebookName = "True Saviour";
            const string dialogue = "[c:bR]I am death and life. Darkness and light.[c:]";

            TrueSaviour.ID = AbilityHelper.New<TrueSaviour>(LobotomyPlugin.pluginGuid,
                "sigilTrueSaviour", rulebookName, "While this card is on the board, it will transform allies into Apostles.", powerLevel: -3, true, dialogue).Id;
        }
    }

    /// <summary>
    /// While this card is on the board, it will transform allies into Apostles.
    /// </summary>
    public class TrueSaviour : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        private readonly string sternDialogue = "[c:bR]Do not deny me.[c:]";

        // True if on the player's side and they have Heretic in hand
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.IsPlayerCard() == playerUpkeep
            && Singleton<PlayerHand>.Instance.CardsInHand.Count(c => c.Info.name == Cards.apostleHeretic) > 0;
        public override IEnumerator OnUpkeep(bool playerUpkeep) => MakeRoomForOneSin();

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            yield return base.PreSuccessfulTriggerSequence();
            yield return SaviourBossUtils.ConvertCardsOnBoard(base.Card.IsPlayerCard(), base.Card, base.GetRandomSeed());
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) {
            if (otherCard != null && otherCard != base.Card) {
                if (otherCard.Info.name != SaviourBossUtils.ONESIN_NAME && otherCard.LacksAllAbilities(ApostleSigil.ID, Confession.ID))
                    return base.Card.OnBoard && base.Card.OpponentCard == otherCard.OpponentCard;
            }
            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) {
            yield return base.PreSuccessfulTriggerSequence();
            yield return SaviourBossUtils.ConvertCardToApostle(otherCard, base.GetRandomSeed());
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) {
            if (killer != null)
                yield return KilledByNonNull(killer);
            else
                yield return KilledByNull();
        }

        private IEnumerator KilledByNonNull(PlayableCard killer) {
            AudioController.Instance.PlaySound2D("mycologist_scream");
            Singleton<UIManager>.Instance?.Effects.GetEffect<ScreenGlitchEffect>().SetIntensity(1f, 0.4f);

            // if not killed by Hundreds of Good Deeds
            if (killer.LacksAbility(Confession.ID)) {
                // kill all Apostles
                foreach (PlayableCard card in Singleton<BoardManager>.Instance.GetCards(!base.Card.OpponentCard, x => x.HasAbility(ApostleSigil.ID))) {
                    yield return card.Die(false, base.Card);
                }
            }

            yield return new WaitForSeconds(0.5f);

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

            if (TurnManager.Instance.Opponent is LobotomyOpponent opp) {
                if (opp.PreventInstantWin(base.Card.Slot, IPreventInstantWin.InstantWinType.Confession))
                    yield return opp.OnInstantWinPrevented(base.Card.Slot, IPreventInstantWin.InstantWinType.Confession);
                else
                    yield return opp.OnInstantWinTriggered(base.Card.Slot, IPreventInstantWin.InstantWinType.Confession);
            }
            else {
                CombatPhaseManager combatManager = Singleton<CombatPhaseManager>.Instance;
                // was a yield return for some reason, check if that makes a difference
                combatManager.DamageDealtThisPhase += 33;

                int excessDamage = Singleton<LifeManager>.Instance.Balance + combatManager.DamageDealtThisPhase - 5;
                int damage = combatManager.DamageDealtThisPhase - excessDamage;

                yield return Singleton<LifeManager>.Instance.ShowDamageSequence(damage, damage, toPlayer: false);
                yield return combatManager.VisualizeExcessLethalDamage(excessDamage, null);

                if (SaveManager.SaveFile.IsPart2)
                    SaveManager.SaveFile.gbcData.currency += excessDamage;
                else
                    RunState.Run.currency += excessDamage;
            }

            if (killer.LacksAbility(Confession.ID)) {
                foreach (CardInfo card in RunState.Run.playerDeck.CardInfos) {
                    RunState.Run.playerDeck.ModifyCard(card, new(1, 2));
                }
                foreach (PlayableCard card in BoardManager.Instance.GetPlayerCards())
                    card.AddTemporaryMod(new(1, 2));
                foreach (PlayableCard card in PlayerHand.Instance.CardsInHand.Where(x => !CardDrawPiles3D.Instance.SideDeckData.Exists(y => x.Info.name == y.name)))
                    card.AddTemporaryMod(new(1, 2));

                DialogueHelper.ShowUntilInput("Divine power infuses the beasts in your caravan, empowering them.");
            }

            // Resets Blessings
            LobotomyPlugin.Log.LogDebug($"Resetting the clock to [0].");
            LobotomyConfigManager.SetBlessings(0);
        }
        private IEnumerator KilledByNull() {
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f, false);
            yield return DialogueHelper.PlayDialogueEvent("WhiteNightKilledByNull");

            yield return new WaitForSeconds(0.2f);
            yield return Singleton<LifeManager>.Instance.ShowDamageSequence(1, 1, toPlayer: true, 0.25f, ResourceBank.Get<GameObject>("Prefabs/Environment/ScaleWeights/Weight_RealTooth"));

            yield return DialogueHelper.ShowUntilInput(sternDialogue, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord, -0.65f, 0.4f);
        }
        private IEnumerator MakeRoomForOneSin() {
            if (Singleton<BoardManager>.Instance.GetCards(!base.Card.OpponentCard).Count < 4)
                yield break;

            // If all slots on the owner's side are full
            yield return new WaitForSeconds(0.2f);

            List<PlayableCard> cardsToKill = BoardManager.Instance.GetCards(!base.Card.OpponentCard, (PlayableCard c) => c != base.Card);
            PlayableCard cardToKill = cardsToKill[SeededRandom.Range(0, cardsToKill.Count, base.GetRandomSeed() + 1)];

            ViewManager.Instance.SwitchToView(View.Hand);
            foreach (PlayableCard card in Singleton<PlayerHand>.Instance.CardsInHand.Where(c => c.HasAbility(Confession.ID)))
                card.Anim.StrongNegationEffect();

            yield return new WaitForSeconds(0.4f);

            ViewManager.Instance.SwitchToView(View.Board);
            cardToKill.Anim.SetShaking(true);
            yield return new WaitForSeconds(0.25f);
            yield return cardToKill.Die(false, base.Card);
            yield return new WaitForSeconds(0.45f);
            yield return DialogueHelper.PlayDialogueEvent("WhiteNightMakeRoom");
        }
    }
}
