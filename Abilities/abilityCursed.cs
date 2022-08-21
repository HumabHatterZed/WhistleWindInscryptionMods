﻿using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

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
                Resources.sigilCursed, Resources.sigilCursed_pixel,
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
            if (!wasSacrifice && killer != null && !killer.Dead)
            {
                return !killer.HasAbility(Ability.MadeOfStone) && !killer.Info.HasTrait(Trait.Giant) && !killer.Info.HasTrait(Trait.Uncuttable);
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
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Did you think you were immune?", -0.65f, 0.4f, Emotion.Laughter);
            }
            yield return LearnAbility();
        }
    }
}
