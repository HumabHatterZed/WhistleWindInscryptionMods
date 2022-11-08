using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_NothingThereEgg_O0620()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                CustomEvolveHelper.specialAbility
            };
            CardHelper.CreateCard(
                "wstl_nothingThereEgg", "An Egg",
                "What is it doing?",
                atk: 0, hp: 3,
                blood: 3, bones: 0, energy: 0,
                Artwork.nothingThereEgg, Artwork.nothingThereEgg_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(), onePerDeck: true,
                cardType: CardHelper.CardType.Rare, metaTypes: CardHelper.MetaType.NonChoice,
                evolveName: "wstl_nothingThereFinal");
        }
    }
}