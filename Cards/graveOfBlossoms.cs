using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void GraveOfBlossoms_O04100()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp,
                Bloodfiend.ability
            };

            WstlUtils.Add(
                "wstl_graveOfBlossoms", "Grave of Cherry Blossoms",
                "A deep sorrow, grown to obsession. Perhaps it's best to leave her be.",
                0, 2, 1, 0,
                Resources.graveOfBlossoms, Resources.graveOfBlossoms_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}