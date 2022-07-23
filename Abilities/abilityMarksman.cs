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
        private void Ability_Hunter()
        {
            const string rulebookName = "Marksman";
            const string rulebookDescription = "You may choose which opposing space a card bearing this sigil strikes.";
            const string dialogue = "Your beast strikes with precision.";

            Marksman.ability = AbilityHelper.CreateAbility<Marksman>(
                Resources.sigilMarksman, Resources.sigilMarksman_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3).Id;
        }
    }
    public class Marksman : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool IsDevil => base.Card.Info.name.ToLowerInvariant().Contains("derfreischutz");

        private readonly string freischutzDialogue = "The Devil proposed a childist contract.";
        private readonly string freischutzDialogue2 = "The seventh bullet would pierce the heart of his most beloved.";
        private readonly string freischutzDialogue3 = "On hearing this, the hunter sought and shot everyone he loved.";
        private int freischutzShots = 0;
        public override bool RespondsToDealDamage(int amount, PlayableCard target)
        {
            return amount > 0;
        }
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            if (IsDevil)
            {
                freischutzShots++;
                if (freischutzShots >= 6)
                {
                    freischutzShots = 0;
                    yield return new WaitForSeconds(0.5f);
                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.5f);

                    if (!PersistentValues.HasSeenDerFreischutzSeventh)
                    {
                        PersistentValues.HasSeenDerFreischutzSeventh = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(freischutzDialogue, -0.65f, 0.4f);
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(freischutzDialogue2, -0.65f, 0.4f);
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(freischutzDialogue3, -0.65f, 0.4f);
                        yield return new WaitForSeconds(0.5f);
                    }

                    foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot.Card != base.Card))
                    {
                        if (slot.Card != null)
                        {
                            yield return slot.Card.TakeDamage(base.Card.Attack, base.Card);
                        }
                    }
                    freischutzShots = 0;
                }
            }

            if (!base.HasLearned)
            {
                yield return new WaitForSeconds(0.4f);
                yield return base.LearnAbility();
            }
        }

        public override bool RespondsToDealDamageDirectly(int amount)
        {
            return amount > 0;
        }
        public override IEnumerator OnDealDamageDirectly(int amount)
        {
            if (IsDevil)
            {
                yield return base.PreSuccessfulTriggerSequence();

                freischutzShots++;
                if (freischutzShots >= 6)
                {
                    freischutzShots = 0;
                    yield return new WaitForSeconds(0.5f);
                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.5f);

                    if (!PersistentValues.HasSeenDerFreischutzSeventh)
                    {
                        PersistentValues.HasSeenDerFreischutzSeventh = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(freischutzDialogue, -0.65f, 0.4f);
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(freischutzDialogue2, -0.65f, 0.4f);
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(freischutzDialogue3, -0.65f, 0.4f);
                        yield return new WaitForSeconds(0.5f);
                    }

                    foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot.Card != base.Card))
                    {
                        if (slot.Card != null)
                        {
                            yield return slot.Card.TakeDamage(base.Card.Attack, base.Card);
                        }
                    }
                    freischutzShots = 0;
                }
            }

            if (!base.HasLearned)
            {
                yield return new WaitForSeconds(0.4f);
                yield return base.LearnAbility();
            }
        }
    }
}
