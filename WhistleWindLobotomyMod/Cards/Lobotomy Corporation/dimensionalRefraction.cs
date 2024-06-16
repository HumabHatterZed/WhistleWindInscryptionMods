using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_DimensionalRefraction_O0388()
        {
            const string dimensionalRefraction = "dimensionalRefraction";

            NewCard(dimensionalRefraction, "Dimensional Refraction Variant", "A strange phenomenon. Or rather, the creature is the phenomena in and of itself.",
                attack: 0, health: 0, blood: 2)
                .SetPortraits(dimensionalRefraction)
                .AddAbilities(Ability.RandomAbility)
                .SetStatIcon(SigilPower.Icon)
                .SetDefaultEvolutionName("4th Dimensional Refraction Variant")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}