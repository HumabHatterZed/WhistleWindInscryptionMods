using DiskCardGame;
using Infiniscryption.Core.Helpers;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class DestroyAllCardsOnDeath : AbilityBehaviour
    {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();
            ViewManager.Instance.SwitchToView(View.Board);

            // Kill EVERYTHING
            foreach (var slot in BoardManager.Instance.OpponentSlotsCopy)
                if (slot.Card != null)
                    yield return slot.Card.Die(true);

            foreach (var slot in BoardManager.Instance.PlayerSlotsCopy)
                if (slot.Card != null)
                    yield return slot.Card.Die(true);

            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();

            ViewManager.Instance.SwitchToView(View.Default);
        }

        public static void Register()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Cataclysm";
            info.rulebookDescription = "When [creature] dies, all other cards on the boards perish as well.";
            info.canStack = false;
            info.powerLevel = 6;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("nuke_pixel"));

            DestroyAllCardsOnDeath.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(DestroyAllCardsOnDeath),
                AssetHelper.LoadTexture("ability_nuke")
            ).Id;
        }
    }
}
