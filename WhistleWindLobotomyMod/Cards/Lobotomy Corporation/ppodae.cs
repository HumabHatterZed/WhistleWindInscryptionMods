using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Ppodae_D02107()
        {
            const string name = "Ppodae";
            const string ppodae = "ppodae";
            const string ppodaeBuff = "ppodaeBuff";
            Tribe[] tribes = new[] { Tribe.Canine };

            CardInfo ppodaeBuffCard = CardManager.New(pluginPrefix, ppodaeBuff, name,
                attack: 3, health: 2)
                .SetBonesCost(8)
                .SetPortraits(ModAssembly, ppodaeBuff)
                .AddAbilities(Ability.DebuffEnemy)
                .AddTribes(tribes)
                .AddMetaCategories(DonatorCard)
                .Build();

            CardManager.New(pluginPrefix, ppodae, name,
                attack: 1, health: 1, "An innocent little puppy.")
                .SetBonesCost(4)
                .SetPortraits(ModAssembly, ppodae)
                .AddAbilities(Ability.DebuffEnemy, Ability.Evolve)
                .AddTribes(tribes)
                .SetEvolve(ppodaeBuffCard, 1)
                .AddMetaCategories(DonatorCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}