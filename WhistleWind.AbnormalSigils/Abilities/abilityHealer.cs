using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Patches;
using WhistleWind.AbnormalSigils.Properties;

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
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Healer : SniperSelectSlot
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool IsDoctor => base.Card.Info.name == "wstl_plagueDoctor";
        public override string NoAlliesDialogue => "No one to heal.";
        public override string SelfTargetDialogue => "You must choose one of your other cards to heal.";
        public override string NullTargetDialogue => "You can't heal the air.";

        private readonly string failAsDoctorDialogue = "No allies to receive a blessing. [c:bR]An enemy[c:] will suffice instead.";
        private readonly string failExtraHardDialogue = "No enemies either. It seems no blessings will be given this turn.";

        public bool TriggerBless;
        public bool TriggerClock;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;

        public override IEnumerator OnTurnEnd(bool playerTurnEnd) => base.SelectionSequence();

        public override IEnumerator OnValidTargetSelected(PlayableCard card)
        {
            card.HealDamage(2);
            card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.2f);
        }

        public override IEnumerator OnPostValidTargetSelected()
        {
            if (IsDoctor && base.Card.TriggerHandler.RespondsToTrigger(Trigger.AttackEnded))
            {
                TriggerBless = true;
                TriggerClock = true;
                yield return base.Card.TriggerHandler.OnTrigger(Trigger.AttackEnded);
                TriggerBless = false;
                TriggerClock = false;
            }
            yield break;
        }

        public override IEnumerator OnNoValidAllies()
        {
            // if not Plague Doctor, simply play dialogue
            if (!IsDoctor)
            {
                yield return AbnormalCustomMethods.PlayAlternateDialogue(dialogue: NoAlliesDialogue);
                yield break;
            }
            yield return AbnormalCustomMethods.PlayAlternateDialogue(dialogue: failAsDoctorDialogue);

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
                TriggerBless = true;
                yield return base.Card.TriggerHandler.OnTrigger(Trigger.AttackEnded);
                TriggerBless = false;
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return AbnormalCustomMethods.PlayAlternateDialogue(Emotion.Anger, dialogue: failExtraHardDialogue);
                yield break;
            }

            // return
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

            // Call the Clock if an opponent is healed
            TriggerClock = true;
            yield return base.Card.TriggerHandler.OnTrigger(Trigger.AttackEnded);
            TriggerClock = false;
        }
    }
}
