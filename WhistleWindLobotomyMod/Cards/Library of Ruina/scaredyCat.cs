using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string scaredyCat = "wstl_scaredyCat";
        public const string scaredyCatStrong = "wstl_scaredyCatStrong";
        private static void ScaredyCat_F02115()
        {
            string textureName = "scaredyCatStrong";
            string textureName2 = "scaredyCat";
            Trait[] traits = new[] { EmeraldCity };
            SpecialTriggeredAbility[] specialAbilities = new[] { Cowardly.specialAbility };

            CardManager.New(LobotomyPlugin.pluginPrefix, scaredyCatStrong, "Scaredy Cat",
                attack: 2, health: 6)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddSpecialAbilities(specialAbilities)
                .AddTraits(traits)
                .AddMetaCategories(RuinaCard)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, scaredyCat, "Scaredy Cat",
                attack: 0, health: 1)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddSpecialAbilities(specialAbilities)
                .AddTraits(traits)
                .AddMetaCategories(RuinaCard)
                .Build();
        }
    }
}