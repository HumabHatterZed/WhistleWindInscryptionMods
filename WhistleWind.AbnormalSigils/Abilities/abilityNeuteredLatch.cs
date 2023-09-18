using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_NeuteredLatch()
        {
            const string rulebookName = "Neutered Latch";
            const string rulebookDescription = "Once per turn, pay [sigilcost:2 Bones] to choose a creature to gain the Neutered sigil, then increase this sigil's activation cost by 1 Bone.";
            const string dialogue = "The will to fight has been lost.";
            const string triggerText = "[creature] prevents the chosen creature from attacking.";
            NeuteredLatch.ability = AbnormalAbilityHelper.CreateActivatedAbility<NeuteredLatch>(
                "sigilNeuteredLatch",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4).Id;
        }
    }
    public class NeuteredLatch : ActivatedSelectSlotBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override Ability LatchAbility => Neutered.ability;
        public override int StartingBonesCost => 2;
        public override int OnActivateBonesCostMod => 1;

        public override bool IsInvalidTarget(CardSlot slot)
        {
            if (slot.Card != null && slot.Card != base.Card)
                return slot.Card.HasAbility(Neutered.ability) || slot.Card.Attack == 0;

            return base.IsInvalidTarget(slot);
        }
    }
}
