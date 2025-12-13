using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string sweeper = "wstl_sweeper";
        public const string sweeper2 = "wstl_sweeper2";
        public const string sweeper3 = "wstl_sweeper3";
        private static void Cards_IndigoOrdeal() {
            string textureName = "sweeper";
            string textureName2 = "sweeper2";
            string textureName3 = "sweeper3";
            CardManager.New(LobotomyPlugin.pluginPrefix, sweeper, "Sweeper A",
                attack: 1, health: 2)
                .SetBloodCost(2).SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.SplitStrike, Persistent.ability, Bloodfiend.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .SetOrdealCard(OrdealType.Indigo)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, sweeper2, "Sweeper B",
                attack: 1, health: 2)
                .SetBloodCost(2).SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Ability.DoubleStrike, Persistent.ability, Bloodfiend.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .SetOrdealCard(OrdealType.Indigo)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, sweeper3, "Sweeper C",
                attack: 2, health: 2)
                .SetBloodCost(2).SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(Ability.Sniper, Persistent.ability, Bloodfiend.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.RedEmission)
                .AddTribes(TribeAnthropoid)
                .SetOrdealCard(OrdealType.Indigo)
                .Build();
        }
    }
}