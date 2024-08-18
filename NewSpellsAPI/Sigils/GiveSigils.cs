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
    public class GiveSigils : GiveAbility
    {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public const int MaxSigilsToGive = 4;

        public override IEnumerator OnValidTarget(PlayableCard card)
        {
            List<Ability> shownAbilitiesOnTarget = CardHelpers.GetDistinctShownAbilities(card.Info, card.TemporaryMods, card.Status.hiddenAbilities);
            if (shownAbilitiesOnTarget.Count > MaxSigilsToGive)
                yield break;
            
            List<Ability> abilitiesToAdd = base.Card.AllAbilities();
            abilitiesToAdd.RemoveAll(ab => ab.GetExtendedPropertyAsBool("Spells:GiveAbility") == true);

            CardModificationInfo defaultAbilities = new();
            CardModificationInfo mergedAbilities = new() { fromCardMerge = true };
            List<Ability> stackedAbilities = new();

            // we want to avoid adding more abilities than can be rendered
            // so we add stackable abilities that already exist
            // otherwise, check if we can add unique abilities to the base/merged sections

            foreach (Ability abilityToCheck in abilitiesToAdd)
            {
                if (shownAbilitiesOnTarget.Count + defaultAbilities.abilities.Count + mergedAbilities.abilities.Count >= MaxSigilsToGive)
                    break;

                // ignore duplicate abilities that can't stack, add stacks that already exist on the card
                if (AbilityManager.AllAbilityInfos.AbilityByID(abilityToCheck).canStack)
                {
                    stackedAbilities.Add(abilityToCheck);
                }
                else if (card.Info.Abilities.Count + defaultAbilities.abilities.Count < 4)
                {
                    defaultAbilities.AddAbilities(abilityToCheck);
                }
                else if (card.Info.ModAbilities.Count + card.TemporaryMods.Count(tm => tm.fromCardMerge) + mergedAbilities.abilities.Count < 4)
                {
                    mergedAbilities.abilities.Add(abilityToCheck);
                }
            }

            defaultAbilities.abilities.AddRange(stackedAbilities);

            card.Anim.PlayTransformAnimation();
            if (card.Info.name == "!DEATHCARD_BASE")
            {
                card.AddTemporaryMods(defaultAbilities, mergedAbilities);
            }
            else
            {
                CardInfo info = card.Info.Clone() as CardInfo;
                info.Mods.Add(defaultAbilities);
                info.Mods.Add(mergedAbilities);
                card.SetInfo(info);
            }
        }

        public static void Register()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Give Sigils";
            info.rulebookDescription = "Gives this card's sigils to the target.";
            info.canStack = true;
            info.powerLevel = 3;
            info.opponentUsable = false;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("give_sigils_pixel"));
            info.SetExtendedProperty("Spells:GiveAbility", true);

            GiveSigils.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(GiveSigils),
                AssetHelper.LoadTexture("ability_give_sigils")
            ).Id;
        }
    }
}
