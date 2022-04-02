using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Laetitia_O0167()
        {
            List<Ability> abilities = new()
            {
                GiftGiver.ability
            };

            WstlUtils.Add(
                "wstl_laetitia", "Laetitia",
                "A little witch carrying a heart-shaped gift.",
                1, 2, 1, 0,
                Resources.laetitia, Resources.laetitia_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}