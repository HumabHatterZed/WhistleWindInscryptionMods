using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Core.Helpers
{
    public static class LobotomyAbilityHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static AbilityManager.FullAbility CreateAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription,
            string dialogue, int powerLevel = 0, bool canStack = false)
            where T : AbilityBehaviour
        {
            return AbilityHelper.CreateAbility<T>(
                pluginGuid, texture, gbcTexture,
                rulebookName, rulebookDescription, dialogue, powerLevel, false, canStack);
        }
        public static AbilityManager.FullAbility CreateActivatedAbility<T>(
            byte[] texture, byte[] gbcTexture,
            string rulebookName, string rulebookDescription, string dialogue, int powerLevel = 0)
            where T : ActivatedAbilityBehaviour
        {
            return AbilityHelper.CreateActivatedAbility<T>(
                pluginGuid, texture, gbcTexture,
                rulebookName, rulebookDescription, dialogue, powerLevel);
        }
        public static AbilityManager.FullAbility CreateRulebookAbility<T>(string rulebookName, string rulebookDescription)
            where T : AbilityBehaviour
        {
            return AbilityHelper.CreateFillerAbility<T>(
                pluginGuid, rulebookName, rulebookDescription, Artwork.sigilAbnormality, Artwork.sigilAbnormality_pixel);
        }

        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreatePaperTalkingCard<T>(string rulebookName)
            where T : PaperTalkingCard => SpecialTriggeredAbilityManager.Add(pluginGuid, rulebookName, typeof(T));
    }
}
