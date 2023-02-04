using DiskCardGame;
using Infiniscryption.Core.Helpers;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class GiveSigils : AbilityBehaviour
    {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        private bool OpponentTargetsPlayerSlots
        {
            get
            {
                int powerLevel = 0;
                foreach (Ability ab in base.Card.AllAbilities())
                    powerLevel += AbilitiesUtil.GetInfo(ab).powerLevel;

                return powerLevel < 0;
            }
        }

        public override bool RespondsToSacrifice() => true;
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            if (slot.Card != null)
            {
                bool validTarget = base.Card.OpponentCard == slot.Card.OpponentCard;

                if (base.Card.OpponentCard && OpponentTargetsPlayerSlots)
                    validTarget = base.Card.OpponentCard != slot.Card.OpponentCard;

                return validTarget;
            }

            return false;
        }
        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();

        public override IEnumerator OnSacrifice()
        {
            PlayableCard card = Singleton<BoardManager>.Instance.CurrentSacrificeDemandingCard;
            if (card != null)
            {
                Effect(card);
                yield return new WaitForSeconds(0.5f);
                yield return base.LearnAbility();
            }
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            Effect(slot.Card);
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();
        }
        public override IEnumerator OnResolveOnBoard()
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(base.Card.IsPlayerCard()))
            {
                if (slot.Card != null)
                    Effect(slot.Card);
            }
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();
        }

        private void Effect(PlayableCard card)
        {
            List<Ability> distinctShownAbilities = CardHelpers.GetDistinctShownAbilities(card.Info, card.TemporaryMods, card.Status.hiddenAbilities);

            if (distinctShownAbilities.Count >= 8)
                return;

            List<Ability> abilitiesToAdd = new(base.Card.AllAbilities());
            abilitiesToAdd.RemoveAll(ab => ab == GiveStats.AbilityID || ab == GiveSigils.AbilityID || ab == GiveStatsSigils.AbilityID);

            CardModificationInfo baseMod = new();
            CardModificationInfo mergeMod = new() { fromCardMerge = true };
            CardModificationInfo stackMod = new();

            // we want to avoid adding more abilities than can be rendered
            // so we add stackable abilities that already exist
            // otherwise, check if we can add unique abilities to the base/merged sections

            foreach (Ability abilityToCheck in abilitiesToAdd)
            {
                if (distinctShownAbilities.Count + baseMod.abilities.Count + mergeMod.abilities.Count >= 8)
                    break;

                // ignore duplicate abilities that can't stack, add stacks that already exist on the card
                if (card.HasAbility(abilityToCheck) || baseMod.abilities.Contains(abilityToCheck) || mergeMod.abilities.Contains(abilityToCheck))
                {
                    if (AbilitiesUtil.GetInfo(abilityToCheck).canStack)
                        stackMod.abilities.Add(abilityToCheck);
                    continue;
                }

                if (card.Info.Abilities.Count + baseMod.abilities.Count < 4)
                    baseMod.abilities.Add(abilityToCheck); // if we can fit an ability onto the bottom
                else if (card.Info.ModAbilities.Count + card.TemporaryMods.Where(tm => tm.fromCardMerge).Count() + mergeMod.abilities.Count < 4)
                    mergeMod.abilities.Add(abilityToCheck);
            }

            List<CardModificationInfo> comboList = new() { baseMod, mergeMod };
            distinctShownAbilities = CardHelpers.GetDistinctShownAbilities(card.Info,
                card.TemporaryMods.Concat(comboList).ToList(), card.Status.hiddenAbilities);

            while (distinctShownAbilities.Count > 8)
            {
                if (mergeMod.abilities.Count > 0)
                    mergeMod.abilities.RemoveAt(0);
                else if (baseMod.abilities.Count > 0)
                    baseMod.abilities.RemoveAt(0);

                comboList = new() { baseMod, mergeMod };
                distinctShownAbilities = CardHelpers.GetDistinctShownAbilities(card.Info,
                    card.TemporaryMods.Concat(comboList).ToList(), card.Status.hiddenAbilities);
            }

            card.Anim.PlayTransformAnimation();
            if (card.Info.name == "!DEATHCARD_BASE")
            {
                card.AddTemporaryMod(baseMod);
                card.AddTemporaryMod(mergeMod);
                card.AddTemporaryMod(stackMod);
            }
            else
            {
                CardInfo info = card.Info.Clone() as CardInfo;
                info.Mods = new(card.Info.Mods)
                {
                    baseMod, mergeMod, stackMod
                };
                card.SetInfo(info);
            }
        }

        public static void Register()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Give Sigils";
            info.rulebookDescription = "Gives this card's sigils to the target.";
            info.canStack = false;
            info.powerLevel = 4;
            info.opponentUsable = false;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("give_sigils_pixel"));

            GiveSigils.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(GiveSigils),
                AssetHelper.LoadTexture("ability_give_sigils")
            ).Id;
        }
    }
}
