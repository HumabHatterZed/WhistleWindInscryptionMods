using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void Ppodae_D02107()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.DebuffEnemy,
                Ability.Evolve
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Canine
            };

            WstlUtils.Add(
                "wstl_ppodae", "Ppodae",
                "A cute little doggo...did I say that?",
                1, 1, 0, 6,
                Resources.ppodae,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.ppodae_emission,
                evolveId: new EvolveIdentifier("wstl_ppodaeBuff", 1));
        }
    }
}