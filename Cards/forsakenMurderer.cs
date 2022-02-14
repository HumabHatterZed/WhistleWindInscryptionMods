using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ForsakenMurderer_T0154()
        {
            WstlUtils.Add(
                "wstl_forsakenMurderer", "Forsaken Murderer",
                "Experimented on then forgotten. What was anger has become abhorrence.",
                4, 1, 0, 8,
                Resources.forsakenMurderer,
                new List<Ability>(), new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.forsakenMurderer_emission);
        }
    }
}