using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void WarmHeartedWoodsman_F0532()
        {
            List<Ability> abilities = new()
            {
                Woodcutter.ability
            };

            WstlUtils.Add(
                "wstl_warmHeartedWoodsman", "Warm-Hearted Woodsman",
                "A tin woodsman on the search for a heart. Perhaps you can give him yours.",
                2, 3, 2, 0,
                Resources.warmHeartedWoodsman,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.warmHeartedWoodsman_emission);
        }
    }
}