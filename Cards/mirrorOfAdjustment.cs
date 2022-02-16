using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void MirrorOfAdjustment_O0981()
        {
            List<Ability> abilities = new()
            {
                Woodcutter.ability
            };
            List<SpecialTriggeredAbility> triggeredAbilities = new()
            {
                SpecialTriggeredAbility.Mirror
            };
            WstlUtils.Add(
                "wstl_mirrorOfAdjustment", "The Mirror of Adjustment",
                "A mirror that reflects nothing on its surface.",
                0, 1, 1, 0,
                Resources.mirrorOfAdjustment,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), triggeredAbilities: triggeredAbilities,
                metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.mirrorOfAdjustment_emission);
        }
    }
}