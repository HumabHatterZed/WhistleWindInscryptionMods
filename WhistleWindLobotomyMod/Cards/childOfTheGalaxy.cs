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
        private void Card_ChildOfTheGalaxy_O0155()
        {
            const string childOfTheGalaxy = "childOfTheGalaxy";
            
            NewCard(childOfTheGalaxy, "Child of the Galaxy", "A small child longing for a friend. Will you be his?",
                attack: 1, health: 1, blood: 1)
                .SetPortraits(childOfTheGalaxy)
                .AddAbilities(FlagBearer.ability, Ability.BoneDigger)
                .AddTribes(TribeAnthropoid)
                .SetDefaultEvolutionName("Teen of the Galaxy")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}