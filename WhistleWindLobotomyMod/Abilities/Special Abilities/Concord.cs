using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Concord : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Concord";
        public static readonly string rDesc = "When Yang is adjacent to Yin, activate a special sequence.";
        public override bool RespondsToResolveOnBoard() => true;
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) => otherCard.Info.name == "wstl_yin";
        public override IEnumerator OnResolveOnBoard()
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.PlayableCard.Slot).Where(s => s.Card != null))
            {
                if (slot.Card.Info.name == "wstl_yin")
                {
                    yield return DragonSequence(slot.Card);
                    break;
                }
            }
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.PlayableCard.Slot).Where(s => s != null && s.Card != null))
            {
                if (slot.Card == otherCard)
                {
                    yield return DragonSequence(otherCard);
                    break;
                }
            }
            yield break;
        }
        private IEnumerator DragonSequence(PlayableCard card)
        {
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;

            yield return new WaitForSeconds(0.5f);
            yield return DialogueEventsManager.PlayDialogueEvent("YinDragonIntro");

            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            yield return new WaitForSeconds(0.2f);
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy)
            {
                if (slot.Card != null)
                    yield return slot.Card.DieTriggerless();

                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName("wstl_yinYangHead"), slot);
            }
            yield return new WaitForSeconds(0.4f);

            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy)
            {
                if (slot.Card != null)
                {
                    slot.Card.RemoveFromBoard();
                    yield return new WaitForSeconds(0.05f);
                }
            }
            yield return new WaitForSeconds(0.5f);

            int balance = Singleton<LifeManager>.Instance.Balance * -2;
            int damageToDeal = Mathf.Abs(balance);
            bool isNegative = balance < 0;

            Singleton<CombatPhaseManager>.Instance.DamageDealtThisPhase = damageToDeal;
            if (damageToDeal != 0)
            {
                yield return Singleton<LifeManager>.Instance.ShowDamageSequence(damageToDeal, 1, toPlayer: isNegative);
                yield return new WaitForSeconds(0.5f);

                if (isNegative)
                    yield return AbnormalMethods.PlayAlternateDialogue(dialogue: "The end at the beginning.");
                else
                    yield return AbnormalMethods.PlayAlternateDialogue(dialogue: "The beginning at the end.");
            }
            else
                yield return AbnormalMethods.PlayAlternateDialogue(dialogue: "Everything is equal. Everything is as it should be.");
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }
    }
    public class RulebookEntryConcord : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_Concord()
        {
            RulebookEntryConcord.ability = AbilityHelper.CreateRulebookAbility<RulebookEntryConcord>(Concord.rName, Concord.rDesc).Id;
        }
        private void SpecialAbility_Concord()
        {
            Concord.specialAbility = AbilityHelper.CreateSpecialAbility<Concord>(Concord.rName).Id;
        }
    }
}
