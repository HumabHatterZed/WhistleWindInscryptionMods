using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_HappyTeddyBear_T0406()
        {
            const string happyTeddyBear = "happyTeddyBear";

            CardManager.New(pluginPrefix, happyTeddyBear, "Happy Teddy Bear",
                attack: 1, health: 5, "A discarded stuffed bear. Its memories began with a warm hug.")
                .SetBonesCost(6)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, happyTeddyBear)
                .AddAbilities(Ability.GuardDog)
                .SetDefaultEvolutionName("Big, Happy Teddy Bear")
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}