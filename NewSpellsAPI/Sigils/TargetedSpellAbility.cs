using DiskCardGame;
using Infiniscryption.Core.Helpers;
using InscryptionAPI.Card;
using System.Linq;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class TargetedSpellAbility : VariableStatBehaviour
    {
        // Why is this a stat behavior when these cards have no stats?
        // Simple. I want to cover over the health and attack icons.
        // I want these cards to have 0 health and 0 attack at all times in all zones.
        // This is the best way to do that.

        // I'm following the pattern of HealthForAnts

        private static SpecialStatIcon _icon;
        public static SpecialStatIcon Icon => _icon;
        public override SpecialStatIcon IconType => _icon;

        private static SpecialTriggeredAbility _id;
        public static SpecialTriggeredAbility ID => _id;

        public static void Register()
        {
            StatIconInfo info = ScriptableObject.CreateInstance<StatIconInfo>();
            info.appliesToAttack = true;
            info.appliesToHealth = true;
            info.rulebookName = "Spell (Targeted)";
            info.rulebookDescription = "When played, this card will target and affect a chosen space on the board and then disappear.";
            info.gbcDescription = "Targeted spell.";
            info.iconGraphic = AssetHelper.LoadTexture("targeted_spell_stat_icon");
            info.SetPixelIcon(AssetHelper.LoadTexture("targeted_spell_icon_pixel"));
            info.SetDefaultPart1Ability();

            TargetedSpellAbility._icon = StatIconManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(TargetedSpellAbility)
            ).Id;

            // Honestly, this should be a trait or something.
            // But for backwards compatibility, I'm leaving it.
            TargetedSpellAbility._id = StatIconManager.AllStatIcons.FirstOrDefault((StatIconManager.FullStatIcon sii) => sii.Id == Icon).AbilityId;
        }

        // No stats for these cards!
        public override int[] GetStatValues() => new int[] { 0, 0 };
    }
}