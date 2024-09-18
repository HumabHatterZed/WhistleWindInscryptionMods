using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WillBeBadWolf_F0258()
        {
            const string willBeBadWolf = "willBeBadWolf";

            CardManager.New(pluginPrefix, willBeBadWolf, "Big and Will Be Bad Wolf",
                attack: 2, health: 4, "It's the fate of all wolves to be the villains of fairy tales.")
                .SetBloodCost(3)
                .SetPortraits(ModAssembly, willBeBadWolf)
                .AddAbilities(Assimilator.ability, BitterEnemies.ability)
                .AddSpecialAbilities(CrimsonScar.specialAbility)
                .AddTribes(Tribe.Canine)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}