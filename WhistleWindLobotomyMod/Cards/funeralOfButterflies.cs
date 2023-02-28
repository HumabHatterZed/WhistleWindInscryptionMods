using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_FuneralOfButterflies_T0168()
        {
            List<Ability> abilities = new()
            {
                Ability.DoubleStrike
            };
            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };
            CreateCard(
                "wstl_funeralOfButterflies", "Funeral of the Dead Butterflies",
                "The coffin is a tribute to the fallen. A memorial to those who can't return home.",
                atk: 1, hp: 3,
                blood: 2, bones: 0, energy: 0,
                Artwork.funeralOfButterflies, Artwork.funeralOfButterflies_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He,
                evolveName: "2nd {0}");
        }
    }
}