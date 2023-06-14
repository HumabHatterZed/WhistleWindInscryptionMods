using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
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

            CardInfo dreamOfABlackSwanCard = NewCard(
                dreamOfABlackSwan,
                "Dream of a Black Swan",
                "Sister of six brothers. Tirelessly she worked to protect them, all for naught.",
                attack: 2, health: 5, blood: 3)
                .SetPortraits(dreamOfABlackSwan)
                .AddAbilities(Nettles.ability)
                .AddTribes(Tribe.Bird)
                .SetEvolveInfo("[name]Dream of an Elder Swan");

            CreateCard(dreamOfABlackSwanCard, CardHelper.ChoiceType.Rare, RiskLevel.Waw);
        }
    }
}