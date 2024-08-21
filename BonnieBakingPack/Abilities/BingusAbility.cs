using DiskCardGame;
using InscryptionAPI.Triggers;
using Pixelplacement;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static InscryptionAPI.Slots.SlotModificationManager;

namespace BonniesBakingPack
{
    public class BingusAbility : SpecialCardBehaviour, IGetOpposingSlots
    {
        public static SpecialTriggeredAbility SpecialAbility;
        private bool finishedCoroutine = false;
        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard()
        {
            SpewOutWeights(base.PlayableCard.Slot, Random.RandomRangeInt(0, 7));
            Tween.LocalScale(base.PlayableCard.transform, new(Random.Range(0.3f, 1.7f), Random.Range(0.3f, 1.7f), Random.Range(0.3f, 1.7f)), Random.Range(0f, 1f), 0f, loop: Tween.LoopType.PingPong);
            Tween.LocalRotation(base.PlayableCard.transform, Random.rotation, Random.Range(0f, 1f), 0f, loop: Tween.LoopType.Loop);
            yield break;
        }
        private void SpewOutWeights(CardSlot slot, int numWeights)
        {
            if (numWeights == 0)
                return;

            List<Transform> newWeights = new();
            for (int i = 0; i < Mathf.Min(20, numWeights); i++)
            {
                GameObject gameObject = Object.Instantiate(Singleton<CombatPhaseManager3D>.Instance.weightPrefab);
                Vector3 vector = new(0f, 0f, slot.IsPlayerSlot ? 0.75f : (-0.75f));
                gameObject.transform.position = slot.transform.position + vector + new Vector3((float)i * 0.1f, 0f, (float)i * 0.1f);
                gameObject.transform.eulerAngles = Random.insideUnitSphere;
                newWeights.Add(gameObject.transform);
            }
            Singleton<CombatPhaseManager3D>.Instance.damageWeights.AddRange(newWeights);
            Singleton<TableVisualEffectsManager>.Instance?.ThumpTable(0.075f * (float)Mathf.Min(10, numWeights));
            foreach (Transform item in newWeights)
            {
                if (item != null)
                {
                    item.gameObject.SetActive(value: true);
                    item.GetComponent<Rigidbody>().AddForce(Vector3.up * 4f, ForceMode.VelocityChange);
                }
            }
        }

        public override bool RespondsToPlayFromHand() => true;
        public override IEnumerator OnPlayFromHand()
        {
            SaveManager.SaveFile.CurrentDeck.RemoveCardByName("bbp_bingus");
            if (!BakingPlugin.BingusCrash.Value)
            {
                base.StartCoroutine(BingusAllOverThePlace(true));
                float lastSave = SaveManager.lastSaveTime;
                ProgressionData.Data.introducedCards.Remove(base.PlayableCard.Info.name);
                SaveManager.SaveToFile(false);
                base.StartCoroutine(CrashGame(lastSave));
                yield return TextDisplayer.Instance.ShowThenClear("You shouldn't have done that.", 5f);
                yield return new WaitUntil(() => finishedCoroutine);
                yield break;
            }
            yield return BingusAllOverThePlace(false);
            yield return TextDisplayer.Instance.ShowUntilInput("You can't escape Bingus.");
        }

        private IEnumerator CrashGame(float lastSave)
        {
            BakingPlugin.BingusCrash.Value = true;
            BakingPlugin.Configs.Save();
            yield return new WaitForSeconds(2f);
            yield return new WaitUntil(() => lastSave != SaveManager.lastSaveTime);
            BakingPlugin.Log.LogError("BingusReferenceException: Object reference not set to an instance of a non-bingus object\nStack trace:\nBonniesBakingPack.bingus+<IAmAHacker>d__5.MoveNext :3 (at <7ec68bbingus44is17coming4to31yourhouse322e>:0)\nUnityEngine.DoxPlayerReal.InvokeMoveOut (System.Collections.IEnumerator enumerator, System.IntPtr playersHomeAddress) (at <3f8c3579heres23bingus9afcaaf82e>:0)");
            Application.Quit();
        }

        private IEnumerator BingusAllOverThePlace(bool crashingTheGame)
        {
            AudioController.Instance.PlaySound2D("broken_hum");
            Singleton<CameraEffects>.Instance.Shake(0.1f, 1f);
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenGlitchEffect>().SetIntensity(1f, 1f);

            if (crashingTheGame)
            {
                yield return BingusTheBoard(true);
                yield break;
            }
            int maxProbability = 1;
            List<IEnumerator> list = new()
            {
                BingusTheScales(),
                BingusTheBoard(),
                BingusTheHand(),
                BingusTheDeck()
            };
            list.Randomize();

            for (int i = 0; i < list.Count; i++)
            {
                if (Random.RandomRangeInt(0, maxProbability) == 0)
                {
                    yield return list[i];
                }
                else
                {
                    maxProbability += 2;
                }
            }
            finishedCoroutine = true;
        }

        private IEnumerator BingusTheBoard(bool crashing = false)
        {
            ViewManager.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.2f);

