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

            if (!hasBloodCost)
            {
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
                if (SaveManager.SaveFile.IsPart1)
                {
                    possibleFoodPool.RemoveAll(x => !x.StartsWith(BakingPlugin.pluginPrefix));
                }
                else if (SaveManager.SaveFile.IsPart3)
                {
                    possibleFoodPool.RemoveAll(x => !x.StartsWith(BakingPlugin.pluginPrefix3));
                }
                else if (SaveManager.SaveFile.IsGrimora)
                {
                    possibleFoodPool.RemoveAll(x => !x.StartsWith(BakingPlugin.pluginPrefixG));
                }
                else if (SaveManager.SaveFile.IsMagnificus)
                {
                    possibleFoodPool.RemoveAll(x => !x.StartsWith(BakingPlugin.pluginPrefixM));
                }
            }

            string chosenFood = possibleFoodPool[SeededRandom.Range(0, possibleFoodPool.Count, randomSeed)];
            if (chosenFood.Equals("bbp_act3_n"))
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
}