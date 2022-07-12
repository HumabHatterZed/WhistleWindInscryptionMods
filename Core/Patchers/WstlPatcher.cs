using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public static class WstlPatcher
    {
        // Adds select Kaycee Mod sigils to the Part 1 rulebook
        [HarmonyPatch(typeof(RuleBookInfo), nameof(RuleBookInfo.AbilityShouldBeAdded))]
        [HarmonyPostfix]
        private static void AddKayceeAbilities(ref int abilityIndex, ref AbilityMetaCategory rulebookCategory, ref bool __result)
        {
            AbilityInfo info = AbilitiesUtil.GetInfo((Ability)abilityIndex);
            if (!SaveFile.IsAscension && info.metaCategories.Contains(AbilityMetaCategory.AscensionUnlocked))
            {
                if (info.name.Equals("BoneDigger") || //info.name.Equals("DeathShield") ||
                    info.name.Equals("DoubleStrike") || //info.name.Equals("OpponentBones")
                    info.name.Equals("StrafeSwap") || info.name.Equals("Morsel"))
                {
                    __result = true;
                }
            }
        }
        // Resets NumOfBlessings when the event ends with WhiteNight on the board
        [HarmonyPatch(typeof(BoardManager), nameof(BoardManager.CleanUp))]
        [HarmonyPostfix]
        private static void ResetBlessings(ref BoardManager __instance)
        {
            if (ConfigUtils.Instance.NumOfBlessings >= 12 && __instance.AllSlotsCopy.FindAll((CardSlot s) => s.Card != null && s.Card.Info.name == "wstl_whiteNight").Count > 0)
            {
                ConfigUtils.Instance.UpdateBlessings(-ConfigUtils.Instance.NumOfBlessings);
                WstlPlugin.Log.LogDebug($"Resetting the clock to [0].");
            }
        }
        // Prevents bones from dropping under certain conditions
        [HarmonyPatch(typeof(ResourcesManager), nameof(ResourcesManager.AddBones))]
        [HarmonyPostfix]
        public static IEnumerator AddBones(IEnumerator enumerator, CardSlot slot)
        {
            if (slot != null && slot.Card != null)
            {
                bool train = slot.Card.Info.GetExtendedProperty("wstl:KilledByTrain") != null && (bool)slot.Card.Info.GetExtendedPropertyAsBool("wstl:KilledByTrain");
                bool whiteNight = slot.Card.Info.HasAbility(TrueSaviour.ability) || slot.Card.Info.HasAbility(Apostle.ability) || slot.Card.Info.HasAbility(Confession.ability);
                if (train || whiteNight)
                {
                    if (train)
                    {
                        slot.Card.Info.SetExtendedProperty("wstl:KilledByTrain", false);
                    }
                    yield break;
                }
            }
            yield return enumerator;
        }
        // Increases damage taken by amount of Prudence a card has
        [HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.TakeDamage))]
        [HarmonyPostfix]
        public static void TakePrudenceDamage(PlayableCard __instance, ref int damage)
        {
            int prudence = !(__instance.Info.GetExtendedPropertyAsInt("wstl:Prudence") != null) ? 0 : (int)__instance.Info.GetExtendedPropertyAsInt("wstl:Prudence");
            if (prudence > 0)
            {
                damage += prudence;
            }
        }
        // Adds custom death cards
        [HarmonyPatch(typeof(DefaultDeathCards), nameof(DefaultDeathCards.CreateDefaultCardMods))]
        [HarmonyPostfix]
        public static void AddDeathCards(ref List<CardModificationInfo> __result)
        {
            __result.Add(new CardModificationInfo(3, 2)
            {
                nameReplacement = "Mirabelle",
                singletonId = "wstl_mirabelleDeathCard",
                abilities = { Ability.GuardDog },
                bloodCostAdjustment = 2,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.SettlerWoman, 5, 1)
            });
            __result.Add(new CardModificationInfo(2, 1)
            {
                nameReplacement = "Poussey",
                singletonId = "wstl_posseyDeathCard",
                abilities = { Ability.Strafe, Ability.Flying },
                bonesCostAdjustment = 4,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.SettlerMan, 5, 5)
            });
            __result.Add(new CardModificationInfo(1, 2)
            {
                nameReplacement = "Stemcell-642",
                singletonId = "wstl_stemCell642DeathCard",
                abilities = { Ability.SplitStrike },
                bloodCostAdjustment = 1,
                deathCardInfo = new DeathCardInfo(CompositeFigurine.FigurineType.Chief, 5, 2)
            });
        }
    }
}
