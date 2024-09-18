using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_YouMustBeHappy_T0994()
        {
            const string youMustBeHappy = "youMustBeHappy";

            CardManager.New(pluginPrefix, youMustBeHappy, "You Must Be Happy",
                attack: 0, health: 2, "Those that undergo the procedure find themselves rested and healthy again.")
                .SetEnergyCost(2)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(ModAssembly, youMustBeHappy)
                .AddAbilities(Scrambler.ability)
                .SetSpellType(SpellType.TargetedStats)
                .SetDefaultEvolutionName("You Must Be Happier")
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}