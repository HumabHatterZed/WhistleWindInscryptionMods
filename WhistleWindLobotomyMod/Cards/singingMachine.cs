using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SingingMachine_O0530()
        {
            List<Ability> abilities = new()
            {
                TeamLeader.ability,
                Aggravating.ability
            };

            CardHelper.CreateCard(
                "wstl_singingMachine", "Singing Machine",
                "A wind-up music machine. The song it plays is to die for.",
                atk: 0, hp: 8,
                blood: 2, bones: 0, energy: 0,
                Artwork.singingMachine, Artwork.singingMachine_emission, pixelTexture: Artwork.singingMachine_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.He);
        }
    }
}