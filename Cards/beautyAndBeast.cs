using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BeautyAndBeast_O0244()
        {
            List<Ability> abilities = new List<Ability>
            {
                Cursed.ability
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Canine,
                Tribe.Hooved,
                Tribe.Insect
            };

            WstlUtils.Add(
                "wstl_beautyAndBeast", "Beauty and the Beast",
                "A pitiable creature. Death would be a mercy for it.",
                1, 1, 1, 0,
                Resources.beautyAndBeast,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.beautyAndBeast_emission);
        }
    }
}