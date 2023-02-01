using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        public const string RightfulHeirStart = "Once per turn, pay ";
        public const string RightfulHeirEnd = " Bones to choose a creature to be transformed into a Pumpkin, then increase this sigil's activation cost by 1 Bone. [define:wstl_ozmaPumpkin]";
        private void Ability_RightfulHeir()
        {
            const string rulebookName = "Rightful Heir";
            const string rulebookDescription = RightfulHeirStart + "3" + RightfulHeirEnd;
            const string dialogue = "All she has left now are her children.";
            RightfulHeir.ability = AbnormalAbilityHelper.CreateActivatedAbility<RightfulHeir>(
                Artwork.sigilRightfulHeir, Artwork.sigilRightfulHeir_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3).Id;
        }
    }
    public class RightfulHeir : ActivatedSelectSlotBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int StartingBonesCost => 3;
        public override string InvalidTargetDialogue => "That card is fine as it is.";
        public override bool RespondsToUpkeep(bool playerUpkeep) => false;
        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            CardInfo info = CardLoader.GetCardByName("wstl_ozmaPumpkin");
            yield return slot.Card.TransformIntoCard(info);
            yield return new WaitForSeconds(0.5f);
        }
        public override IEnumerator OnPostValidTargetSelected()
        {
            yield break;
        }
        public override bool CardIsNotValid(PlayableCard card) => card.Info.name.Contains("ozmaPumpkin");
    }
}
