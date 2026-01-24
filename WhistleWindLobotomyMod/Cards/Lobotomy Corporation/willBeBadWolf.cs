using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string willBeBadWolf = "wstl_willBeBadWolf";
        private static void WillBeBadWolf_F0258() {
            string textureName = "willBeBadWolf";
            CardManager.New(LobotomyPlugin.pluginPrefix, willBeBadWolf, "Big and Will Be Bad Wolf",
                attack: 2, health: 1, "It's the fate of all wolves to be the villains of fairy tales.")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.GainAttackOnKill)
                .AddSpecialAbilities(CrimsonScar.specialAbility)
                .AddTribes(Tribe.Canine)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}