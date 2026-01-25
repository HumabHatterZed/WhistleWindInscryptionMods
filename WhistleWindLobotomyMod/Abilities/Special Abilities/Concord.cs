using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public class Concord : SpecialCardBehaviour {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Concord";
        public const string rDesc = "When Yang is adjacent to Yin, kill all cards on the board then invert the scales.";
        public override bool RespondsToResolveOnBoard() => true;
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) => otherCard.Info.name == Cards.yin;
        public override IEnumerator OnResolveOnBoard() {
            List<PlayableCard> cards = base.PlayableCard.Slot.GetAdjacentCards();
            PlayableCard yin = cards.Find(x => x.Info.name == Cards.yin);
            if (yin != null) {
                yield return DragonSequence(yin);
            }
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard) {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.PlayableCard.Slot).Where(s => s != null && s.Card != null)) {
                if (slot.Card == otherCard) {
                    yield return DragonSequence(otherCard);
                    break;
                }
            }
            yield break;
        }
        private IEnumerator DragonSequence(PlayableCard card) {
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            bool canInitiateCombat = LobotomyHelpers.AllowInitiateCombat(false);
            yield return new WaitForSeconds(0.2f);
            base.PlayableCard.Anim.LightNegationEffect();
            card.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.2f);

            yield return DialogueHelper.PlayDialogueEvent("YinDragonIntro");

            base.PlayableCard.RemoveFromBoard();
            card.RemoveFromBoard();

            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            yield return new WaitForSeconds(0.5f);

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy) {
                if (slot.Card != null) {
                    slot.Card.Info.SetExtendedProperty("wstl:NoBones", true);
                    yield return slot.Card.DieTriggerless();
                }

                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName(Cards.yinYangHead), slot);
            }
            yield return new WaitForSeconds(0.66f);

            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            List<CardSlot> reverseSlots = Singleton<BoardManager>.Instance.AllSlotsCopy;
            reverseSlots.Reverse();

            foreach (CardSlot slot in reverseSlots) {
                if (slot.Card != null) {
                    slot.Card.RemoveFromBoard();
                    yield return new WaitForSeconds(0.05f);
                }
            }
            yield return new WaitForSeconds(0.66f);

            int balance = Singleton<LifeManager>.Instance.Balance * -2;
            int damageToDeal = Mathf.Abs(balance);

            Singleton<CombatPhaseManager>.Instance.DamageDealtThisPhase = damageToDeal;

            if (damageToDeal > 0) {
                bool isNegative = balance < 0;

                yield return Singleton<LifeManager>.Instance.ShowDamageSequence(damageToDeal, 1, toPlayer: isNegative);
                yield return new WaitForSeconds(0.5f);

                if (isNegative)
                    yield return DialogueHelper.PlayAlternateDialogue(dialogue: "The end at the beginning.");
                else
                    yield return DialogueHelper.PlayAlternateDialogue(dialogue: "The beginning at the end.");
            }
            else {
                Singleton<ViewManager>.Instance.SwitchToView(View.Scales);
                yield return new WaitForSeconds(0.5f);
                yield return DialogueHelper.PlayAlternateDialogue(dialogue: "Everything is equal. Everything is as it should be.");
            }

            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.4f);

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            LobotomyHelpers.AllowInitiateCombat(canInitiateCombat);
        }
    }
    public class RulebookEntryConcord : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class Abilities {
        private static void Rulebook_Concord()
            => RulebookEntryConcord.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryConcord>(Concord.rName, Concord.rDesc).Id;
        private static void AddSpecial_Concord()
            => Concord.specialAbility = AbilityHelper.CreateSpecialAbility<Concord>(LobotomyPlugin.pluginGuid, Concord.rName).Id;
    }
}
