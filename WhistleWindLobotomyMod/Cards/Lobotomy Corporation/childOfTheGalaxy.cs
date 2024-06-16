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

            NewCard(childOfTheGalaxy, "Child of the Galaxy", "A small child longing for an eternal friend. Will you be his?",
                attack: 0, health: 0, blood: 1, temple: CardTemple.Wizard)
                .SetPortraits(childOfTheGalaxy)
                .AddAbilities(Lonely.ability)
                .SetSpellType(SpellType.Targeted)
                .AddMetaCategories(CannotGiveSigils)
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}