using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
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
                Resources.laetitia,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.rudoltaSleigh_emission);
        }
    }
}