using DiskCardGame;
using Infiniscryption.Core.Helpers;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class Fishhook : AbilityBehaviour
    {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            if (base.Card.Info.IsSpell() && slot.Card != null && slot.opposingSlot.Card == null)
                return base.Card.OpponentCard != slot.Card.OpponentCard;

            return false;
        }

        public override IEnumerator OnResolveOnBoard()
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(base.Card.OpponentCard))
            {
                if (slot.Card != null && slot.opposingSlot.Card == null)
                {
                    yield return HookEffect(slot);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            yield return base.LearnAbility();
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            yield return HookEffect(slot);
            yield return new WaitForSeconds(0.2f);
        }
        private IEnumerator HookEffect(CardSlot slot)
        {
            PlayableCard targetCard = slot.Card;

            targetCard.SetIsOpponentCard(false);
            targetCard.transform.eulerAngles += new Vector3(0f, 0f, -180f);
            yield return BoardManager.Instance.AssignCardToSlot(targetCard, slot.opposingSlot, 0.1f, null, true);

            if (targetCard.FaceDown)
            {
                targetCard.SetFaceDown(false, false);
                targetCard.UpdateFaceUpOnBoardEffects();
            }
        }

        public static void Register()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Gain Control";
            info.rulebookDescription = "Takes control of the targeted creature.";
            info.powerLevel = 8;
            info.canStack = false;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("fishhook_pixel"));

            Fishhook.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(Fishhook),
                AssetHelper.LoadTexture("ability_fishhook")
            ).Id;
        }
    }
}
