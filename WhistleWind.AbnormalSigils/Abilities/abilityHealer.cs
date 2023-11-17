using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using InscryptionCommunityPatch.Card;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.AbilityClasses;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Healer()
        {
            const string rulebookName = "Healer";
            const string rulebookDescription = "At the end of your turn, you may choose one of your other cards to heal by 2 Health.";
            const string dialogue = "Never underestimate the importance of a healer.";
            const string triggerText = "[creature] heals the chosen creature!";
            Healer.ability = AbnormalAbilityHelper.CreateAbility<Healer>(
                "sigilHealer",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class Healer : SniperSelectSlot
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override string NoTargetsDialogue => "No one to heal.";
        public override bool IsPositiveEffect => true;
        public override List<CardSlot> InitialTargets => BoardManager.Instance.GetSlotsCopy(!base.Card.OpponentCard);
        private PlagueDoctorClass DoctorComponent => base.Card.GetComponent<PlagueDoctorClass>();

        private const string failAsDoctorDialogue = "No allies to receive a blessing. [c:bR]An enemy[c:] will suffice instead.";
        private const string failExtraHardDialogue = "No enemies either. It seems no blessings will be given this turn.";

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) => base.SelectionSequence();

        public override IEnumerator OnValidTargetSelected(CardSlot slot) => HealCard(slot);
        public override IEnumerator OnPostValidTargetSelected()
        {
            if (DoctorComponent != null)
            {
                yield return DoctorComponent.TriggerBlessing();
                yield return DoctorComponent.TriggerClock();
            }
            yield break;
        }

        public override IEnumerator OnNoValidTargets()
        {
            // if not Plague Doctor, simply play dialogue
            if (DoctorComponent.SafeIsUnityNull())
            {
                yield return DialogueHelper.PlayAlternateDialogue(dialogue: NoTargetsDialogue);
                yield break;
            }
            yield return DialogueHelper.PlayAlternateDialogue(emotion: Emotion.Anger, dialogue: failAsDoctorDialogue);

            CardSlot randSlot;
            List<CardSlot> opposingSlots = BoardManager.Instance.GetSlotsCopy(base.Card.OpponentCard);
            List<CardSlot> validTargets = opposingSlots.FindAll((CardSlot x) => x.Card != null && x.Card != base.Card);
            int randomSeed = base.GetRandomSeed();

            // If there are valid targets on the opposing side, heal a random one of their cards.
            // Else spit out another failure message then break
            if (validTargets.Count > 0)
            {
                CombatPhaseManager instance = Singleton<CombatPhaseManager>.Instance;
                Part1SniperVisualizer visualiser = null;
                if ((SaveManager.SaveFile?.IsPart1).GetValueOrDefault())
                {
                    visualiser = instance.GetComponent<Part1SniperVisualizer>() ?? instance.gameObject.AddComponent<Part1SniperVisualizer>();
                }
                randSlot = validTargets[SeededRandom.Range(0, validTargets.Count, randomSeed++)];
                instance.VisualizeConfirmSniperAbility(randSlot);
                visualiser?.VisualizeConfirmSniperAbility(randSlot);
                yield return new WaitForSeconds(0.25f);
                yield return HealCard(randSlot);
                instance.VisualizeClearSniperAbility();
                visualiser?.VisualizeClearSniperAbility();

                yield return DoctorComponent?.TriggerBlessing();

                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayAlternateDialogue(Emotion.Anger, dialogue: failExtraHardDialogue);
                yield break;
            }

            // return
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

            // Call the Clock if an opponent is healed
            yield return DoctorComponent?.TriggerClock();
        }

        private IEnumerator HealCard(CardSlot slot)
        {
            bool faceDown = slot.Card.FaceDown;
            yield return slot.Card.FlipFaceUp(faceDown);
            slot.Card.Anim.LightNegationEffect();
            slot.Card.HealDamage(2);
            yield return new WaitForSeconds(0.1f);
            yield return slot.Card.FlipFaceDown(faceDown);
            if (faceDown)
                yield return new WaitForSeconds(0.4f);
        }
    }
}
