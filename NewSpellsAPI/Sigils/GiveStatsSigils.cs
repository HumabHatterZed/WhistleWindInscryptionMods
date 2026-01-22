using DiskCardGame;
using Infiniscryption.Core.Helpers;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils {
    public class GiveStatsSigils : GiveAbility {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public override IEnumerator OnValidTarget(PlayableCard card) {
            if (base.Card.LacksAbility(GiveStats.AbilityID)) {
                yield return GiveStats.AddStatsFromHostToTarget(base.Card, card);
                if (card == null || card.Dead) {
                    yield break;
                }
            }

            if (base.Card.LacksAbility(GiveSigils.AbilityID)) {
                yield return GiveSigils.AddAbilitiesFromHostToTarget(base.Card, card);
            }
        }

        public static void Register() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Give Stats and Sigils";
            info.rulebookDescription = "Gives this card's stats and sigils to the target.";
            info.canStack = true;
            info.powerLevel = 5;
            info.opponentUsable = false;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("give_stats_sigils_pixel"));
            info.SetExtendedProperty("Spells:GiveAbility", true);

            GiveStatsSigils.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(GiveStatsSigils),
                AssetHelper.LoadTexture("ability_give_stats_sigils")
            ).Id;
        }
    }
}
