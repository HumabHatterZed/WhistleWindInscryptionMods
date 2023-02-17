using DiskCardGame;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_NeuteredLatch()
        {
            const string rulebookName = "Neutered Latch";
            const string rulebookDescription = "Once per turn, pay [sigilcost:2 Bones] to choose a creature to gain the Neutered sigil, then increase this sigil's activation cost by 2 Bones.";
            const string dialogue = "The will to fight has been lost.";
            NeuteredLatch.ability = AbnormalAbilityHelper.CreateActivatedAbility<NeuteredLatch>(
                Artwork.sigilNeuteredLatch, Artwork.sigilNeuteredLatch_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4).Id;
        }
    }
    public class NeuteredLatch : ActivatedSelectSlotBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override Ability LatchAbility => Neutered.ability;
        public override int StartingBonesCost => 2;
        public override int OnActivateBonesCostMod => 2;

        public override bool CardSlotCanBeTargeted(CardSlot slot) => slot.Card != null && slot.Card != base.Card;
    }
}
