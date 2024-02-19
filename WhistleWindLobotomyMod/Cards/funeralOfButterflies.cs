using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_FuneralOfButterflies_T0168()
        {
            const string funeralOfButterflies = "funeralOfButterflies";

            NewCard(funeralOfButterflies, "Funeral of the Dead Butterflies", "The coffin is a tribute to the fallen. A memorial to those who can't return home.",
                attack: 1, health: 3, blood: 2)
                .SetPortraits(funeralOfButterflies)
                .AddAbilities(Ability.DoubleStrike)
                .AddTribes(Tribe.Insect)
                .SetDefaultEvolutionName("2nd Funeral of the Dead Butterflies")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}