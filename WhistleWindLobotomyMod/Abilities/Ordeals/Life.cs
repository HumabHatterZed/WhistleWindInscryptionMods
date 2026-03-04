using Core.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddLife() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Life";
            info.rulebookDescription = "When this card is played, create 2 random Doubts or Processes of Understanding on the owner's side of the board. After two turns, return to the queue.";
            info.powerLevel = 4;
            Life.ID = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(Life), TextureLoader.LoadTextureFromFile("sigilLife.png")).Id;
        }
    }
    /// <summary>
    /// When this card is played, create 2 random Doubts or Processes of Understanding on the owner's side of the board. After two turns, return to the queue.
    /// </summary>
    public class Life : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        private Texture life2 = null;
        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            life2 ??= TextureLoader.LoadTextureFromFile("sigilLife_2.png");
            base.Card.RenderInfo.OverrideAbilityIcon(Life.ID, life2);
            base.Card.RenderCard();
            yield return SummonCards();

        }
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            int diff = TurnManager.Instance.TurnNumber - base.Card.TurnPlayed;
            if (diff > 0) {
                base.Card.Slot.Card = null;
                base.Card.Slot = BoardManager.Instance.OpponentSlotsCopy.FindAll(x => !TurnManager.Instance.Opponent.QueuedSlots.Contains(x)).GetSeededRandom(base.GetRandomSeed());
                yield return TurnManager.Instance.Opponent.ReturnCardToQueue(base.Card, 0.2f);
            }
            else {
                base.Card.RenderInfo.OverrideAbilityIcon(Life.ID, AbilityManager.AllAbilities.AbilityByID(ID).Texture);
                base.Card.RenderCard();
            }
            yield return new WaitForSeconds(0.5f);
        }

        public IEnumerator SummonCards() {
            int rand = base.GetRandomSeed();
            AudioController.Instance.PlaySound3D("disk_card_transform", MixerGroup.CardPaperSFX, base.transform.position);
            yield return CombatHelpers.CreateCardInRandomSlot(CardLoader.GetCardByName(GetRandomCardId(rand++)), BoardManager.Instance.GetOpponentOpenSlots());
            yield return CombatHelpers.CreateCardInRandomSlot(CardLoader.GetCardByName(GetRandomCardId(rand++)), BoardManager.Instance.GetOpponentOpenSlots());
            yield return new WaitForSeconds(0.5f);
        }

        private string GetRandomCardId(int randomSeed) {
            //  A   B   Y   O  P
            // .17 .41 .69 .92 1
            // .10 .32 .64 .89 1
            // .03 .23 .59 .86 1
            float randomValue = SeededRandom.Value(randomSeed);
            int extraDifficulty = Mathf.Max(0, TurnManager.Instance.Opponent.Difficulty + base.Card.TurnPlayed - 7);
            if (base.Card.TurnPlayed == 0) {
                if (randomValue <= 0.22f - extraDifficulty * 0.04f) {
                    return Cards.doubtA;
                }
                else if (randomValue <= 0.92f - extraDifficulty * 0.03f) {
                    return Cards.doubtB;
                }
                else {
                    return Cards.doubtY;
                }
            }
            else {
                if (randomValue <= Mathf.Max(0.03f, 0.17f - extraDifficulty * 0.06f)) {
                    return Cards.doubtA;
                }
                else if (randomValue <= Mathf.Max(0.23f, 0.41f - extraDifficulty * 0.07f)) {
                    return Cards.doubtB;
                }
                else if (randomValue <= Mathf.Max(0.59f, 0.69f - extraDifficulty * 0.04f)) {
                    return Cards.doubtY;
                }
                else if (randomValue <= Mathf.Max(0.83f, 0.92f - extraDifficulty * 0.02f)) {
                    return Cards.doubtO;
                }
                else {
                    return Cards.doubtProcess;
                }
            }
        }
    }
}
