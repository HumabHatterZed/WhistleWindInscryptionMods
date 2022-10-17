using InscryptionAPI;
using InscryptionAPI.Nodes;
using InscryptionAPI.Encounters;
using DiskCardGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace WhistleWindLobotomyMod.Core
{
    public static class CustomExtensions
    {
        /// <summary>
        /// Checks the Card for the number of stacks of the given ability it has.
        /// </summary>
        /// <param name="ability">Ability to check for.</param>
        /// <returns>Number of stacks of the given ability.</returns>
        public static int GetAbilityStacks(this PlayableCard card, Ability ability)
        {
            int num = 0;
            foreach (Ability item in card.Info.Abilities)
                if (item == ability)
                    num++;
            return num;
        }
        /// <summary>
        /// Checks the Card for any of the given abilities.
        /// </summary>
        /// <param name="abilities">Abilities to check for.</param>
        /// <returns>True if any of the given abilities are present and aren't negated.</returns>
        public static bool HasAnyOfAbilities(this PlayableCard card, params Ability[] abilities)
        {
            foreach (Ability item in abilities)
                if (card.Info.HasAbility(item) || card.temporaryMods.Exists((CardModificationInfo x) => x.abilities.Contains(item)))
                    if (!card.temporaryMods.Exists((CardModificationInfo x) => x.negateAbilities.Contains(item)))
                        return true;
            return false;
        }
        /// <summary>
        /// Checks the CardInfo for all of the given traits.
        /// </summary>
        /// <param name="traits">Traits to check for.</param>
        /// <returns>True if all of the given traits are present.</returns>
        public static bool HasAllOfTraits(this CardInfo info, params Trait[] traits)
        {
            foreach (Trait item in traits)
                if (!info.traits.Contains(item))
                    return false;
            return true;
        }
        /// <summary>
        /// Checks the CardInfo for any of the given traits.
        /// </summary>
        /// <param name="traits">Traits to check for.</param>
        /// <returns>True if any of the given traits are present.</returns>
        public static bool HasAnyOfTraits(this CardInfo info, params Trait[] traits)
        {
            foreach (Trait item in traits)
                if (info.traits.Contains(item))
                    return true;
            return false;
        }
        /// <summary>
        /// Grabs the Card in the opposing slot.
        /// </summary>
        /// <returns>The opposing Card object.</returns>
        public static PlayableCard OpposingCard(this PlayableCard card)
        {
            return card.Slot.opposingSlot.Card;
        }
        /// <summary>
        /// Removes this card from the board.
        /// </summary>
        /// <param name="removeFromDeck">Whether to also remove from the player's deck. Does not affect opponent cards.</param>
        /// <param name="tweenLength">How long the ExitBoard animation should take to play.</param>
        public static void RemoveFromBoard(this PlayableCard item, bool removeFromDeck = false, float tweenLength = 0.3f)
        {
            // Remove from deck if this card is owned by the player
            if (removeFromDeck && !item.OpponentCard && !item.OriginatedFromQueue)
            {
                RunState.Run.playerDeck.RemoveCard(item.Info);
            }
            item.UnassignFromSlot();
            SpecialCardBehaviour[] components = item.GetComponents<SpecialCardBehaviour>();
            for (int i = 0; i < components.Length; i++)
            {
                components[i].OnCleanUp();
            }
            item.ExitBoard(tweenLength, Vector3.zero);
        }
        /// <summary>
        /// Kills this card without triggering OnDie or OnOtherCardDie. Triggers OnDie if it has the PackMule special ability.
        /// </summary>
        public static IEnumerator DieTriggerless(this PlayableCard card)
        {
            if (!card.Dead)
            {
                card.Dead = true;
                CardSlot slotBeforeDeath = card.Slot;
                if (card.Info != null && card.Info.name.ToLower().Contains("squirrel"))
                {
                    AscensionStatsData.TryIncrementStat(AscensionStat.Type.SquirrelsKilled);
                }
                card.Anim.SetShielded(shielded: false);
                yield return card.Anim.ClearLatchAbility();
                if (card.HasAbility(Ability.PermaDeath))
                {
                    card.Anim.PlayPermaDeathAnimation();
                    yield return new WaitForSeconds(1.25f);
                }
                else
                {
                    card.Anim.PlayDeathAnimation();
                }
                if (!card.HasAbility(Ability.QuadrupleBones) && slotBeforeDeath.IsPlayerSlot)
                {
                    yield return Singleton<ResourcesManager>.Instance.AddBones(1, slotBeforeDeath);
                }
                if (card.Info.SpecialAbilities.Contains(SpecialTriggeredAbility.PackMule) && card.TriggerHandler.RespondsToTrigger(Trigger.Die, false, null))
                {
                    yield return card.TriggerHandler.OnTrigger(Trigger.Die, false, null);
                }
                card.UnassignFromSlot();
                card.StartCoroutine(card.DestroyWhenStackIsClear());
            }
        }
        /// <summary>
        /// Replaces the current blueprint with a custom blueprint.
        /// </summary>
        /// <param name="encounter">Blueprint to add.</param>
        /// <param name="removeLockedCards">Removes locks cards.</param>
        public static IEnumerator ReplaceWithCustomBlueprint(this Opponent opponent, EncounterBlueprintData encounter, bool removeLockedCards = false)
        {
            opponent.Blueprint = encounter;
            int difficulty = 0;
            if (Singleton<TurnManager>.Instance.BattleNodeData != null)
            {
                difficulty = Singleton<TurnManager>.Instance.BattleNodeData.difficulty + RunState.Run.DifficultyModifier;
            }
            List<List<CardInfo>> plan = DiskCardGame.EncounterBuilder.BuildOpponentTurnPlan(opponent.Blueprint, difficulty, removeLockedCards);
            opponent.ReplaceAndAppendTurnPlan(plan);
            yield return opponent.QueueNewCards();
        }
    }
}
