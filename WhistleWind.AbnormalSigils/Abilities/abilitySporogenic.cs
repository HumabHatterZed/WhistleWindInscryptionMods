using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Sporogenic()
        {
            const string rulebookName = "Sporogenic";
            const string rulebookDescription = "Creatures adjacent to this card gain 1 Spores at the end of its owner's turn. This sigil activates before other sigils.";
            const string dialogue = "They will love this curse like a blessing.";
            const string triggerText = "[creature] scatters spores on the adjacent cards!";
            Sporogenic.ability = AbnormalAbilityHelper.CreateAbility<Sporogenic>(
                "sigilSporogenic",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: false, opponent: true, canStack: true).Id;
        }
    }
    public class Sporogenic : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool CheckValid(PlayableCard card)
        {
            if (card != null)
                return card.LacksTrait(AbnormalPlugin.SporeFriend) && card.GetComponent<Spores>() == null;
            return false;
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
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

            base.LearnAbility(0.4f);
        }
        private IEnumerator AddSporesToCard(PlayableCard card)
        {
            // apply extra Spore if this ability has stacks
            int extraStacks = Mathf.Max(0, base.Card.GetAbilityStacks(ability) - 1);
            card.Anim.LightNegationEffect();
            card.AddPermanentBehaviour<Spores>();
            Spores component = card.GetComponent<Spores>();
            component.turnPlayed = Singleton<TurnManager>.Instance.TurnNumber;
            component.effectCount += extraStacks;
            card.AddTemporaryMods(component.GetEffectCountMod(), component.GetEffectDecalMod());
            yield return new WaitForSeconds(0.1f);
        }
    }
}