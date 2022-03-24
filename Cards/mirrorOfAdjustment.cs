using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void MirrorOfAdjustment_O0981()
        {
            List<Ability> abilities = new()
            {
                Woodcutter.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                SpecialTriggeredAbility.Mirror
            };
            WstlUtils.Add(
                "wstl_mirrorOfAdjustment", "The Mirror of Adjustment",
                "A mirror that reflects nothing on its surface.",
                0, 1, 1, 0,
                Resources.mirrorOfAdjustment, Resources.mirrorOfAdjustment_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                isTerrain: true, isChoice: true);
        }
    }
}