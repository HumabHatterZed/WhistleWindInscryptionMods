using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public class ReturnCard : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) => base.Card.Info.IsTargetedSpell() && slot.IsPlayerSlot && slot.Card != null;

        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            CardInfo copy = slot.Card.Info.Clone() as CardInfo;
            // includes status effects
            List<SpecialCardBehaviour> behaviours = slot.Card.GetComponents<SpecialCardBehaviour>()?.ToList() ?? new();
            PlayableCardStatus status = new(slot.Card.Status);
            List<CardModificationInfo> tempMods = slot.Card.TemporaryMods;
            tempMods.Add(new()
            {
                bloodCostAdjustment = -slot.Card.BloodCost(),
                bonesCostAdjustment = GetBonesCost(slot.Card) - slot.Card.BonesCost(),
                energyCostAdjustment = -slot.Card.EnergyCost,
                nullifyGemsCost = true
            });

            slot.Card.RemoveFromBoard(false);
            yield return HelperMethods.ChangeCurrentView(View.Default);
            yield return CardSpawner.Instance.SpawnCardToHand(copy, tempMods, 0.25f, (PlayableCard x) =>
            {
                x.Status = status;
                for (int i = 0; i < behaviours.Count; i++)
                {
                    var copy = CopyComponent(behaviours[i], x.gameObject);
                    x.TriggerHandler.permanentlyAttachedBehaviours.Add(copy);
                }
            });
            yield return new WaitForSeconds(0.2f);
        }
        private static T CopyComponent<T>(T original, GameObject gameObject) where T : SpecialCardBehaviour
        {
            System.Type type = original.GetType();
            Component component = gameObject.AddComponent(type);
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(component, field.GetValue(original));
            }
            return component as T;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            foreach (var slot in BoardManager.Instance.PlayerSlotsCopy.Where(x => x.Card != null))
            {
                CardInfo copy = slot.Card.Info.Clone() as CardInfo;
                PlayableCardStatus status = new(slot.Card.Status);
                List<CardModificationInfo> tempMods = slot.Card.TemporaryMods;
                tempMods.Add(new()
                {
                    bloodCostAdjustment = -slot.Card.BloodCost(),
                    bonesCostAdjustment = GetBonesCost(slot.Card) - slot.Card.BonesCost(),
                    energyCostAdjustment = -slot.Card.EnergyCost,
                    nullifyGemsCost = true
                });

                slot.Card.RemoveFromBoard(false);
                yield return HelperMethods.ChangeCurrentView(View.Default, 0.1f, 0.1f);
                yield return CardSpawner.Instance.SpawnCardToHand(copy, tempMods, 0.25f, (PlayableCard x) => { x.Status = status; });
                yield return new WaitForSeconds(0.2f);
            }
        }
        private int GetBonesCost(PlayableCard card) => Mathf.Max(0, 2 - (TurnManager.Instance.TurnNumber - card.TurnPlayed));
    }

    public partial class AbnormalPlugin
    {
        private void Ability_ReturnCard()
        {
            const string rulebookName = "Return Card to Hand";
            const string rulebookDescription = "Returns the selected card to your hand with all changes and statuses retained. Change the selected card's cost to 0-2 Bones based on how recently it was played.";
            ReturnCard.ability = AbnormalAbilityHelper.CreateAbility<ReturnCard>(
                "sigilReturnCard", rulebookName, rulebookDescription,
                null, powerLevel: 0, canStack: false).Id;
        }
    }
}