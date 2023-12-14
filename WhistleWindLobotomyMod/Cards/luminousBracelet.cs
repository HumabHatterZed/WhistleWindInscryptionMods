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

            NewCard(luminousBracelet, "Luminous Bracelet", "A bracelet that will heal its bearer. It does not forgive the greedy.",
                attack: 0, health: 2, energy: 3, temple: CardTemple.Tech)
                .SetPortraits(luminousBracelet)
                .AddAbilities(GreedyHealing.ability, GiveStatsSigils.AbilityID)
                .SetSpellType(SpellType.TargetedStats)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}