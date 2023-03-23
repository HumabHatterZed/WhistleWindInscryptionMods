using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Bloodbath_T0551()
        {
            List<Ability> abilities = new() { Ability.TripleBlood };
            List<Trait> traits = new() { Trait.Goat };
            
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                WristCutter.specialAbility,
            };
            CreateCard(
                "wstl_bloodBath", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.bloodBath, Artwork.bloodBath_emission, Artwork.bloodBath_pixel,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: traits,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Teth);

            CreateCard(
                "wstl_bloodBath1", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.bloodBath1, Artwork.bloodBath1_emission, Artwork.bloodBath1_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                statIcon: SpecialStatIcon.SacrificesThisTurn);

            abilities.Add(Ability.QuadrupleBones);

            CreateCard(
                "wstl_bloodBath2", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                atk: 0, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.bloodBath2, Artwork.bloodBath2_emission, Artwork.bloodBath2_pixel,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                statIcon: SpecialStatIcon.SacrificesThisTurn);

            CreateCard(
                "wstl_bloodBath3", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                atk: 1, hp: 2,
                blood: 2, bones: 0, energy: 0,
                Artwork.bloodBath3, Artwork.bloodBath3_emission, Artwork.bloodBath3_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                statIcon: SpecialStatIcon.SacrificesThisTurn);
        }
    }
}