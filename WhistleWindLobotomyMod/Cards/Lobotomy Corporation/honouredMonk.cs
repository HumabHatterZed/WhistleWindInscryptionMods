using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_HonouredMonk_D01110()
        {
            const string honouredMonk = "honouredMonk";
            const string cloudedMonk = "cloudedMonk";
            Tribe[] tribes = new[] { TribeAnthropoid };

            CardInfo cloudedMonkCard = CardManager.New(pluginPrefix, cloudedMonk, "Clouded Monk",
                attack: 4, health: 2)
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, cloudedMonk)
                .AddTribes(tribes)
                .AddMetaCategories(DonatorCard)
                .Build();

            CardManager.New(pluginPrefix, honouredMonk, "Honoured Monk",
                attack: 2, health: 1, "A monk seeking enlightenment through good deeds. But surely there's a quicker way to nirvana...")
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, honouredMonk)
                .AddAbilities(Ability.Evolve)
                .AddTribes(tribes)
                .SetEvolve(cloudedMonkCard, 1)
                .AddMetaCategories(DonatorCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}