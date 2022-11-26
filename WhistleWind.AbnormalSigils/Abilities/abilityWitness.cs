using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Witness()
        {
            const string rulebookName = "Witness";
            const string rulebookDescription = "Activate: Pay 2 bones to increase a selected card's Health by 2 and increase their taken damage by 1. This effect stacks up to 3 times per card.";
            const string dialogue = "The truth will set you free.";

            Witness.ability = AbnormalAbilityHelper.CreateActivatedAbility<Witness>(
                Artwork.sigilWitness, Artwork.sigilWitness_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 1).Id;
        }
    }
    public class Witness : ActivatedSelectSlotBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool TargetAll => false;
        public override bool TargetAllies => true;
        public override string NoTargetsDialogue => "There's no one to hear your message.";
        public override string InvalidTargetDialogue => "You must choose one of your other cards to proselytise.";
        public override int StartingBonesCost => 2;

        public override bool CanActivate()
        {
            // Are there other cards to affect, are any of them valid targets, and is this card on the player's side
            foreach (var slot in HelperMethods.GetSlotsCopy(base.Card.OpponentCard).Where((CardSlot slot) => slot.Card != base.Card))
            {
                if (slot.Card != null)
                {
                    // if there is at least one other card that can take more Prudence
                    int prudence = !(slot.Card.Info.GetExtendedPropertyAsInt("wstl:Prudence") != null) ? 0 : (int)slot.Card.Info.GetExtendedPropertyAsInt("wstl:Prudence");
                    if (prudence < 3)
                        return true;
                }
            }
            return false;
        }

        public override IEnumerator OnPostValidTargetSelected()
        {
            yield break;
        }
        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            int prudence = !(slot.Card.Info.GetExtendedPropertyAsInt("wstl:Prudence") != null) ? 0 : (int)slot.Card.Info.GetExtendedPropertyAsInt("wstl:Prudence");
            yield return slot.Card.Info.SetExtendedProperty("wstl:Prudence", prudence + 1);
            slot.Card.HealDamage(2);
            slot.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return base.LearnAbility();
        }

        public override bool CardIsNotValid(PlayableCard card)
        {
            int prudence = card.Info.GetExtendedPropertyAsInt("wstl:Prudence") ?? -1;

            if (prudence == -1)
                return false;

            return prudence >= 3;
        }
    }
}
