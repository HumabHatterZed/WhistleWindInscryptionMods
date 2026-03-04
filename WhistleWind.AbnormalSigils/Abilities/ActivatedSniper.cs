using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_ActivatedSniper() {
            const string rulebookName = "Gun For Hire";
            const string rulebookDescription = "Pay 2 Energy to give this card Sniper until the end of its next attack.";
            const string dialogue = "Aim for the heart.";
            const string triggerText = "[creature] prepares to fire.";
            ActivatedSniper.ID = AbnormalAbilityHelper.CreateActivatedAbility<ActivatedSniper>(
                "sigilActivatedSniper",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2)
                .Id;
        }
    }
    /// <summary>
    /// Pay 2 Energy to give this card Sniper until the end of its next attack.
    /// </summary>
    [HarmonyPatch]
    public class ActivatedSniper : ActivatedAbilityBehaviour {
        public const string MAGIC_BULLET_ID = "wstl:MagicBullets,";
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
        public override int EnergyCost => 2;
        private bool IsHunter => base.Card.Info.name.Contains("derFreischutz");
        private bool activated = false;
        public override bool CanActivate() => base.CanActivate() && !activated && base.Card.LacksAbility(Ability.Sniper);
        public override IEnumerator Activate() {
            activated = true;
            bool triggerSeventhBullet = false;
            CardModificationInfo mod = new(Ability.Sniper) { singletonId = "wstl:ActivatedSniper" };
            if (IsHunter) {
                // keep track of bullets via a card mod with a properties id
                CardModificationInfo bulletMod = base.Card.Info.Mods.Find(x => HelperMethods.StartsWithSingleton(x.singletonId, MAGIC_BULLET_ID));
                if (bulletMod == null) {
                    //Debug.Log("New");
                    bulletMod = new() { singletonId = MAGIC_BULLET_ID + 7 };
                    RunState.Run.playerDeck.ModifyCard(base.Card.Info, bulletMod);
                }

                //Debug.Log("Preshoot:" + bulletMod.singletonId);
                if (!int.TryParse(bulletMod.singletonId.Substring(bulletMod.singletonId.Length - 1, 1), out int magicBullets)) {
                    magicBullets = 7;
                }

                magicBullets--;
                mod.attackAdjustment += 2;

                if (magicBullets == 0) {
                    magicBullets = 7;
                    triggerSeventhBullet = true;
                    //mod.attackAdjustment++;
                    mod.abilities.Add(Piercing.ID);
                }

                bulletMod.singletonId = bulletMod.singletonId.Replace(bulletMod.singletonId.Substring(bulletMod.singletonId.Length - 1, 1), magicBullets.ToString());
                //Debug.Log($"Postshoot:({magicBullets})" + bulletMod.singletonId);
            }

            yield return HelperMethods.ChangeCurrentView(View.Board);
            base.Card.Anim.PlayTransformAnimation();
            yield return new WaitForSeconds(0.15f);
            base.Card.Status.hiddenAbilities.Add(this.Ability);
            if (triggerSeventhBullet && !base.Card.HasAbility(Piercing.ID)) {
                base.Card.Status.hiddenAbilities.Add(Piercing.ID);
            }
            base.Card.AddTemporaryMod(mod);

            if (triggerSeventhBullet) {
                List<CardSlot> allSlots = BoardManager.Instance.AllSlotsCopy;
                allSlots.Remove(base.Card.Slot);
                CardSlot target = allSlots.GetRandom();
                yield return DialogueHelper.PlayDialogueEvent("SeventhMagicBullet");
                GameObject obj = TargetIconHelper.CreateTargetIcon(target);
                yield return new WaitForSeconds(0.5f);
                yield return Singleton<CombatPhaseManager>.Instance.SlotAttackSlot(base.Card.Slot, target);
                TargetIconHelper.CleanUpTargetIcon(obj);
                yield return new WaitForSeconds(0.5f);

                base.Card.Anim.PlayTransformAnimation();
                yield return new WaitForSeconds(0.15f);
                base.Card.Status.hiddenAbilities.Remove(this.Ability);
                base.Card.Status.hiddenAbilities.Remove(Piercing.ID);
                base.Card.RemoveTemporaryMod(mod);
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("SeventhMagicBullet2");
            }

            yield return base.LearnAbility(0.4f);
        }

        public override bool RespondsToAttackEnded() => activated;
        public override IEnumerator OnAttackEnded() {
            CardModificationInfo mod = base.Card.TemporaryMods.Find(x => x.singletonId == "wstl:ActivatedSniper");
            if (mod != null) {
                yield return HelperMethods.ChangeCurrentView(View.Board);
                base.Card.Anim.PlayTransformAnimation();
                yield return new WaitForSeconds(0.15f);
                base.Card.Status.hiddenAbilities.Remove(this.Ability);
                base.Card.RemoveTemporaryMod(mod);
                yield return new WaitForSeconds(0.4f);
            }
            activated = false;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(AbilityBehaviour), nameof(AbilityBehaviour.GetNonDefaultModsFromSelf))]
        private static void ActivatedSniperCannotBeInherited(ref List<CardModificationInfo> __result) {
            if (__result.Count > 0) {
                CardModificationInfo mod = __result[0];
                mod.abilities.Remove(ActivatedSniper.ID);
            }
        }
    }
}
