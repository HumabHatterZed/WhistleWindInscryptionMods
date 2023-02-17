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
        private void Ability_RightfulHeir()
        {
            const string rulebookName = "Rightful Heir";
            const string rulebookDescription = "Once per turn, pay [sigilcost:3 Bones] to choose a creature to be transformed into a Pumpkin, then increase this sigil's activation cost by 1 Bone. [define:wstl_ozmaPumpkin]";
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

        public override string InvalidTargetDialogue => "That card is fine as it is.";
        public override int TurnDelay => 1;
        public override int StartingBonesCost => 3;
        public override int OnActivateBonesCostMod => 1;
        public override bool CardSlotCanBeTargeted(CardSlot slot) => slot.Card != base.Card;
        
        public override bool RespondsToUpkeep(bool playerUpkeep) => false;
        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            CardInfo info = CardLoader.GetCardByName("wstl_ozmaPumpkin");
            yield return slot.Card.TransformIntoCard(info);
            yield return new WaitForSeconds(0.5f);
        }
        public override bool CardIsNotValid(PlayableCard card) => card.Info.name.Contains("ozmaPumpkin");
    }
}
