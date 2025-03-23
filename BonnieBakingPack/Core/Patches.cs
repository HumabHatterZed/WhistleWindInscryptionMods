using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BonniesBakingPack
{
    [HarmonyPatch]
    public static class Patches
    {
        [HarmonyPrefix, HarmonyPatch(typeof(AudioController), nameof(AudioController.GetAudioClip))]
        private static void AddAudioClips(AudioController __instance) => __instance.SFX.AddRange(BakingPlugin.AudioClips.Where(x => !__instance.SFX.Contains(x)));

        [HarmonyPostfix, HarmonyPatch(typeof(WizardBattlePortraitSlot), nameof(WizardBattlePortraitSlot.RespondsToOtherCardResolve))]
        private static void PreventPortraitureForBonnie(ref bool __result, PlayableCard otherCard)
        {
            if (__result && otherCard.Slot.GetComponent<CreateBunnieTrigger>() != null)
            {
                __result = false;
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(DrawRandomCardOnDeath), nameof(DrawRandomCardOnDeath.CardToDraw), MethodType.Getter)]
        private static void PhoneMouseCallsThePopo(DrawRandomCardOnDeath __instance, ref CardInfo __result)
        {
            if (__instance.Card?.Info.name == "bbp_act1_mousePhone")
                __result = CardLoader.GetCardByName("bbp_act1_policeWolf");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CardDisplayer3D), nameof(CardDisplayer3D.DisplaySpecialStatIcons))]
        private static void BingusIsInfinite(CardDisplayer3D __instance, PlayableCard playableCard)
        {
            if (__instance.info.SpecialStatIcon == BingusStatIcon.Icon)
            {
                __instance.SetHealthAndAttackIconsActive(true, true);
                __instance.StatIcons.AssignStatIcon(BingusStatIcon.Icon, playableCard);
                __instance.StatIcons.SetInteractionEnabled(false);
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.CanBeSacrificed), MethodType.Getter)]
        private static void NoSacForBingus(PlayableCard __instance, ref bool __result)
        {
            if (__instance.Info.SpecialStatIcon == BingusStatIcon.Icon)
                __result = false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CardMergeSequencer), nameof(CardMergeSequencer.GetValidCardsForHost))]
        [HarmonyPatch(typeof(CardMergeSequencer), nameof(CardMergeSequencer.GetValidCardsForSacrifice))]
        private static void RemoveSpecialCards(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.name.StartsWith("bbp") && x.onePerDeck);
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlayerHand), nameof(PlayerHand.PlayCardOnSlot))]
        private static IEnumerator RemoveBingusInHand(IEnumerator enumerator, PlayerHand __instance, PlayableCard card, CardSlot slot)
        {
            if (card.Info.SpecialStatIcon != BingusStatIcon.Icon)
            {
                yield return enumerator;
            }
            else
            {
                if (__instance.CardsInHand.Contains(card))
                {
                    __instance.RemoveCardFromHand(card);
                    yield return card.TriggerHandler.OnTrigger(Trigger.PlayFromHand);
                }
                card.Anim.PlayDeathAnimation(false);
                __instance.StartCoroutine(card.DestroyWhenStackIsClear());
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(GravestoneRenderStatsLayer), nameof(GravestoneRenderStatsLayer.RenderCard))]
        private static void AkaMousoEmission(GravestoneRenderStatsLayer __instance, ref CardRenderInfo info)
        {
            if (info.baseInfo.name == "bbp_grimora_akaMouso")
            {
                __instance.SetEmissionColor(new UnityEngine.Color(1f, 0f, 0f));
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(DeckInfo), nameof(DeckInfo.AddCard))]
        private static void AddNineMod(CardInfo card)
        {
            if (card.name == "bbp_grimora_nine" && !card.Mods.Exists(x => x.singletonId == NineAbility.NINE_LIVES_ID))
                card.Mods.Add(new() { singletonId = NineAbility.NINE_LIVES_ID });
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ActivatedDealDamage), nameof(ActivatedDealDamage.Activate))]
        private static IEnumerator PandaShootOnActivatedDamage(IEnumerator enumerator, ActivatedDealDamage __instance)
        {
            bool panda = __instance.Card.HasSpecialAbility(PandaAbility.SpecialAbility);
            if (panda) __instance.Card.SwitchToAlternatePortrait();
            yield return enumerator;
            if (panda) __instance.Card.SwitchToDefaultPortrait();
        }

        //[HarmonyPostfix, HarmonyPatch(typeof(PlayerHand), nameof(PlayerHand.AddCardToHand))]
        //private static IEnumerator DrawFreshFoodAfterPositioning(IEnumerator enumerator, PlayableCard card)
        //{
        //    yield return enumerator;
        //    if (card.HasAbility(FreshFood.ability) || card.HasAbility(FreshFoodMagnificus.ability))
        //    {
        //        BakingPlugin.Log.LogDebug("FreshFood");
        //        AbilityBehaviour trigger = card.TriggerHandler.triggeredAbilities.FirstOrDefault(x => x.Item1 == FreshFood.ability || x.Item1 == FreshFoodMagnificus.ability).Item2;
        //        if (trigger != null)
        //        {
        //            BakingPlugin.Log.LogDebug("FreshFood OnDrawn");
        //            yield return trigger.OnDrawn();
        //        }
        //    }
        //}

        [HarmonyPrefix, HarmonyPatch(typeof(CardChoicesSequencer), nameof(CardChoicesSequencer.ExamineCardWithDialogue))]
        private static bool ExamineBingusWithDialogue(SelectableCard card, ref string message)
        {
            if (card?.Info != null && card.Info.SpecialStatIcon == BingusStatIcon.Icon && BakingPlugin.BingusCrash.Value)
            {
                message =  $"{card.Info.displayedName} wants to apologise for {card.Info.displayedName.ToLowerInvariant()}ing your game. She hopes you'll give her another chance.";
            }
            return true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CardLoader), nameof(CardLoader.GetUnlockedCards))]
        private static void BingusOnlyIfBonnie(CardMetaCategory category, CardTemple temple, List<CardInfo> __result)
        {
            if (category == CardMetaCategory.Rare && !SaveManager.SaveFile.CurrentDeck.Cards.Exists(x => x.GetExtendedPropertyAsBool("IsBonnie") ?? false))
            {
                BakingPlugin.Log.LogDebug("No Bonnie, remove Bingus");
                __result.RemoveAll(x => x.SpecialStatIcon == BingusStatIcon.Icon);
            }
        }
    }
}
