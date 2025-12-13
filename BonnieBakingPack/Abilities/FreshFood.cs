using DiskCardGame;
using InscryptionAPI.Card;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BonniesBakingPack {
    public partial class BakingPlugin {
        private void AddFreshFood() {
            const string rulebookName = "Fresh Food";
            const string rulebookDescription = "Remove this card from the board and draw Bunnie into your hand next turn.\nWhen this card is drawn, create a random Food in your hand.";
            const string dialogue = "A freshly baked confectionary, made with love and care.";
            const string triggerText = "[creature] books it!";

            FreshFood.ability = AbilityManager.New(pluginGuid, rulebookName, rulebookDescription, typeof(FreshFood), GetTexture("sigilFreshFood.png"))
                .SetAbilityLearnedDialogue(dialogue)
                .SetGBCTriggerText(triggerText)
                .SetPowerlevel(3)
                .SetActivated()
                .SetPixelAbilityIcon(GetTexture("sigilFreshFood_pixel.png"))
                .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook)
                .ability;
        }
    }

    public class FreshFood : ActivatedAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDrawn() => true;
        public override IEnumerator OnDrawn() {
            base.StartCoroutine(SpawnFoodToHoof(this, base.Card));
            return base.OnOtherCardDrawn(base.Card);
        }

        public override IEnumerator Activate() {
            yield return base.PreSuccessfulTriggerSequence();
            yield return PrepareForBunnie(base.Card);
        }

        public static IEnumerator PrepareForBunnie(PlayableCard card) {
            CardSlot slot = card.Slot;
            bool moveLeft = slot.Index < 2;

            card.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.4f);

            ViewManager.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.2f);
            yield return ShimmyCardOutOfHere(card.transform, moveLeft, card.OpponentCard);

            slot.gameObject.AddComponent<CreateBunnieTrigger>().Initialise(card);
            card.UnassignFromSlot();
            slot.StartCoroutine(card.DestroyWhenStackIsClear());
        }

        public static IEnumerator ShimmyCardOutOfHere(Transform transform, bool moveLeft, bool opponentSide) {
            bool satUp = false;
            Tween.LocalPosition(transform,
                new Vector3(transform.localPosition.x, transform.localPosition.y + 1f, transform.localPosition.z),
                0.2f, 0f);

            Tween.LocalRotation(transform, Vector3.zero, 0.2f, 0f, completeCallback: delegate {
                satUp = true;
            });

            yield return new WaitUntil(() => satUp);
            yield return new WaitForSeconds(0.4f);

            Tween.LocalPosition(transform, new(moveLeft ? -20f : 20f, transform.localPosition.y, transform.localPosition.z + (opponentSide ? 0.2f : -0.2f)), 3f, 0f,
                startCallback: delegate {
                    Tween.LocalRotation(transform, Quaternion.Euler(0f, 10f, -15f), 0.1f, 0f);
                    Tween.LocalRotation(transform, Quaternion.Euler(0f, 10f, 15f), 0.1f, 0.1f, loop: Tween.LoopType.PingPong);
                });
        }

        public static IEnumerator SpawnFoodToHoof(AbilityBehaviour behav, PlayableCard card) {
            yield return new WaitUntil(() => PlayerHand.Instance.CardsInHand.Contains(card));
            yield return new WaitForSeconds(0.1f);
            CardInfo info = CardLoader.GetCardByName(GetRandomFoodName(behav.GetRandomSeed()));
            yield return CardSpawner.Instance.SpawnCardToHand(info);
            yield return behav.LearnAbility(0.5f);
        }

        public static string GetRandomFoodName(int randomSeed) {
            List<string> possibleFoodPool = new()
            {
                "bbp_act1_pastry", // give hp
                "bbp_act1_whiteDonut", // bones
                "bbp_act1_meetBun", // blood
                "bbp_act1_scones",
                "bbp_act1_eggTart",
                "bbp_act1_redVelvet", // give atk
                "bbp_act3_pastry", // give hp
                "bbp_act3_n", // gem dust - placeholder, is replaced with a valid name if chosen
                "bbp_act3_meetBun",
                "bbp_act3_scones",
                "bbp_act3_eggTart",
                "bbp_act3_redVelvet", // upgrade/battery spell
                "bbp_grimora_redVelvet", // give atk
                "bbp_grimora_pastry", // give hp
                "bbp_grimora_whiteDonut", // bones
                "bbp_grimora_meetBun",
                "bbp_grimora_scones",
                "bbp_grimora_eggTart",
                "bbp_magnificus_redVelvet", // orange gem
                "bbp_magnificus_pastry", // trap
                "bbp_magnificus_whiteDonut", // blue gem
                "bbp_magnificus_meetBun",
                "bbp_magnificus_scones",
                "bbp_magnificus_eggTart"
            };

            List<CardInfo> playableCards = CardDrawPiles.Instance.Deck.Cards.Concat(PlayerHand.Instance.CardsInHand.Select(x => x.Info)).ToList();

            bool hasBloodCost = playableCards.Exists(x => x.BloodCost > 0);
            bool hasManaCost = BakingPlugin.ScrybeCompat.MagnificusEnabled && playableCards.Exists(BakingPlugin.ScrybeCompat.HasManaCost);

            bool hasGreenGem = playableCards.Exists(x => x.GemsCost.Contains(GemType.Green));
            bool hasBlueGem = playableCards.Exists(x => x.GemsCost.Contains(GemType.Blue));
            bool hasOrangeGem = playableCards.Exists(x => x.GemsCost.Contains(GemType.Orange));

            if (!hasBloodCost) {
                possibleFoodPool.Remove("bbp_act1_meetBun");
            }

            if (!hasManaCost) // if no cards cost mana (sac gems)
            {
                if (!hasGreenGem && !hasBlueGem && !hasOrangeGem) // remove all gem-giving pastries
                {
                    possibleFoodPool.Remove("bbp_act3");
                    possibleFoodPool.Remove("bbp_magnificus_redVelvet");
                    possibleFoodPool.Remove("bbp_magnificus_whiteDonut");
                    possibleFoodPool.Remove("bbp_magnificus_eggTart");
                }
                else // don't give gem-giving patries if their respective colour isn't needed
                {
                    if (!hasGreenGem)
                        possibleFoodPool.Remove("bbp_magnificus_eggTart");

                    if (!hasBlueGem)
                        possibleFoodPool.Remove("bbp_magnificus_whiteDonut");

                    if (!hasOrangeGem)
                        possibleFoodPool.Remove("bbp_magnificus_redVelvet");
                }
            }

            if (!BakingPlugin.SplitByAct.Value) // remove cards from other acts if they aren't allowed
            {
                if (SaveManager.SaveFile.IsPart1) {
                    possibleFoodPool.RemoveAll(x => !x.StartsWith(BakingPlugin.pluginPrefix));
                }
                else if (SaveManager.SaveFile.IsPart3) {
                    possibleFoodPool.RemoveAll(x => !x.StartsWith(BakingPlugin.pluginPrefix3));
                }
                else if (SaveManager.SaveFile.IsGrimora) {
                    possibleFoodPool.RemoveAll(x => !x.StartsWith(BakingPlugin.pluginPrefixG));
                }
                else if (SaveManager.SaveFile.IsMagnificus) {
                    possibleFoodPool.RemoveAll(x => !x.StartsWith(BakingPlugin.pluginPrefixM));
                }
            }

            string chosenFood = possibleFoodPool[SeededRandom.Range(0, possibleFoodPool.Count, randomSeed)];
            if (chosenFood.Equals("bbp_act3_n")) {
                chosenFood = GetRandomNoise(randomSeed);
            }

            return chosenFood;
        }
        private static string GetRandomNoise(int randomSeed) {
            int val = SeededRandom.Range(0, 7, ++randomSeed);

            if (val <= 1) {
                return "bbp_act3_whiteDonut_red";
            }
            if (val <= 3) {
                return "bbp_act3_whiteDonut_blue";
            }
            if (val <= 5) {
                return "bbp_act3_whiteDonut_green";
            }

            return "bbp_act3_whiteDonut";
        }
    }
}