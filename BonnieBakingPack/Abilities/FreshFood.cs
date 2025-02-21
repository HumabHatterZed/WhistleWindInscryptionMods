using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void AddFreshFood()
        {
            const string rulebookName = "Fresh Food";
            const string rulebookDescription = "Remove this card from the board. At the start of the owner's next turn, a Bunnie is created in your hand.\n\nWhen this card is drawn, create a random Food in your hand.";
            const string dialogue = "A freshly baked confectionary, made with love and care.";
            const string triggerText = "[creature] books it!";

            FreshFood.ability = AbilityManager.New(pluginGuid, rulebookName, rulebookDescription, typeof(FreshFood), GetTexture("sigilFreshFood.png"))
                .SetAbilityLearnedDialogue(dialogue)
                .SetGBCTriggerText(triggerText)
                .SetPowerlevel(3)
                .SetActivated()
                .SetPixelAbilityIcon(GetTexture("sigilFreshFood_pixel.png"))
                .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook)
                .ability;
        }
    }

    public class FreshFood : ActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override IEnumerator Activate()
        {
            bool satUp = false;
            bool moveLeft = base.Card.Slot.Index < 2;

            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.4f);

            ViewManager.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.2f);

            Tween.LocalPosition(base.Card.transform,
                new Vector3(base.Card.transform.localPosition.x, base.Card.transform.localPosition.y + 1f, base.Card.transform.localPosition.z),
                0.2f, 0f);

            Tween.LocalRotation(base.Card.transform, Vector3.zero, 0.2f, 0f, completeCallback: delegate
            {
                satUp = true;
            });

            yield return new WaitUntil(() => satUp);
            base.Card.Slot.gameObject.AddComponent<CreateBunnieTrigger>().Initialise(base.Card);
            yield return new WaitForSeconds(0.4f);
            Tween.LocalPosition(base.Card.transform, new(moveLeft ? -20f : 20f, base.Card.transform.localPosition.y, base.Card.transform.localPosition.z + (base.Card.OpponentCard ? 0.2f : -0.2f)), 3f, 0f,
                startCallback: delegate
                {
                    Tween.LocalRotation(base.Card.transform, Quaternion.Euler(0f, 10f, -15f), 0.1f, 0f);
                    Tween.LocalRotation(base.Card.transform, Quaternion.Euler(0f, 10f, 15f), 0.1f, 0.1f, loop: Tween.LoopType.PingPong);
                });

            base.Card.UnassignFromSlot();
            base.StartCoroutine(base.Card.DestroyWhenStackIsClear());
        }

        public override bool RespondsToDrawn() => false;
        public override IEnumerator OnDrawn()
        {
            yield return new WaitForSeconds(0.1f);
            CardInfo info = CardLoader.GetCardByName(GetRandomFoodName(base.GetRandomSeed()));
            yield return CardSpawner.Instance.SpawnCardToHand(info);
            yield return base.LearnAbility(0.5f);
        }
        public static string GetRandomFoodName(int randomSeed)
        {
            int val = 0;
            List<string> possibleFoodPool = new()
            {
                "bbp_act1_pastry",
                "bbp_act1_whiteDonut",
                "bbp_act1_meetBun",
                "bbp_act1_scones",
                "bbp_act1_eggTart",
                "bbp_act1_redVelvet",
                "bbp_act3_pastry",
                "bbp_act3", // placeholder, will be replaced with a valid name if chosen
                "bbp_act3_meetBun",
                "bbp_act3_scones",
                "bbp_act3_eggTart",
                "bbp_act3_redVelvet",
                "bbp_grimora_redVelvet",
                "bbp_grimora_pastry",
                "bbp_grimora_whiteDonut",
                "bbp_grimora_meetBun",
                "bbp_grimora_scones",
                "bbp_grimora_eggTart",
                "bbp_magnificus_redVelvet",
                "bbp_magnificus_pastry",
                "bbp_magnificus_whiteDonut",
                "bbp_magnificus_meetBun",
                "bbp_magnificus_scones",
                "bbp_magnificus_eggTart"
            };

            if (!BakingPlugin.SplitByAct.Value)
            {
                if (SaveManager.SaveFile.IsPart1)
                {
                    possibleFoodPool.RemoveAll(x => !x.StartsWith("bbp_act1"));
                }
                else if (SaveManager.SaveFile.IsPart3)
                {
                    possibleFoodPool.RemoveAll(x => !x.StartsWith("bbp_act3"));
                }
                else if (SaveManager.SaveFile.IsGrimora)
                {
                    possibleFoodPool.RemoveAll(x => !x.StartsWith("bbp_grimora"));
                }
            }

            // remove cards that are useless to the player every now and then
            // makes this ability a tad more consistent in utility
            if (SeededRandom.Bool(++randomSeed))
            {
                List<CardInfo> deck = CardDrawPiles.Instance.Deck.Cards.Concat(PlayerHand.Instance.CardsInHand.Select(x => x.Info)).ToList();

                if (!deck.Exists(x => x.GemsCost.Count > 0))
                {
                    possibleFoodPool.Remove("bbp_act3");
                    possibleFoodPool.Remove("bbp_magnificus_redVelvet");
                    possibleFoodPool.Remove("bbp_magnificus_whiteDonut");
                    possibleFoodPool.Remove("bbp_magnificus_eggTart");
                }
                else if (SaveManager.SaveFile.IsMagnificus || BakingPlugin.SplitByAct.Value)
                {
                    if (deck.Select(x => x.GemsCost?.Count(x => x == GemType.Orange) ?? 0).Sum() == 0)
                    {
                        possibleFoodPool.Remove("bbp_magnificus_redVelvet");
                    }
                    if (deck.Select(x => x.GemsCost?.Count(x => x == GemType.Blue) ?? 0).Sum() == 0)
                    {
                        possibleFoodPool.Remove("bbp_magnificus_whiteDonut");
                    }
                    if (deck.Select(x => x.GemsCost?.Count(x => x == GemType.Green) ?? 0).Sum() == 0)
                    {
                        possibleFoodPool.Remove("bbp_magnificus_eggTart");
                    }
                }

                if (!deck.Exists(x => x.BloodCost > 0))
                {
                    possibleFoodPool.Remove("bbp_act1_meetBun");
                }
            }


            string chosenFood = possibleFoodPool[SeededRandom.Range(0, possibleFoodPool.Count, randomSeed)];
            if (chosenFood.Equals("bbp_act3"))
            {
                chosenFood = GetRandomNoise(randomSeed);
            }

            return chosenFood;
        }
        private static string GetRandomNoise(int randomSeed)
        {
            int val = SeededRandom.Range(0, 7, ++randomSeed);

            if (val <= 1)
            {
                return "bbp_act3_whiteDonut_red";
            }
            if (val <= 3)
            {
                return "bbp_act3_whiteDonut_blue";
            }
            if (val <= 5)
            {
                return "bbp_act3_whiteDonut_green";
            }

            return "bbp_act3_whiteDonut";
        }
    }
    public class CreateBunnieTrigger : NonCardTriggerReceiver
    {
        private bool opponent;
        private int turnCreated;
        private List<CardModificationInfo> cardmods;

        public override bool TriggerBeforeCards => true;
        public void Initialise(PlayableCard parent)
        {
            opponent = parent.OpponentCard;
            cardmods = new(parent.Info.Mods);
            turnCreated = TurnManager.Instance.TurnNumber;
        }
        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return playerUpkeep != opponent && TurnManager.Instance.TurnNumber > turnCreated;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            string infoName;
            if (SaveManager.SaveFile.IsPart3)
            {
                infoName = "bbp_act3_bunnie";
            }
            else if (SaveManager.SaveFile.IsGrimora)
            {
                infoName = "bbp_grimora_bunnie";
            }
            else
            {
                infoName = "bbp_act1_bunnie";
            }

            CardInfo cardInfo = CardLoader.GetCardByName(infoName);
            cardInfo.Mods = cardmods;
            if (opponent)
            {
                CardSlot queue = BoardManager.Instance.GetOpenSlots(false).FirstOrDefault();
                if (queue == null)
                {
                    queue = BoardManager.Instance.OpponentSlotsCopy[SeededRandom.Range(0, BoardManager.Instance.OpponentSlotsCopy.Count, base.GetRandomSeed())];
                    PlayableCard card = TurnManager.Instance.Opponent.Queue.Find(x => x.QueuedSlot == queue);
                    card.ExitBoard(0.2f, new Vector3(-1f, -2f, 5f));
                    TurnManager.Instance.Opponent.Queue.Remove(card);
                }
                yield return TurnManager.Instance.Opponent.QueueCard(cardInfo, queue);
            }
            else
            {
                ViewManager.Instance.SwitchToView(View.Default);
                yield return new WaitForSeconds(0.2f);
                yield return CardSpawner.Instance.SpawnCardToHand(cardInfo);
            }

            if (infoName.Equals("bbp_grimora_bunnie") && !ProgressionData.IntroducedCard(cardInfo))
            {
                yield return new WaitForSeconds(0.2f);
                yield return TextDisplayer.Instance.ShowThenClear(cardInfo.description, 3f);
                ProgressionData.SetCardIntroduced(cardInfo);

            }
            Destroy();
        }
    }

}