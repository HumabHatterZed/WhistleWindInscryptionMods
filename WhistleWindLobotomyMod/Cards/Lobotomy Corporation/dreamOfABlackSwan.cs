using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_DreamOfABlackSwan_F0270()
        {
            const string dreamOfABlackSwan = "dreamOfABlackSwan";

            CardManager.New(pluginPrefix, dreamOfABlackSwan, "Dream of a Black Swan",
                attack: 2, health: 5, "Sister of six brothers. Tirelessly she worked to protect them, all for naught.")
                .SetBloodCost(3)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, dreamOfABlackSwan)
                .AddAbilities(Nettles.ability)
                .AddTribes(Tribe.Bird)
                .SetDefaultEvolutionName("Dream of an Elder Swan")
                .Build(CardHelper.CardType.Rare, RiskLevel.Waw, true);
        }
    }
}