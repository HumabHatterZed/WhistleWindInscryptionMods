using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string bloodBath = "wstl_bloodBath";
        public const string bloodBath1 = "wstl_bloodBath1";
        public const string bloodBath2 = "wstl_bloodBath2";
        public const string bloodBath3 = "wstl_bloodBath3";
        private static void Bloodbath_T0551() {
            string bathName = "Bloodbath";
            string textureName = "bloodBath";
            string textureName2 = "bloodBath1";
            string textureName3 = "bloodBath2";
            string textureName4 = "bloodBath3";
            Ability[] abilities = new[] { Ability.TripleBlood };
            SpecialTriggeredAbility[] specialAbilities = new[] { WristCutter.specialAbility };

            CardManager.New(LobotomyPlugin.pluginPrefix, bloodBath, bathName,
                attack: 0, health: 1, "A bathtub of full of blood. Do you recognise the hands of your loved ones?")
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddSpecialAbilities(specialAbilities)
                .AddTraits(Trait.Goat)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);

            CardManager.New(LobotomyPlugin.pluginPrefix, bloodBath1, bathName,
                attack: 0, health: 1)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, bloodBath2, bathName,
                attack: 0, health: 2)
                .SetBloodCost(1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetStatIcon(SpecialStatIcon.SacrificesThisTurn)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, bloodBath3, bathName,
                attack: 1, health: 2)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName4)
                .AddAbilities(Ability.TripleBlood, Ability.QuadrupleBones)
                .SetStatIcon(SpecialStatIcon.SacrificesThisTurn)
                .Build();
        }
    }
}