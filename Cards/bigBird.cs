using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void BigBird_O0240()
        {
            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };

            WstlUtils.Add(
                "wstl_bigBird", "Big Bird",
                "Its eyes light up the darkness like stars.",
                2, 4, 2, 0,
                Resources.bigBird,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.bigBird_emission, onePerDeck: true);
        }
    }
}