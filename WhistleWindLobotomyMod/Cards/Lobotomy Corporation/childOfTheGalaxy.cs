using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string childOfTheGalaxy = "wstl_childOfTheGalaxy";
        private static void ChildOfTheGalaxy_O0155()
        {
            string name = "Child of the Galaxy";
            string desc = "A small child longing for an eternal friend. Will you be his?";
            string textureName = "childOfTheGalaxy";
            CardManager.New(LobotomyPlugin.pluginPrefix, childOfTheGalaxy, name,
                attack: 0, health: 0, desc)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Lonely.ability)
                .SetSpellType(SpellType.Targeted)
                .AddTraits(CannotGiveSigils)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.He);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 0, desc)
                .SetGemsCost(DiskCardGame.GemType.Blue)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Lonely.ability)
                .SetSpellType(SpellType.Targeted)
                .AddTraits(CannotGiveSigils)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}