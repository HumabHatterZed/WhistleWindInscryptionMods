using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_JudgementBird_O0262()
        {
            const string judgementBird = "judgementBird";

            CardManager.New(pluginPrefix, judgementBird, "Judgement Bird",
                attack: 1, health: 1, "A long-necked bird that swiftly judges sinners, guilty or no.")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, judgementBird)
                .AddAbilities(Ability.Sniper)
                .AddTribes(Tribe.Bird)
                .AddTraits(BlackForest, Executioner)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}