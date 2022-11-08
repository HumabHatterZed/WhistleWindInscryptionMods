using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_NothingThereTrue_O0620()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                CustomEvolveHelper.specialAbility
            };
            List<Tribe> tribes = new()
            {
                Tribe.Canine,
                Tribe.Hooved,
                Tribe.Reptile
            };
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.NonChoice
            };
            CardHelper.CreateCard(
                "wstl_nothingThereTrue", "Nothing There",
                "What is that?",
                atk: 3, hp: 3,
                blood: 2, bones: 0, energy: 0,
                Artwork.nothingThereTrue, Artwork.nothingThereTrue_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: new(),
                cardType: CardHelper.CardType.Rare, metaTypes: CardHelper.MetaType.NonChoice,
                evolveName: "wstl_nothingThereEgg");
        }
    }
}