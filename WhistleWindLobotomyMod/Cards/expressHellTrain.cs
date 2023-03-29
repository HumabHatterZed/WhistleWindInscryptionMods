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
        private void Card_ExpressHellTrain_T0986()
        {
            List<Ability> abilities = new() { GroupHealer.ability };
            List<Tribe> tribes = new() { TribeMechanical };

            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TicketTaker.specialAbility
            };
            CreateCard(
                "wstl_expressHellTrain", "Express Train to Hell",
                "When the time comes, the train will sound its mighty horn.",
                atk: 0, hp: 4,
                blood: 0, bones: 4, energy: 0,
                Artwork.expressHellTrain, Artwork.expressHellTrain_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: RiskLevel.Waw,
                evolveName: "[name]Express Train to Turbo Hell");

            abilities = new() { TheTrain.ability };
            CreateCard(
                "wstl_BottledExpressHellTrain", "Express Train to Hell",
                "When the time comes, the train will sound its mighty horn.",
                atk: 0, hp: 0,
                blood: 2, bones: 0, energy: 0,
                Artwork.expressHellTrain, Artwork.expressHellTrain_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                evolveName: "[name]Express Train to Turbo Hell", spellType: SpellType.Global);
        }
    }
}