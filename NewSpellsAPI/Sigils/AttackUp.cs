using DiskCardGame;
using Infiniscryption.Core.Helpers;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class AttackBuff : AbilityBehaviour
    {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public static void Register()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Attack Up";
            info.rulebookDescription = "Increases the target's attack for the rest of the battle.";
            info.canStack = true;
            info.powerLevel = 1;
            info.opponentUsable = false;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("attack_up_pixel"));

            AttackBuff.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(AttackBuff),
                AssetHelper.LoadTexture("ability_attack_up")
            ).Id;
        }

        public override bool RespondsToSacrifice() => true;
        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) => slot.IsPlayerSlot && slot.Card != null;

        public override IEnumerator OnSacrifice()
        {
            PlayableCard card = Singleton<BoardManager>.Instance.CurrentSacrificeDemandingCard;

            if (card != null)
                yield return Effect(card);
        }
        public override IEnumerator OnResolveOnBoard()
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(true))
            {
                if (slot.Card != null)
                    slot.Card.AddTemporaryMod(new(1, 0));
            }
            yield return base.LearnAbility(0.5f);
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) => Effect(slot.Card);

        private IEnumerator Effect(PlayableCard card)
        {
            card.AddTemporaryMod(new(1, 0));
            yield return base.LearnAbility(0.5f);
        }
    }
}
