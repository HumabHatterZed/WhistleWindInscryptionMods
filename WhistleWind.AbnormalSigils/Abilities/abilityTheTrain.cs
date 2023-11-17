using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_TheTrain()
        {
            const string rulebookName = "The Train";
            const string rulebookDescription = "When this card is played, kill all creatures on the board. Creatures killed this way do not drop bones.";
            const string dialogue = "The train boards those that don't step away from the tracks.";
            const string triggerText = "The train blows its mighty horn.";
            TheTrain.ability = AbnormalAbilityHelper.CreateAbility<TheTrain>(
                "sigilTheTrain",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 5,
                special: true).Id;
        }
    }
    public class TheTrain : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToResolveOnBoard() => true;
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnResolveOnBoard()
        {
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            AudioController.Instance.PlaySound2D("combatbell_vibrate");
            List<CardSlot> affectedSlots = Singleton<BoardManager>.Instance.AllSlotsCopy;
            affectedSlots.RemoveAll(x => x.Card == null || x.Card.HasAnyOfTraits(Trait.Giant, Trait.Uncuttable, AbnormalPlugin.ImmuneToInstaDeath));
            affectedSlots.Remove(base.Card.Slot);

            foreach (CardSlot slot in affectedSlots)
            {
                slot.Card.Anim.SetShaking(true);
            }
            yield return new WaitForSeconds(0.55f);
            foreach (CardSlot slot in affectedSlots)
            {
                slot.Card.AddTemporaryMod(new() { singletonId = "wstl:KilledByTrain" });
                yield return slot.Card.Die(false, null);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.55f);

            if (base.Card.Slot != null)
                yield return base.Card.Die(false, null);

            yield return new WaitForSeconds(0.4f);
            yield return base.LearnAbility();
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.DefaultView);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }
    }
}
