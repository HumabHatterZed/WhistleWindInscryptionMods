using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void Alriune_T0453()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.Strafe
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Hooved
            };


            WstlUtils.Add(
                "wstl_alriune", "Alriune",
                "A doll yearning to be a human. A human yearning to be a doll.",
                5, 4, 3, 0,
                Resources.alriune,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.alriune_emission);
        }
    }
}