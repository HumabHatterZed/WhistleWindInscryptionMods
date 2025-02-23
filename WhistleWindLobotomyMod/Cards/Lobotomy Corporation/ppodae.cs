using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string ppodae = "wstl_ppodae";
        public const string ppodaeBuff = "wstl_ppodaeBuff";
        private static void Ppodae_D02107()
        {
            string name = "Ppodae";
            string textureName = "ppodaeBuff";
            string textureName2 = "ppodae";
            Tribe[] tribes = new[] { Tribe.Canine };

            CardInfo ppodaeBuffCard = CardManager.New(LobotomyPlugin.pluginPrefix, ppodaeBuff, name,
                attack: 3, health: 2)
                .SetBonesCost(8)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.DebuffEnemy)
                .AddTribes(tribes)
                .AddMetaCategories(DonatorCard)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, ppodae, name,
                attack: 1, health: 1, "Just an innocent wittle puppy, yes he is!!")
                .SetBonesCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Ability.DebuffEnemy, Ability.Evolve)
                .AddTribes(tribes)
                .SetEvolve(ppodaeBuffCard, 1)
                .AddMetaCategories(DonatorCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}