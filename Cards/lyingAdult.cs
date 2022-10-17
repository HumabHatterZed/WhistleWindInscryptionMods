using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_AdultWhoTellsLies_F01117()
        {
            List<Ability> abilities = new()
            {
                FalseThrone.ability
            };
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.RuinaCard,
                CardHelper.MetaType.EventCard
            };

            CardHelper.CreateCard(
                "wstl_lyingAdult", "The Adult Who Tells Lies",
                "",
                1, 6, 2, 0,
                Artwork.lyingAdult, Artwork.lyingAdult_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Rare, metaTypes: metaTypes);
        }
    }
}