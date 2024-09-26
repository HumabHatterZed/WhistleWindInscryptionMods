using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using GrimoraMod;
using HarmonyLib;
using Infiniscryption.PackManagement;
using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using InscryptionAPI.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BonniesBakingPack
{
    [HarmonyPatch]
    public static class Patches
    {
        [HarmonyPrefix, HarmonyPatch(typeof(AudioController), nameof(AudioController.GetAudioClip))]
        private static void AddAudioClips(AudioController __instance) => __instance.SFX.AddRange(BakingPlugin.AudioClips.Where(x => !__instance.SFX.Contains(x)));

        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.TakeDamage))]
        private static IEnumerator ChangeHitSound(IEnumerator enumerator, PlayableCard __instance, int damage, PlayableCard attacker)
        {
            if (attacker != null && attacker.HasAnyOfSpecialAbilities(PandaAbility.SpecialAbility, BunnieAbility.SpecialAbility))
            {
                if (attacker.HasSpecialAbility(PandaAbility.SpecialAbility))
                {
                    __instance.Info.Mods.Add(new() { singletonId = "BBP_Sound:panda_gun" });
                }
                else
                {
                    __instance.Info.Mods.Add(new() { singletonId = "BBP_Sound:bonnie_bonk" });
                }
            }
            yield return enumerator;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(PaperCardAnimationController), nameof(PaperCardAnimationController.PlayHitAnimation))]
        private static bool PlayCustomHitSoundPaper(PaperCardAnimationController __instance)
        {
            if (!__instance.deathAnimationStarted)
            {
                CardModificationInfo mod = __instance.Card.Info.Mods.Find(x => !string.IsNullOrEmpty(x.singletonId) && x.singletonId.StartsWith("BBP_Sound"));
                if (mod != null)
                {
                    __instance.Card.Info.Mods.Remove(mod);
                    string customSoundId = mod.singletonId.Replace("BBP_Sound:", "");
                    AudioController.Instance.PlaySound3D(customSoundId, MixerGroup.TableObjectsSFX, __instance.transform.position, 1f, 0f, new AudioParams.Pitch(AudioParams.Pitch.Variation.Small));
                    __instance.Anim.SetTrigger("take_hit");
                    __instance.FlashDamageMarks();
                    return false;
                }
            }
            return true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(GravestoneCardAnimationController), nameof(GravestoneCardAnimationController.PlayHitAnimation))]
        private static bool PlayCustomHitSoundGrave(GravestoneCardAnimationController __instance)
        {
            CardModificationInfo mod = __instance.Card.Info.Mods.Find(x => !string.IsNullOrEmpty(x.singletonId) && x.singletonId.StartsWith("BBP_Sound"));
            if (mod != null)
            {
                __instance.Card.Info.Mods.Remove(mod);
                string customSoundId = mod.singletonId.Replace("BBP_Sound:", "");
                AudioController.Instance.PlaySound3D(customSoundId, MixerGroup.TableObjectsSFX, __instance.transform.position, 1f, 0f, new AudioParams.Pitch(AudioParams.Pitch.Variation.Small));
                __instance.Anim.Play("take_hit", 0, 0f);
                __instance.FlashDamageMarks();
                __instance.damageParticles.Play();
                return false;
            }
            return true;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(DrawRandomCardOnDeath), nameof(DrawRandomCardOnDeath.CardToDraw), MethodType.Getter)]
        private static void PhoneMouseCallsThePopo(DrawRandomCardOnDeath __instance, ref CardInfo __result)
        {
            if (__instance.Card?.Info.name == "bbp_mousePhone")
                __result = CardLoader.GetCardByName("bbp_policeWolf");
        }

        [HarmonyPostfix, HarmonyPatch(typeof(CardDisplayer3D), nameof(CardDisplayer3D.DisplaySpecialStatIcons))]
        private static void BingusIsInfinite(CardDisplayer3D __instance, PlayableCard playableCard)
        {
            if (__instance.info.name == "bbp_bingus")
            {
                __instance.SetHealthAndAttackIconsActive(true, true);
                __instance.StatIcons.AssignStatIcon(BingusStatIcon.Icon, playableCard);
                __instance.StatIcons.SetInteractionEnabled(false);
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.CanBeSacrificed), MethodType.Getter)]
        private static void NoSacForBingus(PlayableCard __instance, ref bool __result)
        {
            if (__instance.Info.name == "bbp_bingus")
                __result = false;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CardMergeSequencer), nameof(CardMergeSequencer.GetValidCardsForHost))]
        [HarmonyPatch(typeof(CardMergeSequencer), nameof(CardMergeSequencer.GetValidCardsForSacrifice))]
        private static void RemoveSpecialCards(ref List<CardInfo> __result)
        {
            __result.RemoveAll(x => x.name.StartsWith("bbp") && x.onePerDeck);
        }

