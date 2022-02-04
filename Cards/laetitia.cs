using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void Laetitia_O0167()
        {
            List<Ability> abilities = new List<Ability>
            {
                GiftGiver.ability
            };

            WstlUtils.Add(
                "wstl_laetitia", "Laetitia",
                "A small child carrying a heart-shaped gift.",
                2, 1, 1, 0,
                Resources.laetitia,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.rudoltaSleigh_emission);
        }
    }
}