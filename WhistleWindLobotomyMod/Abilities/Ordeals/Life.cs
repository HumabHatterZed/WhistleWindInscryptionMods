using Core.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            info.rulebookDescription = "When this card is played, create two random Dawn of Greens on the board. Two turns after being played, return to the queue.";
            info.powerLevel = 4;
            Life.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(Life), TextureLoader.LoadTextureFromFile("sigilLife.png")).Id;
        }
    }

    public class Life : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            int rand = base.GetRandomSeed();
            yield return CombatHelpers.CreateCardInRandomSlot(CardLoader.GetCardByName(GetRandomCardId(rand++)), BoardManager.Instance.GetOpponentOpenSlots());
            yield return CombatHelpers.CreateCardInRandomSlot(CardLoader.GetCardByName(GetRandomCardId(rand++)), BoardManager.Instance.GetOpponentOpenSlots());
        }
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep && TurnManager.Instance.TurnNumber > base.Card.TurnPlayed + 2;
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            base.Card.Slot.Card = null;
            base.Card.Slot = BoardManager.Instance.OpponentSlotsCopy.FindAll(x => !TurnManager.Instance.Opponent.QueuedSlots.Contains(x)).GetSeededRandom(base.GetRandomSeed());
            yield return TurnManager.Instance.Opponent.ReturnCardToQueue(base.Card, 0.2f);
        }
        private string GetRandomCardId(int randomSeed)
        {
            // .48 .66 .80 .92 1
            // .41 .57 .75 .89 1
            // .34 .48 .70 .86 1
            float randomValue = SeededRandom.Value(randomSeed);
            int extraDifficulty = AscensionSaveData.Data.GetNumChallengesOfTypeActive(AscensionChallenge.BaseDifficulty);
            if (randomValue <= Mathf.Max(0.15f, 0.55f - extraDifficulty * 0.07f))
            {
                return Cards.doubtA;
            }
            else if (randomValue <= Mathf.Max(0.25f, 0.75f - extraDifficulty * 0.09f))
            {
                return Cards.doubtB;
            }
            else if (randomValue <= Mathf.Max(0.45f - extraDifficulty * 0.05f))
            {
                return Cards.doubtY;
            }
            else if (randomValue <= Mathf.Max(0.75f, 0.95f - extraDifficulty * 0.03f))
            {
                return Cards.doubtO;
            }
            else
            {
                return Cards.doubtProcess;
            }
        }
    }
}
