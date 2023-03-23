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
        private void Card_AdultWhoTellsLies_F01117()
        {
            List<Ability> abilities = new() { FalseThrone.ability };
            List<Tribe> tribes = new() { TribeAnthropoid };

            CreateCard(
                "wstl_lyingAdult", "The Adult Who Tells Lies",
                "",
                atk: 1, hp: 6,
                blood: 2, bones: 0, energy: 0,
                Artwork.lyingAdult, Artwork.lyingAdult_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, modTypes: ModCardType.Ruina | ModCardType.EventCard);
        }
    }
}