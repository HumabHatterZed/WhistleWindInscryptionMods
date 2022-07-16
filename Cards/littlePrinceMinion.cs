using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void TheLittlePrinceMinion_O0466()
        {
            CardHelper.CreateCard(
                "wstl_theLittlePrinceMinion", "Spore Mold Creature",
                "An aged storyteller. She can tell you any tale, even those that can't exist.",
                0, 0, 0, 0,
                Resources.theLittlePrinceMinion, Resources.theLittlePrinceMinion_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(), riskLevel: 4);
        }
    }
}