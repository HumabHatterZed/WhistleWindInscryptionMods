using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_Pumpkin_F04116()
        {
            CardHelper.CreateCard(
                "wstl_ozmaPumpkin", "Pumpkin",
                "An orange gourd.",
                0, 1, 0, 0,
                Resources.ozmaPumpkin, Resources.ozmaPumpkin_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isTerrain: true, isChoice: false,
                evolveName: "wstl_ozmaPumpkinJack");
        }
    }
}