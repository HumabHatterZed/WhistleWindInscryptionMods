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
        private NewAbility Ability_Hunter()
        {
            const string rulebookName = "Hunter";
            const string rulebookDescription = "You may choose which opposing space a card bearing this sigil strikes.";
            const string dialogue = "Your beast strikes with precision.";

            return WstlUtils.CreateAbility<Hunter>(
                Resources.sigilHunter,
                rulebookName, rulebookDescription, dialogue, 3);
        }
    }
    public class Hunter : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string freischutzDialogue = "The Devil proposed a childist contract.";
        private readonly string freischutzDialogue2 = "The seventh bullet would pierce the heart of his most beloved.";
        private readonly string freischutzDialogue3 = "On hearing this, the hunter sought and shot everyone he loved.";
        private int freischutzShots = 0;

        public override bool RespondsToResolveOnBoard()
        {
            return true;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Status.hiddenAbilities.Add(Ability.Sniper);
            base.Card.AddTemporaryMod(new CardModificationInfo(Ability.Sniper));
        }

        public override bool RespondsToDealDamage(int amount, PlayableCard target)
        {
            return amount > 0;
        }
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();

            if (base.Card.Info.name.ToLowerInvariant().Contains("judgementbird"))
            {
                if (target.Health > 0)
                {
                    target.Anim.PlaySacrificeSound();
                    target.Anim.SetSacrificeHoverMarkerShown(shown: true);
                    yield return new WaitForSeconds(0.2f);
                    target.Anim.SetShaking(shaking: true);
                    yield return new WaitForSeconds(0.4f);
                    target.Anim.PlaySacrificeSound();
                    target.Anim.PlaySacrificeParticles();
                    target.Anim.DeactivateSacrificeHoverMarker();
                    yield return target.Die(false, base.Card);
                }
            }
            if (base.Card.Info.name.ToLowerInvariant().Contains("derfreischutz"))
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
            yield return base.PreSuccessfulTriggerSequence();

            if (base.Card.Info.name.ToLowerInvariant().Contains("derfreischutz"))
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
    }
}
