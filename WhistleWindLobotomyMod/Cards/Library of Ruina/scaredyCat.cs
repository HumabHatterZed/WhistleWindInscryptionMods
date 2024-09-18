using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ScaredyCat_F02115()
        {
            const string catName = "Scaredy Cat";
            const string scaredyCat = "scaredyCat";
            const string scaredyCatStrong = "scaredyCatStrong";
            Trait[] traits = new[] { EmeraldCity };
            SpecialTriggeredAbility[] specialAbilities = new[] { Cowardly.specialAbility };

            CardManager.New(pluginPrefix, scaredyCatStrong, catName,
                attack: 2, health: 6)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, scaredyCatStrong)
                .AddSpecialAbilities(specialAbilities)
                .AddTraits(traits)
                .AddMetaCategories(RuinaCard)
                .Build();

            CardManager.New(pluginPrefix, scaredyCat, catName,
                attack: 0, health: 1)
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, scaredyCat)
                .AddSpecialAbilities(specialAbilities)
                .AddTraits(traits)
                .AddMetaCategories(RuinaCard)
                .Build();
        }
    }
}