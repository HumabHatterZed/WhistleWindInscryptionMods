using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_NothingThereFinal_O0620()
        {
            CardHelper.CreateCard(
                "wstl_nothingThereFinal", "Nothing There",
                "A grotesque attempt at mimicry. Pray it does not improve its disguise.",
                atk: 9, hp: 9,
                blood: 4, bones: 0, energy: 0,
                Artwork.nothingThereFinal, Artwork.nothingThereFinal_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(),
                cardType: CardHelper.CardType.Rare, metaTypes: CardHelper.MetaType.NonChoice);
        }
    }
}