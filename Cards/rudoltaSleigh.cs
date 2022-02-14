using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void RudoltaSleigh_F0249()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe,
                GiftGiver.ability
            };

            List<Tribe> tribes = new()
            {
                Tribe.Hooved
            };

            WstlUtils.Add(
                "wstl_rudoltaSleigh", "Rudolta of the Sleigh",
                "A grotesque effigy of a reindeer. With its infinite hate, it bequeaths gifts onto you.",
                2, 3, 2, 0,
                Resources.rudoltaSleigh,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.rudoltaSleigh_emission);
        }
    }
}