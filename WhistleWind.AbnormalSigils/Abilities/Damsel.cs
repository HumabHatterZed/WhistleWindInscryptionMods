using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Damsel() {
            const string rulebookName = "Damsel";
            const string rulebookDescription = "Cards adjacent to [creature] will redirect themselves to strike at the first card targeting this card.";
            const string dialogue = "The damsel demands warriors to destroy its tormentor.";
            Damsel.ability = AbnormalAbilityHelper.CreateAbility<Damsel>(
                "sigilDamsel",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: false, opponent: false, canStack: false)
                .Info.SetFlipYIfOpponent()
                .ability;
        }
    }
    /// <summary>
    /// Attacks from creatures adjacent to [creature] are redirected to creatures targeting this card.
    /// </summary>
    [HarmonyPatch]
    public class Damsel : AbilityBehaviour, IOnPostSlotAttackSequence {
        public static Ability ability;
        public override Ability Ability => ability;

        public bool RespondsToPostSlotAttackSequence(CardSlot attackingSlot) {
            return base.Card.Slot.GetAdjacentSlots(true).Contains(attackingSlot);
        }

        public IEnumerator OnPostSlotAttackSequence(CardSlot attackingSlot) {
            yield return base.PreSuccessfulTriggerSequence();
            yield return base.LearnAbility(0.5f);
        }

        public static List<CardSlot> GetTormentorSlots(PlayableCard damselCard) {
            return BoardManager.Instance.GetCardSlots(damselCard.OpponentCard, x => x.Card != null && x.Card.GetOpposingSlots().Contains(damselCard.Slot));
        }

        private static void ResetOpposingSlot(PlayableCard card) {
            if (card.Slot.opposingSlot.Index != card.Slot.Index) {
                AbnormalPlugin.Log.LogDebug("[DamselOverrideOpposingSlot] Reset opposing slot");
                card.Slot.opposingSlot = BoardManager.Instance.GetSlotsCopy(!card.Slot.IsPlayerSlot)[card.Slot.Index];
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.GetOpposingSlots))]
        [HarmonyPriority(HarmonyLib.Priority.High)]
        private static void DamselOverrideOpposingSlot(PlayableCard __instance) {
            PlayableCard damselCard = __instance.Slot.GetAdjacentCards().Find(x => x.HasAbility(Damsel.ability));
            if (damselCard != null) {
                AbnormalPlugin.Log.LogDebug("[DamselOverrideOpposingSlot] Damsel exists");
                List<CardSlot> slots = Damsel.GetTormentorSlots(damselCard);
                if (slots.Count > 0) {
                    AbnormalPlugin.Log.LogDebug("[DamselOverrideOpposingSlot] Override opposing slot");
                    __instance.Slot.opposingSlot = slots[0];
                }
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.GetOpposingSlots))]
        private static void DamselOverrideOpposingSlotPrefix(PlayableCard __instance) {
            if (__instance.Slot.opposingSlot.Index != __instance.Slot.Index) {
                AbnormalPlugin.Log.LogDebug("[DamselOverrideOpposingSlot] Reset opposing slot");
                __instance.Slot.opposingSlot = BoardManager.Instance.GetSlotsCopy(!__instance.Slot.IsPlayerSlot)[__instance.Slot.Index];
            }
        }
    }
}
