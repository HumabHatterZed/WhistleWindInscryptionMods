using Core.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class Abilities
    {
        private static void AddLife()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Life";
            info.rulebookDescription = "Two turns after this card has been played, return this card to the queue, then create 2-3 Dawns/Noons of Green on the board.";
            info.powerLevel = 4;
            Life.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(Life), TextureLoader.LoadTextureFromFile("sigilLife.png")).Id;
        }
    }

    public class Life : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private int numTimesActivated = 0;
        private Texture life2 = null;
        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            if (base.Card.TurnPlayed > 1) {
                life2 ??= TextureLoader.LoadTextureFromFile("sigilLife_2.png");
                base.Card.RenderInfo.OverrideAbilityIcon(Life.ability, life2);
                base.Card.RenderCard();
            }
            yield break;

        }
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            numTimesActivated++;
            int diff = TurnManager.Instance.TurnNumber - base.Card.TurnPlayed;
            if (diff > 1 || base.Card.TurnPlayed < 2) {
                int rand = base.GetRandomSeed();
                base.Card.Slot.Card = null;
                base.Card.Slot = BoardManager.Instance.OpponentSlotsCopy.FindAll(x => !TurnManager.Instance.Opponent.QueuedSlots.Contains(x)).GetSeededRandom(base.GetRandomSeed());
                AudioController.Instance.PlaySound3D("disk_card_transform", MixerGroup.CardPaperSFX, base.transform.position);
                CustomCoroutine.Instance.StartCoroutine(TurnManager.Instance.Opponent.ReturnCardToQueue(base.Card, 0.2f));
                yield return CombatHelpers.CreateCardInRandomSlot(CardLoader.GetCardByName(GetRandomCardId(rand++)), BoardManager.Instance.GetOpponentOpenSlots());
                yield return CombatHelpers.CreateCardInRandomSlot(CardLoader.GetCardByName(GetRandomCardId(rand++)), BoardManager.Instance.GetOpponentOpenSlots());
                if (AscensionSaveData.Data.GetNumChallengesOfTypeActive(AscensionChallenge.BaseDifficulty) > 1) {
                    yield return CombatHelpers.CreateCardInRandomSlot(CardLoader.GetCardByName(GetRandomCardId(rand++)), BoardManager.Instance.GetOpponentOpenSlots());
                }
                yield return new WaitForSeconds(0.5f);


            }
            else {
                base.Card.RenderInfo.OverrideAbilityIcon(Life.ability, AbilityManager.AllAbilities.AbilityByID(ability).Texture);
                base.Card.RenderCard();
            }
            yield return new WaitForSeconds(0.5f);
        }

        private string GetRandomCardId(int randomSeed)
        {
            //  A   B   Y   O  P
            // .17 .41 .69 .92 1
            // .10 .32 .64 .89 1
            // .03 .23 .59 .86 1
            float randomValue = SeededRandom.Value(randomSeed);
            int extraDifficulty = AscensionSaveData.Data.GetNumChallengesOfTypeActive(AscensionChallenge.BaseDifficulty);
            if (randomValue <= 0.17f - extraDifficulty * 0.07f) {
                return Cards.doubtA;
            }
            else if (randomValue <= 0.41f - extraDifficulty * 0.09f) {
                return Cards.doubtB;
            }
            else if (randomValue <= 0.69f - extraDifficulty * 0.05f) {
                return Cards.doubtY;
            }
            else if (randomValue <= 0.92f - extraDifficulty * 0.03f) {
                return Cards.doubtO;
            }
            else {
                return Cards.doubtProcess;
            }
        }
    }
}
