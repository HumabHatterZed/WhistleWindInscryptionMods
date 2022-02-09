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
        private NewSpecialAbility SpecialAbility_Despair()
        {
            const string rulebookName = "Despair";
            const string rulebookDescription = "Adjacent cards take 1 less damage. Transforms when they die.";
            return WstlUtils.CreateSpecialAbility<MagicalGirlSpade>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class MagicalGirlSpade : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Despair");
            }
        }

        private readonly string protectDialogue = "The knight softens the oncoming blows.";
        private readonly string transformDialogue = "Having failed to protect again, the knight fell into despair.";

        public override bool RespondsToOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            if (!attacker.Dead)
            {
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.PlayableCard.Slot).Where(slot => slot.Card != null))
                {
                    if (slot.Card == target)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override IEnumerator OnOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            base.Card.Anim.StrongNegationEffect();
            if (!PersistentValues.HasSeenDespairProtect)
            {
                PersistentValues.HasSeenDespairProtect = true;
                yield return new WaitForSeconds(0.4f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(protectDialogue, -0.65f, 0.4f);
            }
            yield return target.Status.damageTaken -= 1;
            yield return new WaitForSeconds(0.25f);
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.PlayableCard.Slot).Where(slot => slot.Card != null))
            {
                if (slot.Card == card)
                {
                    return fromCombat;
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
    }
}
