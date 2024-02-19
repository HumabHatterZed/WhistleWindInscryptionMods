using DiskCardGame;
using Infiniscryption.Core.Helpers;
using InscryptionAPI.Card;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class GlobalSpellAbility : VariableStatBehaviour
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
            info.rulebookName = "Spell (Global)";
            info.rulebookDescription = "When this card is played anywhere on the board, it will cause an immediate effect and then disappear.";
            info.gbcDescription = "GLOBAL SPELL.";
            info.iconGraphic = AssetHelper.LoadTexture("global_spell_stat_icon");
            info.SetPixelIcon(AssetHelper.LoadTexture("global_spell_icon_pixel"));
            info.SetDefaultPart1Ability();

            StatIconManager.FullStatIcon full = StatIconManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info, typeof(GlobalSpellAbility)
            );

            _icon = full.Id;
            _id = full.AbilityId;
        }

        // No stats for these cards!
        public override int[] GetStatValues() => new int[] { 0, 0 };
    }
}