using DiskCardGame;
using Infiniscryption.Core.Helpers;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils {
    public class GiveSigils : GiveAbility {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public const int MaxSigilsToGive = 4;

        public static IEnumerator AddAbilitiesFromHostToTarget(PlayableCard host, PlayableCard target) {
            CardModificationInfo abilitiesToAdd = new();
            CardModificationInfo mergedAbilitiesToAdd = new() { fromCardMerge = true };
            List<Ability> modAbilities = AbilitiesUtil.GetAbilitiesFromMods(
                host.TemporaryMods.Concat(host.Info.Mods).Where(x => x.fromCardMerge).ToList()
                );

            abilitiesToAdd.abilities.AddRange(host.Info.DefaultAbilities);

            foreach (CardModificationInfo info in host.Info.Mods.Where(x => x.abilities.Count > 0)) {
                foreach (Ability ab in info.abilities) {
                    bool addToMerge = info.fromCardMerge;
                    // if can stack or can be added to the appropriate card info
                    if (AbilitiesUtil.GetInfo(ab).canStack || (addToMerge ? !mergedAbilitiesToAdd.abilities.Contains(ab) : !abilitiesToAdd.abilities.Contains(ab))) {
                        if (addToMerge) {
                            mergedAbilitiesToAdd.abilities.Add(ab);
                        }
                        else {
                            abilitiesToAdd.abilities.Add(ab);
                        }
                    }
                }
            }

            foreach (CardModificationInfo info in host.TemporaryMods.Where(x => x.abilities.Count > 0 && !x.fromTotem && !x.fromLatch && !x.fromOverclock)) {
                foreach (Ability ab in info.abilities) {
                    bool addToMerge = info.fromCardMerge;
                    // if can stack or can be added to the appropriate card info
                    if (AbilitiesUtil.GetInfo(ab).canStack || (addToMerge ? !mergedAbilitiesToAdd.abilities.Contains(ab) : !abilitiesToAdd.abilities.Contains(ab))) {
                        if (addToMerge) {
                            mergedAbilitiesToAdd.abilities.Add(ab);
                        }
                        else {
                            abilitiesToAdd.abilities.Add(ab);
                        }
                    }
                }
            }

            abilitiesToAdd.abilities.RemoveAll(x => x.GetExtendedPropertyAsBool("Spells:GiveAbility") == true);
            mergedAbilitiesToAdd.abilities.RemoveAll(x => x.GetExtendedPropertyAsBool("Spells:GiveAbility") == true);

            if (abilitiesToAdd.abilities.Count > MaxSigilsToGive) {
                abilitiesToAdd.abilities.RemoveRange(MaxSigilsToGive, abilitiesToAdd.abilities.Count - MaxSigilsToGive);
            }

            if (mergedAbilitiesToAdd.abilities.Count > MaxSigilsToGive) {
                mergedAbilitiesToAdd.abilities.RemoveRange(MaxSigilsToGive, mergedAbilitiesToAdd.abilities.Count - MaxSigilsToGive);
            }

            target.Anim.PlayTransformAnimation();
            target.AddTemporaryMod(abilitiesToAdd);
            target.AddTemporaryMod(mergedAbilitiesToAdd);
            yield return new WaitForSeconds(0.15f);
        }

        public override IEnumerator OnValidTarget(PlayableCard card) {
            yield return AddAbilitiesFromHostToTarget(base.Card, card);
        }

        public static void Register() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Give Sigils";
            info.rulebookDescription = "Gives this card's sigils to the target.";
            info.canStack = true;
            info.powerLevel = 3;
            info.opponentUsable = false;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("give_sigils_pixel"));
            info.SetExtendedProperty("Spells:GiveAbility", true);

            GiveSigils.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(GiveSigils),
                AssetHelper.LoadTexture("ability_give_sigils")
            ).Id;
        }
    }
}
