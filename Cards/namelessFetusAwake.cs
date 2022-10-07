using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_NamelessFetusAwake_O0115()
        {
            List<Ability> abilities = new()
            {
                Aggravating.ability,
                Ability.PreventAttack
            };

            CardHelper.CreateCard(
                "wstl_namelessFetusAwake", "Nameless Fetus",
                "Only a sacrifice will stop its piercing wails.",
                0, 1, 0, 5,
                Artwork.namelessFetusAwake, Artwork.namelessFetusAwake,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}