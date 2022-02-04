using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void WarmHeartedWoodsman_F0532()
        {
            List<Ability> abilities = new List<Ability>
            {
                Woodcutter.ability
            };

            WstlUtils.Add(
                "wstl_warmHeartedWoodsman", "Warm-Hearted Woodsman",
                "A tin woodsman on the search for a heart. Perhaps you can give him yours.",
                3, 2, 2, 0,
                Resources.warmHeartedWoodsman,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}