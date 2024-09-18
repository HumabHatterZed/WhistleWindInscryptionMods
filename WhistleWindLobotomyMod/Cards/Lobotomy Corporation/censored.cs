using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_CENSORED_O0389()
        {
            const string name = "CENSORED";
            const string censored = "censored";
            const string censoredMinion = "censoredMinion";

            CardManager.New(pluginPrefix, censored, name,
                attack: 4, health: 4, "It's best you never learn what it looks like.")
                .SetBloodCost(3)
                .SetPortraits(ModAssembly, censored)
                .AddAbilities(Bloodfiend.ability)
                .AddSpecialAbilities(CensoredSpecial.specialAbility)
                .SetDefaultEvolutionName("CENSORED CENSORED")
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph, true);

            CardManager.New(pluginPrefix, censoredMinion, name,
                attack: 0, health: 0)
                .SetPortraits(ModAssembly, censoredMinion)
                .SetDefaultEvolutionName("CENSORED CENSORED")
                .Build();
        }
    }
}