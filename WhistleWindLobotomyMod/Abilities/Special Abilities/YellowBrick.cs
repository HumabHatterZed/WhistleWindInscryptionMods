using Core.Helpers;
using DigitalRuby.LightningBolt;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
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
    public class YellowBrick : SpecialCardBehaviour, IOnOtherCardResolveInHand, IOnOtherCardAddedToHand
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Yellow Brick Road";
        public const string rDesc = "Gain a special card when Ozma, The Road Home, Warm-Hearted Woodsman, and Scarecrow Searching for Wisdom are all on the same side of the board.";

        private List<string>[] ValidCardNames = new List<string>[]
        {
            new() { Cards.wisdomScarecrow, Cards.wisdomScarecrowPixel },
            new() { Cards.warmHeartedWoodsman },
            new() { Cards.ozma, Cards.ozmaPixel },
            new() { Cards.scaredyCat, Cards.scaredyCatStrong }
        };

        public override bool RespondsToDrawn() => !LobotomyConfigManager.NoEvents;
        public override bool RespondsToOtherCardDrawn(PlayableCard card)
        {
            if (LobotomyConfigManager.NoEvents)
                return false;

            return card != base.PlayableCard && !base.PlayableCard.OpponentCard;
        }
        public override bool RespondsToResolveOnBoard() => !LobotomyConfigManager.NoEvents;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (LobotomyConfigManager.NoEvents)
                return false;

            return otherCard != base.PlayableCard && otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        }
        public bool RespondsToOtherCardResolveInHand(PlayableCard card) => RespondsToOtherCardResolve(card);
        public override IEnumerator OnDrawn() => CheckForOtherCards();
        public override IEnumerator OnOtherCardDrawn(PlayableCard card) => CheckForOtherCards();
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

            if (BoardManager.Instance.GetCards(!base.PlayableCard.OpponentCard).Count < 3)
                yield break;

            bool isOpponent = base.PlayableCard.OpponentCard;
            PlayableCard[] cardsOnBoard = new PlayableCard[5] { base.PlayableCard, null, null, null, null };

            // check cards on the board first
            foreach (PlayableCard card in BoardManager.Instance.GetCards(!isOpponent, x => x.HasTrait(LobotomyCardManager.EmeraldCity)))
            {
                if (card == base.PlayableCard || LobotomyHelpers.CardIsMimicking(card))
                    continue;

                string cardName = card.Info.name;
                if (ValidCardNames[0].Contains(cardName))
                    cardsOnBoard[1] = card;
                else if (ValidCardNames[1].Contains(cardName))
                    cardsOnBoard[2] = card;
                else if (ValidCardNames[2].Contains(cardName))
                    cardsOnBoard[3] = card;
                else if (ValidCardNames[3].Contains(cardName))
                    cardsOnBoard[4] = card;
            }

            if (cardsOnBoard.Count(x => x != null) < 3) // at least 3 of the Oz cards must be on the board, with the rest in the hoof/queue
                yield break;

            if (isOpponent)
            {
                foreach (PlayableCard card in Singleton<Opponent>.Instance.Queue.Where(x => x.HasTrait(LobotomyCardManager.EmeraldCity)))
                {
                    if (card == base.PlayableCard || LobotomyHelpers.CardIsMimicking(card))
                        continue;

                    string cardName = card.Info.name;
                    if (ValidCardNames[0].Contains(cardName))
                        cardsOnBoard[1] = card;
                    else if (ValidCardNames[1].Contains(cardName))
                        cardsOnBoard[2] = card;
                    else if (ValidCardNames[2].Contains(cardName))
                        cardsOnBoard[3] = card;
                    else if (ValidCardNames[3].Contains(cardName))
                        cardsOnBoard[4] = card;
                }
            }
            else
            {
                foreach (PlayableCard card in PlayerHand.Instance.CardsInHand.Where(x => x.HasTrait(LobotomyCardManager.EmeraldCity)))
                {
                    if (card == base.PlayableCard || LobotomyHelpers.CardIsMimicking(card))
                        continue;

                    string cardName = card.Info.name;
                    if (ValidCardNames[0].Contains(cardName))
                        cardsOnBoard[1] = card;
                    else if (ValidCardNames[1].Contains(cardName))
                        cardsOnBoard[2] = card;
                    else if (ValidCardNames[2].Contains(cardName))
                        cardsOnBoard[3] = card;
                    else if (ValidCardNames[3].Contains(cardName))
                        cardsOnBoard[4] = card;
                }
            }

            if (cardsOnBoard.Count(x => x != null) < 5) // we don't have all 5 Oz cards
                yield break;

            yield return BeginEmerald(cardsOnBoard, isOpponent);
        }

        private IEnumerator BeginEmerald(PlayableCard[] cardsOnBoard, bool opponentCard)
        {
            PlayableCard roadHome = cardsOnBoard[0];
            PlayableCard scarecrow = cardsOnBoard[1];
            PlayableCard woodsman = cardsOnBoard[2];
            PlayableCard ozma = cardsOnBoard[3];
            PlayableCard scaredyCat = cardsOnBoard[4];
            CardInfo info = CardLoader.GetCardByName(Cards.lyingAdult);

            bool firstTime = !DialogueEventsData.EventIsPlayed("LyingAdultOutro");
            bool canInitiateCombat = LobotomyHelpers.AllowInitiateCombat(false);
            yield return new WaitForSeconds(0.5f);

            if (!SaveManager.SaveFile.IsPart2)
                yield return BoardEffects.EmeraldTableEffects();

            AudioController.Instance.SetLoopVolume(0.5f * (Singleton<GameFlowManager>.Instance as Part1GameFlowManager)?.GameTableLoopVolume ?? 1f, 0.5f);
            AudioController.Instance.SetLoopAndPlay("red_noise", 1);
            AudioController.Instance.SetLoopVolumeImmediate(0.3f, 1);

            yield return DialogueHelper.PlayDialogueEvent("LyingAdultIntro");

            if (opponentCard)
            {
                List<CardSlot> validSlots = BoardManager.Instance.GetSlotsCopy(!opponentCard).FindAll(x => x.Card == null);
                yield return CombatHelpers.CreateCardInRandomSlot(info, validSlots);
            }
            else
            {
                RunState.Run.playerDeck.AddCard(info);
                info.Mods.Add(new() { bloodCostAdjustment = -info.cost });
                Singleton<ViewManager>.Instance.SwitchToView(View.Hand, lockAfter: true);
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(info, null, 0.25f, null);

                LobotomySaveManager.OwnsLyingAdult = true;
            }

            yield return new WaitForSeconds(0.2f);
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultIntro2", 0.4f);

            yield return LookAtCard(ozma); // ozma removal sequence
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultOzma");
            yield return ModifyCard(ozma, new(1, 0));

            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.2f);
            yield return RemoveFromBoardOrHand(ozma);
            yield return new WaitForSeconds(0.5f);

            yield return DialogueHelper.PlayDialogueEvent("LyingAdultOzma2", 0.4f);
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultIntro3", 0.4f);

            // modify friend cards
            yield return LookAtCard(scarecrow);
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultScarecrow");
            yield return ModifyCard(scarecrow, new() { bonesCostAdjustment = -1 });

            yield return LookAtCard(woodsman);
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultWoodsman");
            yield return ModifyCard(woodsman, new(0, 1));

            yield return LookAtCard(scaredyCat);
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultScaredyCat");
            scaredyCat.Anim.StrongNegationEffect();
            if (!LobotomySaveManager.UnlockedLyingAdult)
                scaredyCat.AddTemporaryMod(new(1, 0));

            yield return new WaitForSeconds(0.5f);

            yield return LookAtCard(roadHome);
            yield return DialogueHelper.PlayDialogueEvent("LyingAdultRoadHome");
            yield return ModifyCard(roadHome, new(0, 2));

            yield return DialogueHelper.PlayDialogueEvent("LyingAdultIntro4");
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.2f);

            // Remove cards
            yield return RemoveFriendFromStage(scarecrow, "LyingAdultScarecrow2");
            yield return RemoveFriendFromStage(woodsman, "LyingAdultWoodsman2");
            yield return RemoveFriendFromStage(scaredyCat, "LyingAdultScaredyCat2");
            yield return RemoveFriendFromStage(roadHome, "LyingAdultRoadHome2");

            yield return DialogueHelper.PlayDialogueEvent("LyingAdultOutro");

            LobotomySaveManager.UnlockedLyingAdult = true;

            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            LobotomyHelpers.AllowInitiateCombat(canInitiateCombat);
        }

        private IEnumerator RemoveFriendFromStage(PlayableCard card, string dialogueId)
        {
            yield return DialogueHelper.PlayDialogueEvent(dialogueId, 0f);
            yield return RemoveFromBoardOrHand(card);
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
        private IEnumerator LookAtCard(PlayableCard card)
        {
            Singleton<ViewManager>.Instance.SwitchToView(!card.InHand ? View.Board : View.Default);
            yield return new WaitForSeconds(0.2f);
            if (card.InHand)
                (Singleton<PlayerHand>.Instance as PlayerHand3D)?.MoveCardAboveHand(card);
        }

        public bool RespondsToOtherCardAddedToHand(PlayableCard card)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator OnOtherCardAddedToHand(PlayableCard card)
        {
            throw new System.NotImplementedException();
        }
    }
    public class RulebookEntryYellowBrick : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class Abilities
    {
        private static void Rulebook_YellowBrick()
            => RulebookEntryYellowBrick.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryYellowBrick>(YellowBrick.rName, YellowBrick.rDesc).Id;
        private static void AddSpecial_YellowBrick()
            => YellowBrick.specialAbility = AbilityHelper.CreateSpecialAbility<YellowBrick>(LobotomyPlugin.pluginGuid, YellowBrick.rName).Id;
    }
}
