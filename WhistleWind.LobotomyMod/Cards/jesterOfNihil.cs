using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
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
            LobotomyCardHelper.CreateCard(
                "wstl_jesterOfNihil", "The Jester of Nihil",
                "",
                atk: 0, hp: 15,
                blood: 4, bones: 0, energy: 0,
                Artwork.jesterOfNihil, Artwork.jesterOfNihil_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, modTypes: LobotomyCardHelper.ModCardType.Ruina | LobotomyCardHelper.ModCardType.EventCard,
                customTribe: TribeFae);
        }
    }
}