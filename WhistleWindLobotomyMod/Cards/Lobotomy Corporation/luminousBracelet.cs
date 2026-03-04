using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string luminousBracelet = "wstl_luminousBracelet";
        private static void LuminousBracelet_O0995() {
            string name = "Luminous Bracelet";
            string desc = "A bracelet that heals it wearer regardless of injury or need.";
            string textureName = "luminousBracelet";
            CardManager.New(LobotomyPlugin.pluginPrefix, luminousBracelet, name,
                attack: 0, health: 0, desc)
                .SetEnergyCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(GreedyHealing.ID, GiveSigils.AbilityID)
                .SetSpellType(SpellType.TargetedSigils)
                .RemoveMetaCategories(AbnormalPlugin.CannotGiveSigils)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 0, desc)
                .SetEnergyCost(1)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(GreedyHealing.ID, GiveSigils.AbilityID)
                .SetSpellType(SpellType.TargetedSigils)
                .RemoveMetaCategories(AbnormalPlugin.CannotGiveSigils)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}