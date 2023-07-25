using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_RedShoes_O0408()
        {
            const string redShoes = "redShoes";

            NewCard(redShoes, "Red Shoes", "How pretty. Maybe they'll fit.",
                attack: 0, health: 3, bones: 3)
                .SetPortraits(redShoes)
                .AddAbilities(Ability.Sharp, Ability.GuardDog)
                .SetTerrain()
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}