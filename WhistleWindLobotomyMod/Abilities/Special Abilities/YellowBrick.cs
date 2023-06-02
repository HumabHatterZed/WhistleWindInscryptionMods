using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class YellowBrick : SpecialCardBehaviour, IOnOtherCardResolveInHand
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Yellow Brick Road";
        public const string rDesc = "Gain a special card when Ozma, The Road Home, Warm-Hearted Woodsman, and Scarecrow Searching for Wisdom are all on the same side of the board.";

        public override bool RespondsToDrawn() => !LobotomyConfigManager.Instance.NoEvents;
        public override bool RespondsToResolveOnBoard() => !LobotomyConfigManager.Instance.NoEvents;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => !LobotomyConfigManager.Instance.NoEvents && otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        public bool RespondsToOtherCardResolveInHand(PlayableCard card) => !LobotomyConfigManager.Instance.NoEvents && card.OpponentCard == base.PlayableCard.OpponentCard;

        public override IEnumerator OnDrawn() => CheckForOtherCards();
        public override IEnumerator OnResolveOnBoard() => CheckForOtherCards();
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => CheckForOtherCards();
        public IEnumerator OnOtherCardResolveInHand(PlayableCard card) => CheckForOtherCards();
        private IEnumerator CheckForOtherCards()
        {
            if (LobotomySaveManager.OwnsLyingAdult)
            {
                LobotomyPlugin.Log.LogDebug("Player already has Adult Who Tells Lies.");
                yield break;
            }

            List<PlayableCard> cardsOnBoard = new() { null, null, null, null, null };

            // check cards on the board first
            foreach (CardSlot slot in HelperMethods.GetSlotsCopy(base.PlayableCard.OpponentCard).Where((CardSlot s) => s.Card != null))
            {
                string cardName = slot.Card.Info.name;
                if (slot == base.PlayableCard.Slot)
                    cardsOnBoard[0] ??= slot.Card;
                else if (cardName.StartsWith("wstl_scaredyCat"))
                    cardsOnBoard[1] ??= slot.Card;
                else if (cardName == "wstl_wisdomScarecrow")
                    cardsOnBoard[2] ??= slot.Card;
                else if (cardName == "wstl_warmHeartedWoodsman")
                    cardsOnBoard[3] ??= slot.Card;
                else if (cardName == "wstl_ozma")
                    cardsOnBoard[4] ??= slot.Card;
            }

            if (cardsOnBoard.Count(x => x != null) < 4)
                yield break;

            if (base.PlayableCard.OpponentCard)
            {
                foreach (PlayableCard card in Singleton<Opponent>.Instance.Queue.Where(x => x.HasTrait(LobotomyCardManager.TraitEmeraldCity)))
                {
                    if (!cardsOnBoard.Contains(card))
                    {
                        yield return Emerald(cardsOnBoard, card);
                        break;
                    }
                }
            }
            else
            {
                foreach (PlayableCard card in PlayerHand.Instance.CardsInHand.Where(x => x.HasTrait(LobotomyCardManager.TraitEmeraldCity)))
                {
                    if (!cardsOnBoard.Contains(card))
                    {
                        yield return Emerald(cardsOnBoard, card);
                        break;
                    }
                }
            }
        }

        private IEnumerator Emerald(List<PlayableCard> cardsOnBoard, PlayableCard cardInHand = null)
        {
            PlayableCard ozma = cardsOnBoard[4] ?? cardInHand;
            PlayableCard scarecrow = cardsOnBoard[2] ?? cardInHand;
            PlayableCard woodsman = cardsOnBoard[3] ?? cardInHand;
            PlayableCard scaredyCat = cardsOnBoard[1] ?? cardInHand;
            PlayableCard roadHome = cardsOnBoard[0] ?? cardInHand;

            bool firstTime = !DialogueEventsData.EventIsPlayed("LyingAdultOutro");
            bool opponentCard = base.PlayableCard.OpponentCard;

            yield return new WaitForSeconds(0.5f);
            yield return BoardEffects.EmeraldTableEffects();
            AudioController.Instance.SetLoopVolume(0.5f * (Singleton<GameFlowManager>.Instance as Part1GameFlowManager).GameTableLoopVolume, 0.5f);
            AudioController.Instance.SetLoopAndPlay("red_noise", 1);
            AudioController.Instance.SetLoopVolumeImmediate(0.3f, 1);

            yield return DialogueHelper.PlayDialogueEvent("LyingAdultIntro");

            CardInfo info = CardLoader.GetCardByName("wstl_lyingAdult");
            if (opponentCard)
            {
                List<CardSlot> validSlots = HelperMethods.GetSlotsCopy(opponentCard).FindAll(x => x.Card == null);
                if (validSlots.Count > 0)
                {
                    Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue, lockAfter: true);
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(info, validSlots[SeededRandom.Range(0, validSlots.Count - 1, RunState.RandomSeed)], resolveTriggers: false);
                }
                else
                {
                    Singleton<ViewManager>.Instance.SwitchToView(View.Board, lockAfter: true);
                    yield return HelperMethods.QueueCreatedCard(info);
                }
            }
            else
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.Hand, lockAfter: true);

                RunState.Run.playerDeck.AddCard(info);
                info.cost = 0;

                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(info, null, 0.25f, null);

                LobotomySaveManager.OwnsLyingAdult = true;
            }

            yield return new WaitForSeconds(0.2f);
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultIntro2", 0.4f);

            yield return LookAtCard(ozma, cardInHand);
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultOzma");
            yield return ModifyCard(ozma, new(1, 0));


            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.2f);
            yield return RemoveFromBoardOrHand(ozma);
            yield return new WaitForSeconds(0.5f);

            yield return DialogueHelper.PlayDialogueEvent("LyingAdultOzma2", 0.4f);
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultIntro3", 0.4f);

            yield return LookAtCard(scarecrow, cardInHand);
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultScarecrow");
            yield return ModifyCard(scarecrow, new() { bonesCostAdjustment = -1 });

            yield return LookAtCard(woodsman, cardInHand);
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultWoodsman");
            yield return ModifyCard(woodsman, new(0, 1));

            yield return LookAtCard(scaredyCat, cardInHand);
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultScaredyCat");
            scaredyCat.Anim.StrongNegationEffect();
            if (!LobotomySaveManager.UnlockedLyingAdult)
                scaredyCat.AddTemporaryMod(new(1, 0));
            yield return new WaitForSeconds(0.5f);

            yield return LookAtCard(roadHome, cardInHand);
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultRoadHome");
            yield return ModifyCard(roadHome, new(0, 2));

            yield return DialogueHelper.PlayDialogueEvent("LyingAdultIntro4");
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.2f);

            // Remove cards
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultScarecrow2", 0f);
            yield return RemoveFromBoardOrHand(scarecrow);

            yield return DialogueHelper.PlayDialogueEvent("LyingAdultWoodsman2", 0f);
            yield return RemoveFromBoardOrHand(woodsman);

            yield return DialogueHelper.PlayDialogueEvent("LyingAdultScaredyCat2", 0f);
            yield return RemoveFromBoardOrHand(scaredyCat);

            yield return DialogueHelper.PlayDialogueEvent("LyingAdultRoadHome2", 0f);
            yield return RemoveFromBoardOrHand(roadHome);

            yield return DialogueHelper.PlayDialogueEvent("LyingAdultOutro");

            LobotomySaveManager.UnlockedLyingAdult = true;
            CardManager.AllCardsCopy.Find(x => x.name == "wstl_theRoadHome").baseAttack = 2;
            CardManager.AllCardsCopy.Find(x => x.name == "wstl_scaredyCatStrong").baseAttack = 3;
            CardManager.AllCardsCopy.Find(x => x.name == "wstl_ozma").baseHealth = 3;
            CardManager.AllCardsCopy.Find(x => x.name == "wstl_warmHeartedWoodsman").baseHealth = 4;
            CardManager.AllCardsCopy.Find(x => x.name == "wstl_wisdomScarecrow").bonesCost = 3;

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
        }
        private IEnumerator ModifyCard(PlayableCard card, CardModificationInfo mod)
        {
            card.Anim.StrongNegationEffect();
            if (!LobotomySaveManager.UnlockedLyingAdult && !card.OpponentCard && !card.OriginatedFromQueue)
                RunState.Run.playerDeck.ModifyCard(card.Info, mod);

            yield return new WaitForSeconds(0.5f);
        }
        private IEnumerator RemoveFromBoardOrHand(PlayableCard card)
        {
            if (card.InHand)
                Singleton<PlayerHand>.Instance.RemoveCardFromHand(card);

            if (card.InOpponentQueue)
                Singleton<Opponent>.Instance.Queue.Remove(card);

            card.RemoveFromBoard();
            yield return new WaitForSeconds(0.5f);
        }
        private IEnumerator LookAtCard(PlayableCard card, PlayableCard cardInHand)
        {
            Singleton<ViewManager>.Instance.SwitchToView(card != cardInHand ? View.Board : View.Default);
            yield return new WaitForSeconds(0.2f);
            if (card.InHand)
                (Singleton<PlayerHand>.Instance as PlayerHand3D).MoveCardAboveHand(card);
        }
    }
    public class RulebookEntryYellowBrick : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_YellowBrick()
            => RulebookEntryYellowBrick.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryYellowBrick>(YellowBrick.rName, YellowBrick.rDesc).Id;
        private void SpecialAbility_YellowBrick()
            => YellowBrick.specialAbility = AbilityHelper.CreateSpecialAbility<YellowBrick>(pluginGuid, YellowBrick.rName).Id;
    }
}
