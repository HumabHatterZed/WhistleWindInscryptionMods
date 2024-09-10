using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_GroupHealer()
        {
            const string rulebookName = "Group Healer";
            const string rulebookDescription = "At the start of the owner's turn, [creature] will heal all injured allies by 1 Health.";
            const string dialogue = "You only delay the inevitable.";
            const string triggerText = "[creature] heals all its friends.";
            GroupHealer.ability = AbnormalAbilityHelper.CreateAbility<GroupHealer>(
                "sigilGroupHealer",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: false, canStack: true).Id;
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
            List<CardSlot> cardsToHeal = BoardManager.Instance.GetSlotsCopy(!base.Card.OpponentCard).FindAll(slot => slot.Card != null);
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
                yield return slot.Card.FlipFaceDown(false);
                slot.Card.Anim.LightNegationEffect();
                slot.Card.HealDamage(1);
                yield return new WaitForSeconds(0.2f);
                yield return slot.Card.FlipFaceDown(faceDown);
            }
            yield return base.LearnAbility(0.4f);
        }
    }
}
