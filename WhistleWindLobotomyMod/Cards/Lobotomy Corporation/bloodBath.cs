using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Bloodbath_T0551()
        {
            const string bathName = "Bloodbath";
            const string bloodBath = "bloodBath";
            const string bloodBath1 = "bloodBath1";
            const string bloodBath2 = "bloodBath2";
            const string bloodBath3 = "bloodBath3";
            Ability[] abilities = new[] { Ability.TripleBlood };
            SpecialTriggeredAbility[] specialAbilities = new[] { WristCutter.specialAbility };

            CardManager.New(pluginPrefix, bloodBath, bathName,
                attack: 0, health: 1, "A bathtub of full of blood. Do you recognise the hands of your loved ones?")
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, bloodBath)
                .AddSpecialAbilities(specialAbilities)
                .AddTraits(Trait.Goat)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);

            CardManager.New(pluginPrefix, bloodBath1, bathName,
                attack: 0, health: 1)
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, bloodBath1)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .Build();

            CardManager.New(pluginPrefix, bloodBath2, bathName,
                attack: 0, health: 2)
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, bloodBath2)
                .AddAbilities(abilities)
                .AddSpecialAbilities(specialAbilities)
                .SetStatIcon(SpecialStatIcon.SacrificesThisTurn)
                .Build();

            CardManager.New(pluginPrefix, bloodBath3, bathName,
                attack: 1, health: 2)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, bloodBath3)
                .AddAbilities(Ability.TripleBlood, Ability.QuadrupleBones)
                .SetStatIcon(SpecialStatIcon.SacrificesThisTurn)
                .Build();
        }
    }
}