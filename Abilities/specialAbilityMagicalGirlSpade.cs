using System.Linq;
using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewSpecialAbility SpecialAbility_MagicalGirlSpade()
        {
            const string rulebookName = "Spade";
            const string rulebookDescription = "Adjacent cards take 1 less damage. Will transform when they die.";
            return WstlUtils.CreateSpecialAbility<MagicalGirlSpade>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class MagicalGirlSpade : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;

        private CardModificationInfo mod = new CardModificationInfo(0, 1);

        private readonly string protectDialogue = "The knight softens the oncoming blows.";

        private readonly string transformDialogue = "Having failed to protect again, the knight fell into despair.";

        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Spade");
            }
        }

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            CardSlot thisSlot = null;
            foreach (var cardSlot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(cardSlot => cardSlot && cardSlot.Card == base.Card))
            {
                if (cardSlot != null)
                {
                    thisSlot = cardSlot;
                }
            }
            if (thisSlot != null)
            {
                foreach (CardSlot cardSlot in Singleton<BoardManager>.Instance.GetAdjacentSlots(thisSlot).Where(cardSlot => cardSlot.Card != null))
                {
                    if (cardSlot.Card == slot.Card)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            base.Card.Anim.StrongNegationEffect();
            if (!PersistentValues.HasSeenDespairProtect)
            {
                PersistentValues.HasSeenDespairProtect = true;
                yield return new WaitForSeconds(0.4f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(protectDialogue, -0.65f, 0.4f);
            }
            yield return new WaitForSeconds(0.25f);
            slot.Card.AddTemporaryMod(mod);
        }
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            CardSlot thisSlot = null;
            foreach (var slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot && slot.Card == base.Card))
            {
                if (slot != null)
                {
                    thisSlot = slot;
                }
            }
            if (thisSlot != null)
            {
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(thisSlot).Where(slot => slot.Card != null))
                {
                    if (slot.Card == card)
                    {
                        return fromCombat;
                    }
                }
            }
            return false;
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return new WaitForSeconds(0.15f);
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.15f);
            CardInfo cardByName = CardLoader.GetCardByName("wstl_knightOfDespair");
            yield return base.PlayableCard.TransformIntoCard(cardByName);
            yield return new WaitForSeconds(0.5f);
            if (!PersistentValues.HasSeenDespairTransformation)
            {
                PersistentValues.HasSeenDespairTransformation = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(transformDialogue, -0.65f, 0.4f);
            }
            yield return new WaitForSeconds(0.25f);
        }
        public bool ActivateOnPlay()
        {
            int num = 0;
            CardSlot thisSlot = null;
            foreach (var slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot && slot.Card == base.Card))
            {
                if (slot != null)
                {
                    thisSlot = slot;
                }
            }
            if (thisSlot != null)
            {
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(thisSlot).Where(slot => slot.Card != null))
                {
                    num++;
                }
            }
            return num > 0;
        }

    }
}
