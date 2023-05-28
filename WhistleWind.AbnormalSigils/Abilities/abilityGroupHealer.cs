using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_GroupHealer()
        {
            const string rulebookName = "Group Healer";
            const string rulebookDescription = "At the start of its owner's turn, this card will heal all allies that have taken damage by 1 Health.";
            const string dialogue = "You only delay the inevitable.";
            GroupHealer.ability = AbnormalAbilityHelper.CreateAbility<GroupHealer>(
                Artwork.sigilGroupHealer, Artwork.sigilGroupHealer_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class GroupHealer : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Board);

            // remove cards that are the base card or whose health is greater than/equal to the max
            List<CardSlot> cardsToHeal = HelperMethods.GetSlotsCopy(base.Card.OpponentCard).FindAll(slot => slot.Card != null);
            cardsToHeal.RemoveAll(x => x.Card == base.Card || x.Card.Health >= x.Card.MaxHealth);

            if (cardsToHeal.Count == 0)
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield break;
            }

            foreach (CardSlot slot in cardsToHeal)
            {
                bool faceDown = slot.Card.FaceDown;
                yield return slot.Card.FlipFaceUp(faceDown);
                slot.Card.Anim.LightNegationEffect();
                slot.Card.HealDamage(1);
                yield return new WaitForSeconds(0.2f);
                yield return slot.Card.FlipFaceDown(faceDown);
            }
            yield return base.LearnAbility(0.4f);
        }
    }
}
