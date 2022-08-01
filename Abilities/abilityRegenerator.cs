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
            const string rulebookDescription = "Adjacent cards gain 1 Health at the end of the opponent's turn.";
            const string dialogue = "Wounds heal, but the scars remain.";

            Regenerator.ability = AbilityHelper.CreateAbility<Regenerator>(
                Resources.sigilRegenerator, Resources.sigilRegenerator_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: true).Id;
        }
    }
    public class Regenerator : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string dialogue = "You got greedy with your beast's [c:br]Health[c:].";
        private readonly string dragonDialogue = "The end becomes the beginning.";
        private readonly string dragonDialogue2 = "At the end of the beginning, [c:bR]the dragon[c:] soared through the sky toward the unknown.";
        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return base.Card.OpponentCard != playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.25f);

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(Card.Slot).Where(slot => slot.Card != null))
            {
                slot.Card.Anim.StrongNegationEffect();
                // If the target card is overhealed by 2, trigger death sequence
                if (slot.Card.Health + 2 >= slot.Card.MaxHealth)
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
                        slot.Card.TakeDamage(-i, null);
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
                    yield return new WaitForSeconds(0.4f);
                    yield return LearnAbility(0.4f);
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
        }
        private IEnumerator DragonSequence(PlayableCard card)
        {
            yield return new WaitForSeconds(0.5f);
            if (!WstlSaveManager.HasSeenDragon)
            {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dragonDialogue, -0.65f, 0.4f);
            }
            RunState.Run.playerDeck.RemoveCard(base.Card.Info);
            RunState.Run.playerDeck.RemoveCard(card.Info);
            yield return CleanUpCard(base.Card);
            yield return CleanUpCard(card);
            yield return new WaitForSeconds(0.5f);
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy)
            {
                if (slot.Card != null)
                {
                    // Remove abilities that can cause a softlock
                    if ((slot.Card.HasAbility(Ability.DrawCopy) || slot.Card.HasAbility(Ability.DrawCopyOnDeath)) && slot.Card.HasAbility(Ability.CorpseEater))
                    {
                        slot.Card.Info.RemoveBaseAbility(Ability.CorpseEater);
                    }
                    if (slot.Card.HasAbility(Ability.IceCube))
                    {
                        slot.Card.Info.RemoveBaseAbility(Ability.IceCube);
                    }
                    yield return slot.Card.Die(false, null);
                }
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName("wstl_yinYangHead"),slot);
            }
            yield return new WaitForSeconds(0.4f);
            if (!WstlSaveManager.HasSeenDragon)
            {
                WstlSaveManager.HasSeenDragon = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dragonDialogue2, -0.65f, 0.4f);
            }
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy)
            {
                if (slot.Card != null)
                {
                    yield return CleanUpCard(slot.Card);
                }
            }
            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
        }
        private IEnumerator CleanUpCard(PlayableCard item)
        {
            item.UnassignFromSlot();
            SpecialCardBehaviour[] components = item.GetComponents<SpecialCardBehaviour>();
            for (int i = 0; i < components.Length; i++)
            {
                components[i].OnCleanUp();
            }
            item.ExitBoard(0.3f, Vector3.zero);
            yield break;
        }
    }
}
