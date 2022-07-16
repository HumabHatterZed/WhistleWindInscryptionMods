using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void CanOfWellCheers_F0552()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe,
                Ability.Submerge
            };

            CardHelper.CreateCard(
                "wstl_canOfWellCheers", "Opened Can of WellCheers",
                "A vending machine dispensing ocean soda.",
                1, 1, 1, 0,
                Resources.canOfWellCheers, Resources.canOfWellCheers_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 1);
        }
    }
}