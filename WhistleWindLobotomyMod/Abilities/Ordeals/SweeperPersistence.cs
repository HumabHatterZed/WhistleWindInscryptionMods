using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddSweeperPersistence() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Persistent Sweeping";
            info.rulebookDescription = "This card is considered Persistent. After attacking, this card will strike adjacent cards that aren't Sweepers. Once per battle at low Health, switch places with a queued Sweeper.";
            SweeperPersistence.ID = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(SweeperPersistence), TextureLoader.LoadTextureFromFile("sigilSweeper.png", LobotomyPlugin.ModAssembly))
                .SetAbilityRedirect("Persistent", Persistent.ID, GameColors.Instance.red)
                .Id;
        }
    }

    /// <summary>
    /// This card is considered Persistent. After attacking, this card will strike adjacent cards that aren't Sweepers. Once per battle at low Health, switch places with a queued Sweeper.
    /// </summary>
    public class SweeperPersistence : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        private bool canReturnToQueue = true;

        public override bool RespondsToAttackEnded() => true;
        public override IEnumerator OnAttackEnded() {
            List<PlayableCard> corpses = base.Card.Slot.GetAdjacentCards().Where(x => x.Info.DisplayedNameEnglish != "Sweeper").ToList();
            if (corpses.Count > 0) {
                yield return HelperMethods.ChangeCurrentView(View.Board);
                yield return DialogueHelper.PlayDialogueEvent("OrdealPersistence");
                foreach (PlayableCard card in corpses) {
                    yield return Singleton<CombatPhaseManager3D>.Instance.SlotAttackSlot(base.Card.Slot, card.Slot);
                }
            }
        }
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            if (canReturnToQueue && TurnManager.Instance.Opponent.Queue.Count > 0 && base.Card.Health == 1 & base.Card.MaxHealth != 1) {
                CardSlot slot = base.Card.Slot;
                PlayableCard queuedCard = TurnManager.Instance.Opponent.Queue.Find(x => x.QueuedSlot == slot);
                queuedCard ??= TurnManager.Instance.Opponent.Queue.GetSeededRandom(base.GetRandomSeed());

                base.Card.UnassignFromSlot();
                slot.Card = null;

                ViewManager.Instance.SwitchToView(View.OpponentQueue);
                yield return new WaitForSeconds(0.3f);
                queuedCard.QueuedSlot = null;
                queuedCard.OnPlayedFromOpponentQueue();
                base.Card.AddTemporaryMod(new(Shadowed.ID));
                base.StartCoroutine(TurnManager.Instance.Opponent.ReturnCardToQueue(base.Card, 0.2f));
                yield return BoardManager.Instance.ResolveCardOnBoard(queuedCard, slot);
                TurnManager.Instance.Opponent.Queue.Remove(queuedCard);
                canReturnToQueue = false;
            }
        }

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            if (!base.Card.Info.Mods.Exists(x => x.singletonId == "wstl:Sweeper")) {
                base.Card.Info.Mods.Add(new(Persistent.ID) { singletonId = "wstl:Sweeper" });
            }
            base.Card.TriggerHandler.AddAbility(Persistent.ID);
            base.Card.Status.hiddenAbilities.Add(Persistent.ID);
            yield break;
        }

        public override int Priority => 1000;
    }
}