/*        [HarmonyPostfix, HarmonyPatch(typeof(CardStatBoostSequencer), nameof(CardStatBoostSequencer.StatBoostSequence), MethodType.Enumerator)]
        private static IEnumerator ReturnBonnieToDeck(IEnumerator enumerator)
        {
            bool hasBonnie = SaveManager.SaveFile.CurrentDeck.cardIds.Contains("bbp_bonnie");
            Debug.Log($"{hasBonnie}");
            yield return enumerator;
            if (hasBonnie && !SaveManager.SaveFile.CurrentDeck.cardIds.Contains("bbp_bonnie"))
            {
                Debug.Log($"dck");
                SaveManager.SaveFile.CurrentDeck.AddCard(CardLoader.GetCardByName("bbp_bonnie"));
                yield return TextDisplayer.Instance.PlayDialogueEvent("BonnieStatBoost");
            }
        }*/

        [HarmonyPostfix, HarmonyPatch(typeof(PlayerHand), nameof(PlayerHand.PlayCardOnSlot))]
        private static IEnumerator RemoveBingusInHand(IEnumerator enumerator, PlayerHand __instance, PlayableCard card, CardSlot slot)
        {
            if (card.LacksSpecialAbility(BingusAbility.SpecialAbility))
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
        private static void PrefixChangeEmissionColorBasedOnModSingletonId(GravestoneRenderStatsLayer __instance, ref CardRenderInfo info)
        {
            if (info.baseInfo.name == "bbp_akaMouso")
            {
                __instance.SetEmissionColor(new UnityEngine.Color(1f, 0f, 0f));
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(CardLoader), nameof(CardLoader.GetUnlockedCards))]
        private static void AddNonAct1CardsToAct1(ref List<CardInfo> __result, CardMetaCategory category, CardTemple temple)
        {
            List<CardInfo> result = new(__result);
            if (temple == CardTemple.Nature)
            {
                if (BakingPlugin.OverrideAct3.Value.HasFlag(BakingPlugin.ActOverride.Act1))
                    __result.AddRange(BakingPlugin.P03Cards.Where(x => x.HasCardMetaCategory(category) && !result.Contains(x)));

                if (BakingPlugin.OverrideGrimora.Value.HasFlag(BakingPlugin.ActOverride.Act1))
                    __result.AddRange(BakingPlugin.GrimoraCards.Where(x => x.HasCardMetaCategory(category) && !result.Contains(x)));
            }
            else
            {
                __result.RemoveAll(x => x.name == "bbp_bingus");
                if (temple == CardTemple.Undead)
                {
                    if (BakingPlugin.OverrideAct1.Value.HasFlag(BakingPlugin.ActOverride.ActGrimora))
                        __result.AddRange(BakingPlugin.Act1Cards.Where(x => x.HasCardMetaCategory(category) && !result.Contains(x)));

                    if (BakingPlugin.OverrideAct3.Value.HasFlag(BakingPlugin.ActOverride.ActGrimora))
                        __result.AddRange(BakingPlugin.P03Cards.Where(x => x.HasCardMetaCategory(category) && !result.Contains(x)));
                }
                else if (temple == CardTemple.Tech)
                {
                    if (BakingPlugin.OverrideAct1.Value.HasFlag(BakingPlugin.ActOverride.Act3))
                        __result.AddRange(BakingPlugin.Act1Cards.Where(x => x.HasCardMetaCategory(category) && !result.Contains(x)));

                    if (BakingPlugin.OverrideGrimora.Value.HasFlag(BakingPlugin.ActOverride.Act3))
                        __result.AddRange(BakingPlugin.GrimoraCards.Where(x => x.HasCardMetaCategory(category) && !result.Contains(x)));
                }
            }

            __result = CardLoader.RemoveDeckSingletonsIfInDeck(__result);

            // double chance of bingus
            if (temple == CardTemple.Nature && category == CardMetaCategory.Rare && SaveManager.SaveFile.CurrentDeck.Cards.Exists(x => x.name == "bbp_bonnie"))
            {
                CardInfo bingus = __result.Find(x => x.name == "bbp_bingus");
                if (bingus != null) __result.Add(bingus);
            }
        }
        [HarmonyPrefix, HarmonyPatch(typeof(DeckInfo), nameof(DeckInfo.AddCard))]
        private static void AddNineMod(CardInfo card)
        {
            if (card.name == "bbp_nine" && !card.Mods.Exists(x => x.singletonId == NineAbility.NINE_LIVES_ID))
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

        [HarmonyPostfix, HarmonyPatch(typeof(PlayerHand), nameof(PlayerHand.AddCardToHand))]
        private static IEnumerator DrawFreshFoodAfterPositioning(IEnumerator enumerator, PlayableCard card)
        {
            yield return enumerator;
            if (card.HasAbility(FreshFood.ability))
            {
                AbilityBehaviour trigger = card.TriggerHandler.triggeredAbilities.FirstOrDefault(x => x.Item1 == FreshFood.ability).Item2;
                if (trigger != null)
                {
                    yield return trigger.OnDrawn();
                }
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(CardChoicesSequencer), nameof(CardChoicesSequencer.ExamineCardWithDialogue))]
        private static bool ExamineBingusWithDialogue(SelectableCard card, ref string message)
        {
            if (card.Info.HasSpecialAbility(BingusAbility.SpecialAbility) && BakingPlugin.BingusCrash.Value)
            {
                message = "Bingus wants to apologise for bingusing your game. She hopes you'll give her another chance.";
            }
            return true;
        }
    }
}
