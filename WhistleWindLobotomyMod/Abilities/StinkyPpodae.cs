using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddPpodaeStinky() {
            AbilityManager.FullAbility full = AbilityManager.AllAbilities.AbilityByID(Ability.DebuffEnemy);
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.SetRulebookName("Goodest Boy in the World")
                .SetRulebookDescription(full.BaseRulebookDescription)
                .SetPowerlevel(full.Info.powerLevel)
                .SetPixelAbilityIcon(full.Info.pixelIcon.texture)
                .SetCanStack(full.Info.canStack)
                .SetFlipYIfOpponent(full.Info.flipYIfOpponent)
                .SetOpponentUsable(full.Info.opponentUsable)
                .AddMetaCategories(AbilityMetaCategory.Part1Rulebook)
                .SetPassive(full.Info.passive);

            PpodaeStinky.ID = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(PpodaeStinky), full.Texture).Id;
        }
    }

    [HarmonyPatch]
    public class PpodaeStinky : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.HasAbility))]
        private static void CountsAsStinky(PlayableCard __instance, Ability ability, ref bool __result) {
            if (__result)
                return;

            if (ability == Ability.DebuffEnemy) {
                
                __result = __instance.HasAbility(PpodaeStinky.ID);
                //LobotomyPlugin.Log.LogInfo($"Fuck {__result}");
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(CardInfo), nameof(CardInfo.HasAbility))]
        private static void InfoIsStinky(CardInfo __instance, Ability ability, ref bool __result) {
            if (__result)
                return;

            if (ability == Ability.DebuffEnemy) {
                __result = __instance.HasAbility(PpodaeStinky.ID);
                //LobotomyPlugin.Log.LogInfo($"Fuck2 {__result}");
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.GetPassiveAttackBuffs))]
        private static void Fuck(PlayableCard __instance, ref int __result) {
            //Debug.Log($"{__instance.name} | {__result} | {__instance.slot?.opposingSlot?.Card?.HasAbility(Ability.DebuffEnemy)}");
            if (__instance.OnBoard && !__instance.HasAbility(Ability.MadeOfStone)) {
                if (__instance.HasTrait(Trait.Giant)) {
                    if (BoardManager.Instance.GetSlotsCopy(__instance.OpponentCard).Exists(x => x.Card != null && x.Card.HasAbility(PpodaeStinky.ID))) {
                        __result--;
                    }
                }
                else if (__instance.slot.opposingSlot.Card != null && __instance.slot.opposingSlot.Card.HasAbility(PpodaeStinky.ID)) {
                    __result--;
                }
            }
        }
    }
}
