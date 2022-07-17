using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_TheTrain()
        {
            const string rulebookName = "The Train";
            const string rulebookDescription = "Pay 12 bones to kill all cards on the board, including this card. Cards killed this way do not drop bones.";
            const string dialogue = "The train boards those that don't step away from the tracks.";

            TheTrain.ability = AbilityHelper.CreateActivatedAbility<TheTrain>(
                Resources.sigilTheTrain, Resources.sigilTheTrain_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 5).Id;
        }
    }
    public class TheTrain : ActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override int BonesCost => 12;

        public override bool CanActivate()
        {
            var validCards = Singleton<BoardManager>.Instance.AllSlotsCopy.Where((CardSlot s) => s.Card != null).Count();
            return validCards > 1;
        }

        // Kills all cards on the board after a li'l sequence
        public override IEnumerator Activate()
        {
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            AudioController.Instance.PlaySound2D("combatbell_vibrate");
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null))
            {
                if (slot.Card != base.Card)
                {
                    slot.Card.Anim.SetShaking(true);
                }
            }
            yield return new WaitForSeconds(0.55f);
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null))
            {
                if (slot.Card != base.Card)
                {
                    yield return slot.Card.Info.SetExtendedProperty("wstl:KilledByTrain", true);
                    yield return slot.Card.Die(false, base.Card);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return new WaitForSeconds(0.5f);
            yield return base.Card.Info.SetExtendedProperty("wstl:KilledByTrain", true);
            yield return base.Card.Die(false, base.Card);
            yield return new WaitForSeconds(0.4f);
            yield return base.LearnAbility();
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.DefaultView);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }
    }
}
