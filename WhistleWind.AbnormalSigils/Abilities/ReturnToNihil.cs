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
        private void Ability_ReturnToNihil()
        {
            const string rulebookName = "Return to Nihil";
            const string rulebookDescription = "At the end of the owner's turn, all other cards on the board take damage equal to this card's Power.";
            const string dialogue = "One step closer to oblivion.";
            const string triggerText = "The void calls.";
            ReturnToNihil.ability = AbnormalAbilityHelper.CreateAbility<ReturnToNihil>(
                "sigilReturnToNihil",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 5,
                modular: false, special: true, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class ReturnToNihil : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd && base.Card.Attack > 0;

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            List<PlayableCard> allies = BoardManager.Instance.GetCards(!base.Card.OpponentCard, x => x != base.Card);
            List<PlayableCard> opposing = BoardManager.Instance.GetCards(base.Card.OpponentCard);
            if (allies.Count == 0 && opposing.Count == 0)
                yield break;

            yield return base.PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Board, lockAfter: true);
            foreach (PlayableCard card in allies)
            {
                yield return card.TakeDamage(base.Card.Attack, null);
            }
            foreach (PlayableCard card in opposing)
            {
                yield return card.TakeDamage(base.Card.Attack, null);
            }
            yield return new WaitForSeconds(0.2f);
            yield return base.LearnAbility();
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }
    }
}
