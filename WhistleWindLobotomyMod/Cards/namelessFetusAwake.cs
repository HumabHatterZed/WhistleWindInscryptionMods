using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
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
                atk: 0, hp: 1,
                blood: 0, bones: 4, energy: 0,
                Artwork.namelessFetusAwake, Artwork.namelessFetusAwake,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}