using DiskCardGame;
using Infiniscryption.Core.Helpers;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class GiveStats : AbilityBehaviour
    {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public override bool RespondsToSacrifice() => true;
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            if (slot.Card != null)
                return base.Card.OpponentCard == slot.Card.OpponentCard;

            return false;
        }
        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();

        public override IEnumerator OnSacrifice()
        {
            PlayableCard card = Singleton<BoardManager>.Instance.CurrentSacrificeDemandingCard;
            if (card != null)
                yield return SingleEffect(card);
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) => SingleEffect(slot.Card);
        public override IEnumerator OnResolveOnBoard()
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(base.Card.IsPlayerCard()))
            {
                if (slot.Card != null)
                    slot.Card.AddTemporaryMod(new CardModificationInfo(base.Card.Attack, base.Card.Health));
            }
            yield return base.LearnAbility(0.5f);
        }

        private IEnumerator SingleEffect(PlayableCard card)
        {
            card.AddTemporaryMod(new CardModificationInfo(base.Card.Attack, base.Card.Health));
            yield return base.LearnAbility(0.5f);
        }


        public static void Register()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Give Stats";
            info.rulebookDescription = "Gives this card's stats to the target.";
            info.canStack = false;
            info.powerLevel = 4;
            info.opponentUsable = false;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("give_stats_pixel"));

            GiveStats.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(GiveStats),
                AssetHelper.LoadTexture("ability_give_stats")
            ).Id;
        }
    }
}
