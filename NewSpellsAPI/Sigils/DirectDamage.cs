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
    public class DirectDamage : AbilityBehaviour
    {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            if (slot.Card != null)
                return base.Card.OpponentCard != slot.Card.OpponentCard;

            return false;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            yield return new WaitForSeconds(0.2f);

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlotsCopy(!base.Card.OpponentCard))
            {
                if (slot.Card != null)
                    yield return slot.Card.TakeDamage(1, null);
            }
            yield return base.LearnAbility(0.5f);
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            yield return slot.Card.TakeDamage(1, attacker);
            yield return base.LearnAbility(0.5f);
        }

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
    }
}
