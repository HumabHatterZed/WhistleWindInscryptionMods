using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using System.Collections.Generic;
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

            CardInfo luminousBraceletCard = NewCard(
                luminousBracelet,
                "Luminous Bracelet",
                "A bracelet that will heal those nearby. It does not forgive the greedy.",
                attack: 0, health: 2, energy: 3)
                .SetPortraits(luminousBracelet)
                .AddAbilities(GreedyHealing.ability, GiveStatsSigils.AbilityID)
                .SetSpellType(SpellType.TargetedSigils);

            CreateCard(luminousBraceletCard, CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}