            foreach (CardSlot slot in BoardManager.Instance.AllSlotsCopy.Where(x => x != base.PlayableCard.Slot))
            {
                if (crashing || Random.RandomRangeInt(0, 3) == 0)
                {
                    if (slot.Card != null)
                    {
                        PlayableCard card = slot.Card;
                        card.Anim.PlayDeathAnimation(false);
                        card.UnassignFromSlot();
                        card.StartCoroutine(card.DestroyWhenStackIsClear());
                    }
                    yield return BoardManager.Instance.CreateCardInSlot(CardLoader.GetCardByName("bbp_bingus"), slot, 0f);
                }
            }
        }
        private IEnumerator BingusTheHand()
        {
            List<CardInfo> removedInfos = new();
            int cardsInHand = PlayerHand.Instance.CardsInHand.Count;
            List<PlayableCard> cards = new(PlayerHand.Instance.CardsInHand);
            foreach (PlayableCard card in cards)
            {
                if (Random.RandomRangeInt(0, 3) == 0)
                {
                    removedInfos.Add(card.Info.Clone() as CardInfo);
                    PlayerHand.Instance.RemoveCardFromHand(card);
                    yield return new WaitForSeconds(0.04f);
                }

                for (int i = 0; i < cardsInHand; i++)
                {
                    if (Random.RandomRangeInt(0, cardsInHand / 2 - i) == 0)
                    {
                        break;
                    }

                    CardInfo info = CardLoader.GetCardByName("bbp_bingus");
                    if (removedInfos.Count > 0)
                    {
                        info.Mods = removedInfos[0].Mods;
                        info.Mods.Add(new() { nameReplacement = removedInfos[0].DisplayedNameLocalized + " Bingus" });
                        removedInfos.RemoveAt(0);
                    }
                    yield return CardSpawner.Instance.SpawnCardToHand(info, 0.05f);
                    PlayerHand.Instance.cardsInHand.Randomize();
                }
            }
        }
        private IEnumerator BingusTheDeck()
        {
            for (int i = 0; i < CardDrawPiles3D.Instance.Deck.CardsInDeck; i++)
            {
                if (Random.RandomRangeInt(0, 3) == 0)
                {
                    CardInfo newInfo = CardLoader.GetCardByName("bbp_bingus");
                    newInfo.Mods = new(CardDrawPiles3D.Instance.Deck.cards[i].Mods)
                    {
                        new() { nameReplacement = CardDrawPiles3D.Instance.Deck.cards[i].DisplayedNameLocalized + " Bingus" }
                    };
                    CardDrawPiles3D.Instance.Deck.cards[i] = newInfo;
                }
            }
            for (int i = 0; i < CardDrawPiles3D.Instance.SideDeck.CardsInDeck; i++)
            {
                if (Random.RandomRangeInt(0, 3) == 0)
                {
                    CardInfo newInfo = CardLoader.GetCardByName("bbp_bingus");
                    newInfo.Mods = new(CardDrawPiles3D.Instance.SideDeck.cards[i].Mods)
                    {
                        new() { nameReplacement = CardDrawPiles3D.Instance.SideDeck.cards[i].DisplayedNameLocalized + " Bingus" }
                    };
                    CardDrawPiles3D.Instance.SideDeck.cards[i] = newInfo;
                }
            }
            yield break;
        }
        private IEnumerator BingusTheScales()
        {
            yield return InvertScales();
            for (int i = 0; i < 1 + Random.RandomRangeInt(0, 3); i++)
            {
                int rand = Random.RandomRangeInt(0, 3);
                if (rand > 0)
                {
                    yield return Singleton<LifeManager>.Instance.ShowDamageSequence(rand, 1, toPlayer: Random.value <= 0.5f);
                    yield return new WaitForSeconds(0.11f);
                }
                yield return InvertScales();
                yield return new WaitForSeconds(0.13f);
            }
            yield return new WaitForSeconds(0.5f);
        }
        private IEnumerator InvertScales()
        {
            int balance = Singleton<LifeManager>.Instance.Balance * -2;
            if (balance == 0)
                yield break;

            int damageToDeal = Mathf.Abs(balance);
            Singleton<CombatPhaseManager>.Instance.DamageDealtThisPhase = damageToDeal;
            yield return Singleton<LifeManager>.Instance.ShowDamageSequence(damageToDeal, damageToDeal * 7, toPlayer: balance < 0);
        }

        #region Prevent Attacking
        public bool RespondsToGetOpposingSlots() => true;
        public List<CardSlot> GetOpposingSlots(List<CardSlot> originalSlots, List<CardSlot> otherAddedSlots) => new();
        public bool RemoveDefaultAttackSlot() => true;
        #endregion
    }

    public class BingusStatIcon : VariableStatBehaviour
    {
        public static SpecialStatIcon Icon;
        public override SpecialStatIcon IconType => Icon;

        public override int[] GetStatValues()
        {
            return new int[] { int.MaxValue / 4, int.MaxValue / 4 };
        }
    }
}
