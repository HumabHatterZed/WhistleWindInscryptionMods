using APIPlugin;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Cursed()
        {
            const string rulebookName = "Cursed";
            const string rulebookDescription = "When a card bearing this sigil dies, the killer transforms into this card.";
            const string dialogue = "The curse continues unabated.";
            return WstlUtils.CreateAbility<Cursed>(
                Resources.sigilCursed,
                rulebookName, rulebookDescription, dialogue, 0);
        }
    }
    public class Cursed : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return !wasSacrifice;
        }

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            if (killer != null)
            {
                yield return PreSuccessfulTriggerSequence();

                Singleton<ViewManager>.Instance.SwitchToView(View.Board, immediate: false, lockAfter: true);
                yield return new WaitForSeconds(0.15f);
                killer.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.15f);
                yield return killer.TransformIntoCard(this.Card.Info);
                yield return new WaitForSeconds(0.4f);
                if (!PersistentValues.HasSeenBeautyTransform && killer.Slot.IsPlayerSlot)
                {
                    PersistentValues.HasSeenBeautyTransform = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Did you think you were immune?", -0.65f, 0.4f, Emotion.Laughter);
                }
                yield return LearnAbility(0.4f);
            }
        }
    }
}
