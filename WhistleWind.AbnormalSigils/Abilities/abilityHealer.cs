﻿using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core;
using WhistleWind.Core.AbilityClasses;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Healer()
        {
            const string rulebookName = "Healer";
            const string rulebookDescription = "On turn's end, heal a selected ally for 2 Health.";
            const string dialogue = "Never underestimate the importance of a healer.";
            Healer.ability = AbnormalAbilityHelper.CreateAbility<Healer>(
                Artwork.sigilHealer, Artwork.sigilHealer_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class Healer : SniperSelectSlot
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool IsDoctor => base.Card.GetComponent<PlagueDoctorClass>() != null;
        public override string NoTargetsDialogue => "No one to heal.";

        private readonly string failAsDoctorDialogue = "No allies to receive a blessing. [c:bR]An enemy[c:] will suffice instead.";
        private readonly string failExtraHardDialogue = "No enemies either. It seems no blessings will be given this turn.";

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) => base.SelectionSequence();

        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            slot.Card.Anim.LightNegationEffect();
            slot.Card.HealDamage(2);
            yield return new WaitForSeconds(0.1f);
        }

        public override IEnumerator OnPostValidTargetSelected()
        {
            if (IsDoctor)
            {
                yield return base.Card.GetComponent<PlagueDoctorClass>().TriggerBlessing();
                yield return base.Card.GetComponent<PlagueDoctorClass>().TriggerClock();
            }
            yield break;
        }

        public override IEnumerator OnNoValidTargets()
        {
            // if not Plague Doctor, simply play dialogue
            if (!IsDoctor)
            {
                yield return HelperMethods.PlayAlternateDialogue(dialogue: NoTargetsDialogue);
                yield break;
            }
            yield return HelperMethods.PlayAlternateDialogue(emotion: Emotion.Anger, dialogue: failAsDoctorDialogue);

            CardSlot randSlot;
            List<CardSlot> opposingSlots = base.Card.OpponentCard ? Singleton<BoardManager>.Instance.PlayerSlotsCopy : Singleton<BoardManager>.Instance.OpponentSlotsCopy;
            List<CardSlot> validTargets = opposingSlots.FindAll((CardSlot x) => x.Card != null && x.Card != base.Card);
            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<TurnManager>.Instance.TurnNumber;

            // If there are valid targets on the opposing side, heal a random one of their cards.
            // Else spit out another failure message then break
            if (validTargets.Count > 0)
            {
                CombatPhaseManager instance = Singleton<CombatPhaseManager>.Instance;
                WstlPart1SniperVisualiser visualiser = null;
                if ((SaveManager.SaveFile?.IsPart1).GetValueOrDefault())
                {
                    visualiser = instance.GetComponent<WstlPart1SniperVisualiser>() ?? instance.gameObject.AddComponent<WstlPart1SniperVisualiser>();
                }
                randSlot = validTargets[SeededRandom.Range(0, validTargets.Count, randomSeed)];
                instance.VisualizeConfirmSniperAbility(randSlot);
                visualiser?.VisualizeConfirmSniperAbility(randSlot);
                yield return new WaitForSeconds(0.25f);
                randSlot.Card.HealDamage(2);
                randSlot.Card.Anim.StrongNegationEffect();
                instance.VisualizeClearSniperAbility();
                visualiser?.VisualizeClearSniperAbility();

                yield return base.Card.GetComponent<PlagueDoctorClass>().TriggerBlessing();

                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return HelperMethods.PlayAlternateDialogue(Emotion.Anger, dialogue: failExtraHardDialogue);
                yield break;
            }

            // return
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

            // Call the Clock if an opponent is healed
            yield return base.Card.GetComponent<PlagueDoctorClass>().TriggerClock();
        }
    }
}
