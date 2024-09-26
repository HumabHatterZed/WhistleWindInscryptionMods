using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
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
            int val = SeededRandom.Range(0, 6, randomSeed);
            if (SaveManager.SaveFile.IsPart3)
            {
                return val switch
                {
                    0 => "bbp_pastry_act3",
                    1 => "bbp_whiteDonut_act3",
                    2 => "bbp_meetBun_act3",
                    3 => "bbp_scones_act3",
                    4 => "bbp_eggTart_act3",
                    _ => "bbp_redVelvet_act3"
                };
            }
            else
            {
                return val switch
                {
                    0 => "bbp_pastry",
                    1 => "bbp_whiteDonut",
                    2 => "bbp_meetBun",
                    3 => "bbp_scones",
                    4 => "bbp_eggTart",
                    _ => "bbp_redVelvet"
                };
            }
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
            CardInfo cardInfo = SaveManager.SaveFile.IsPart3 ? CardLoader.GetCardByName("bbp_bunnie_act3") : CardLoader.GetCardByName("bbp_bunnie");
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

            Destroy();
        }
    }

}