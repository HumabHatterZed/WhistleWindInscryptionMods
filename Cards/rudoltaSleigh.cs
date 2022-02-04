using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void RudoltaSleigh_F0249()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.Strafe,
                GiftGiver.ability
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Hooved
            };

            WstlUtils.Add(
                "wstl_rudoltaSleigh", "Rudolta of the Sleigh",
                "A grotesque effigy of a reindeer. With its infinite hate, it bequeaths gifts onto you.",
                3, 2, 2, 0,
                Resources.rudoltaSleigh,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.rudoltaSleigh_emission);
        }
    }
}