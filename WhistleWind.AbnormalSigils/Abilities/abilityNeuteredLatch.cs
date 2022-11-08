using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_NeuteredLatch()
        {
            const string rulebookName = "Neutered Latch";
            const string rulebookDescription = "Pay 6 Bones to choose a creature to gain the Neutered sigil. This can only be used once per turn.";
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
        public override int BonesCost => 6;
    }
}
