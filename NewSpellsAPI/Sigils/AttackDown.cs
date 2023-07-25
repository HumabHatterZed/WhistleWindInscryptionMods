using DiskCardGame;
using Infiniscryption.Core.Helpers;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class AttackNerf : AbilityBehaviour
    {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            if (slot.Card != null && slot.Card.Attack > 0)
                return base.Card.OpponentCard != slot.Card.OpponentCard;

            return false;
        }

        public override IEnumerator OnResolveOnBoard()
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            yield return new WaitForSeconds(0.2f);

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(base.Card.OpponentCard))
            {
                if (slot.Card != null)
                    slot.Card.AddTemporaryMod(new(-1, 0));
            }
            yield return base.LearnAbility(0.5f);
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            slot.Card.AddTemporaryMod(new(-1, 0));
            yield return base.LearnAbility(0.5f);
        }

        public static void Register()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Attack Down";
            info.rulebookDescription = "Decreases the target's attack for the rest of the battle.";
            info.canStack = true;
            info.powerLevel = 1;
            info.opponentUsable = false;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("attack_down_pixel"));

            AttackNerf.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(AttackNerf),
                AssetHelper.LoadTexture("ability_attack_down")
            ).Id;
        }
    }
}
