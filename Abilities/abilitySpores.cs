using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Spores()
        {
            const string rulebookName = "Spores";
            const string rulebookDescription = "Adjacent cards gain 1 Spore and take damage equal to their Spore at the end of each turn. If a card with Spore is killed, create a Spore Mold Creature in that card's slot whose stats are equal to the card's Spore.";
            const string dialogue = "Even if this turns to be a curse, they will love this curse like a blessing.";
            Spores.ability = WstlUtils.CreateAbility<Spores>(
                Resources.sigilSpores,
                rulebookName, rulebookDescription, dialogue, 2).Id;
        }
    }
    public class Spores : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (base.Card != null)
            {
                return base.Card.OpponentCard != playerTurnEnd;
            }
            return false;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            CardSlot toLeft = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: true);
            CardSlot toRight = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: false);
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            yield return AddSpore(toLeft, toRight);
        }
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return fromCombat && card.Info.GetExtendedPropertyAsInt("wstl:Spore") != null;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            int spores = card.Info.GetExtendedPropertyAsInt("wstl:Spore") != null ? (int)card.Info.GetExtendedPropertyAsInt("wstl:Spore") : 0;
            if (spores < 1)
            {
                yield break;
            }
            CardInfo minion = CardLoader.GetCardByName("wstl_theLittlePrinceMinion");
            minion.Mods.Add(new(spores, spores));

            yield return base.PreSuccessfulTriggerSequence();
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(minion, deathSlot, 0.15f);
        }
        private IEnumerator AddSpore(CardSlot toLeft, CardSlot toRight)
        {
            bool validLeft = toLeft != null && toLeft.Card != null && toLeft.Card.Info.name != "wstl_theLittlePrinceMinion";
            bool validRight = toRight != null && toRight.Card != null && toRight.Card.Info.name != "wstl_theLittlePrinceMinion";
            if (validLeft || validRight)
            {
                yield return base.PreSuccessfulTriggerSequence();
            }
            else
            {
                yield break;
            }
            if (validLeft)
            {
                int sporeLeft = toLeft.Card.Info.GetExtendedPropertyAsInt("wstl:Spore") != null ? (int)toLeft.Card.Info.GetExtendedPropertyAsInt("wstl:Spore") : 0;
                yield return toLeft.Card.Info.SetExtendedProperty("wstl:Spore", sporeLeft + 1);
                toLeft.Card.Anim.StrongNegationEffect();

            }
            if (validRight)
            {
                int sporeRight = toRight.Card.Info.GetExtendedPropertyAsInt("wstl:Spore") != null ? (int)toRight.Card.Info.GetExtendedPropertyAsInt("wstl:Spore") : 0;
                yield return toRight.Card.Info.SetExtendedProperty("wstl:Spore", sporeRight + 1);
                toRight.Card.Anim.StrongNegationEffect();
            }
            yield return new WaitForSeconds(0.4f);
            if (validLeft)
            {
                yield return toLeft.Card.TakeDamage((int)toLeft.Card.Info.GetExtendedPropertyAsInt("wstl:Spore"), null);
            }
            if (validRight)
            {
                yield return toRight.Card.TakeDamage((int)toRight.Card.Info.GetExtendedPropertyAsInt("wstl:Spore"), null);
            }
            if (validLeft || validRight)
            {
                yield return base.LearnAbility();
            }
        }
    }
}