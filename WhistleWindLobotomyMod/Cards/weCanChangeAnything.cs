using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WeCanChangeAnything_T0985()
        {
            List<Ability> abilities = new() { Grinder.ability };
            List<Tribe> tribes = new() { TribeMechanical };

            CreateCard(
                "wstl_weCanChangeAnything", "We Can Change Anything",
                "Whatever you're dissatisfied with, this machine will fix it. You just have to step inside.",
                atk: 0, hp: 3,
                blood: 1, bones: 0, energy: 0,
                Artwork.weCanChangeAnything, Artwork.weCanChangeAnything_emission, pixelTexture: Artwork.weCanChangeAnything_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Zayin,
                evolveName: "{0} 2.0");
        }
    }
}