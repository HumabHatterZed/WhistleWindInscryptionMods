using DiskCardGame;
using GBC;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Bleachproof() {
            const string rulebookName = "Bleachproof";
            const string rulebookDescription = "[creature] cannot have its sigils removed by effects like the Bleach Pot.";
            Bleachproof.ID = AbnormalAbilityHelper.CreateAbility<Bleachproof>(
                "sigilBleachproof",
                rulebookName, rulebookDescription, powerLevel: 2,
                modular: false, opponent: false, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// [creature] cannot have its sigils removed by effects like the Bleach Pot.
    /// </summary>
    [HarmonyPatch]
    public class Bleachproof : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        [HarmonyPostfix, HarmonyPatch(typeof(BleachPotItem), nameof(BleachPotItem.GetValidOpponentSlots))]
        private static void RemoveImmuneCards(ref List<CardSlot> __result) {
            __result.RemoveAll(x => x.Card.HasAbility(Bleachproof.ID));
        }

        [HarmonyPrefix, HarmonyPatch(typeof(MagnificusBattleSequencer), nameof(MagnificusBattleSequencer.RemoveCardAbilities))]
        private static bool DontRemoveAbilities(PlayableCard card) {
            if (card.HasAbility(Bleachproof.ID)) {
                AudioController.Instance.PlaySound2D("magnificus_brush_splatter_bleach", MixerGroup.None, 0.5f);
                return false;
            }
            return true;
        }
        [HarmonyPrefix, HarmonyPatch(typeof(MagnificusBattleSequencer), nameof(MagnificusBattleSequencer.AddNewAbility))]
        private static bool FixPaintedSigils(MagnificusBattleSequencer __instance, PlayableCard card) {
            if (card.HasAbility(Bleachproof.ID)) {
                // I'm just going to copy-paste the vanilla code here for simplicity's sake
                // and by simplicity i mean not needing to do a transpiler
                AudioController.Instance.PlaySound2D("magnificus_brush_splatter_color", MixerGroup.None, 0.5f);
                Ability ability = ((!card.OpponentCard) ? __instance.ADDITIVE_ABILITIES[Random.Range(0, __instance.ADDITIVE_ABILITIES.Count)] : __instance.OPPONENT_ADDITIVE_ABILITIES[Random.Range(0, __instance.OPPONENT_ADDITIVE_ABILITIES.Count)]);
                CardModificationInfo cardModificationInfo = new(ability) {
                    singletonId = "paint"
                };
                card.AddTemporaryMod(cardModificationInfo);
                GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Prefabs/GBCUI/TextAdditive/Magnificus_" + ability));
                obj.transform.parent = __instance.GetCardPaintPrefabParent(card);
                // set the initial localPos to zero just in case we cannot find a match
                obj.transform.localPosition = Vector3.zero;

                // what we're doing here is looking for the position of the added sigil icon
                // so we can reposition the paint game obj onto it (the two are not connected, just some visual trickery from dm)

                // get the current active ability icon group
                GameObject activeIconGroup = card.GetComponentInChildren<PixelCardDisplayer>().abilityIcons.abilityIconGroups.Find(x => x.activeSelf);
                
                if (activeIconGroup != null) {
                    for (int i = 0; i < activeIconGroup.transform.childCount; i++) {
                        // check each sprite renderer in the active group to find the correct one
                        SpriteRenderer r = activeIconGroup.transform.GetChild(i).GetComponent<SpriteRenderer>();
                        // if we have found the sprite renderer displaying the correct sigil, reposition the paint obj over it
                        if (r != null && r.sprite == AbilitiesUtil.GetInfo(ability).pixelIcon) {
                            obj.transform.localPosition = r.transform.localPosition;
                            break;
                        }
                    }
                }
                
                // you might be wondering: what  happens if sigils are modified after the painted sigil is added?
                // well don't. i'm not accounting for that because i hate you and hope you specifically die, hypothetical person reading this hiefr5wu46 er6zs
                card.Anim.LightNegationEffect();
                return false;
            }
            return true;
        }
    }
}
