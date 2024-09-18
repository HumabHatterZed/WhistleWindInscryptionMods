using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_PunishingBird_O0256()
        {
            const string punishingBird = "punishingBird";

            CardManager.New(pluginPrefix, punishingBird, "Punishing Bird",
                attack: 1, health: 1, "A small bird on a mission to punish evildoers.")
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, punishingBird)
                .AddAbilities(Ability.Flying, Punisher.ability)
                .AddTribes(Tribe.Bird)
                .AddTraits(BlackForest)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}