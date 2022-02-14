using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void HundredsGoodDeeds_O0303()
        {
            List<Ability> abilities = new()
            {
                Confession.ability
            };

            List<Trait> traits = new()
            {
                Trait.Uncuttable,
                Trait.Pelt
            };

            WstlUtils.Add(
                "wstl_hundredsGoodDeeds", "Hundreds of Good Deeds",
                "Its hollow sockets see through you.",
                0, 777, 0, 0,
                Resources.hundredsGoodDeeds,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), traits: traits);
        }
    }
}