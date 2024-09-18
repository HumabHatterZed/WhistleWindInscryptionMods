using DiskCardGame;
using InscryptionAPI.Card;
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

            CardManager.New(pluginPrefix, childOfTheGalaxy, "Child of the Galaxy",
                attack: 0, health: 0, "A small child longing for an eternal friend. Will you be his?")
                .SetBloodCost(1)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(ModAssembly, childOfTheGalaxy)
                .AddAbilities(Lonely.ability)
                .SetSpellType(SpellType.Targeted)
                .AddTraits(CannotGiveSigils)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}