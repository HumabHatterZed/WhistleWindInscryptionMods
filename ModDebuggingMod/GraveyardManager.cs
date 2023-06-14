/*using DiskCardGame;
using HarmonyLib;
using MonoMod.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModDebuggingMod
{
    public class GraveyardManager : Singleton<GraveyardManager>
    {
        // dictionary containing card infos and whether they died via sacrifice or not
        //public readonly Dictionary<CardInfo, bool> deadCards = new();

        public readonly Dictionary<CardInfo, bool> playerCards = new();
        public readonly Dictionary<CardInfo, bool> opponentCards = new();
        public readonly Dictionary<CardInfo, bool> allDeadCards = new();

        public Dictionary<CardInfo, bool> AllDeadCards
        {
            get
            {
                allDeadCards.Clear();
                allDeadCards.AddRange(playerCards);
                allDeadCards.AddRange(opponentCards);
                return allDeadCards;
            }
        }
        public bool Exhausted => AllDeadCards.Count == 0;

        public int randomSeed;
        public void Initialise()
        {
            randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
        }
        public void CleanUp()
        {
            playerCards.Clear();
            opponentCards.Clear();
            allDeadCards.Clear();
        }

        public IEnumerator DrawCard(CardInfo specificCard = null, DrawType poolToDrawnFrom = DrawType.Player)
        {
            Dictionary<CardInfo, bool> cardPool = GetCardDrawPool(poolToDrawnFrom);
            yield return DrawCard(specificCard, cardPool);
        }
        public IEnumerator DrawCard(Ability withAbility, DrawType poolToDrawnFrom = DrawType.Player)
        {
            Dictionary<CardInfo, bool> cardPool = GetCardDrawPool(poolToDrawnFrom);
            CardInfo cardToDraw = cardPool.FirstOrDefault(x => x.Key.HasAbility(withAbility)).Key;
            if (cardToDraw == null)
            {
                Debug.Log($"No card with ability:{withAbility} found in pool");
                yield break;
            }
            yield return DrawCard(cardToDraw, cardPool);
        }
        public IEnumerator DrawCard(bool wasSacrifice, DrawType poolToDrawnFrom = DrawType.Player)
        {
            Dictionary<CardInfo, bool> cardPool = GetCardDrawPool(poolToDrawnFrom);
            CardInfo cardToDraw = cardPool.FirstOrDefault(x => x.Value == wasSacrifice).Key;
            if (cardToDraw == null)
            {
                Debug.Log($"No card with wasSacrifice:{wasSacrifice} found in pool");
                yield break;
            }
            yield return DrawCard(cardToDraw, cardPool);
        }
        private IEnumerator DrawCard(CardInfo specifiedCard, Dictionary<CardInfo, bool> cardPool)
        {
            if (Exhausted)
            {
                Debug.Log($"No cards/card does not exist");
                yield break;
            }
            CardInfo cardToDraw = specifiedCard ?? cardPool.Keys.ToList()[SeededRandom.Range(0, cardPool.Count, randomSeed++)];
            cardPool.Remove(cardToDraw);
            yield return CardSpawner.Instance.SpawnCardToHand(cardToDraw, 0.15f);
        }

        private Dictionary<CardInfo, bool> GetCardDrawPool(DrawType poolToDrawnFrom)
        {
            return poolToDrawnFrom switch
            {
                DrawType.Player => playerCards,
                DrawType.Opponent => opponentCards,
                _ => allDeadCards
            };
        }
        public enum DrawType
        {
            Player,
            Opponent,
            All
        }
    }
    [HarmonyPatch]
    internal static class DeadCardPilePatches
    {
        [HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.Die))]
        [HarmonyPrefix]
        private static void AddDeadCardToPile(PlayableCard __instance, bool wasSacrifice)
        {
            CardInfo deadInfo;
            if (__instance.Info.name == "Ant Queen")
            {
                deadInfo = CardLoader.GetCardByName("wstlcard");
            }
            else
            {
                deadInfo = __instance.Info.Clone() as CardInfo;
                deadInfo.Mods = new(__instance.Info.Mods);
            }
            (__instance.OpponentCard ? GraveyardManager.Instance.opponentCards : GraveyardManager.Instance.playerCards).Add(deadInfo, wasSacrifice);
        }

        [HarmonyPatch(typeof(BoardManager3D), nameof(BoardManager3D.Initialize))]
        [HarmonyPrefix]
        private static void AddDeadCardPile(BoardManager3D __instance)
        {
            // shorthand condensing a null check (the ??) with a method call; AddComponent returns the added component instance btw
            (GraveyardManager.Instance ?? __instance.gameObject.AddComponent<GraveyardManager>()).Initialise();
        }

        [HarmonyPatch(typeof(BoardManager3D), nameof(BoardManager3D.CleanUp))]
        [HarmonyPrefix]
        private static void ClearDeadCardPile()
        {
            GraveyardManager.Instance.CleanUp();
        }
    }
}
*/