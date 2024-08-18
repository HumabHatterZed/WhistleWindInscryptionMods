using DiskCardGame;
using Infiniscryption.Core.Helpers;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class GiveStats : GiveAbility
    {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public override IEnumerator OnValidTarget(PlayableCard card)
        {
            CardModificationInfo statMod = new(base.Card.Attack, 0);
            if (base.Card.Health < 0)
            {
                card.AddTemporaryMod(statMod);
                yield return card.TakeDamage(-base.Card.Health, null);
            }
            else
            {
                statMod.healthAdjustment = base.Card.Health;
                card.AddTemporaryMod(statMod);
            }
        }


        public static void Register()
        {
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
