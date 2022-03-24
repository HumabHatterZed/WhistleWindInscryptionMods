using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void ForsakenMurderer_T0154()
        {
            WstlUtils.Add(
                "wstl_forsakenMurderer", "Forsaken Murderer",
                "Experimented on then forgotten. What was anger has become abhorrence.",
                4, 1, 0, 8,
                Resources.forsakenMurderer, Resources.forsakenMurderer_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}