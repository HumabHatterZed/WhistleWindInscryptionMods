using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApocalypseBird_O0263()
        {
            const string apocalypseBird = "apocalypseBird";
            CardManager.New(pluginPrefix, apocalypseBird, "Apocalypse Bird",
                attack: 3, health: 9)
                .SetPortraits(ModAssembly, apocalypseBird)
                .SetBloodCost(4)
                .AddAbilities(Ability.AllStrike, Ability.SplitStrike)
                .AddTribes(Tribe.Bird)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .SetNodeRestrictions(true, false, false, true)
                .SetDefaultEvolutionName("Final Apocalypse Bird")
                .SetEventCard(true)
                .Build(riskLevel: RiskLevel.Aleph);
        }
    }
}