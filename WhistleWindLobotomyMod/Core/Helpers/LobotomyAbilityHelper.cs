using DiskCardGame;
using InscryptionAPI.Card;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static InscryptionAPI.Card.AbilityManager;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core.Helpers
{
    public static class LobotomyAbilityHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static FullAbility CreateAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription,
            string dialogue, int powerLevel = 0, bool canStack = false)
            where T : AbilityBehaviour
        {
            return AbilityHelper.CreateAbility<T>(
                pluginGuid, texture, gbcTexture,
                rulebookName, rulebookDescription, dialogue, powerLevel, false, canStack);
        }
        public static FullAbility CreateActivatedAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription, string dialogue, int powerLevel = 0)
            where T : ActivatedAbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

            return AbilityHelper.CreateActivatedAbility<T>(
                info, pluginGuid, texture, gbcTexture,
                rulebookName, rulebookDescription, dialogue, powerLevel);
        }
        public static FullAbility CreateRulebookAbility<T>(string rulebookName, string rulebookDescription)
            where T : AbilityBehaviour
        {
            return AbilityHelper.CreateFillerAbility<T>(
                pluginGuid, rulebookName, rulebookDescription, Artwork.sigilAbnormality, Artwork.sigilAbnormality_pixel);
        }

        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreatePaperTalkingCard<T>(string rulebookName)
            where T : PaperTalkingCard => SpecialTriggeredAbilityManager.Add(pluginGuid, rulebookName, typeof(T));
    }
}
