using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void WarmHeartedWoodsman_F0532()
        {
            List<Ability> abilities = new()
            {
                Woodcutter.ability
            };
            CardHelper.CreateCard(
                "wstl_warmHeartedWoodsman", "Warm-Hearted Woodsman",
                "A tin woodsman on the search for a heart. Perhaps you can give him yours.",
                2, 3, 2, 0,
                Resources.warmHeartedWoodsman, Resources.warmHeartedWoodsman_emission, gbcTexture: Resources.warmHeartedWoodsman_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 3);
        }
    }
}