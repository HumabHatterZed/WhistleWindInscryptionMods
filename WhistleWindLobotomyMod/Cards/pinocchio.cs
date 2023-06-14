using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Pinocchio_F01112()
        {
            const string pinocchio = "pinocchio";

            CardInfo pinocchioCard = NewCard(
                pinocchio,
                "Pinocchio",
                "A wooden doll that mimics the beasts it encounters. Can you see through its lie?",
                attack: 0, health: 1, bones: 1)
                .SetPortraits(pinocchio)
                .AddAbilities(Copycat.ability)
                .AddTribes(TribeBotanic);

            CreateCard(pinocchioCard, CardHelper.ChoiceType.Common, RiskLevel.Teth, ModCardType.Ruina);
        }
    }
}