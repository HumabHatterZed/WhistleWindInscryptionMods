using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_LuminousBracelet_O0995()
        {
            const string luminousBracelet = "luminousBracelet";

            CardManager.New(pluginPrefix, luminousBracelet, "Luminous Bracelet",
                attack: 0, health: 2, "A bracelet that will heal its bearer. It does not forgive the greedy.")
                .SetEnergyCost(3)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(ModAssembly, luminousBracelet)
                .AddAbilities(GreedyHealing.ability, GiveStatsSigils.AbilityID)
                .SetSpellType(SpellType.TargetedStats)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}