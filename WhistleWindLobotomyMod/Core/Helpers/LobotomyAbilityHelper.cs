using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static InscryptionAPI.Card.AbilityManager;

namespace WhistleWindLobotomyMod.Core.Helpers {
    public static class LobotomyAbilityHelper // Base code taken from GrimoraMod and SigilADay_julienperge
    {
        public static FullAbility CreateActivatedAbility<T>(
            string textureName,
            string rulebookName, string rulebookDescription,
            string dialogue = null, string triggerText = null,
            int powerLevel = 0)
            where T : ActivatedAbilityBehaviour {
            return AbilityHelper.New<T>(LobotomyPlugin.pluginGuid, textureName, rulebookName, rulebookDescription, powerLevel, true, dialogue, triggerText)
                .SetActivated();
        }
        public static FullAbility CreateRulebookAbility<T>(string rulebookName, string rulebookDescription) where T : AbilityBehaviour {
            return AbilityHelper.NewFiller<T>(LobotomyPlugin.pluginGuid, "sigilAbnormality", rulebookName, rulebookDescription);
        }

        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreatePaperTalkingCard<T>(string rulebookName)
            where T : PaperTalkingCard => SpecialTriggeredAbilityManager.Add(LobotomyPlugin.pluginGuid, rulebookName, typeof(T));
    }
}
