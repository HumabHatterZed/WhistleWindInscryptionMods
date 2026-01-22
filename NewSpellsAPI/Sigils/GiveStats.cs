using DiskCardGame;
using Infiniscryption.Core.Helpers;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils {
    public class GiveStats : GiveAbility {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public static IEnumerator AddStatsFromHostToTarget(PlayableCard host, PlayableCard target) {
            CardModificationInfo statMod = new(host.Attack, 0);
            if (host.Health < 0) {
                target.AddTemporaryMod(statMod);
                yield return host.TakeDamage(-host.Health, null);
            }
            else {
                statMod.healthAdjustment = host.Health;
                target.AddTemporaryMod(statMod);
            }
        }
        public override IEnumerator OnValidTarget(PlayableCard card) {
            yield return AddStatsFromHostToTarget(base.Card, card);
        }


        public static void Register() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Give Stats";
            info.rulebookDescription = "Gives this card's stats to the target.";
            info.canStack = true;
            info.powerLevel = 2;
            info.opponentUsable = false;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("give_stats_pixel"));
            info.SetExtendedProperty("Spells:GiveAbility", true);
            GiveStats.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(GiveStats),
                AssetHelper.LoadTexture("ability_give_stats")
            ).Id;
        }
    }
}
