using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void CanOfWellCheers_F0552()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe,
                Ability.Submerge

            };

            WstlUtils.Add(
                "wstl_canOfWellCheers", "Opened Can of WellCheers",
                "A vending machine dispensing ocean soda.",
                1, 1, 1, 0,
                Resources.canOfWellCheers,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}