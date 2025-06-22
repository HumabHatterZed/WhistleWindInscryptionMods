using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using MagnificusMod;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BonniesBakingPack {
    [HarmonyPatch]
    public static class AttackSoundPatches {
        private const string PANDA_GUN = "BBP_Sound:panda_gun";
        private const string BONNIE_BONK = "BBP_Sound:bonnie_bonk";

        [HarmonyPrefix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.TakeDamage))]
        private static bool ChangeHitSound(PlayableCard __instance, PlayableCard attacker) {
            if (attacker != null) {
                if (attacker.HasSpecialAbility(PandaAbility.SpecialAbility)) {
                    __instance.Info.Mods.Add(new() { singletonId = PANDA_GUN }); // since PlayHitAnimation doesn't track the attacker, we do that here
                }
                else if (attacker.HasSpecialAbility(BunnieAttackAbility.SpecialAbility)) {
                    __instance.Info.Mods.Add(new() { singletonId = BONNIE_BONK });
                }
            }
            return true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(PaperCardAnimationController), nameof(PaperCardAnimationController.PlayHitAnimation))]
        private static bool PlayCustomHitSoundPaper(PaperCardAnimationController __instance) {
            if (!__instance.deathAnimationStarted && PlayHitSoundFromMod(__instance.Card)) {
                __instance.Anim.SetTrigger("take_hit");
                __instance.FlashDamageMarks();
                return false;
            }
            return true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(GravestoneCardAnimationController), nameof(GravestoneCardAnimationController.PlayHitAnimation))]
        private static bool PlayCustomHitSoundGrave(GravestoneCardAnimationController __instance) {
            if (PlayHitSoundFromMod(__instance.Card)) {
                __instance.Anim.Play("take_hit", 0, 0f);
                __instance.FlashDamageMarks();
                __instance.damageParticles.Play();
                return false;
            }
            return true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(WizardCardAnimationController), nameof(WizardCardAnimationController.PlayHitAnimation))]
        private static bool PlayCustomHitSoundWizard(WizardCardAnimationController __instance) {
            if (__instance.WizardPortrait != null)
                PlayHitSoundFromMod(__instance.Card);

            return true;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.Die))]
        private static bool PlayCustomHitSoundOnDie(PlayableCard __instance) {
            if (!__instance.Dead)
                PlayHitSoundFromMod(__instance);

            return true;
        }

        private static bool PlayHitSoundFromMod(Card card) {
            CardModificationInfo mod = card.Info.Mods.Find(x => !string.IsNullOrEmpty(x.singletonId) && x.singletonId.StartsWith("BBP_Sound"));
            if (mod != null) {
                card.Info.Mods.Remove(mod);
                string customSoundId = mod.singletonId.Replace("BBP_Sound:", "");
                AudioController.Instance.PlaySound3D(customSoundId, MixerGroup.TableObjectsSFX, card.transform.position, 1f, 0f, new AudioParams.Pitch(AudioParams.Pitch.Variation.Small));
                return true;
            }
            return false;
        }
    }
}
