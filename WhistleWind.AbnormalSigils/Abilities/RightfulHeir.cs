using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_RightfulHeir() {
            const string rulebookName = "Rightful Heir";
            const string rulebookDescription = "Pay [sigilcost:2 Bones] to select any creature on the board and transform them into a Sturdy Pumpkin, then increase this sigil's activation cost by 1 Bone. Opposing Pumpkins will be rotten instead.";
            const string dialogue = "All she has left now are her children.";
            const string triggerText = "[creature] sprinkles cinnamon dust.";
            RightfulHeir.ID = AbnormalAbilityHelper.CreateActivatedAbility<RightfulHeir>(
                "sigilRightfulHeir",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3)
                .Id;
        }
    }
    /// <summary>
    /// Pay [sigilcost:2 Bones] to select any creature on the board and transform them into a Sturdy Pumpkin, then increase this sigil's activation cost by 1 Bone. Opposing Pumpkins will be rotten instead.
    /// </summary>
    public class RightfulHeir : ActivatedSelectSlotBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public override bool CanActivate() => base.CanActivate() && ValidTargets.Count > 0;
        public override string InvalidTargetDialogue(CardSlot slot) {
            if (slot.Card.Info.name.Contains("ozmaPumpkin"))
                return "No need, it's already perfect.";
            return "That card is fine as it is.";
        }

        public override int StartingBonesCost => 2;
        public override int OnActivateBonesCostMod => 1;
        public override bool IsValidTarget(CardSlot slot) {
            if (!base.IsValidTarget(slot))
                return false;

            return slot.Card.LacksAllTraits(Trait.Giant, Trait.Uncuttable) && !slot.Card.Info.name.Contains("ozmaPumpkin");
        }

        public override bool RespondsToUpkeep(bool playerUpkeep) => false;
        public override IEnumerator OnValidTargetSelected(CardSlot slot) {
            CardInfo info;
            if (slot.IsPlayerSlot == base.Card.Slot.IsPlayerSlot) {
                info = CardLoader.GetCardByName("wstl_ozmaPumpkin");
            }
            else {
                info = CardLoader.GetCardByName("wstl_ozmaPumpkinWeak");
            }
            yield return slot.Card.TransformIntoCard(info);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
