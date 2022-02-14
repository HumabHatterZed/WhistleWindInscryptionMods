using APIPlugin;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Regenerator()
        {
            const string rulebookName = "Regenerator";
            const string rulebookDescription = "Adjacent cards gain 1 Health at the end of the opponent's turn.";
            const string dialogue = "Wounds heal, but the scars remain.";

            return WstlUtils.CreateAbility<Regenerator>(
                Resources.sigilRegenerator,
                rulebookName, rulebookDescription, dialogue, 3);
        }
    }
    public class Regenerator : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string dialogue = "";

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (!Card.Slot.IsPlayerSlot)
            {
                return playerTurnEnd;
            }
            return !playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.25f);

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(Card.Slot))
            {
                if (slot.Card != null)
                {
                    slot.Card.Anim.StrongNegationEffect();
                    if (slot.Card.Health + 2 >= slot.Card.MaxHealth)
                    {
                        yield return new WaitForSeconds(0.55f);
                        yield return slot.Card.Die(false, slot.Card);
                        yield return new WaitForSeconds(0.25f);
                        yield return slot.Card.Slot.opposingSlot.Card.TakeDamage(10, slot.Card);
                        foreach (CardSlot slots in Singleton<BoardManager>.Instance.GetAdjacentSlots(slot.Card.Slot))
                        {
                            if (slots.Card != null)
                            {
                                yield return slots.Card.TakeDamage(10, slot.Card);
                            }
                        }
                        yield return new WaitForSeconds(0.4f);
                        if (!PersistentValues.HasSeenRegeneratorExplode)
                        {
                            PersistentValues.HasSeenRegeneratorExplode = true;
                            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f);
                        }
                    }
                    else
                    {
                        slot.Card.HealDamage(1);
                        yield return new WaitForSeconds(0.4f);
                        yield return LearnAbility(0.4f);
                    }
                }
            }
        }
    }
}
