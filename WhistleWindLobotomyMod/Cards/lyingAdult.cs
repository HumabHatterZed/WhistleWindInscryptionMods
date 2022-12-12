using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_AdultWhoTellsLies_F01117()
        {
            List<Ability> abilities = new()
            {
                FalseThrone.ability
            };
            LobotomyCardHelper.CreateCard(
                "wstl_lyingAdult", "The Adult Who Tells Lies",
                "",
                atk: 1, hp: 6,
                blood: 2, bones: 0, energy: 0,
                Artwork.lyingAdult, Artwork.lyingAdult_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, modTypes: LobotomyCardHelper.ModCardType.Ruina | LobotomyCardHelper.ModCardType.EventCard,
                customTribe: TribeFae);
        }
    }
}