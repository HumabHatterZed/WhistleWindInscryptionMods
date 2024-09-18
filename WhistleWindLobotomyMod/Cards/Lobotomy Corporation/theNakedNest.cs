using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheNakedNest_O0274()
        {
            const string theNakedNest = "theNakedNest";

            CardManager.New(pluginPrefix, theNakedNest, "The Naked Nest",
                attack: 0, health: 3, "They can enter your body through any aperture.")
                .SetBonesCost(4)
                .SetPortraits(ModAssembly, theNakedNest)
                .AddAbilities(SerpentsNest.ability)
                .AddTraits(Trait.KillsSurvivors)
                .SetDefaultEvolutionName("The Elder Naked Nest")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}