using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_JesterOfNihil_O01118()
        {
            List<Ability> abilities = new() { ReturnToNihil.ability };
            List<Tribe> tribes = new() { TribeFae };

            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                BoardEffects.specialAbility
            };
            CreateCard(
                "wstl_jesterOfNihil", "The Jester of Nihil",
                "",
                atk: 0, hp: 15,
                blood: 4, bones: 0, energy: 0,
                Artwork.jesterOfNihil, Artwork.jesterOfNihil_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, modTypes: ModCardType.Ruina | ModCardType.EventCard,
                statIcon: Nihil.Icon);
        }
    }
}