using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Cursed()
        {
            const string rulebookName = "Cursed";
            const string rulebookDescription = "When a card bearing this sigil dies, the killer transforms into this card.";
            const string dialogue = "The curse continues unabated.";
            Cursed.ability = AbilityHelper.CreateAbility<Cursed>(
                Artwork.sigilCursed, Artwork.sigilCursed_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: true, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Cursed : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            if (!wasSacrifice && killer != null && !killer.Dead && killer.Health != 0)
            {
                return !killer.HasAbility(Ability.MadeOfStone) && !killer.Info.HasAnyOfTraits(Trait.Giant, Trait.Uncuttable);
            }
            return false;
        }

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return PreSuccessfulTriggerSequence();

            Singleton<ViewManager>.Instance.SwitchToView(View.Board, immediate: false, lockAfter: true);
            yield return new WaitForSeconds(0.15f);
            killer.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            yield return killer.TransformIntoCard(this.Card.Info);
            yield return new WaitForSeconds(0.4f);
            if (!WstlSaveManager.HasSeenBeautyTransform && killer.Slot.IsPlayerSlot)
            {
                WstlSaveManager.HasSeenBeautyTransform = true;
                yield return CustomMethods.PlayAlternateDialogue(Emotion.Laughter, dialogue: "The lie falls apart, revealing your pitiful true self.");
            }
            yield return LearnAbility();
        }
    }
}
