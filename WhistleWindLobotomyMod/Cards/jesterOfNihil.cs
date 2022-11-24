using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Helpers.CardHelper;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_JesterOfNihil_O01118()
        {
            List<Ability> abilities = new()
            {
                Nihil.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                BoardEffects.specialAbility
            };
            CardHelper.CreateCard(
                "wstl_jesterOfNihil", "The Jester of Nihil",
                "",
                atk: 0, hp: 15,
                blood: 4, bones: 0, energy: 0,
                Artwork.jesterOfNihil, Artwork.jesterOfNihil_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Rare, metaTypes: MetaType.Ruina | MetaType.EventCard);
        }
    }
}