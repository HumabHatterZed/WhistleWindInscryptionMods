using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_NothingThereFinal_O0620()
        {
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.NonChoice
            };

            CardHelper.CreateCard(
                "wstl_nothingThereFinal", "Nothing There",
                "A grotesque attempt at mimicry. Pray it does not improve its disguise.",
                9, 9, 4, 0,
                Artwork.nothingThereFinal, Artwork.nothingThereFinal_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(),
                choiceType: CardHelper.ChoiceType.Rare, metaTypes: metaTypes);
        }
    }
}