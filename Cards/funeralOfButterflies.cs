using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void FuneralOfButterflies_T0168()
        {
            List<Ability> abilities = new()
            {
                Ability.SplitStrike
            };

            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };

            CardHelper.CreateCard(
                "wstl_funeralOfButterflies", "Funeral of the Dead Butterflies",
                "The coffin is a tribute to the fallen. A memorial to those who can't return home.",
                2, 2, 2, 0,
                Resources.funeralOfButterflies, Resources.funeralOfButterflies_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                isRare: true, riskLevel: 3);
        }
    }
}