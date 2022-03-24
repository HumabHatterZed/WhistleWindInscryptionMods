using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SnowQueenIceBlock_F0137()
        {
            WstlUtils.Add(
                "wstl_snowQueenIceBlock", "Block of Ice",
                "The palace was cold and lonely.",
                0, 1, 0, 0,
                Resources.snowQueenIceBlock, Resources.snowQueenIceBlock_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isTerrain: true);
        }
    }
}