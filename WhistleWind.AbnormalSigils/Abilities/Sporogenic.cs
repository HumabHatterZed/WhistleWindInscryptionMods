using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Sporogenic() {
            const string rulebookName = "Sporogenic";
            const string rulebookDescription = "Creatures adjacent to this card gain 1 Spores at the end of its owner's turn. This sigil activates before other sigils.";
            const string dialogue = "They will love this curse like a blessing.";
            const string triggerText = "[creature] scatters spores on the adjacent cards!";
            Sporogenic.ability = AbnormalAbilityHelper.CreateAbility<Sporogenic>(
                "sigilSporogenic",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 0,
                modular: false, opponent: true, canStack: false)
                .SetAbilityRedirect("Spores", Spores.iconId, GameColors.Instance.blue)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    /// <summary>
    /// Creatures adjacent to this card gain 1 Spores at the end of its owner's turn. This sigil activates before other sigils.
    /// </summary>
    public class Sporogenic : AbilityBehaviour, IPreTurnEnd {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int Priority => int.MaxValue;
        private bool CheckValid(PlayableCard card) {
            return card != null && card.LacksAllTraits(AbnormalPlugin.SporeFriend, AbnormalPlugin.ImmuneToAilments);
        }

        public bool RespondsToPreTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public IEnumerator OnPreTurnEnd(bool playerTurnEnd) {
            PlayableCard leftCard = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, true)?.Card;
            PlayableCard rightCard = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, false)?.Card;
            bool leftValid = CheckValid(leftCard);
            bool rightValid = CheckValid(rightCard);

            if (!leftValid && !rightValid)
                yield break;

            yield return base.PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Board);

            if (leftValid)
                yield return AddSporesToCard(leftCard);
            if (rightValid)
                yield return AddSporesToCard(rightCard);

            yield return base.LearnAbility(0.4f);
        }
        private IEnumerator AddSporesToCard(PlayableCard card) {
            // apply extra Spore if this ability has stacks
            int stacks = base.Card.GetAbilityStacks(ability);
            card.Anim.LightNegationEffect();

            // add the status effect to the card and update the turn played
            yield return card.AddStatusEffect<Spores>(stacks, true);
            yield return new WaitForSeconds(0.1f);
        }
    }
}