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
        private void Card_SingingMachine_O0530()
        {
            List<Ability> abilities = new()
            {
                TeamLeader.ability,
                Aggravating.ability
            };
            List<Tribe> tribes = new() { TribeMechanical };

            CreateCard(
                "wstl_singingMachine", "Singing Machine",
                "A wind-up music machine. The song it plays is to die for.",
                atk: 0, hp: 4,
                blood: 1, bones: 0, energy: 0,
                Artwork.singingMachine, Artwork.singingMachine_emission, pixelTexture: Artwork.singingMachine_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new() { AbnormalPlugin.Orchestral },
                appearances: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He);
        }
    }
}