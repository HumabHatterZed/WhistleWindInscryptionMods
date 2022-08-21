using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Regenerator()
        {
            const string rulebookName = "Regenerator";
            const string rulebookDescription = "Adjacent cards gain 1 Health on upkeep.";
            const string dialogue = "Wounds heal, but the scars remain.";

            Regenerator.ability = AbilityHelper.CreateAbility<Regenerator>(
                Resources.sigilRegenerator, Resources.sigilRegenerator_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: true, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Regenerator : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string dialogue = "You got greedy with your beast's [c:bR]Health[c:].";
        private readonly string dragonDialogue = "Now you become the sky, and I the land.";
        private readonly string dragonDialogue2 = "When the end became the beginning, [c:bR]the dragon[c:] soared through the sky toward the unknown.";
        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return base.Card.OpponentCard != playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            yield return PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(Card.Slot).Where(slot => slot.Card != null))
            {
                slot.Card.Anim.LightNegationEffect();
                // If the target card is overhealed by 2, trigger death sequence
                if (slot.Card.Health >= slot.Card.MaxHealth + 2)
                {
                    if (slot.Card.FaceDown)
                    {
                        slot.Card.SetFaceDown(false);
                        slot.Card.UpdateFaceUpOnBoardEffects();
                    }
                    yield return new WaitForSeconds(0.55f);
                    // Take negative damage to simulate excessive regeneration, then die
                    for (int i = 0; i < 4; i++)
                    {
                        yield return slot.Card.TakeDamage(-i - 1, null);
                        yield return new WaitForSeconds(0.2f);
                    }
                    yield return slot.Card.Die(false, slot.Card);
                    yield return new WaitForSeconds(0.25f);
                    if (!WstlSaveManager.HasSeenRegeneratorExplode)
                    {
                        WstlSaveManager.HasSeenRegeneratorExplode = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f);
                    }
                }
                else
                {
                    slot.Card.HealDamage(1);
                    yield return new WaitForSeconds(0.3f);
                    yield return LearnAbility();
                }
            }
        }

        // Code for Yin-Yang
        public override bool RespondsToResolveOnBoard()
        {
            if (base.Card.Info.name == "wstl_yang")
            {
                return true;
            }
            return false;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(s => s != null && s.Card != null))
            {
                if (slot.Card.Info.name == "wstl_yin")
                {
                    yield return DragonSequence(slot.Card);
                    break;
                }
            }
        }
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            if (base.Card.Info.name == "wstl_yang")
            {
                return otherCard.Info.name == "wstl_yin";
            }
            return false;
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(s => s != null && s.Card != null))
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
            if (!WstlSaveManager.HasSeenDragon)
            {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dragonDialogue, -0.65f, 0.4f);
            }
            base.Card.RemoveFromBoard(true);
            yield return new WaitForSeconds(0.1f);
            card.RemoveFromBoard(true);
            yield return new WaitForSeconds(0.5f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            yield return new WaitForSeconds(0.2f);
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy)
            {
                if (slot.Card != null)
                {
                    yield return slot.Card.DieTriggerless();
                }
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName("wstl_yinYangHead"),slot);
            }
            yield return new WaitForSeconds(0.4f);
            if (!WstlSaveManager.HasSeenDragon)
            {
                WstlSaveManager.HasSeenDragon = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dragonDialogue2, -0.65f, 0.4f);
                yield return new WaitForSeconds(0.2f);
            }
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
                {
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("The end at the beginning.", -0.65f, 0.4f);
                }
                else
                {
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("The beginning at the end.", -0.65f, 0.4f);
                }
            }
            else
            {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Everything is equal. Everything is as it should be.", -0.65f, 0.4f);
            }
            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }
    }
}
