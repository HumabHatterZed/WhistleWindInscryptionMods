using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void PpodaeBuff_D02107()
        {
            List<Ability> abilities = new()
            {
                Ability.DebuffEnemy
            };

            List<Tribe> tribes = new()
            {
                Tribe.Canine
            };

            WstlUtils.Add(
                "wstl_ppodaeBuff", "Ppodae",
                "",
                3, 2, 0, 8,
                Resources.ppodaeBuff, Resources.ppodaeBuff_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());
        }
    }
}