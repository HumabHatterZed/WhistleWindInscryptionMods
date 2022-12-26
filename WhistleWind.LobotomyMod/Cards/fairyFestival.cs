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
        private void Card_FairyFestival_F0483()
        {
            List<Ability> abilities = new()
            {
                Bloodfiend.ability
            };
            LobotomyCardHelper.CreateCard(
                "wstl_fairyFestival", "Fairy Festival",
                "Everything will be peaceful while you're under the fairies' care.",
                atk: 1, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.fairyFestival, Artwork.fairyFestival_emission, pixelTexture: Artwork.fairyFestival_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Zayin,
                customTribe: TribeFae);
        }
    }
}