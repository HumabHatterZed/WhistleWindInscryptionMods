using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class TicketTaker : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public const string rName = "Ticket Taker";
        public const string rDesc = "When Express Train to Hell has been on the board for 4 turns, kill all cards on the board. Opponent cards will also drop bones.";

        private int turnCount;

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            if (base.PlayableCard.OnBoard)
                return base.PlayableCard.OpponentCard != playerUpkeep;

            return false;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            turnCount++;
            if (turnCount >= 4)
            {
                turnCount = 0;
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.55f);
                Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
                yield return DialogueHelper.PlayDialogueEvent("ExpressHellTrainWipe", 0f);
                yield return new WaitForSeconds(0.4f);
                AudioController.Instance.PlaySound2D("combatbell_vibrate");
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null))
                {
                    if (slot.Card != base.Card)
                        slot.Card.Anim.SetShaking(true);
                }
                yield return new WaitForSeconds(0.55f);
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null))
                {
                    if (slot.Card != base.Card)
                    {
                        yield return slot.Card.Die(false, null);
                        if (slot.Card.OpponentCard)
                            yield return Singleton<ResourcesManager>.Instance.AddBones(1, slot);
                        yield return new WaitForSeconds(0.1f);
                    }
                }
                yield return new WaitForSeconds(0.5f);
                yield return base.PlayableCard.Die(false, null);
                yield return new WaitForSeconds(0.4f);
                Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.DefaultView);
                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
                yield return new WaitForSeconds(0.2f);
            }
            yield break;
        }
    }
    public class RulebookEntryTicketTaker : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_TicketTaker()
            => RulebookEntryTicketTaker.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryTicketTaker>(TicketTaker.rName, TicketTaker.rDesc).Id;
        private void SpecialAbility_TicketTaker()
            => TicketTaker.specialAbility = AbilityHelper.CreateSpecialAbility<TicketTaker>(pluginGuid, TicketTaker.rName).Id;
    }
}
