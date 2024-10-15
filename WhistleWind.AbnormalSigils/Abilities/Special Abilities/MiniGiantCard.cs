using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

namespace WhistleWind.AbnormalSigils
{
    // for triggering special card behaviour in Healer
    [HarmonyPatch]
    public class MiniGiantCard : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility Id;
        public SpecialTriggeredAbility SpecialAbility => Id;

        private const int _max = int.MaxValue;

        private CardSlot secondSlot = null;
        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard()
        {
            secondSlot = base.PlayableCard.Slot.GetAdjacent(false) ?? base.PlayableCard.Slot.GetAdjacent(true);
            if (secondSlot != null)
            {
                secondSlot.Card = base.PlayableCard;
            }
            yield break;
        }
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            secondSlot.Card = null;
            yield break;
        }
        public override int Priority => _max;
    }

    [HarmonyPatch]
    internal class MiniGiantPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(CardSpawner), nameof(CardSpawner.SpawnPlayableCard))]
        private static void ResizeMiniGiantCard(CardInfo info, PlayableCard __result)
        {
            if (!info.HasSpecialAbility(MiniGiantCard.Id))
                return;

            Transform child = SaveManager.SaveFile.IsGrimora ? __result.transform.GetChild(0) : __result.transform;
            if (info.HasTrait(Trait.Giant))
            {
                if (SaveManager.SaveFile.IsGrimora)
                    child.localPosition = new Vector3(0.3f, 0f, 0f);
                else
                    child.localPosition = new Vector3(-0.3f, 0.025f, 0f);

                child.localScale = new Vector3(0.485f, 1f, 1f);
            }
            else
            {
                if (SaveManager.SaveFile.IsGrimora)
                    child.localPosition = new Vector3(-0.7f, 1.05f, 0f);
                else
                    child.localPosition = new Vector3(0.7f, 0.025f, 1.05f);

                child.localScale = new Vector3(2.1f, 2.1f, 1f);
            }
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(BoardManager), nameof(BoardManager.AssignCardToSlot), MethodType.Enumerator)]
        private static IEnumerable<CodeInstruction> ChangeFinalLocalPosition(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new(instructions);

            int addCode = codes.FindIndex(x => x.opcode == OpCodes.Add);
            if (addCode != -1)
            {
                int start = addCode - 6, end = addCode + 2;
                codes.RemoveRange(start, end - start);
                codes.Insert(start++, new(OpCodes.Ldloc_1));
                codes.Insert(start++, new(OpCodes.Ldarg_0));
                codes.Insert(start++, new(OpCodes.Ldfld, codes.Find(x => x.opcode == OpCodes.Ldfld && x.operand.ToString() == "DiskCardGame.PlayableCard card").operand));
                codes.Insert(start++, new(OpCodes.Call,
                    AccessTools.Method(typeof(MiniGiantPatches), nameof(MiniGiantPatches.ModifyFinalLocalPosition),
                    new Type[] { typeof(BoardManager), typeof(PlayableCard) }
                )));
            }

            return codes;
        }

        private static Vector3 ModifyFinalLocalPosition(BoardManager instance, PlayableCard card)
        {
            if (card.Info.HasSpecialAbility(MiniGiantCard.Id))
            {
                if (card.HasTrait(Trait.Giant))
                {
                    if (SaveManager.SaveFile.IsGrimora)
                        return new Vector3(0.3f, 0f, 0f);
                    else
                        return new Vector3(-0.3f, 0.025f, 0f);
                }
                else
                {
                    if (SaveManager.SaveFile.IsGrimora)
                        return new Vector3(-0.7f, 1.05f, 0f);
                    else
                        return new Vector3(0.7f, 0.025f, 1.05f);
                }
            }
            return Vector3.up * (instance.SlotHeightOffset + card.SlotHeightOffset);
        }

        [HarmonyPrefix, HarmonyPatch(typeof(CardAbilityIcons), nameof(CardAbilityIcons.UpdateAbilityIcons))]
        private static void FixAbilityIcons(CardAbilityIcons __instance, CardInfo info)
        {
            if (info.LacksSpecialAbility(MiniGiantCard.Id) || info.LacksTrait(Trait.Giant))
                return;

            //__instance.transform.localScale = new(1f, 0.485f, 1f);
            Transform fourGroup = __instance.transform.Find("DefaultIcons_4Abilities");

            Transform child1 = fourGroup.GetChild(0);
            child1.localPosition = new(-0.12f, 0.16f, 0f);
            child1.GetComponent<BoxCollider>().size = new(1.25f, 0.6f, 1f);
            //child1.localScale = new(0.2f, 0.2f, 1f);
            Transform child2 = fourGroup.GetChild(1);
            child2.localPosition = new(0.12f, 0.16f, 0f);
            //child2.localScale = new(0.2f, 0.2f, 1f);
            Transform child3 = fourGroup.GetChild(2);
            child3.localPosition = new(-0.12f, -0.135f, 0f);
            //child3.localScale = new(0.2f, 0.2f, 1f);
            Transform child4 = fourGroup.GetChild(3);
            child4.localPosition = new(0.12f, -0.135f, 0f);
            //child4.localScale = new(0.2f, 0.2f, 1f);
        }
    }

    public partial class AbnormalPlugin
    {
        private void SpecialAbility_MiniGiant()
        {
            MiniGiantCard.Id = SpecialTriggeredAbilityManager.Add(pluginGuid, "MiniGiantCard", typeof(MiniGiantCard)).Id;
        }
    }
}
