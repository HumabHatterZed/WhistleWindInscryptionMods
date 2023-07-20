using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WallLady_F0118()
        {
            const string wallLady = "wallLady";

            NewCard(wallLady, "The Lady Facing the Wall", "A deep sorrow, grown to obsession. Perhaps it's best to leave her be.",
                attack: 1, health: 2, bones: 4)
                .SetPortraits(wallLady)
                .AddAbilities(Ability.Sharp)
                .AddTribes(TribeAnthropoid)
                .SetDefaultEvolutionName("The Elder Lady Facing the Wall")
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}