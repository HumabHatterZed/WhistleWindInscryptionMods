using DiskCardGame;
using Infiniscryption.Core.Helpers;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class GiveStatsSigils : GiveAbility
    {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public override IEnumerator OnValidTarget(PlayableCard card)
        {
            CardModificationInfo baseMod = new(base.Card.Attack, 0);
            if (base.Card.LacksAbility(GiveStats.AbilityID))
            {
                if (base.Card.Health < 0)
                {
                    baseMod = new(base.Card.Attack, 0);
                    yield return card.TakeDamage(-base.Card.Health, null);
                    if (card == null || card.Dead)
                        yield break;
                }
                else
                {
                    baseMod.healthAdjustment = base.Card.Health;
                }
            }

            if (base.Card.LacksAbility(GiveSigils.AbilityID))
            {
                List<Ability> shownAbilitiesOnTarget = CardHelpers.GetDistinctShownAbilities(card.Info, card.TemporaryMods, card.Status.hiddenAbilities);
                if (shownAbilitiesOnTarget.Count > 4)
                    yield break;

                List<Ability> abilitiesToAdd = base.Card.AllAbilities();
                abilitiesToAdd.RemoveAll(ab => ab.GetExtendedPropertyAsBool("Spells:GiveAbility") == true);

                CardModificationInfo mergedAbilities = new() { fromCardMerge = true };
                List<Ability> stackedAbilities = new();

                // we want to avoid adding more abilities than can be rendered
                // so we add stackable abilities that already exist
                // otherwise, check if we can add unique abilities to the base/merged sections

                foreach (Ability abilityToCheck in abilitiesToAdd)
                {
                    if (shownAbilitiesOnTarget.Count + baseMod.abilities.Count + mergedAbilities.abilities.Count >= 4)
                        break;

                    // ignore duplicate abilities that can't stack, add stacks that already exist on the card
                    if (AbilityManager.AllAbilityInfos.AbilityByID(abilityToCheck).canStack)
                    {
                        stackedAbilities.Add(abilityToCheck);
                    }
                    else if (card.Info.Abilities.Count + baseMod.abilities.Count < 4)
                    {
                        baseMod.AddAbilities(abilityToCheck);
                    }
                    else if (card.Info.ModAbilities.Count + card.TemporaryMods.Count(tm => tm.fromCardMerge) + mergedAbilities.abilities.Count < 4)
                    {
                        mergedAbilities.abilities.Add(abilityToCheck);
                    }
                }

                baseMod.abilities.AddRange(stackedAbilities);

                card.Anim.PlayTransformAnimation();
                if (card.Info.name == "!DEATHCARD_BASE")
                {
                    card.AddTemporaryMods(baseMod, mergedAbilities);
                }
                else
                {
                    CardInfo info = card.Info.Clone() as CardInfo;
                    info.Mods.Add(baseMod);
                    info.Mods.Add(mergedAbilities);
                    card.SetInfo(info);
                }
            }
            else
            {
                card.AddTemporaryMod(baseMod);
            } 
        }

        public static void Register()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Give Stats and Sigils";
            info.rulebookDescription = "Gives this card's stats and sigils to the target.";
            info.canStack = true;
            info.powerLevel = 5;
            info.opponentUsable = false;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("give_stats_sigils_pixel"));

            GiveStatsSigils.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(GiveStatsSigils),
                AssetHelper.LoadTexture("ability_give_stats_sigils")
            ).Id;
        }
    }
}
