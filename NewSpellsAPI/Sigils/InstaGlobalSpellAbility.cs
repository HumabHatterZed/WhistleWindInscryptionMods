using DiskCardGame;
using Infiniscryption.Core.Helpers;
using InscryptionAPI.Card;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class InstaGlobalSpellAbility : VariableStatBehaviour
    {
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
            info.rulebookName = "Spell (Global, Instant)";
            info.rulebookDescription = "When this card is played, it will cause an immediate effect and then disappear.";
            info.gbcDescription = "INSTA-GLOBAL SPELL.";
            info.iconGraphic = AssetHelper.LoadTexture("insta_global_spell_stat_icon");
            info.SetPixelIcon(AssetHelper.LoadTexture("insta_global_spell_icon_pixel"));
            info.SetDefaultPart1Ability();

            StatIconManager.FullStatIcon full = StatIconManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info, typeof(InstaGlobalSpellAbility)
            );

            _icon = full.Id;
            _id = full.AbilityId;
        }

        // No stats for these cards!
        public override int[] GetStatValues() => new int[] { 0, 0 };
    }
}