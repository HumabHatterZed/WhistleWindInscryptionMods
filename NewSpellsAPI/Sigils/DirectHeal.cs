using DiskCardGame;
using Infiniscryption.Core.Helpers;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class DirectHeal : AbilityBehaviour
    {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public override bool RespondsToSacrifice() => true;
        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            if (slot.Card != null)
                return base.Card.OpponentCard == slot.Card.OpponentCard;

            return false;
        }

        public override IEnumerator OnSacrifice()
        {
            PlayableCard card = Singleton<BoardManager>.Instance.CurrentSacrificeDemandingCard;
            if (card != null)
            {
                card.HealDamage(1);
                yield return new WaitForSeconds(0.45f);
                yield return base.LearnAbility(0.5f);
            }
        }
        public override IEnumerator OnResolveOnBoard()
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(true))
            {
                if (slot.Card != null)
                    Card.HealDamage(1);
            }
            yield return new WaitForSeconds(0.45f);
            yield return base.LearnAbility();
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            slot.Card.HealDamage(1);
            yield return new WaitForSeconds(0.45f);
            yield return base.LearnAbility();
        }

        public static void Register()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Direct Heal";
            info.rulebookDescription = "Heals the target. This can heal the target beyond its original max health.";
            info.canStack = true;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("direct_heal_pixel"));

            DirectHeal.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(DirectHeal),
                AssetHelper.LoadTexture("ability_health_up")
            ).Id;
        }
    }
}
