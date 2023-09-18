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

            NewCard(dreamOfABlackSwan, "Dream of a Black Swan", "Sister of six brothers. Tirelessly she worked to protect them, all for naught.",
                attack: 2, health: 5, blood: 3)
                .SetPortraits(dreamOfABlackSwan)
                .AddAbilities(Nettles.ability)
                .AddTribes(Tribe.Bird)
                .SetDefaultEvolutionName("Dream of an Elder Swan")
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.Waw);
        }
    }
}