using DiskCardGame;
using EasyFeedback.APIs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            const string rulebookDescription = "While this card is on the board, all allies whose Health is below their maximum regain 1 Health on upkeep.";
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
            List<CardSlot> allyCards = Singleton<BoardManager>.Instance.GetSlots(!base.Card.OpponentCard).Where(slot => slot.Card != null).ToList();
            allyCards.RemoveAll(x => x.Card == base.Card || x.Card.MaxHealth <= x.Card.Health);

            if (allyCards.Count > 0)
            {
                foreach (CardSlot slot in allyCards)
                {
                    slot.Card.Anim.LightNegationEffect();
                    slot.Card.HealDamage(1);
                    slot.Card.OnStatsChanged();
                }
                yield return base.LearnAbility(0.4f);
            }
            else
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
            }
        }
    }
}
