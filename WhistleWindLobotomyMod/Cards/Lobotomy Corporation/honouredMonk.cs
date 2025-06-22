using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string honouredMonk = "wstl_honouredMonk";
        public const string cloudedMonk = "wstl_cloudedMonk";
        private static void HonouredMonk_D01110() {
            string name = "Clouded Monk";
            string name2 = "Honoured Monk";
            string desc = "A monk seeking enlightenment through good deeds. But surely there's a quicker way to nirvana...";
            string textureName = "cloudedMonk";
            string textureName2 = "honouredMonk";
            Tribe[] tribes = new[] { TribeAnthropoid };

            CardInfo cloudedMonkCard = CardManager.New(LobotomyPlugin.pluginPrefix, cloudedMonk, name,
                attack: 4, health: 2)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddTribes(tribes)
                .AddMetaCategories(DonatorCard)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, honouredMonk, name2,
                attack: 2, health: 1, desc)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Ability.Evolve)
                .AddTribes(tribes)
                .SetEvolve(cloudedMonkCard, 1)
                .AddMetaCategories(DonatorCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw);

            CardInfo cloudedMonkCard2 = CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 4, health: 2)
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddTribes(tribes)
                .AddMetaCategories(DonatorCard)
                .Build();

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName2, name2,
                attack: 2, health: 1, desc)
                .SetGemsCost(GemType.Orange, GemType.Blue)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Ability.Evolve)
                .AddTribes(tribes)
                .SetEvolve(cloudedMonkCard2, 1)
                .AddMetaCategories(DonatorCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}