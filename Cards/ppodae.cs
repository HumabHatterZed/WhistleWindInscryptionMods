using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void Ppodae_D02107()
        {
            List<Ability> abilities = new()
            {
                Ability.DebuffEnemy,
                Ability.Evolve
            };

            List<Tribe> tribes = new()
            {
                Tribe.Canine
            };

            WstlUtils.Add(
                "wstl_ppodae", "Ppodae",
                "A cute little doggo...did I say that?",
                1, 1, 0, 4,
                Resources.ppodae,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.ppodae_emission,
                evolveId: new EvolveIdentifier("wstl_ppodaeBuff", 1));
        }
    }
}