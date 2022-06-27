using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Regenerator()
        {
            const string rulebookName = "Regenerator";
            const string rulebookDescription = "Adjacent cards gain 1 Health at the end of the opponent's turn.";
            const string dialogue = "Wounds heal, but the scars remain.";

            Regenerator.ability = WstlUtils.CreateAbility<Regenerator>(
                Resources.sigilRegenerator,
                rulebookName, rulebookDescription, dialogue, 3).Id;
        }
    }
    public class Regenerator : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string dialogue = "Greed leads to excessive regeneration.";

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return base.Card.Slot.IsPlayerSlot ? playerUpkeep : !playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.25f);

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(Card.Slot).Where(slot => slot.Card != null))
            {
                slot.Card.Anim.StrongNegationEffect();
                // If the target card is overhealed by 2, trigger death sequence
                if (slot.Card.Health + 2 >= slot.Card.MaxHealth)
                {
                    yield return new WaitForSeconds(0.55f);
                    // Take negative damage to simulate excessive regeneration, then die
                    for (int i = 0; i < 4; i++)
                    {
                        slot.Card.TakeDamage(-i, null);
                    }
                    yield return slot.Card.Die(false, slot.Card);
                    yield return new WaitForSeconds(0.25f);
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
