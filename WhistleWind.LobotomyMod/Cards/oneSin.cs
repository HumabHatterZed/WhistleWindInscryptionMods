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
        private void Card_OneSin_O0303()
        {
            List<Ability> abilities = new()
            {
                Martyr.ability
            };
            LobotomyCardHelper.CreateCard(
                "wstl_oneSin", "One Sin and Hundreds of Good Deeds",
                "A floating skull. Its hollow sockets see through you.",
                atk: 0, hp: 1,
                blood: 0, bones: 2, energy: 0,
                Artwork.oneSin, Artwork.oneSin_emission, pixelTexture: Artwork.oneSin_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Zayin,
                customTribe: TribeDivine);
        }
    }
}