using DiskCardGame;
using Infiniscryption.Core.Helpers;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class DirectDamage : AbilityBehaviour
    {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public static void Register()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Direct Damage";
            info.rulebookDescription = "Deals damage directly to a target.";
            info.canStack = true;
            info.powerLevel = 1;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("damage_pixel"));

            DirectDamage.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(DirectDamage),
                AssetHelper.LoadTexture("ability_damage")
            ).Id;
        }

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            return slot.Card != null && slot.IsOpponentSlot();
        }

        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            yield return slot.Card.TakeDamage(1, attacker);
            yield return base.LearnAbility(0.5f);
        }
    }
}
